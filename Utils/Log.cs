/********************************************************************
*
* 类  名：Log
*
* 作  者：lgengy
*
* 描  述：通过编程的方式实现log4net配置。GetLogger方法实现参见
*         https://stackoverflow.com/questions/52912682/cant-configure-log4net-logger-programmatically
*
********************************************************************/

using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace ProgrammeFrame.Utils
{
    public class Log
    {
        private string logPath = @"D:\Fiscan\Log\";
        private string logName = "Log";
        private string logNameError = "LogError";
        private string maxFileSize;
        private int maxLogCount;

        /// <summary>
        /// 日志构造函数
        /// </summary>
        /// <param name="logPath">日志路径，默认D:\Fiscan\Log\</param>
        /// <param name="logName">日志名，默认Log</param>
        /// <param name="logNameError">错误日志名，默认LogError</param>
        /// <param name="maxFileSize">日志最大容量,单位KB、MB、GB，默认10MB</param>
        /// <param name="maxLogCount">日志最大保存数，默认50</param>
        public Log(string logPath = "", string logName = "", string logNameError = "", string maxFileSize = "10MB", int maxLogCount = 50)
        {
            if (!string.IsNullOrEmpty(logPath)) this.logPath = logPath;
            if (!string.IsNullOrEmpty(logName)) this.logName = logName;
            if (!string.IsNullOrEmpty(logNameError)) this.logNameError = logNameError;
            this.maxFileSize = maxFileSize;
            this.maxLogCount = maxLogCount;
        }

        /// <summary>
        /// 获取ILog实例
        /// </summary>
        /// <param name="name">实例名</param>
        /// <param name="enableErrorLog">是否单独记录错误日志</param>
        /// <returns></returns>
        public ILog GetLogger(string name, bool enableErrorLog)
        {
            var patternLayout = new PatternLayout("%date [%thread] %-5level %logger - %message%newline");
            patternLayout.ActivateOptions();

            var hierarchy = (Hierarchy)LogManager.GetRepository();

            RollingFileAppender rollingFileAppender = null, rollingFileAppenderEx = null;
            rollingFileAppender = new RollingFileAppender
            {
                Name = logName,
                File = logPath + logName + ".log",
                AppendToFile = true,
                PreserveLogFileNameExtension = true,
                MaximumFileSize = maxFileSize,
                MaxSizeRollBackups = maxLogCount,
                StaticLogFileName = true,
                Layout = patternLayout,
            };
            rollingFileAppender.ActivateOptions();

            log4net.Filter.IFilter filter, filterEx;
            if (enableErrorLog)
            {
                rollingFileAppenderEx = new RollingFileAppender
                {
                    Name = logNameError,
                    File = logPath + logNameError + ".log",
                    AppendToFile = true,
                    PreserveLogFileNameExtension = true,
                    MaximumFileSize = maxFileSize,
                    MaxSizeRollBackups = maxLogCount,
                    StaticLogFileName = true,
                    Layout = patternLayout,
                };
                rollingFileAppenderEx.ActivateOptions();

                filter = new log4net.Filter.LevelRangeFilter() { LevelMin = Level.Debug, LevelMax = Level.Warn };
                filterEx = new log4net.Filter.LevelRangeFilter() { LevelMin = Level.Error, LevelMax = Level.Fatal };
                filter.ActivateOptions();
                filterEx.ActivateOptions();

                rollingFileAppender.AddFilter(filter);
                rollingFileAppenderEx.AddFilter(filterEx);
            }

            var logger = hierarchy.GetLogger(name, hierarchy.LoggerFactory);
            logger.Hierarchy = hierarchy;
            logger.AddAppender(rollingFileAppender);
            if (enableErrorLog) logger.AddAppender(rollingFileAppenderEx);
            logger.Repository.Configured = true;
            logger.Level = Level.Debug;
            logger.Additivity = false;

            hierarchy.Configured = true;

            return LogManager.GetLogger(name);
        }
    }
}
