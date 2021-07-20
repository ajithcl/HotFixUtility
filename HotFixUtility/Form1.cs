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

namespace HotFixUtility
{
    public partial class Form1 : Form
    {
        string environmentXML;
        List<string> environmentList;

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
            // TODO: What if Configuration file doesn't have the EnvironmentXML file entry?

            try
            {
                ConfigDetails confDtl = new ConfigDetails(environmentXML);
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
                    uiElement.BackColor = System.Drawing.Color.Gray;
                    break;
            }
        }

        private void btnTransferAsciiFiles_Click(object sender, EventArgs e)
        {
            ChangeBackgroundColor(this.btnTransferAsciiFiles, StatusTypes.Success);
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

            DataTable dt = Operations.ReadCSVFile(txtInputFile.Text);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Blank input file", "Input file error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

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
        }
    }
}
