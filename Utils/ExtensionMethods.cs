/********************************************************************************
*
* 类  名：ExtensionMethods
*
* 描  述：扩展方法类，包含DataGridView双缓冲、控件委托调用(WZD)。
*
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

        /// <summary>
        /// 对控件属性进行修改
        /// </summary>
        /// <param name="a">无参委托</param>
        public static void fAction(this Control ctrl, Action a)
        {
            DAction dAction = new DAction();
            dAction.fExe(ctrl, a);
        }

        /// <summary>
        /// 获取控件属性值
        /// </summary>
        /// <typeparam name="T">要获取属性的数据类型</typeparam>
        /// <param name="ctrl"></param>
        /// <param name="f">无参委托</param>
        /// <returns></returns>
        public static T fFunc<T>(this Control ctrl, Func<T> f)
        {
            DFunc<T> dAction = new DFunc<T>();
            return dAction.fExe(ctrl, f);
        }
    }

    /// <summary>
    /// 在线程中委托调用控件，对控件进行修改，避免跨线程控件调用报错。
    /// </summary>
    public class DAction
    {
        Action<Control, Action> action;

        public DAction()
        {
            action = new Action<Control, Action>(fExe);
        }

        public void fExe(Control _c, Action _a)
        {
            if (_c.InvokeRequired)
            {
                if (_c.FindForm() != null)
                {
                    _c.FindForm().Invoke(action, new object[] { _c, _a });
                }

            }
            else
            {
                _a();
            }
        }
    }

    /// <summary>
    /// 在线程中委托调用控件，获取控件属性，避免跨线程控件调用报错。
    /// <summary>
    public class DFunc<T>
    {
        Func<Control, Func<T>, T> func;

        public DFunc()
        {
            func = new Func<Control, Func<T>, T>(fExe);
        }

        public T fExe(Control _c, Func<T> _f)
        {
            if (_c.InvokeRequired)
            {
                if (_c.FindForm() != null)
                    return (T)_c.FindForm().Invoke(func, new object[] { _c, _f });
                else
                    return default;
            }
            else
            {
                return _f();
            }
        }
    }
}
