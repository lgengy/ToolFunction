/********************************************************************************

** 类名称： ExtensionMethods

** 描述：扩展方法类

*********************************************************************************/

using System;
using System.Reflection;
using System.Windows.Forms;

namespace ProgrammeFrame
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// 双缓冲，避免DataGridView卡顿
        /// </summary>
        /// <param name="dgv">不用设置</param>
        /// <param name="setting">true</param>
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {

            Type dgvType = dgv.GetType();

            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",

            BindingFlags.Instance | BindingFlags.NonPublic);

            pi.SetValue(dgv, setting, null);

        }
    }
}
