using ProgrammeFrame.Common;
using System.Windows.Forms;

namespace ProgrammeFrame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            GlobalData.logger.Info("==============================程序启动==============================");
            GlobalData.logger.Info("==========================版本号：1.0.0.1===========================");
            GlobalData.logger.Info("====================================================================");
        }
    }
}
