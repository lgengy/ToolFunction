/********************************************************************************

** 类名称： GlobalData

** 描述：存放全局使用的变量、函数等

*********************************************************************************/

using friUIMessageBox;
using log4net;
using ProgrammeFrame.Entity;

namespace ProgrammeFrame.Common
{
    class GlobalData
    {
        public static CUIMessageBox messageBox = new CUIMessageBox();//提示框
        public static readonly ILog logger = LogManager.GetLogger("ProgrammeFrame");
        //public static readonly ILog logger = new Log(1).GetLogger("ProgrammeFrame", true,true, "ProgrammeFrame", "ProgrammeFrameErr");
        public static EntityConfig config = EntityConfig.GetConfig();
        public static db.QueryControlImplement myQueryControl = new db.QueryControlImplement();
    }
}
