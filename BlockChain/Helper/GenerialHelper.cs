using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Helper
{
    public static class GenerialHelper
    {
        #region Properteis

        #region [ExePath]
        public static string ExePath { get; set; } = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        #endregion

        #endregion //Properteis 
    }
}
