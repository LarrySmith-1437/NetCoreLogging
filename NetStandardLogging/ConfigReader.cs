using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NetStandardLogging
{
    public static class ConfigReader
    {
        public static NetStandardLoggingConfigSettings ReadConfig(string fileName)
        {
            var configFileInfo = new FileInfo(fileName);
            if (!configFileInfo.Exists)
                throw new ApplicationException($"No config file was found for filename {fileName}");


            var builder = new ConfigurationBuilder()
                .SetBasePath(configFileInfo.DirectoryName)
                .AddJsonFile(configFileInfo.Name, optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            
            NetStandardLoggingConfigSettings config = new NetStandardLoggingConfigSettings();
            configuration.GetSection("NetStandardLogger").Bind(config);

            if(!ValidateConfiguration(config))
                throw new ApplicationException("Could not read configuration section 'NetStandardLOgger'");

            return config;
        }

        public static bool ValidateConfiguration(NetStandardLoggingConfigSettings config)
        {
            if (null == config.ConsoleLogLevel) return false;
            if (null == config.FileLogLevel) return false;
            if (null == config.LogDirectory) return false;
            if (null == config.LogEntryLayout) return false;
            if (null == config.LogFileName) return false;

            return true;
        }

        public static NetStandardLoggingConfigSettings ReadConfig()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var fileNameList = new List<string>()
            {
                "NetStandardLogging.json",
                "config.json",
                "applicationsettings.json",
            };
            foreach (var file in fileNameList)
            {
                var fullFilePath = Path.Combine(currentDir, file);
                if (File.Exists(fullFilePath))
                {
                    return ReadConfig(fullFilePath);
                }
            }
            throw new ApplicationException("None of the standard configuration file names are present.");
        }
    }
}
