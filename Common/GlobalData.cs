/********************************************************************************

** 类名称： GlobalData

** 描述：存放全局使用的变量、函数等

*********************************************************************************/

using friUIMessageBox;
using log4net;

namespace ProgrammeFrame.Common
{
    class GlobalData
    {
        public static CUIMessageBox messageBox = new CUIMessageBox();//提示框
        public static readonly ILog logger = LogManager.GetLogger("ProgrammeFrame");
    }
}
