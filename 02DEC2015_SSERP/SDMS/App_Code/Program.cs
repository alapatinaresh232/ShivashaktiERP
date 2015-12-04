using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace SDMS
{
    static class Program
    {
        private static Mutex m_Mutex = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool blAppliactionLoaded;
            Mutex m_Mutex = new Mutex(true, "SSCRM", out blAppliactionLoaded);
            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAreaEnabled;
           if(blAppliactionLoaded)
            Application.Run(new Login());
        }
    }
}
