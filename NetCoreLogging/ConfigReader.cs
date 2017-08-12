using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NetCoreLogging
{
    public static class ConfigReader
    {
        public static NetCoreLoggingConfigSettings ReadConfig(string fileName)
        {
            var configFileInfo = new FileInfo(fileName);
            if (!configFileInfo.Exists)
                throw new ApplicationException($"No config file was found for filename {fileName}");


            var builder = new ConfigurationBuilder()
                .SetBasePath(configFileInfo.DirectoryName)
                .AddJsonFile(configFileInfo.Name, optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            NetCoreLoggingConfigSettings config = new NetCoreLoggingConfigSettings();

            configuration.GetSection("NetCoreLogger").Bind(config);

            return config;
        }
        public static NetCoreLoggingConfigSettings ReadConfig()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var fileNameList = new List<string>()
            {
                "config.json",
                "applicationsettings.json",
                "netcorelogging.json"
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
