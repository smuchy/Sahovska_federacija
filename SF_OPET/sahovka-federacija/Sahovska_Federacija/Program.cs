using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sahovska_Federacija
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// dodala sam komentar ljubavi *cao kiss pozz*
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());
        }
    }
}
