using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace NetStandardLogging
{
    public static class LogFileManagement
    {
        private static DateTime _lastFileDeleteCheck = DateTime.Now.AddDays(-1);
        public static string SetUpLogFile(LoggingData data)
        {
            CreateDirIfNotExists(data.ConfigurationSettings.LogDirectory);
            var dirFullName = Path.GetFullPath(data.ConfigurationSettings.LogDirectory);
            var dirInfo = new DirectoryInfo(dirFullName);
            if (!dirInfo.Exists)
                throw new ArgumentException($"Directory {data.ConfigurationSettings.LogDirectory} not exists and/or could not be created");

            DeleteOldLogFiles(data, dirFullName);

            var renderer = new ContentRenderer
            {
                LayoutData = data
            };

            var filename = renderer.Render(data.ConfigurationSettings.LogFileName);
            return Path.Combine(data.ConfigurationSettings.LogDirectory, filename);
        }

        public static void DeleteOldLogFiles(LoggingData data,string dirFullName)
        {
            if (
                data.ConfigurationSettings.DaysToRetain <= 0            //don't bother to check if we're not configured to delete files.
                || _lastFileDeleteCheck > DateTime.Now.AddMinutes(-5)  //only check every 5 minutes, avoid costly call on every log entry.
                ) 
                return;

            Console.WriteLine($"Checking delete log files, dir: {dirFullName}");

            var numberOfDays = data.ConfigurationSettings.DaysToRetain;
            var cutoffDate = DateTime.Now.Date.AddDays(numberOfDays * -1);

            var dirInfo = new DirectoryInfo(dirFullName);
            var listOfFilesToDelete =  dirInfo.GetFiles().Where(x => x.CreationTime.Date < cutoffDate)
                .Select(x => x)
                .ToList();

            foreach(var file in listOfFilesToDelete)
            {
                Console.WriteLine($"Deleting old log file: {file.Name}");
                file.Delete();
            }
            _lastFileDeleteCheck = DateTime.Now;
        }

        public static void CreateDirIfNotExists(string logDir)
        {
            var dirInfo = new DirectoryInfo(logDir);
            if (!dirInfo.Exists)
            {
                Console.WriteLine($"Creating log directory {logDir}");
                Directory.CreateDirectory(logDir);
            }
        }

        public static void WriteLog(string logFile, string content)
        {
            File.AppendAllText(logFile, content, Encoding.UTF8);
        }

        //public static async Task WriteLogAsync(string logFile, string content)
        //{
        //    await File.AppendAllTextAsync(logFile, content, Encoding.UTF8);
        //}
    }
}
