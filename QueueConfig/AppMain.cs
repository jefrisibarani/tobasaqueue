using System;
using System.Windows.Forms;

namespace Tobasa
{
    static class AppMain
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormServerConfig());
        }
    }
}
