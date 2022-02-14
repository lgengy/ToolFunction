/********************************************************************
*
* 类  名：MainForm
*
* 作  者：lgengy
*
* 描  述：主界面函数
* 
* 使  用：1、建议通过脚手架一键生成开发环境；
*         2、默认使用通过配置文件设置的log4net，若要改为通过编程配置，请注释调Program类16行、GlobalData类18行，取消对GlobalData类19行的注释
*
********************************************************************/

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
