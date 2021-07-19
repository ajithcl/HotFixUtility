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

            if (!VerifyAllEnvironmentDirectories())
            {
                MessageBox.Show("Invalid directories found in configuration", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Close the form
                Load += (s, e) => Close();
                return;
                
            }    
        }
        private void LoadInitialSetups()
        {
            environmentXML = ConfigurationManager.AppSettings.Get("EnvironmentXML");
            // What if Configuration file doesn't have the EnvironmentXML file entry?
            // What if invalid file name?
            // TODO : Error validation should be here.

            ConfigDetails confDtl = new ConfigDetails(environmentXML);
            environmentList = confDtl.GetEnvironmentList();

            // Get the list of environments in the combobox.
            cmbEnvironment.Items.AddRange(environmentList.ToArray());

        }
        private bool VerifyAllEnvironmentDirectories()
        {
            // throw new NotImplementedException();
            // TODO
            return true;
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
            }
        }
    }
}
