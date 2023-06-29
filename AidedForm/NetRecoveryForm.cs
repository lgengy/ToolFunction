/********************************************************************************

** 类名称：NetRecoveryForm

** 描  述：故障恢复页面

** 作  者：lgy

*********************************************************************************/

using ProgrammeFrame.Common;
using System;
using System.Timers;
using System.Windows.Forms;

namespace ProgrammeFrame.AidedForm
{
    public partial class NetRecoveryForm : Form
    {
        private System.Timers.Timer timerNetRecover = new System.Timers.Timer(5000);//网络故障之后每5秒ping一次
        private System.Timers.Timer timerSystemTime = new System.Timers.Timer(1000);

        public bool isShowed = false;

        public delegate void DelNetBroken();
        public event DelNetBroken NetBrokenEvent;
        public delegate void DelNetRecover();
        public event DelNetRecover NetRecoverEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationName">故障恢复页面显示的站点名字</param>
        /// <param name="systemName">故障恢复页面显示的系统名字</param>
        public NetRecoveryForm(string stationName, string systemName)
        {
            InitializeComponent();

            lbl_StationName.Text = stationName;
            lbl_SystemName.Text = systemName;
            TopMost = true;

            timerNetRecover.Elapsed += new ElapsedEventHandler(TimerNetRecover_Event);

            timerSystemTime.Elapsed += TimerSystemTime_Elapsed;
        }

        private void TimerSystemTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            BeginInvoke(new Action(() => {
                lbl_SystemTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }));
        }

        private void TimerNetRecover_Event(object sender, ElapsedEventArgs e)
        {
            //先ping一次，如果通的话再ping十次，仍然通的话就说明网络以恢复
            if (Utils.NetWorkStatusVerify(GlobalData.config.ServerIP))
            {
                timerNetRecover.Stop();//ping十次需要5000ms，所以要停止计时器
                if (Utils.NetWorkStatusVerify(GlobalData.config.ServerIP, 10))
                {
                    BeginInvoke(new Action(() => {
                        Hide();
                        NetRecoverEvent();
                        timerSystemTime.Stop();
                        isShowed = false;
                        GlobalData.logger.Warn("网络恢复");
                    }));
                }
                else timerNetRecover.Start();
            }
        }

        public void ShowNetRecoverForm()
        {
            Show();
            NetBrokenEvent();
            timerNetRecover.Start();
            timerSystemTime.Start();
            isShowed = true;
            GlobalData.logger.Warn("网络故障");
        }
    }
}
