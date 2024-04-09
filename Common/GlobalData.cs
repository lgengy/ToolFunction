/********************************************************************************

** 类名称： GlobalData

** 描述：存放全局使用的变量、函数等

*********************************************************************************/

using friUIMessageBox;
using log4net;
using ProgrammeFrame.AidedForm;
using ProgrammeFrame.Common.db;
using ProgrammeFrame.Entity;
using System.Windows.Forms;

namespace ProgrammeFrame.Common
{
    class GlobalData
    {
        public static CUIMessageBox messageBox = new CUIMessageBox();//提示框
        public static readonly ILog logger = LogManager.GetLogger("ProgrammeFrame");
        //public static readonly ILog logger = new Log(1).GetLogger("ProgrammeFrame", true,true, "ProgrammeFrame", "ProgrammeFrameErr", "D:\\Log\\ProgrammeFrame\\");
        public static EntityConfig config = EntityConfig.GetConfig($"{Application.StartupPath}\\ProgrammeFrame.xml");
        public static SQLOperation sqlOperation = new SQLOperation(0,config.ServerIP,config.DBName);
        public static NetRecoveryForm netRecoverForm = new NetRecoveryForm("Station Name", "System Name");//网络故障界面
    }
}
