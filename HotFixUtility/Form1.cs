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
    }
}
