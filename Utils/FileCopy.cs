/********************************************************************
*
* 类  名：FileCopy
*
* 作  者：lgengy
*
* 描  述：实现文件同步/异步拷贝
*
********************************************************************/

using ProgrammeFrame.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace ProgrammeFrame
{
    class FileCopy
    {
        private struct StruFileCopyParams
        {
            public string source;
            public string des;
            public bool cover;
            public bool result;
        }

        private string serverSource;
        private string serverDest;
        private BackgroundWorker bgFileCopy = new BackgroundWorker();//执行文件拷贝到远程
        private Queue<StruFileCopyParams> queueFile = new Queue<StruFileCopyParams>();//等待拷贝文件队列

        /// <summary>
        /// 文件拷贝对象实例化
        /// </summary>
        /// <param name="dirDest">目的目录，不存在的话会新建一个</param>
        /// <param name="serverSource">源目录服务器IP，为空说明是本地拷贝，不为空说明涉及服务器拷贝</param>
        /// <param name="serverDest">目的目录服务器IP，为空说明是本地拷贝，不为空说明涉及服务器拷贝</param>
        public FileCopy(string dirDest, string serverSource = "", string serverDest = "")
        {
            this.serverSource = serverSource;
            this.serverDest = serverDest;

            FileDirectoryOperate(dirDest, true);

            bgFileCopy.DoWork += BgFileCopy_DoWork;
            bgFileCopy.RunWorkerCompleted += BgFileCopy_RunWorkerCompleted;
        }

        private void BgFileCopy_DoWork(object sender, DoWorkEventArgs e)
        {
            GlobalData.logger.Info(">" + MethodBase.GetCurrentMethod().Name);
            try
            {
                StruFileCopyParams file = (StruFileCopyParams)e.Argument;
                file.result = CopyFile(file.source, file.des, file.cover);
                e.Result = file;
            }
            catch (Exception ex)
            {
                GlobalData.logger.Warn(ex.Message);
                GlobalData.logger.Error(MethodBase.GetCurrentMethod().Name, ex);
            }
            GlobalData.logger.Info("<" + MethodBase.GetCurrentMethod().Name);
        }

        private void BgFileCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                StruFileCopyParams file = (StruFileCopyParams)e.Result;
                if (e.Error != null || !file.result)//如果异步拷贝期间出现异常或拷贝失败
                {
                    if (File.Exists(file.source))//如果不是由源文件不存在引起的拷贝失败，那么把这张照片加入队列等待重新拷贝
                    {
                        queueFile.Enqueue(file);
                        GlobalData.logger.Warn($"{file.source} 拷贝失败(原因：{e.Error?.Message})，加入文件队列，等待下次拷贝");
                    }
                    else
                        GlobalData.logger.Warn($"{file.source} 拷贝失败(原因：{e.Error?.Message})");
                }
                else
                    GlobalData.logger.Info($"文件 {file.source} 拷贝到 {file.des} 成功");

                if (queueFile.Count > 0)
                {
                    GlobalData.logger.Info("待拷贝文件数：" + queueFile.Count);
                    if (!bgFileCopy.IsBusy)
                    {
                        bgFileCopy.RunWorkerAsync(queueFile.Dequeue());
                    }
                }
            }
            catch(Exception ex)
            {
                GlobalData.logger.Warn(ex.Message);
                GlobalData.logger.Error(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// 文件目录操作
        /// </summary>
        /// <param name="dir">目录</param>
        /// <param name="server">目录所在服务器IP，在本机上操作目录的话此项为空</param>
        /// <param name="create">true-创建 false-删除</param>
        private void FileDirectoryOperate(string dir, bool create)
        {
            GlobalData.logger.Info(">" + MethodBase.GetCurrentMethod().Name);
            try
            {
                //如果目录参数为空抛出异常
                if (string.IsNullOrEmpty(dir))
                {
                    throw new Exception("参数不合法，目录为空");
                }

                //如果是操作远程目录，网络不通畅的话就不操作了
                if (!string.IsNullOrEmpty(serverSource) || !string.IsNullOrEmpty(serverDest))
                {
                    if (!Utils.NetWorkStatusVerify(serverSource) && !Utils.NetWorkStatusVerify(serverDest))
                    {
                        GlobalData.logger.Warn($"网络故障，{dir} 操作失败");
                        return;
                    }
                }

                //目录不存在的话只能创建
                if (!Directory.Exists(dir))
                {
                    if (create)
                    {
                        Directory.CreateDirectory(dir);
                        GlobalData.logger.Info($"{dir} 成功创建");
                    }
                }
                //目录已存在的话只能删除
                else
                {
                    if (!create)
                    {
                        Directory.Delete(dir, true); 
                        GlobalData.logger.Info($"{dir} 成功删除");
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalData.logger.Warn(ex.Message);
                GlobalData.logger.Error(MethodBase.GetCurrentMethod().Name, ex);
            }
            GlobalData.logger.Info("<" + MethodBase.GetCurrentMethod().Name);
        }

        /// <summary>
        /// 文件拷贝
        /// </summary>
        /// <param name="source">源文件地址（完整路径）</param>
        /// <param name="dest">目的文件地址（完整路径）</param>
        /// <param name="cover">是否覆盖</param>
        /// <returns></returns>
        public bool CopyFile(string source, string dest, bool cover)
        {
            bool re = false;
            if (!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(dest))
            {
                try
                {
                    if (File.Exists(source))
                    {
                        if (Utils.NetWorkStatusVerify(serverSource) && Utils.NetWorkStatusVerify(serverDest))
                        {
                            File.Copy(source, dest, cover);
                            re = true;
                        }
                        else
                        {
                            GlobalData.logger.Info("网络故障，文件拷贝失败");
                        }
                    }
                    else GlobalData.logger.Info(source + " 不存在，文件拷贝失败");
                }
                catch (Exception ex)
                {
                    GlobalData.logger.Warn(ex.Message);
                    GlobalData.logger.Error(MethodBase.GetCurrentMethod().Name, ex);
                }
            }
            return re;
        }

        /// <summary>
        /// 异步拷贝文件到远程
        /// </summary>
        /// <param name="source">源文件地址（完整路径）</param>
        /// <param name="dest">目的文件地址（完整路径）</param>
        /// <param name="cover">是否覆盖</param>
        public void CopyFileAsyn(string source, string dest, bool cover)
        {
            StruFileCopyParams file = new StruFileCopyParams
            {
                source = source,
                des = dest,
                cover = cover,
                result = false
            };

            if (!bgFileCopy.IsBusy)
                bgFileCopy.RunWorkerAsync(file);
            else
                queueFile.Enqueue(file);
        }
    }
}
