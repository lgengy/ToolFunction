/********************************************************************
*
* 类  名：MainForm
*
* 作  者：lgengy
*
* 描  述：主界面函数
* 
* 使  用：1、建议通过脚手架一键生成开发环境。
*
********************************************************************/

using ProgrammeFrame.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgrammeFrame
{
    public partial class MainForm : Form
    {
        private System.Timers.Timer timerLogDelete = new System.Timers.Timer(1000 * 60 * 60);//每隔一小时执行一次
        /// <summary>
        /// 标识是否网络故障
        /// </summary>
        private bool isNetBroken = false;

        public MainForm()
        {
            InitializeComponent();

            timerLogDelete.Elapsed += TimerLogDelete_Elapsed;
            GlobalData.netRecoverForm.NetBrokenEvent += NetRecoverForm_NetBrokenEvent;
            GlobalData.netRecoverForm.NetRecoverEvent += NetRecoverForm_NetRecoverEvent;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GlobalData.logger.Info("".PadLeft(50,'=').PadRight(100,'='));
            GlobalData.logger.Info("程序启动".PadLeft(48, '=').PadRight(96, '='));
            GlobalData.logger.Info("版本号：1.0.0.1".PadLeft(51, '=').PadRight(96, '='));
            GlobalData.logger.Info("".PadLeft(50, '=').PadRight(100, '='));

            Task.Factory.StartNew(() => { Utils.DeletingExpiredLogs(@"D:\Log\ProgrammeFrame\", 90); }).ContinueWith(task => timerLogDelete.Start());//创建任务删除过期日志，并在任务结束之后启动timerLogDelete
        }

        #region 系统事件
        private void TimerLogDelete_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //只在凌晨2点至3点删除日志
            if(DateTime.Now.Hour > 1 && DateTime.Now.Hour < 4)
            {
                GlobalData.logger.Info("检查过期日志");
                Task.Factory.StartNew(() => { Utils.DeletingExpiredLogs(@"D:\Log\ProgrammeFrame\", 90); });
            }
        }

        /// <summary>
        /// 网络恢复
        /// </summary>
        private void NetRecoverForm_NetRecoverEvent()
        {
            isNetBroken = false;
        }

        /// <summary>
        /// 网络故障
        /// </summary>
        private void NetRecoverForm_NetBrokenEvent()
        {
            isNetBroken = true;
        }
        #endregion
    }
}
