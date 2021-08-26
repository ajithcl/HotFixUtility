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

            ArrayList programList = new ArrayList();
            string sourceDir, destDir;
            string asciiModuleList = ConfigDetails.GetAsciiModuleList();
            FileLogger.Log("--ASCII File Transfer--");

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
            UpdateStatusLabel("Ascii file transfer - processed.", StatusTypes.General);
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
            FileLogger.Log($"{txtInputFile.Text} loaded.");
            UpdateStatusLabel("Program list loaded.", StatusTypes.Success);
        }
        private void UpdateProcessButtons(bool action)
        {
            btnVersionCheckAll.Enabled = action;
            btnVersionAsciiCheck.Enabled = action;
            btnTransferFiles.Enabled = action;
            btnTransferAsciiFiles.Enabled = action;
            btnAddAsciiFileProlib.Enabled = action;
            btnAddProlibFiles.Enabled = action;
            btnCreateHF.Enabled = action;
            btnRTBTransfer.Enabled = action;

            ChangeBackgroundColor(btnVersionCheckAll, StatusTypes.General);
            ChangeBackgroundColor(btnVersionAsciiCheck, StatusTypes.General);
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
            FileLogger.Log($"Environment: {selectedEnvironment}");
        }

        private void btnTransferFiles_Click(object sender, EventArgs e)
        {
            ArrayList programList = new ArrayList();
            string sourceDir, destDir;
            FileLogger.Log("--All Files Transfer--");
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
            UpdateStatusLabel("File transfer processed", StatusTypes.General);
        }

        private void btnAddAsciiFileProlib_Click(object sender, EventArgs e)
        {
            List<string> programList = new List<string>();
            string proenvCommand = confDtl.GetProEnvCommand();
            Environment env_detail = ConfigDetails.GetEnvironmentDetails(selectedEnvironment);
            string fileName = applicationDirectory + "Asciiprolibcommands.txt";
            string asciiModuleList = ConfigDetails.GetAsciiModuleList();
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
            string sourceDir, sourceFile, destinationDirectory,destinationFile;

            Environment env_detail = ConfigDetails.GetEnvironmentDetails(selectedEnvironment);
            sourceDir = env_detail.RTBSourceDirectory;
            destinationDirectory = env_detail.RTBDestinationDirectory;

            FileLogger.Log("RTB Transfer starting..");

            // Get the RTB mappings from the environment file.
            DataTable dtRTBMappings = ConfigDetails.GetRTBMappings();

            DataTable dtInputFileRTB = new DataTable();
            dtInputFileRTB.Columns.Add("FileName", typeof(string));
            dtInputFileRTB.Columns.Add("SourceFile", typeof(string));
            dtInputFileRTB.Columns.Add("RTBModule", typeof(string));

            foreach(DataRow row in dtInputFile.Rows)
            {
                // source file = Source Directory + rtb module +File name 
                sourceFile = sourceDir + row.ItemArray[1] + @"\" + row.ItemArray[2];
                dtInputFileRTB.Rows.Add(row.ItemArray[2], sourceFile, row.ItemArray[1]);
            }

            foreach (DataRow rowInput in dtInputFileRTB.Rows)
            {
                foreach (DataRow rowRTB in dtRTBMappings.Select($"Module = '{rowInput.Field<string>("RTBModule")}'"))
                {
                    sourceFile = rowInput.Field<string>("SourceFile");
                    destinationFile = destinationDirectory + rowRTB.Field<string>("Directory") + @"/" + rowInput.Field<string>("FileName");
                    
                    try
                    {
                        Operations.CopyFile(sourceFile, destinationFile);
                        FileLogger.Log($"RTB traansfer from {sourceFile} to {destinationFile}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Transfer error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        FileLogger.Log(ex.Message);
                    }
                }
            }
            ChangeBackgroundColor(btnRTBTransfer, StatusTypes.Success);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Input file csv format :\nCompiled object name, RTB module name, source program name, version number",
                            "Inforamation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCreateHF_Click(object sender, EventArgs e)
        {
            Environment env_detail = ConfigDetails.GetEnvironmentDetails(selectedEnvironment);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = confDtl.GetHotfixCommand();
            startInfo.WorkingDirectory = env_detail.HotFixCommandWorkingDirectory;
            process.StartInfo = startInfo;
            process.Start();

            ChangeBackgroundColor(btnCreateHF, StatusTypes.Success);
        }

        private void btnVersionCheckAll_Click(object sender, EventArgs e)
        {
            string programName, programVersion, fileName;

            Environment env_detail = ConfigDetails.GetEnvironmentDetails(selectedEnvironment);

            FileLogger.Log("Normal Version Checking..");
            Cursor.Current = Cursors.WaitCursor;
            for (int index = 0; index < dtInputFile.Rows.Count; index++)
            {
                //programList.Add(dtInputFile.Rows[index][0].ToString());
                programName = dtInputFile.Rows[index][0].ToString();
                programVersion = dtInputFile.Rows[index][3].ToString();

                fileName = env_detail.SourceDirectory + programName;

                UpdateStatusLabel("Version checking..", StatusTypes.General);                
                Operations.VerifyVersion(fileName, programVersion);
            }

            Cursor.Current = Cursors.Default;
            ChangeBackgroundColor(btnVersionCheckAll, StatusTypes.Success);
            UpdateStatusLabel("Version check Completed.", StatusTypes.General);
        }

        private void btnVersionAsciiCheck_Click(object sender, EventArgs e)
        {
            string programName, programVersion, fileName,moduleName, asciiModuleList;
            FileLogger.Log("Ascii Version Checking..");

            if (dtInputFile is null)
            {
                MessageBox.Show("Blank Input File", "Invalid input file", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ChangeBackgroundColor(btnVersionAsciiCheck, StatusTypes.Error );
                UpdateStatusLabel("Invalid input file.", StatusTypes.Error);
                return;
            }

            Environment env_detail = ConfigDetails.GetEnvironmentDetails(selectedEnvironment);
            asciiModuleList = ConfigDetails.GetAsciiModuleList();

            Cursor.Current = Cursors.WaitCursor;
            for (int index = 0; index < dtInputFile.Rows.Count; index++)
            {
                moduleName = dtInputFile.Rows[index][1].ToString();
                if (asciiModuleList.Split(',').Contains(moduleName))
                {
                    programName = dtInputFile.Rows[index][0].ToString();
                    programVersion = dtInputFile.Rows[index][3].ToString();
                    fileName = env_detail.AsciiSourceDirectory + programName;

                    UpdateStatusLabel("Ascii Version checking..", StatusTypes.General);
                    Operations.VerifyVersion(fileName, programVersion);
                }                 
            }
            Cursor.Current = Cursors.Default;
            ChangeBackgroundColor(btnVersionAsciiCheck, StatusTypes.Success);
            UpdateStatusLabel("Ascii Versions checked.", StatusTypes.General);
        }

        private void UpdateStatusLabel(string message, StatusTypes status)
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
