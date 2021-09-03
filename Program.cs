using log4net.Config;
using System;
using System.IO;
using System.Windows.Forms;

namespace ProgrammeFrame
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure(new FileInfo($"{Application.StartupPath}\\ProgrammeFrame.xml"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
