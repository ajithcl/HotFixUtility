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
                if (!File.Exists(sourceFileName))
                {
                    throw new Exception($"{sourceFileName} not exists!");
                }
                destinationFileName = destinationDirectory + programName;
                try
                {
                    File.Copy(sourceFileName, destinationFileName,true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return true;
        }
    }
}
