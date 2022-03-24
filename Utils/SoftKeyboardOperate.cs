/********************************************************************
*
* 类  名：SoftKeyboardOperate
*
* 描  述：软键盘操作
* 
* 参  见：https://blog.csdn.net/u011267225/article/details/113555866
*
********************************************************************/

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ProgrammeFrame.Common;

namespace ProgrammeFrame
{
    public class SoftKeyboardOperate
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);
        private const UInt32 WM_SYSCOMMAND = 0x112;
        private const UInt32 SC_RESTORE = 0xf120;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        private const string OnScreenKeyboadApplication = "osk.exe";

        /// <summary>
        /// 启用系统软键盘
        /// </summary>
        public static void OpenKeyBoardFun()
        {
            try
            {
                // Get the name of the On screen keyboard
                string processName = Path.GetFileNameWithoutExtension(OnScreenKeyboadApplication);

                // Check whether the application is not running 
                var query = from process in Process.GetProcesses()
                            where process.ProcessName == processName
                            select process;

                var keyboardProcess = query.FirstOrDefault();

                // launch it if it doesn't exist
                if (keyboardProcess == null)
                {
                    IntPtr ptr = new IntPtr(); ;
                    bool sucessfullyDisabledWow64Redirect = false;

                    // Disable x64 directory virtualization if we're on x64,
                    // otherwise keyboard launch will fail.
                    if (System.Environment.Is64BitOperatingSystem)
                    {
                        sucessfullyDisabledWow64Redirect = Wow64DisableWow64FsRedirection(ref ptr);
                    }

                    // osk.exe is in windows/system folder. So we can directky call it without path
                    using (Process osk = new Process())
                    {
                        osk.StartInfo.FileName = OnScreenKeyboadApplication;
                        osk.Start();
                        //osk.WaitForInputIdle(2000);
                    }

                    // Re-enable directory virtualisation if it was disabled.
                    if (Environment.Is64BitOperatingSystem)
                        if (sucessfullyDisabledWow64Redirect)
                            Wow64RevertWow64FsRedirection(ptr);
                }

            }
            catch (Exception ex)
            {
                GlobalData.logger.Error("CloseKeyBoardFun", ex);
            }
        }

        /// <summary>
        /// 关闭系统软键盘
        /// </summary>
        public static void CloseKeyBoardFun()
        {
            try
            {
                Process[] pros = Process.GetProcessesByName("osk");
                foreach (Process p in pros)
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                GlobalData.logger.Error("CloseKeyBoardFun", ex);
            }
        }

        /// <summary>
        /// 软键盘是否打开
        /// </summary>
        /// <returns></returns>
        public static bool KeyboardOpend()
        {
            Process[] pro = Process.GetProcessesByName("osk");
            if (pro != null && pro.Length > 0)
            {
                //回到界面最上端(软键盘一直是在界面最上层的，这个针对的是最小化的情况)
                var windowHandle = pro[0].MainWindowHandle;
                SendMessage(windowHandle, WM_SYSCOMMAND, new IntPtr(SC_RESTORE), new IntPtr(0));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查找页面上的所有textbox，并绑定click事件打开软键盘
        /// </summary>
        /// <param name="controls"></param>
        public static void TextBoxTriggerKeyboard(Control.ControlCollection controls)
        {
            if (controls.Count > 0)
            {
                foreach (Control control in controls)
                {
                    if (control is TextBox)
                    {
                        TextBox txtBox = (TextBox)control;
                        txtBox.Click += new EventHandler(TxtBox_Click);
                    }
                    else
                    {
                        TextBoxTriggerKeyboard(control.Controls);
                    }
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 界面所有输入框的点击事件，用于触发软键盘
        /// </summary>
        private static void TxtBox_Click(object sender, EventArgs e)
        {
            if (!KeyboardOpend())
            {
                OpenKeyBoardFun();
            }
        }

    }
}
