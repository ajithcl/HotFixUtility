using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
    }
}
