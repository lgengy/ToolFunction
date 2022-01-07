/********************************************************************************

** 类名称： RegisterUtils

** 描  述：注册表查找、添加、删除等操作，参见https://www.cnblogs.com/tiancai/p/4457828.html

** 作  者：lgy

*********************************************************************************/

using Microsoft.Win32;
using ProgrammeFrame.Common;
using System;

namespace ProgrammeFrame
{
    public class RegisterEditor
    {
        /// <summary>
        /// 判断指定位置的注册项是否存在
        /// </summary>
        /// <param name="registryName">注册项名字</param>
        /// <param name="key">根目录</param>
        /// <param name="subDir">以可变参形式存储的子目录，每一个目录作为一个参数</param>
        /// <returns></returns>
        public static bool IsRegistryExit(string registryName, RegistryKey key, params string[] subDir)
        {
            bool re = false;
            string[] subValuesName;
            RegistryKey registryKey = key;
            foreach(string dir in subDir)
            {
                registryKey = registryKey.OpenSubKey(dir);
                if (registryKey == null) throw new Exception("注册项" + dir + "为null");
            }
            
            subValuesName = registryKey.GetValueNames();
            foreach (string valueName in subValuesName)
            {
                if (valueName.Equals(registryName))
                {
                    re = true;
                    break;
                }
            }
            return re;
        }

        /// <summary>
        /// 获取指定注册项的值
        /// </summary>
        /// <param name="registryName">注册项名字</param>
        /// <param name="key">根目录</param>
        /// <param name="subDir">以可变参形式存储的子目录，每一个目录作为一个参数</param>
        /// <returns></returns>
        public static string GetRegistryValue(string registryName, RegistryKey key, params string[] subDir)
        {
            if (IsRegistryExit(registryName, key, subDir))
            {
                RegistryKey registryKey = key;
                foreach (string dir in subDir)
                {
                    registryKey = registryKey.OpenSubKey(dir);
                }
                return registryKey.GetValue(registryName).ToString();
            }
            return "";
        }

        /// <summary>
        /// 新建注册项
        /// </summary>
        /// <param name="registryName">注册项名字</param>
        /// <param name="registryValue">注册项值</param>
        /// <param name="valueType">值类型</param>
        /// <param name="key">根目录</param>
        /// <param name="subDir">以可变参形式存储的子目录，每一个目录作为一个参数</param>
        public static void CreateRegister(string registryName, object registryValue, RegistryValueKind valueType, RegistryKey key, params string[] subDir)
        {
            if(!IsRegistryExit(registryName, key, subDir))
            {
                RegistryKey registryKey = key;
                foreach (string dir in subDir)
                {
                    registryKey = registryKey.OpenSubKey(dir, true);
                }
                registryKey.SetValue(registryName, registryValue, valueType);
                GlobalData.logger.Info($"写注册表：{key.Name}\\{string.Join("\\", subDir)}\\{registryName}，值{registryKey.GetValue(registryName)}");
            }
            else
                GlobalData.logger.Info($"{key.Name}\\{string.Join("\\", subDir)}\\{registryName}已存在");
        }

        /// <summary>
        /// 删除指定注册项
        /// </summary>
        /// <param name="registryName">注册项名字</param>
        /// <param name="key">根目录</param>
        /// <param name="subDir">以可变参形式存储的子目录，每一个目录作为一个参数</param>
        public static void DeleteRegister(string registryName, RegistryKey key, params string[] subDir)
        {
            if(IsRegistryExit(registryName, key, subDir))
            {
                RegistryKey registryKey = key;
                foreach (string dir in subDir)
                {
                    registryKey = registryKey.OpenSubKey(dir, true);
                }
                foreach(string name in registryKey.GetValueNames())
                {
                    if (name.Equals(registryName))
                    {
                        GlobalData.logger.Info($"删除{key.Name}\\{string.Join("\\", subDir)}\\{registryName}，值{registryKey.GetValue(registryName)}");
                        registryKey.DeleteValue(registryName, false);
                    }
                }
            }
        }
    }


}
