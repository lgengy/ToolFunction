/********************************************************************************

** 类名称： SystemHotKey

** 描  述： 系统热键注册类。参见网络。

** 用  例： protected override void WndProc(ref Message msg)
            {
                try
                {
                    base.WndProc(ref msg);
                    switch (msg.Msg)
                    {
                        case 0x312: //窗口消息：热键
                            int tmpWParam = msg.WParam.ToInt32();
                            if (tmpWParam == 10001)
                            {
                                GlobalData.logger.Info("快捷键：Ctrl+Alt+D");
                                GlobalData.logger.Info($"一共删除了{GlobalData.sqlOperation.DeleteLongTimeAgoBagQueue()}条数据");
                            }
                            break;
                        case 0x1: //窗口消息：创建
                            SystemHotKey.RegHotKey(Handle, 10001, SystemHotKey.KeyModifiers.Alt | SystemHotKey.KeyModifiers.Ctrl, Keys.D);
                            break;
                        case 0x2: //窗口消息：销毁
                            SystemHotKey.UnRegHotKey(Handle, 10001); //销毁热键
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    GlobalData.logger.Error("WndProc", ex);
                }
            }

*********************************************************************************/

using ProgrammeFrame.Common;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProgrammeFrame
{
    public class SystemHotKey
    {
        /// <summary>
        /// 如果函数执行成功，返回值不为0。
        /// 如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复）</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk">定义热键的内容</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">要取消热键的ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 辅助键名称。
        /// Alt, Ctrl, Shift, WindowsKey
        /// </summary>
        [Flags()]
        public enum KeyModifiers { None = 0, Alt = 1, Ctrl = 2, Shift = 4, WindowsKey = 8 }

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        /// <param name="keyModifiers">组合键</param>
        /// <param name="key">热键</param>
        public static void RegHotKey(IntPtr hwnd, int hotKeyId, KeyModifiers keyModifiers, Keys key)
        {
            if (!RegisterHotKey(hwnd, hotKeyId, keyModifiers, key))
            {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode == 1409)
                {
                    GlobalData.logger.Warn("热键被占用！");
                }
                else
                {
                    GlobalData.logger.Warn("注册热键失败！错误代码：" + errorCode);
                }
            }
        }

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKey_id">热键ID</param>
        public static void UnRegHotKey(IntPtr hwnd, int hotKeyId)
        {
            //注销指定的热键
            UnregisterHotKey(hwnd, hotKeyId);
        }

    }
}
