using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HotFixUtility
{
    class ConfigDetails
    {
        string configFilePath;
        Configurations configResult;
        public ConfigDetails(string configFilePath)
        {
            this.configFilePath = configFilePath;
            XmlSerializer ser = new XmlSerializer(typeof(Configurations));

            using (StreamReader sr = new StreamReader(this.configFilePath))
            {
                configResult = (Configurations)ser.Deserialize(sr);
                //TODO: Error validation here.
            }
        }
        public List<string> GetEnvironmentList()
        {
            List<string> environmentList = new List<string>();
            EnvironmentList environment_list = configResult.Environments;
            foreach (Environment env in environment_list)
            {
                environmentList.Add(env.EnvironmentName);
            }


            return environmentList;
            
        }
    }
    [XmlType("Environment")]
    public class Environment
    {
        public Environment()
        {
        }
        public string EnvironmentName
        {
            get; set;
        }
        public string AsciiSourceDirectory
        {
            get; set;
        }
        public string AsciiDestinationDirectory
        {
            get; set;
        }
        public string SourceDirectory
        {
            get;set;
        }
        public string TargetDirectory
        {
            get; set;
        }
        public string ProlibProenvCommand
        {
            get; set;
        }
        public string AsciiProlibProenvCommand
        {
            get; set;
        }
        public string RTBSourceDirectory
        {
            get; set;
        }
        public string RTBDestinationDirectory
        {
            get; set;
        }
    }

    [XmlRoot("Environments")]
    public class EnvironmentList : List<Environment>
    {
    }

    public class Configurations
    {
        public string HotFixCommand
        {
            get; set;    
        }
        public EnvironmentList Environments
        {
            get;set;
        }
    }
}
