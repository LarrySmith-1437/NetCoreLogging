using System;
using System.Threading.Tasks;

namespace NetStandardLogging
{
    public class NetStandardLogger : IAppLogger
    {
        public NetStandardLoggingConfigSettings ConfigurationSettings { get; set; }

        private LogLevel? _fileLogLevelAsEnum = null;
        public LogLevel FileLogLevelAsEnum
        {
            get
            {
                if (!_fileLogLevelAsEnum.HasValue)
                {
                    _fileLogLevelAsEnum = (LogLevel)Enum.Parse(typeof(LogLevel), ConfigurationSettings.FileLogLevel);
                }
                return _fileLogLevelAsEnum.Value;
            }
        }

        private LogLevel? _consoleLogLevelAsEnum = null;
        public LogLevel ConsoleLogLevelAsEnum
        {
            get
            {
                if (!_consoleLogLevelAsEnum.HasValue)
                {
                    _consoleLogLevelAsEnum = (LogLevel)Enum.Parse(typeof(LogLevel), ConfigurationSettings.ConsoleLogLevel);
                }
                return _consoleLogLevelAsEnum.Value;
            }
        }

        public NetStandardLogger()
        {
            ConfigurationSettings = ConfigReader.ReadConfig();
        }

        public NetStandardLogger(string configFileName)
        {
            ConfigurationSettings = ConfigReader.ReadConfig(configFileName);
        }

        public NetStandardLogger(NetStandardLoggingConfigSettings config)
        {
            ConfigurationSettings = config;
        }

        public void Debug(string message) => Debug(message, null);
        public void Debug(string message, Exception exception) => Log(LogLevel.Debug, message, exception);
        //public async Task DebugAsync(string message) => await DebugAsync(message, null);
        //public async Task DebugAsync(string message, Exception exception) => await LogAsync(LogLevel.Debug, message, exception);
        //-----------------------
        public void Info(string message) => Info(message, null);
        public void Info(string message, Exception exception) => Log(LogLevel.Info, message, exception);
        //public async Task InfoAsync(string message) => await InfoAsync(message, null);
        //public async Task InfoAsync(string message, Exception exception) => await LogAsync(LogLevel.Info, message, exception);
        //-----------------------
        public void Warn(string message) => Warn(message, null);
        public void Warn(string message, Exception exception) => Log(LogLevel.Warn, message, exception);
        //public async Task WarnAsync(string message) => await WarnAsync(message, null);
        //public async Task WarnAsync(string message, Exception exception) => await LogAsync(LogLevel.Warn, message, exception);
        //-----------------------
        public void Error(string message) => Error(message, null);
        public void Error(string message, Exception exception) => Log(LogLevel.Error, message, exception);
        //public async Task ErrorAsync(string message) => await ErrorAsync(message, null);
        //public async Task ErrorAsync(string message, Exception exception) => await LogAsync(LogLevel.Error, message, exception);
        //-----------------------
        public void Fatal(string message) => Fatal(message, null);
        public void Fatal(string message, Exception exception) => Log(LogLevel.Fatal, message, exception);
        //public async Task FatalAsync(string message) => await FatalAsync(message, null);
        //public async Task FatalAsync(string message, Exception exception) => await LogAsync(LogLevel.Fatal, message, exception);
        //-----------------------

        private void Log(LogLevel logLevel, string message, Exception exception)
        {
            if (logLevel < FileLogLevelAsEnum && logLevel < ConsoleLogLevelAsEnum)
            {
                return;
            }

            var data = new LoggingData
            {
                ConfigurationSettings = ConfigurationSettings,
                LogLevel = logLevel,
                CurrentDateTime = DateTime.Now,
                Message = message
            };

            var renderer = new ContentRenderer { LayoutData = data };

            //determine log file we're writing to
            var logFileFullName = LogFileManagement.SetUpLogFile(data);
            //format the string to write
            var logContent = renderer.Render(ConfigurationSettings.LogEntryLayout) + Environment.NewLine;

            if (exception != null)
            {
                logContent += exception.StackTrace + Environment.NewLine;
            }

            //if Console logging enabled, write to Console

            if (ConfigurationSettings.LogToConsole && logLevel >= ConsoleLogLevelAsEnum)
                Console.Write(logContent);

            //write/append, non locking
            if (logLevel >= FileLogLevelAsEnum)
                LogFileManagement.WriteLog(logFileFullName, logContent);
        }

        //removed as Net Standard 2.0 does not support asnyc file io, yet.

        //private async Task LogAsync(LogLevel logLevel, string message, Exception exception)
        //{
        //    if(logLevel < FileLogLevelAsEnum && logLevel < ConsoleLogLevelAsEnum)
        //    {
        //        return;
        //    }

        //    var data = new LoggingData
        //    {
        //        ConfigurationSettings = ConfigurationSettings,
        //        LogLevel = logLevel,
        //        CurrentDateTime = DateTime.Now,
        //        Message = message
        //    };

        //    var renderer = new ContentRenderer { LayoutData = data };

        //    //determine log file we're writing to
        //    var logFileFullName = LogFileManagement.SetUpLogFile(data);
        //    //format the string to write
        //    var logContent = renderer.Render(ConfigurationSettings.LogEntryLayout) + Environment.NewLine;

        //    if(exception!= null )
        //    {
        //        logContent += exception.StackTrace + Environment.NewLine;
        //    }

        //    //if Console logging enabled, write to Console

        //    if (ConfigurationSettings.LogToConsole && logLevel >= ConsoleLogLevelAsEnum)
        //        Console.Write(logContent);

        //    //write/append, non locking
        //    if ( logLevel >= FileLogLevelAsEnum)
        //        await LogFileManagement.WriteLogAsync(logFileFullName, logContent);
        //}
    }
}
