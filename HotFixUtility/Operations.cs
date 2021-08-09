using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;

// This class should contain all the operations for this hotfix related process.

namespace HotFixUtility
{
    class Operations
    {
        public static bool VerifyEnvironmentDirectories(string environmentName)
        {
            // TODO
            Environment env = ConfigDetails.GetEnvironmentDetails(environmentName);

            if (Directory.Exists(env.AsciiDestinationDirectory)
                && Directory.Exists(env.AsciiSourceDirectory)
                && Directory.Exists(env.SourceDirectory)
                && Directory.Exists(env.TargetDirectory)
                && Directory.Exists(env.RTBSourceDirectory)
                && Directory.Exists(env.RTBDestinationDirectory))
                return true;
            else
                return false;
        }

        public static DataTable ReadCSVFile(string inputFileName)
        {
            /*
             * CSV file should contain in below format:
             * program name, rtb module name
             */
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.CurrentCulture;
            string pathOnly = Path.GetDirectoryName(inputFileName);
            string fileName = Path.GetFileName(inputFileName);
            string sql = "SELECT * FROM [" + fileName + "]";

            using (OleDbConnection conn = new OleDbConnection(
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" 
                + pathOnly 
                + ";Extended Properties=\"Text;HDR=No\""))
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public static bool CopyFiles(ArrayList programNames, string sourceDirectory, string destinationDirectory)
        {
            string sourceFileName, destinationFileName;
            foreach (string programName in programNames)
            {
                sourceFileName = sourceDirectory + programName;
                destinationFileName = destinationDirectory + programName;
                
                try
                {
                    CopyFile(sourceFileName, destinationFileName);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return true;
        }

        public static bool CopyFile(string sourceFileName, string destinationFileName)
        {            
            if (!File.Exists(sourceFileName))
            {
                throw new Exception($" Source {sourceFileName} not exists!");
            }

            try
            {
                File.Copy(sourceFileName, destinationFileName, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }
        public static bool VerifyVersion(string fileName, string versionNumber)
        {
            
            string[] fileContents = File.ReadAllLines(fileName);
            string lineContent;
            int foundLineNumber = 0, fileLength, currentLineNumber = 1;
            fileLength = fileContents.Length;

            
            FileLogger.Log($"Filename : {fileName} Version: {versionNumber}");

            for (int i = 0; i < fileContents.Length; i++)
            {
                lineContent = fileContents[i];
                if (lineContent.Contains(versionNumber))
                {
                    foundLineNumber = i;
                    break;
                }
            }
            if (foundLineNumber == 0)
                return false;

            bool proceedPrint = true;
            int counter = 5; // TODO : Remove hardcoding.

            currentLineNumber = foundLineNumber - counter;
            //print previous lines
            while (proceedPrint)
            {
                if (currentLineNumber > 0)
                {
                    FileLogger.Log(fileContents[currentLineNumber]);
                }

                counter--;
                currentLineNumber -= counter;
                if (counter == 0)
                    proceedPrint = false;

            }


            //ActualLine
            FileLogger.Log(fileContents[foundLineNumber]);

            currentLineNumber = foundLineNumber;
            proceedPrint = true;

            // Print next lines.
            while (proceedPrint)
            {
                currentLineNumber++;
                if (currentLineNumber < fileContents.Length)
                {
                    FileLogger.Log(fileContents[currentLineNumber]); 
                    proceedPrint = true;
                }
                else
                    proceedPrint = false;
                counter += 1;
                if (counter > 5)
                    proceedPrint = false;
            }



            return true;
        }
    }

    public static  class FileLogger
    {
        public static string filePath = ConfigurationManager.AppSettings.Get("LogFile");
        public static void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath,true) )
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
    }
}
