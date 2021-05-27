/********************************************************************
*
* 类  名：ToolFunction
*
* 作  者：lgengy
*
* 描  述：记录工作中经常用到的一些工具函数。
*
********************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using ToolFunction.Common;
using ToolFunction.Utils;

public class Utils
{
    #region 数据库相关
    /// <summary>
    /// 判断DataSet变量里是否有数据
    /// </summary>
    /// <returns>true-有数据 false-没数据</returns>
    public static bool CheckDBSetValidData(DataSet ds)
    {
        bool returnValue = false;
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                returnValue = true;
            }
            else
            {
                GlobalData.logger.WriteTraceInfor("No datas");
            }
        }
        return returnValue;
    }

    /// <summary>
    /// 判断datarow的列元素是否为null或空
    /// </summary>
    /// <param name="el">DataRow row["CloumnName"]</param>
    /// <returns>true-有数据 false-没数据</returns>
    public static bool CheckRowElement(object el)
    {
        if (el != DBNull.Value && !string.IsNullOrEmpty(el.ToString()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region 文件（夹）相关

    /// <summary>
    /// 读XML文件
    /// </summary>
    /// <param name="XMLNodeName">节点路径</param>
    /// <param name="XMLElementName">节点名</param>
    /// <param name="DefaultValue">出现异常时的默认返回值</param>
    /// <param name="XMLFileName">xml文件名</param>
    /// <returns></returns>
    public static string ReadXMLString(string XMLNodeName, string XMLElementName, string DefaultValue, string XMLFileName)
    {
        try
        {
            XmlDocument XMLData = new XmlDocument();
            XMLData.LoadXml(XMLFileName);

            XmlNode xnUser = XMLData.SelectSingleNode(XMLNodeName);
            string strValue = DefaultValue;
            if (xnUser[XMLElementName] != null)
            {
                strValue = xnUser[XMLElementName].InnerText;
            }
            return strValue;
        }
        catch (Exception)
        {
            return DefaultValue;
        }
    }

    /// <summary>
    /// 写XML文件
    /// </summary>
    /// <param name="XMLNodeName">完整结点路径</param>
    /// <param name="value">要写入的值</param>
    /// <param name="XMLFileName">xml文件名</param>
    /// <returns></returns>
    public static void WriteXMLString(string XMLNodeName, string value, string XMLFileName)
    {
        try
        {
            XmlDocument XMLData = new XmlDocument();
            XMLData.Load(XMLFileName);

            XmlNode xnUser = XMLData.SelectSingleNode(XMLNodeName);
            if (xnUser != null)
            {
                xnUser.InnerText = value.ToString();
                XMLData.Save(XMLFileName);
            }
        }
        catch (Exception ex)
        {
            GlobalData.logger.WriteErrorInfor("WriteXMLString:" + ex.Message);
        }
    }

    /// <summary>
    /// 获取路径下的所有文件
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>文件list</returns>
    public static List<string> GetFileFromPath(string path)
    {
        GlobalData.logger.WriteTraceInfor(">GetFileFromPath[" + path + "]");
        List<string> re = null;
        try
        {
            if (!string.IsNullOrWhiteSpace(path))//字段存在
            {
                if (Directory.Exists(path))//目录存在
                {
                    re = Directory.GetFiles(path).ToList();
                }
                else
                {
                    GlobalData.logger.WriteTraceInfor("No path");
                }
            }
        }
        catch (Exception ex)
        {
            GlobalData.logger.WriteTraceInfor("exception: " + ex.Message);
            GlobalData.logger.WriteErrorInfor("GetFileFromPath: " + ex.StackTrace);
        }
        GlobalData.logger.WriteTraceInfor("<GetFileFromPath");
        return re;
    }

    /// <summary>
    /// 从路径中获取目录
    /// </summary>
    /// <param name="path">完整路径</param>
    /// <param name="level">级别（多级嵌套目录）,默认（0）返回完整目录。路径中没有文件名时请指定级别。</param>
    /// <returns></returns>
    /// <remarks>C:\a\b\c\d\e.txt，0返回C:\a\b\c\d\，1返回C:\，以此类推</remarks>
    public static string GetDirectoryFromPath(string path, int level = 0)
    {
        GlobalData.logger.WriteTraceInfor(">GetDirectoryFromPath");
        string returnDic = "";
        try
        {
            List<string> dic = path.Split('\\').ToList<string>();
            if (level == 0 || level >= (dic.Count - 1))//如果所要级别为0或大于等于当前路径中目录级别
            {
                dic.RemoveAt(dic.Count - 1);
                returnDic = string.Join("\\", dic.ToArray());
            }
            if (level < (dic.Count - 1) && level > 0)
            {
                dic.RemoveRange(level, dic.Count - level);
                returnDic = string.Join("\\", dic.ToArray());
            }
        }
        catch (Exception ex)
        {
            GlobalData.logger.WriteTraceInfor("exception: " + ex.Message);
            GlobalData.logger.WriteErrorInfor("GetDirectoryFromPath: " + ex.StackTrace);
        }
        GlobalData.logger.WriteTraceInfor("<GetDirectoryFromPath[" + returnDic + "]");
        return returnDic;
    }

    /// <summary>
    /// 从完整路径中获取文件名
    /// </summary>
    public static string FindFileNameInPath(string pathName)
    {
        int lastBiasPosition = pathName.LastIndexOf(@"\");

        return pathName.Substring(lastBiasPosition + 1, pathName.Length - lastBiasPosition - 1);
    }

    /// <summary>
    /// 目录中是否有文件
    /// </summary>
    /// <param name="dir">目录</param>
    /// <param name="searchPattern">匹配模式，默认为空</param>
    /// <returns></returns>
    public static bool HaveFiles(string dir, string searchPattern = "")
    {
        if (string.IsNullOrEmpty(searchPattern))
            return Directory.GetFiles(dir).Length > 0;
        else
            return Directory.GetFiles(dir, searchPattern).Length > 0;
    }

    /// <summary>
    /// 删除路径下的所有文件
    /// </summary>
    /// <param name="path">多个路径以逗号“,”进行区分</param>
    public static void DeleteAllFilesFromPath(string paths)
    {
        GlobalData.logger.WriteTraceInfor(">DeleteAllFilesFromPath");

        string[] pathArray = paths.Split(',');

        foreach (string path in pathArray)
        {
            try
            {
                if (Directory.Exists(path))//目录存在
                {
                    Directory.Delete(path, true);
                    GlobalData.logger.WriteTraceInfor("路径: " + path + "已删除");
                }
            }
            catch (Exception ex)
            {
                GlobalData.logger.WriteTraceInfor("exception: " + ex.Message);
                GlobalData.logger.WriteErrorInfor("DeleteAllFilesFromPath: " + ex.StackTrace);
            }
        }

        GlobalData.logger.WriteTraceInfor("<DeleteAllFilesFromPath");
    }
    #endregion

    #region 显示相关
    /// <summary>
    /// picturebox、button、label显示图片
    /// </summary>
    /// <param name="com">控件名</param>
    /// <param name="type">控件类型</param>
    /// <param name="imgPath">图片完整路径</param>
    /// <remarks>winform页面通过BeginInvoke调用此方法，可有效避免程序卡顿</remarks>
    private void DisplayPicture(object com, string type, string imgPath)
    {
        GlobalData.logger.WriteTraceInfor(">DisplayPicture[" + type + " 显示：" + imgPath + "]");

        if (!string.IsNullOrEmpty(imgPath))
        {

            Image img = null;
            Bitmap bmp = null;

            try
            {
                if (!string.IsNullOrEmpty(imgPath))
                {
                    img = Image.FromFile(imgPath);
                    bmp = new Bitmap(img);

                    switch (type)
                    {
                        case "picturebox":
                            PictureZoom.LoadImg(com as PictureBox, bmp);//使图片可缩放
                            //(com as PictureBox).Image = bmp;
                            break;
                        case "button":
                            (com as Button).BackgroundImage = bmp;
                            break;
                        case "label":
                            (com as Label).Image = bmp;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalData.logger.WriteTraceInfor("exception: " + ex.Message);
                GlobalData.logger.WriteErrorInfor("DisplayPicture[exception]: " + ex.StackTrace);
            }
            finally
            {
                if (img != null)
                    img.Dispose();
            }
        }

        GlobalData.logger.WriteTraceInfor("<DisplayPicture");
    }

    /// <summary>
    /// 判断string是否为空，非空的话显示到控件
    /// </summary>
    /// <param name="com">控件名</param>
    /// <param name="type">组件类型：label、textbox、combobox</param>
    /// <param name="text">单字段显示</param>
    /// <param name="texts">多个字段显示到一个控件</param>
    public static void DisplayText(object com, string type, string text, params string[] texts)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            switch (type)
            {
                case "label":
                    (com as Label).Text = text;
                    break;
                case "textbox":
                    (com as TextBox).Text = text;
                    break;
                case "combobox":
                    (com as ComboBox).SelectedText = text;
                    break;
            }
        }
        if (texts.Length > 0)
        {
            foreach (string el in texts)
                if (!string.IsNullOrWhiteSpace(el))
                {
                    switch (type)
                    {
                        case "label":
                            (com as Label).Text += " " + el;
                            break;
                        case "textbox":
                            (com as TextBox).Text += " " + el;
                            break;
                        case "combobox":
                            (com as ComboBox).Text += " " + el;
                            break;
                    }
                }
        }
    }
    #endregion

    #region 合规性判断

    public static bool IsDigital(string text)
    {
        if (!string.IsNullOrEmpty(text) && !Regex.IsMatch(text, @"^[1-9]\d*$"))
        {
            GlobalData.messageBox.ShowDialog("请输入正整数！", friUIMessageBox.CUIMessageBox.MessageBoxButton.OKOnly, friUIMessageBox.CUIMessageBox.MessageBoxIcon.Warning, "警告");
            return false;
        }
        return true;
    }

    public static bool IsFloat(string text)
    {
        if (!string.IsNullOrEmpty(text) && !Regex.IsMatch(text, @"^\d+\.\d{1,2}$"))
        {
            GlobalData.messageBox.ShowDialog("请精确到小数点后两位！", friUIMessageBox.CUIMessageBox.MessageBoxButton.OKOnly, friUIMessageBox.CUIMessageBox.MessageBoxIcon.Warning, "警告");
            return false;
        }
        return true;
    }
    #endregion

    #region 网络相关
    /// <summary>
    /// 网络状态检测
    /// </summary>
    /// <returns>true-连接正常   false-无法连接</returns>
    public static bool NetWorkStatusVerify(string ip, int checkCount = 5)
    {
        int pingCount = 0;
        IPStatus pingStatus = IPStatus.BadRoute;
        try
        {
            while (++pingCount != checkCount)
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(ip, 100);
                pingStatus = reply.Status;
                if (pingStatus == IPStatus.Success) break;
            }

            if (pingStatus == IPStatus.Success)
            {
                return true;
            }
            else
            {
                GlobalData.logger.WriteTraceInfor(">>>>>>>>ping" + ip + "失败，ping了" + pingCount + "次<<<<<<<<");

                return false;
            }
        }
        catch (Exception ex)
        {
            GlobalData.logger.WriteErrorInfor("NetWorkStatusVerify[网络连接检测出现异常]：" + ex.Message);
            return false;
        }
    }

    /// <summary>
    /// 使可访问远程主机的共享目录
    /// </summary>
    /// <param name="ip">远程主机IP</param>
    /// <param name="userName">远程主机用户名</param>
    /// <param name="pwd">远程主机密码</param>
    public static void NetUseServer(string ip, string userName, string pwd)
    {
        Process process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
        process.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
        process.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
        process.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
        process.StartInfo.CreateNoWindow = true;          //不显示程序窗口
        process.Start();
        process.StandardInput.WriteLine(@"net use \\" + ip + " /user:" + userName + " " + pwd + " &&exit");//向cmd窗口发送输入信息
    }

    /// <summary>
    /// 获取与指定网段相匹配的第一个IP
    /// </summary>
    public static string GetLocalIPMatchedSubNetwork(string subNetWork)
    {
        string ip = "";
        foreach (IPAddress el in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (el.ToString().Contains(subNetWork))
            {
                ip = el.ToString();
                break;
            }
        }
        return ip;
    }
    #endregion

    #region 类型转换
    /// <summary>
    /// image转byte数组
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public static byte[] ChangeImageToByteArray(Image image)
    {
        try
        {
            Bitmap bm = new Bitmap(image);
            MemoryStream ms = new MemoryStream();
            bm.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            bm.Dispose();

            return arr;
        }
        catch (Exception ex)
        {
            GlobalData.logger.WriteTraceInfor("exception: Fail to change image to byte array! " + ex.Message);
            GlobalData.logger.WriteErrorInfor("ChangeImageToByteArray: " + ex.StackTrace);
            return null;
        }
    }
    #endregion

    #region 其它
    /// <summary>
    /// 延时
    /// </summary>
    public static void DelayTime(int IntervalbyMilliseconds)
    {
        DateTime StartTime = DateTime.Now;
        TimeSpan Interval;
        do
        {
            Application.DoEvents();
            Interval = DateTime.Now - StartTime;
        } while (Interval.TotalMilliseconds < IntervalbyMilliseconds);
    }

    /// <summary>
    /// 生成指定长度随机数
    /// </summary>
    /// <param name="iLength"></param>
    /// <returns></returns>
    public static string GetRandomString(int iLength)
    {
        string buffer = "0123456789";// 随机字符中也可以为汉字（任何）
        StringBuilder sb = new StringBuilder();
        Random r = new Random();
        int range = buffer.Length;
        for (int i = 0; i < iLength; i++)
        {
            sb.Append(buffer.Substring(r.Next(range), 1));
        }
        return sb.ToString();
    }

    private void EmptyFunftion()
    {
        GlobalData.logger.WriteTraceInfor(">");
        try
        {

        }
        catch (Exception ex)
        {
            GlobalData.logger.WriteTraceInfor("exception: " + ex.Message);
            GlobalData.logger.WriteErrorInfor(": " + ex.StackTrace);
        }
        GlobalData.logger.WriteTraceInfor("<");
    }
    #endregion
}