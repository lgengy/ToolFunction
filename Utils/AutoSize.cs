/********************************************************************
*
* 类  名：AutoSize
*
* 描  述：字体大小自适应。
* 
* 详  情：记录窗体原始大小O，窗体大小改变后与原始窗体大小的比例S，新的字体大小=O*S。
* 
* 使  用：1、创建此类全局对象；
*         2、在load函数或恰当的位置调用ControllInitializeSize函数，并把要自适应的控件作为参数传入；
*         3、监听要自适应控件的SizeChanged事件，在其中调用ControlAutoSize函数，，并把要自适应的控件作为参数传入。
* 
* 参  见：https://www.cnblogs.com/gguozhenqian/p/4288451.html
*         原文是窗体自适应，去掉了窗体自适应的内容，改为字体自适应
*
********************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProgrammeFrame
{
    public class AutoSize
    {
        /// <summary>
        /// 记录窗体和其控件的初始位置和大小。
        /// </summary>
        public struct ControlRect
        {
            public int Left;
            public int Top;
            public int Width;
            public int Height;
        }

        private List<ControlRect> oldCtrl = new List<ControlRect>();
        private int ctrlNo = 0;
        private float originalFontSize;

        public void ControllInitializeSize(Control mForm)
        {
            ControlRect cR;
            cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;
            oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
            AddControl(mForm);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            originalFontSize = mForm.Font.Size;
        }

        private void AddControl(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {
                //放在这里，是先记录控件的子控件，后记录控件本身
                ControlRect objCtrl;
                objCtrl.Left = c.Left; objCtrl.Top = c.Top; objCtrl.Width = c.Width; objCtrl.Height = c.Height;
                oldCtrl.Add(objCtrl);
                //放在这里，是先记录控件本身，后记录控件的子控件
                if (c.Controls.Count > 0)
                    AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
        }

        public void ControlAutoSize(Control mForm)
        {
            if (ctrlNo == 0)
            {
                //要在窗体的SizeChanged中，第一次改变大小时，记录控件原始的大小和位置,这里所有控件的子控件都已经形成
                ControlRect cR;
                cR.Left = 0; cR.Top = 0; cR.Width = mForm.PreferredSize.Width; cR.Height = mForm.PreferredSize.Height;

                oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
                AddControl(mForm);//窗体内其余控件可能嵌套其它控件(比如panel),故单独抽出以便递归调用
            }
            float wScale = mForm.Width / (float)oldCtrl[0].Width;//新旧窗体之间的比例，与最早的旧窗体
            float hScale = mForm.Height / (float)oldCtrl[0].Height;
            ctrlNo = 1;//进入=1，第0个为窗体本身,窗体内的控件,从序号1开始
            AutoScaleControl(mForm, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
        }

        private void AutoScaleControl(Control ctl, float wScale, float hScale)
        {
            //第1个是窗体自身的 Left,Top,Width,Height，所以窗体控件从ctrlNo=1开始
            foreach (Control c in ctl.Controls)
            {
                //if (Math.Min(wScale,hScale) != 1) 
                c.Font = new Font(c.Font.Name, originalFontSize * Math.Max(wScale, hScale), c.Font.Style, c.Font.Unit);
                //else c.Font = new Font(c.Font.Name, originalFontSize, c.Font.Style, c.Font.Unit);
                if (c.Controls.Count > 0)
                    AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
        }
    }
}
