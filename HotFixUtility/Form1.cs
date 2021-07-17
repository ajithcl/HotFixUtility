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
        }
        private void LoadInitialSetups()
        {
            environmentXML = ConfigurationManager.AppSettings.Get("EnvironmentXML");
            // TODO : Error validation should be here.

            ConfigDetails confDtl = new ConfigDetails(environmentXML);
            environmentList = confDtl.GetEnvironmentList();
            cmbEnvironment.Items.AddRange(environmentList.ToArray());
            cmbEnvironment.SelectedIndex=0;

        }
    }
}
