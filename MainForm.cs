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
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ProgrammeFrame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GlobalData.logger.Info("".PadLeft(50,'=').PadRight(100,'='));
            GlobalData.logger.Info("程序启动".PadLeft(48, '=').PadRight(96, '='));
            GlobalData.logger.Info("版本号：1.0.0.1".PadLeft(51, '=').PadRight(96, '='));
            GlobalData.logger.Info("".PadLeft(50, '=').PadRight(100, '='));

            DeletingExpiredLogs(@"D:\Log\ProgrammeFrame\", 90);
        }

        /// <summary>
        /// 删除指定位置、指定日期前的日志
        /// </summary>
        /// <param name="logDir">位置</param>
        /// <param name="expiredDays">日期</param>
        private void DeletingExpiredLogs(string logDir, int expiredDays)
        {
            GlobalData.logger.Info("Inn ");
            try
            {
                List<string> listLogFile = Utils.GetFileFromPath(logDir);
                if (listLogFile.Count > 0)
                    foreach (string logPath in listLogFile)
                    {
                        FileInfo file = new FileInfo(logPath);
                        if ((DateTime.Now - file.LastWriteTime).TotalDays > expiredDays)
                        {
                            file.Delete();
                            GlobalData.logger.Info("Deleting log: " + file.Name);
                        }
                    }
            }
            catch (Exception ex)
            {
                GlobalData.logger.Warn(ex.Message);
                GlobalData.logger.Error(ex);
            }
            GlobalData.logger.Info("Out ");
        }
    }
}
