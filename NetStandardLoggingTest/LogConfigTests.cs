using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandardLogging;
using System;

namespace NetStandardLoggingTest
{
    [TestClass]
    public class LogConfigTests
    {
        [TestMethod]
        public void ReadConfigFromNetStandardLoggingJson()
        {
            var logging = new NetStandardLogger("NetStandardLogging.json");

            Assert.AreEqual(5, logging.ConfigurationSettings.DaysToRetain);
            Assert.AreEqual("Error", logging.ConfigurationSettings.ConsoleLogLevel);
            Assert.AreEqual("Fatal", logging.ConfigurationSettings.FileLogLevel);
            Assert.AreEqual("Loging", logging.ConfigurationSettings.LogDirectory);
            Assert.AreEqual("{date:format=yyyy-MM-dd HH:mm:ss}\t{level}\t{message}", logging.ConfigurationSettings.LogEntryLayout);
            Assert.AreEqual("svclog_{date:format=yyyy-MM-dd}.log", logging.ConfigurationSettings.LogFileName);
            Assert.AreEqual(true, logging.ConfigurationSettings.LogToConsole);
            Assert.AreEqual(LogLevel.Error, logging.ConsoleLogLevelAsEnum);
            Assert.AreEqual(LogLevel.Fatal, logging.FileLogLevelAsEnum);
        }
        [TestMethod]
        public void ReadConfigDefaultFromConfigJson()
        {
            var logging = new NetStandardLogger();

            Assert.AreEqual(3, logging.ConfigurationSettings.DaysToRetain);
            Assert.AreEqual("Debug", logging.ConfigurationSettings.ConsoleLogLevel);
            Assert.AreEqual("Info", logging.ConfigurationSettings.FileLogLevel);
            Assert.AreEqual("Log", logging.ConfigurationSettings.LogDirectory);
            Assert.AreEqual("{date:format=yyyy-MM-dd HH:mm:ss}\t{level}\t{message}", logging.ConfigurationSettings.LogEntryLayout);
            Assert.AreEqual("svclog_{date:format=yyyy-MM-dd}.log", logging.ConfigurationSettings.LogFileName);
            Assert.AreEqual(true, logging.ConfigurationSettings.LogToConsole);
            Assert.AreEqual(LogLevel.Debug, logging.ConsoleLogLevelAsEnum);
            Assert.AreEqual(LogLevel.Info, logging.FileLogLevelAsEnum);
        }
    }

}
