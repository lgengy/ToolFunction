/********************************************************************
*
* 类  名：Utils
*
* 作  者：lgengy
*
* 描  述：记录工作中经常用到的一些工具函数。
*
********************************************************************/

using Microsoft.Win32;
using ProgrammeFrame.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

public class Utils
{
    #region 数据库相关
    /// <summary>
    /// 判断DataSet变量里是否有数据
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="objectt">数据集标识名</param>
    /// <returns>true-有数据 false-没数据</returns>
    public static bool CheckDBSetValidData(DataSet ds)
    {
        bool returnValue = false;
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                returnValue = true;
            }
        }
        return returnValue;
    }
    #endregion

    #region 文件（夹）相关
    /// <summary>
    /// 获取xml文件中NodeType为Text的所有节点信息
    /// </summary>
    /// <param name="xmlNodeList">xml node集合</param>
    /// <param name="dic">保存节点信息的键值对(节点名，节点值)</param>
    /// <param name="info">函数执行过程中的提示信息</param>
    public static void GetAllXMLNode(XmlNodeList xmlNodeList, Dictionary<string, string> dic, out string info)
    {
        info = "";
        try
        {
            foreach (XmlNode node in xmlNodeList)
            {
                if (!node.HasChildNodes && node.NodeType == XmlNodeType.Text)
                {
                    if (!dic.ContainsKey(node.ParentNode.Name))
                        dic.Add(node.ParentNode.Name, node.InnerText);
                    else
                        info += $"{node.ParentNode.Name}重复，值{node.InnerText}，已保存的值为{dic[node.ParentNode.Name]}\n";
                }
                else
                {
                    GetAllXMLNode(node.ChildNodes, dic, out info);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 读取txt文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static StringBuilder ReadTXT(string path)
    {
        StringBuilder sb = new StringBuilder();
        StreamReader sr = new StreamReader(path, Encoding.Default);
        try
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                sb.Append(line);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            sr.Close();
        }
        return sb;
    }

    /// <summary>
    /// 读XML文件的指定节点
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
    /// 把值写入XML文件的指定节点
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
            throw ex;
        }
    }

    /// <summary>
    /// 获取路径下的所有文件
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="delEmptyPath">删除空目录</param>
    /// <param name="comparison">对文件进行排序的委托</param>
    /// <returns>文件list</returns>
    public static List<string> GetFileFromPath(string path, bool delEmptyPath = false, Comparison<string> comparison = null)
    {
        List<string> re = new List<string>();
        try
        {
            if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
            {
                re.AddRange(Directory.GetFiles(path));
                if (Directory.GetDirectories(path).Length > 0)
                {
                    foreach (string dir in Directory.GetDirectories(path))
                    {
                        re.AddRange(GetFileFromPath(dir, delEmptyPath, comparison));
                    }
                }
                if (comparison != null && re.Count > 0) re.Sort(comparison);
                if (delEmptyPath && re.Count == 0) Directory.Delete(path);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
        string returnDir = "";
        try
        {
            List<string> dic = path.Split('\\').ToList<string>();
            if (level == 0 || level >= (dic.Count - 1))//如果所要级别为0或大于等于当前路径中目录级别
            {
                dic.RemoveAt(dic.Count - 1);
                returnDir = string.Join("\\", dic.ToArray());
            }
            if (level < (dic.Count - 1) && level > 0)
            {
                dic.RemoveRange(level, dic.Count - level);
                returnDir = string.Join("\\", dic.ToArray());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnDir;
    }

    /// <summary>
    /// 从完整路径中获取最后一个"\"之后的文件名(没有文件的话就是目录名)
    /// </summary>
    /// <param name="pathName">路径</param>
    /// <param name="keepSuffix">是否保留后缀，默认保留</param>
    public static string FindFileNameInPath(string pathName, bool keepSuffix = true)
    {
        int lastBiasPosition = pathName.LastIndexOf(@"\");

        if (keepSuffix)
            return pathName.Substring(lastBiasPosition + 1, pathName.Length - lastBiasPosition - 1);
        else
            return pathName.Substring(lastBiasPosition + 1, pathName.Length - lastBiasPosition - 1).Split('.')[0];
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
    /// <param name="info">函数执行过程中的提示信息</param>
    public static void DeleteAllFilesFromPath(string paths, out string info)
    {
        string[] pathArray = paths.Split(',');
        info = "";

        foreach (string path in pathArray)
        {
            try
            {
                if (Directory.Exists(path))//目录存在
                {
                    Directory.Delete(path, true);
                    info += "路径: " + path + "已删除\n";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    /// <summary>
    /// 删除指定位置、指定日期前的日志
    /// </summary>
    /// <param name="logDir">位置</param>
    /// <param name="expiredDays">日期</param>
    /// <param name="info">函数执行过程中的提示信息</param>
    public static void DeletingExpiredLogs(string logDir, int expiredDays, out string info)
    {
        info = "";
        try
        {
            if (!string.IsNullOrWhiteSpace(logDir))
            {
                List<string> listLogFile = GetFileFromPath(logDir);
                if (listLogFile.Count > 0)
                    foreach (string logPath in listLogFile)
                    {
                        FileInfo file = new FileInfo(logPath);
                        if ((DateTime.Now - file.LastWriteTime).TotalDays > expiredDays && file.Exists)
                        {
                            file.Delete();
                            info += "Deleting log: " + file.FullName + "\n";
                        }
                    }
            }
            else
                info += "请输入有效日志目录";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 把一个文件夹下所有文件复制到另一个文件夹下 
    /// </summary>
    /// <param name="sourceDirectory">源目录</param>
    /// <param name="targetDirectory">目标目录</param>
    /// <param name="exceptDir">不用复制的目录</param>
    /// <param name="exceptFile">不用复制的文件</param>
    /// <see cref="https://blog.csdn.net/CatchMe_439/article/details/54614404"/>
    public static void DirectoryCopy(string sourceDirectory, string targetDirectory, string[] exceptDir = null, string[] exceptFile = null)
    {
        try
        {
            if (!string.IsNullOrEmpty(sourceDirectory) && !string.IsNullOrEmpty(targetDirectory))
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirectory);
                //获取目录下（不包含子目录）的文件和子目录
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();

                if (!Directory.Exists(targetDirectory)) Directory.CreateDirectory(targetDirectory);

                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (exceptDir == null || (exceptDir != null && !exceptDir.Contains(i.Name)))
                        {
                            if (!Directory.Exists(targetDirectory + "\\" + i.Name))
                            {
                                //目标目录下不存在此文件夹即创建子文件夹
                                Directory.CreateDirectory(targetDirectory + "\\" + i.Name);
                            }
                            //递归调用复制子文件夹
                            DirectoryCopy(i.FullName, targetDirectory + "\\" + i.Name, exceptDir, exceptFile);
                        }
                    }
                    else
                    {
                        //不是文件夹即复制文件，true表示可以覆盖同名文件
                        if (exceptFile == null || (exceptFile != null && !exceptFile.Contains(i.Name)))
                            File.Copy(i.FullName, targetDirectory + "\\" + i.Name, true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Component显示相关
    /// <summary>
    /// picturebox、button、label显示图片
    /// </summary>
    /// <param name="com">控件名</param>
    /// <param name="type">控件类型</param>
    /// <param name="imgPath">图片完整路径</param>
    /// <remarks>winform页面通过BeginInvoke调用此方法，可有效避免程序卡顿</remarks>
    public static bool DisplayPicture(object com, string type, string imgPath)
    {
        bool result = false;

        try
        {
            if (!string.IsNullOrEmpty(imgPath))
            {
                if (type.Equals("picturebox"))
                    (com as PictureBox).ImageLocation = imgPath;
                else
                {
                    FileStream fs = new FileStream(imgPath, FileMode.Open);
                    Image img = Image.FromStream(fs);

                    switch (type)
                    {
                        case "button":
                            if ((com as Button).BackgroundImage != null)
                            {
                                (com as Button).BackgroundImage.Dispose();
                                (com as Button).BackgroundImage = null;
                            }
                            (com as Button).BackgroundImage = img;
                            break;
                        case "label":
                            if ((com as Label).Image != null)
                            {
                                (com as Label).Image.Dispose();
                                (com as Label).Image = null;
                            }
                            (com as Label).Image = img;
                            break;
                    }

                    fs.Close();
                    fs.Dispose();
                }

                result = true;
                GC.Collect();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return result;
    }

    /// <summary>
    /// 判断string是否为空，非空的话显示到控件
    /// </summary>
    /// <param name="com">控件名</param>
    /// <param name="type">组件类型：label、textbox、combobox</param>
    /// <param name="separator">分隔符</param>
    /// <param name="texts">要显示的</param>
    public static string DisplayText(object com, string type, string separator, params string[] texts)
    {
        string showText = "";

        foreach (string el in texts)
            showText +=  el + separator;

        switch (type)
        {
            case "label":
                (com as Label).Text = showText.Substring(0,showText.Length - separator.Length);
                break;
            case "textbox":
                (com as TextBox).Text = showText.Substring(0, showText.Length - separator.Length);
                break;
            case "combobox":
                (com as ComboBox).Text = showText.Substring(0, showText.Length - separator.Length);
                break;
            default:
                showText = showText.Substring(0, showText.Length - separator.Length);
                break;
        }

        return showText;
    }

    /// <summary>
    /// 通用下拉框赋值函数
    /// </summary>
    /// <param name="cbx">下拉框组件</param>
    /// <param name="ds">数据源Dataset</param>
    /// <param name="key">选中后实际获取的值</param>
    /// <param name="value">下拉框显示的值</param>
    /// <param name="isAll">是否添加全部选项，默认不添加</param>
    public static void SetGeneralCombobox(ComboBox cbx, DataSet ds, string key, string value, bool isAll = false)
    {
        List<DictionaryEntry> listComboBox = new List<DictionaryEntry>();
        if (CheckDBSetValidData(ds))
        {
            if (isAll) listComboBox.Add(new DictionaryEntry("", "全部"));
            for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
            {
                //这里没有判断值是否为null就直接添加了，在使用的时候需要判断下
                listComboBox.Add(new DictionaryEntry(ds.Tables[0].Rows[k][key], ds.Tables[0].Rows[k][value]));
            }

            cbx.DataSource = listComboBox;
            cbx.DisplayMember = "value";
            cbx.ValueMember = "key";

            if (isAll) cbx.SelectedIndex = 0;
            else cbx.SelectedItem = null;
        }
    }
    #endregion

    #region 字符串处理
    /// <summary>
    /// 截取指定字符串之间的字符串
    /// </summary>
    /// <see cref="https://blog.csdn.net/u014479921/article/details/79637613"/>
    /// <param name="text">全字符串</param>
    /// <param name="start">开始字符串 </param>
    /// <param name="end">结束字符串 </param>
    /// <returns></returns>
    public static string Substring(string text, string start, string end)
    {
        Regex rg = new Regex("(?<=(" + start + "))[.\\s\\S]*?(?=(" + end + "))", RegexOptions.Multiline | RegexOptions.Singleline);
        return rg.Match(text).Value;
    }

    /// <summary>
    /// xml文件格式化
    /// </summary>
    /// <param name="xmlData"></param>
    /// <returns></returns>
    public static string XmlDataFormatted(string xmlData)
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(xmlData);
        try
        {
            StringBuilder builder = new StringBuilder();
            using (StringWriter sw = new StringWriter(builder))
            {
                using (XmlTextWriter xtw = new XmlTextWriter(sw))
                {
                    xtw.Formatting = Formatting.Indented;
                    xtw.Indentation = 1;
                    xtw.IndentChar = '\t';
                    xml.WriteTo(xtw);
                }
            }
            return builder.ToString();
        }
        catch (Exception ex)
        {
            GlobalData.logger.Error(ex);
            return "";
        }
    }
    #endregion

    #region 合规性判断
    /// <summary>
    /// 正整数判断
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsDigital(string text)
    {
        return !string.IsNullOrEmpty(text) && Regex.IsMatch(text, @"^[1-9]\d*$");
    }

    /// <summary>
    /// 精确到两位的浮点数判断
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsFloat(string text)
    {
        return !string.IsNullOrEmpty(text) && Regex.IsMatch(text, @"^\d+\.\d{1,2}$");
    }

    /// <summary>
    /// 空值判断
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNullOrDBNull(object obj)
    {
        return (obj is DBNull) || obj is null || string.IsNullOrEmpty(obj.ToString());
    }
    #endregion

    #region 网络相关
    /// <summary>
    /// 网络状态检测
    /// </summary>
    /// <param name="ip">连接IP</param>
    /// <param name="checkCount">重连次数</param>
    /// <returns>true-连接正常   false-无法连接</returns>
    public static bool NetWorkStatusVerify(string ip, int checkCount = 5)
    {
        bool re = false;
        if (!string.IsNullOrEmpty(ip))
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
                    re = true;
                }
                else
                {
                    if (!GlobalData.netRecoverForm.isShowed) GlobalData.netRecoverForm.ShowNetRecoverForm();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        return re;
    }

    /// <summary>
    /// 使可访问远程主机的共享目录
    /// </summary>
    /// <param name="ip">远程主机IP</param>
    /// <param name="userName">远程主机用户名</param>
    /// <param name="pwd">远程主机密码</param>
    public static void NetUseServer(string ip, string userName, string pwd)
    {
        try
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
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 获取与指定网段相匹配的第一个IP
    /// </summary>
    public static string GetLocalIPMatchedSubNetwork(string subNetWork)
    {
        string ip = "";
        foreach (IPAddress el in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (!string.IsNullOrEmpty(subNetWork) && el.ToString().Contains(subNetWork))
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
            GlobalData.logger.Error(ex);
            return null;
        }
    }

    /// <summary>
    /// DataTable 转实体类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <remarks>
    /// 参见：https://www.cnblogs.com/zhang1f/p/11469473.html
    /// </remarks>
    public static List<T> DataTabletoEntity<T>(DataTable dt) where T : new()
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            return null;
        }
        List<T> modelList = new List<T>();
        foreach (DataRow dr in dt.Rows)
        {
            T model = new T();
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                if (propertyInfo != null && dr[i] != DBNull.Value)
                    propertyInfo.SetValue(model, dr[i], null);
            }

            modelList.Add(model);
        }
        return modelList;
    }

    /// <summary>
    /// 将DataRow转换为实体类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dr"></param>
    /// <returns></returns>
    public static T DataRowtoEntity<T>(DataRow dr)
    {
        T model = Activator.CreateInstance<T>();
        foreach (PropertyInfo pi in model.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
        {
            if (dr.Table.Columns.Contains(pi.Name) && !IsNullOrDBNull(dr[pi.Name]))
            {
                pi.SetValue(model, HackType(dr[pi.Name], pi.PropertyType), null);
            }
        }
        return model;
    }

    private static object HackType(object value, Type conversionType)
    {
        if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
        {
            if (value == null)
                return null;
            System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
            conversionType = nullableConverter.UnderlyingType;
        }
        if (conversionType == typeof(int) && (value == null || value.ToString().Length == 0))
            value = 0;
        if (conversionType == typeof(double) && (value == null || value.ToString().Length == 0))
            value = 0;
        if (conversionType == typeof(decimal) && (value == null || value.ToString().Length == 0))
            value = 0;
        return Convert.ChangeType(value, conversionType);
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
    /// <param name="iLength">生成字符串长度</param>
    /// <param name="seed">种子库，可以为任何不同字符组成的字符串</param>
    /// <returns></returns>
    public static string GetRandomString(int iLength, string seed = "0123456789")
    {
        StringBuilder sb = new StringBuilder();
        Random r = new Random();
        int range = seed.Length;
        for (int i = 0; i < iLength; i++)
        {
            sb.Append(seed.Substring(r.Next(range), 1));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 执行cmd命令
    /// </summary>
    /// <param name="command">命令</param>
    /// <see cref="https://thrower.cc/2021/csharpusecmd/"/>
    public static void CMDExecute(string command)
    {
        GlobalData.logger.Info("CMDExecute: " + command);
        Process p = new Process();
        p.StartInfo.FileName = "cmd.exe";//要启动的应用程序
        p.StartInfo.UseShellExecute = false;//不使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true; //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = true;//允许输出信息
        p.StartInfo.RedirectStandardError = true;//允许输出错误
        p.StartInfo.CreateNoWindow = true;//不显示程序窗口
        p.Start();//启动程序
        p.StandardInput.WriteLine(command);//向cmd窗口发送输入命令
        p.StandardInput.AutoFlush = true;//自动刷新
        p.Close();//
    }

    /// <summary>
    /// 获取系统主板识别码
    /// </summary>
    /// <see cref="https://zyzsdy.com/article/87"/>
    /// <returns>主板识别码</returns>
    public static string GetSystemUUId()
    {
        string uuid = null;
        using (ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_ComputerSystemProduct"))
        {
            foreach (var item in mos.Get())
            {
                uuid = item["UUID"].ToString();
            }
        }
        return uuid;
    }

    /// <summary>
    /// 判断.Net Framework的Release是否符合需要
    /// (本方法应用环境要求.Net Framework 版本在4.0及以上)
    /// </summary>
    /// <param name="release">需要的版本 version = 4.5 release = 379893</param>
    /// <returns></returns>
    /// <remarks>个版本release可在https://docs.microsoft.com/zh-cn/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed#net_b找到</remarks>
    /// <see cref="https://blog.csdn.net/qq_43024228/article/details/119533955"/>
    public static bool GetDotNetRelease(int release)
    {
        const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
        using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
        {
            if (ndpKey != null && ndpKey.GetValue("Release") != null)
            {
                return (int)ndpKey.GetValue("Release") >= release;
            }
            return false;
        }
    }

    private void EmptyFunftion()
    {
        GlobalData.logger.Info("Inn ");
        try
        {

        }
        catch (Exception ex)
        {
            GlobalData.logger.Warn(ex.Message);
            GlobalData.logger.Error(ex);
        }
        GlobalData.logger.Info("Out ");
    }
    #endregion

    #region 待转正
    #endregion
}