using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;

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

        public static  List<ProgramData> LoadInputFile(string inputFileName)
        {
            List<ProgramData> programList = new List<ProgramData>();
            using (var reader = new StreamReader(inputFileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    {
                        programList.Add(new ProgramData(values[0], values[1]));
                    };
                }
            }
            return programList;
        }

        public static DataTable ReadCSVFile(string inputFileName)
        {
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
        svm }
    }
}
