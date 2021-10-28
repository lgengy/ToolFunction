/********************************************************************************

** 类名称： EntityConfig

** 描述：配置文件类实体，配置文件新增一个节点，这里新增一个私有属性，并设置getter和setter即可。目前支持整型、布尔、字符串和日期

*********************************************************************************/

using ProgrammeFrame.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace ProgrammeFrame.Entity
{
    public class EntityConfig
    {
        private static XmlDocument _XmlDocument = null;

        #region getter and setter
        #endregion

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="xmlPath">配置文件路径</param>
        public static void ReadXMLFile(string xmlPath)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlPath);
                _XmlDocument = xmlDocument;
            }
            catch (Exception ex)
            {
                //GlobalData.logger.Error("ReadXMLFile", ex);//启动运行会报错有这句的话
            }
        }

        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns>配置文件实体对象EntityConfig</returns>
        public static EntityConfig GetConfig()
        {
            EntityConfig config = new EntityConfig();
            try
            {
                if (_XmlDocument != null && _XmlDocument.HasChildNodes)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    Utils.GetAllXMLNode(_XmlDocument.ChildNodes, ref dic);

                    if (dic.Count != 0)
                    {
                        foreach (PropertyInfo property in config.GetType().GetProperties())
                        {
                            if (dic.ContainsKey(property.Name))
                            {
                                switch (property.PropertyType.Name)
                                {
                                    case "Int32":
                                        property.SetValue(config, Convert.ToInt32(dic[property.Name]), null);
                                        break;
                                    case "String":
                                        property.SetValue(config, dic[property.Name], null);
                                        break;
                                    case "Boolean":
                                        property.SetValue(config, Convert.ToInt32(dic[property.Name]) == 0, null);
                                        break;
                                    case "DateTime":
                                        property.SetValue(config, Convert.ToDateTime(dic[property.Name]), null);
                                        break;
                                }
                            }
                            else
                                GlobalData.logger.Warn("配置文件没有" + property.Name + "节点");
                        }
                    }
                    else
                        GlobalData.logger.Warn("没有获取到节点信息");
                }
                else
                {
                    GlobalData.logger.Warn("没有获取到配置文件信息");
                    GlobalData.messageBox.ShowDialog("请检查配置文件是否存在", friUIMessageBox.CUIMessageBox.MessageBoxButton.OKOnly, friUIMessageBox.CUIMessageBox.MessageBoxIcon.Error, "警告");
                }
            }
            catch (Exception ex)
            {
                GlobalData.logger.Error("GetConfig\r\n", ex);
                GlobalData.messageBox.ShowDialog("配置错误，请联系工作人员", friUIMessageBox.CUIMessageBox.MessageBoxButton.OKOnly, friUIMessageBox.CUIMessageBox.MessageBoxIcon.Error, "警告");
            }
            return config;
        }
    }
}
