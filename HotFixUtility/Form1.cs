using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;
using System.IO;

namespace HotFixUtility
{
    public partial class Form1 : Form
    {
        string environmentXML,applicationDirectory;
        List<string> environmentList;
        DataTable dtInputFile;
        private string selectedEnvironment;
        public ConfigDetails confDtl;

        private enum StatusTypes
        {
            Success,
            Error,
            General
        }
        public Form1()
        {
            InitializeComponent();
            LoadInitialSetups();  
        }
        private void LoadInitialSetups()
        {
            environmentXML = ConfigurationManager.AppSettings.Get("EnvironmentXML");
            applicationDirectory = ConfigurationManager.AppSettings.Get("ApplicationDirectory");
            // TODO: What if Configuration file doesn't have the EnvironmentXML file entry?
            // TODO : Error validation for Application directory.
            try
            {
                confDtl = new ConfigDetails(environmentXML);
                environmentList = confDtl.GetEnvironmentList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                //Close the form
                Load += (s, e) => Close();
                return;
            }
            
            // Get the list of environments in the combobox.
            cmbEnvironment.Items.AddRange(environmentList.ToArray());
            UpdateProcessButtons(false);

        }
        private void ChangeBackgroundColor(System.Windows.Forms.Control uiElement ,StatusTypes status)
        {
            switch (status)
            {
                case StatusTypes.Success:
                    uiElement.BackColor = System.Drawing.Color.LightGreen;
                    break;
                case StatusTypes.Error:
                    uiElement.BackColor = System.Drawing.Color.Red;
                    break;
                default:
                    uiElement.BackColor = System.Drawing.Color.LightGray;
                    break;
            }
        }

        private void btnTransferAsciiFiles_Click(object sender, EventArgs e)
        {
            // TODO : Check if the rtb module is in <AsciiModuleList>
            // TODO : If yes, copy the files.
            ArrayList programList = new ArrayList();
            string sourceDir, destDir;
            string asciiModuleList = confDtl.GetAsciiModuleList();            

            for (int index=0; index<dtInputFile.Rows.Count; index++)
            {
                string rtbModule = dtInputFile.Rows[index][1].ToString();
                if (asciiModuleList.Split(',').Contains(rtbModule))
                {
                    programList.Add(dtInputFile.Rows[index][0].ToString());
                }
            }
            if (programList.Count == 0)
            {
                ChangeBackgroundColor(btnTransferAsciiFiles, StatusTypes.Success);
                return;
            }
            try
            {
                Environment env_detail = ConfigDetails.GetEnvironmentDetails(selectedEnvironment);
                sourceDir = env_detail.AsciiSourceDirectory;
                destDir = env_detail.AsciiDestinationDirectory;

                Operations.CopyFiles(programList, sourceDir, destDir);
                ChangeBackgroundColor(btnTransferAsciiFiles, StatusTypes.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while copying", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChangeBackgroundColor(btnTransferAsciiFiles, StatusTypes.Error);
            }
        }

        private void btnFileSelect_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtInputFile.Text = openFileDialog1.FileName;
                errorProvider1.Clear();
            }                
            else
            {
                errorProvider1.SetError(txtInputFile, "Select input file.");
            }
            
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            if (txtInputFile.Text.Length == 0)
            {
                errorProvider1.SetError(txtInputFile, "Select input file.");
                return;
            }

            dtInputFile = Operations.ReadCSVFile(txtInputFile.Text);
            if (dtInputFile.Rows.Count == 0)
            {
                MessageBox.Show("Blank input file", "Input file error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            UpdateProcessButtons(true);
            ChangeBackgroundColor(btnLoadFile, StatusTypes.Success);
            updateStatusLabel("Program list loaded.", StatusTypes.Success);
        }
        private void UpdateProcessButtons(bool action)
        {
            btnTransferFiles.Enabled = action;
            btnTransferAsciiFiles.Enabled = action;
            btnAddAsciiFileProlib.Enabled = action;
            btnAddProlibFiles.Enabled = action;
            btnCreateHF.Enabled = action;
            btnRTBTransfer.Enabled = action;

            ChangeBackgroundColor(btnTransferFiles, StatusTypes.General);
            ChangeBackgroundColor(btnTransferAsciiFiles, StatusTypes.General);
            ChangeBackgroundColor(btnAddAsciiFileProlib, StatusTypes.General);
            ChangeBackgroundColor(btnAddProlibFiles, StatusTypes.General);
            ChangeBackgroundColor(btnCreateHF, StatusTypes.General);
            ChangeBackgroundColor(btnRTBTransfer, StatusTypes.General);

        }

        private void cmbEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verify all directories associated with this environment
            if (cmbEnvironment.SelectedIndex != -1 
                &&!Operations.VerifyEnvironmentDirectories(cmbEnvironment.Text))
            {
                MessageBox.Show("EnvironmentDirectory invalid.", "Environment error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbEnvironment.SelectedIndex = -1;
                return;
            }
            selectedEnvironment = cmbEnvironment.Text;
        }

        private void btnTransferFiles_Click(object sender, EventArgs e)
        {
            ArrayList programList = new ArrayList();
            string sourceDir, destDir;
            for(int index=0; index <dtInputFile.Rows.Count; index++)
            {
                programList.Add(dtInputFile.Rows[index][0].ToString());
            }
            try
            {
                Environment env_detail = ConfigDetails.GetEnvironmentDetails(selectedEnvironment);
                sourceDir = env_detail.SourceDirectory;
                destDir = env_detail.TargetDirectory;

                Operations.CopyFiles(programList, sourceDir, destDir);
                ChangeBackgroundColor(btnTransferFiles, StatusTypes.Success);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while copying", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChangeBackgroundColor(btnTransferFiles, StatusTypes.Error);
            }            
        }

        private void btnAddAsciiFileProlib_Click(object sender, EventArgs e)
        {
            List<string> programList = new List<string>();
            string proenvCommand = confDtl.GetProEnvCommand();
            Environment env_detail = ConfigDetails.GetEnvironmentDetails(selectedEnvironment);
            string fileName = applicationDirectory + "Asciiprolibcommands.txt";
            string asciiModuleList = confDtl.GetAsciiModuleList();
            string cmdTxt;

            for (int index = 0; index < dtInputFile.Rows.Count; index++)
            {
                string rtbModule = dtInputFile.Rows[index][1].ToString();
                if (asciiModuleList.Split(',').Contains(rtbModule))
                {
                    // TODO : Hard coded command need to remove.
                    cmdTxt = $"prolib ahotfix.pl -n -v -r {dtInputFile.Rows[index][0].ToString()}";
                    programList.Add(cmdTxt);
                }                   
            }
            if (programList.Count == 0)
            {
                return;
                // TODO : Show this message in the status bar.
            }
            File.WriteAllLines(fileName, programList);

            // TODO : File validation is missing.
            // Start the proenv console
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = proenvCommand;
            startInfo.WorkingDirectory = env_detail.AsciiProEnvWorkingDirectory;
            process.StartInfo = startInfo;
            process.Start();

            // Start nortepad
            System.Diagnostics.Process.Start(fileName);

            // Set the background to green /success
            ChangeBackgroundColor(btnAddAsciiFileProlib, StatusTypes.Success);
        }

        private void btnAddProlibFiles_Click(object sender, EventArgs e)
        {
            List<string> programList = new List<string>();
            string proenvCommand = confDtl.GetProEnvCommand();
            Environment env_detail = ConfigDetails.GetEnvironmentDetails(selectedEnvironment);
            string fileName = applicationDirectory + "prolibcommands.txt";
            
            for (int index = 0; index < dtInputFile.Rows.Count; index++)
            {
                // TODO : Hard coded command need to remove.
                string cmdTxt = $"prolib hotfix.pl -n -v -r {dtInputFile.Rows[index][0].ToString()}";
                programList.Add(cmdTxt);
            }
            File.WriteAllLines(fileName, programList);



            //TODO : File validation missing.
            // Start the proenv console
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = proenvCommand;
            startInfo.WorkingDirectory = env_detail.ProEnvWorkingDirectory;
            process.StartInfo = startInfo;
            process.Start();

            //Start notedpad
            System.Diagnostics.Process.Start(fileName);

            //Set the back ground color to green.
            ChangeBackgroundColor(btnAddProlibFiles, StatusTypes.Success);
        }

        private void btnRTBTransfer_Click(object sender, EventArgs e)
        {
            //TODO
            DataTable dtRTBMappings = ConfigDetails.GetRTBMappings();
        }

        private void updateStatusLabel(string message, StatusTypes status)
        {
            statusLabel1.Text = message;
            switch (status)
            {
                case StatusTypes.Success:
                    statusLabel1.ForeColor = System.Drawing.Color.Green;
                    break;
                case StatusTypes.Error:
                    statusLabel1.ForeColor = System.Drawing.Color.Red;
                    break;
                case StatusTypes.General: 
                    statusLabel1.ForeColor = System.Drawing.Color.Gray;
                    break;
                default:
                    statusLabel1.ForeColor = System.Drawing.Color.Gray;
                    break;
            }
        }
    }
}
