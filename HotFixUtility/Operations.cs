using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFixUtility
{
    class Operations
    {
        public static bool VerifyEnvironmentDirectories(string environmentName)
        {
            // throw new NotImplementedException();
            // TODO
            Environment env = ConfigDetails.GetEnvironmentDetails(environmentName);
            return true;
        }
    }
}
