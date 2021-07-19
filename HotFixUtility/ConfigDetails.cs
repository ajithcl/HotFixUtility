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
        public string configFilePath;
        static Configurations configResult;

        // LastMessage - Property to hold the message from this class
        public static string LastMessage
        {
            get; private set;
        }

        public ConfigDetails(string configFilePath)
        {
            this.configFilePath = configFilePath;
            XmlSerializer ser = new XmlSerializer(typeof(Configurations));


            try
            {
            using (StreamReader sr = new StreamReader(this.configFilePath))
            {
                configResult = (Configurations)ser.Deserialize(sr);
            }
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Invalid configuration file.");
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("Invalid configuration directory.");
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

        public static Environment GetEnvironmentDetails(string environmentName)
        {
            foreach (Environment env in configResult.Environments)
            {
                if (env.EnvironmentName == environmentName)
                {
                    return env;
                }
            }
            LastMessage = "No Environment found.";
            return null;          
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
        {            get; set;
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

    public class RTBMapping
    {
        public RTBMapping(string rtbModule, string rtbDirectory)
        {
            this.RTBModule = rtbModule;
            this.RTBDirectory = rtbDirectory;
        }
        public RTBMapping()
        {
        }
        public string RTBModule
        {
            get; set;
        }
        public string RTBDirectory
        {
            get; set;
        }
    }

    [XmlRoot("RTBMappings")]
    public class RTBMappingList : List<RTBMapping>
    {
    }

    // root class
    public class Configurations
    {
        public string HotFixCommand
        {
            get; set;    
        }
        public string AsciiModuleList
        {
            get; set;
        }
        public EnvironmentList Environments
        {
            get;set;
        }
        public RTBMappingList RTBMappings
        {
            get; set;
        }
    }

    // Data structure for program input file
    public class ProgramData
    {
        public string ProgramName     {get; set;}
        public string ProgramModule   {get; set;}
        public ProgramData(string programName, string programModule)
        {
            this.ProgramName = programName;
            this.ProgramModule = programModule;
        }
    }
}
