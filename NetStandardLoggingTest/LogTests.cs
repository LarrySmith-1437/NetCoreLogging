using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandardLogging;
using System;
using System.IO;

namespace NetStandardLoggingTest
{
    [TestClass]
    public class LogTests
    {
        [TestMethod]
        public void LogLevelsAreEnforcedWhenWritingLog()
        {
            var logfilename = "testlog.log";
            var logsettings = new NetStandardLoggingConfigSettings
            {
                ConsoleLogLevel = "Fatal",
                DaysToRetain = 1,
                FileLogLevel = "Info",
                LogDirectory = ".",
                LogEntryLayout = "{date:format=yyyy-MM-dd HH:mm:ss}\t{level}\t{message}",
                LogFileName = logfilename,
                LogToConsole = false
            };
            var logger = new NetStandardLogger(logsettings);

            if (File.Exists(logfilename))
            {
                File.Delete(logfilename);
            }
            var debugMessage = "This is a debug message.";
            var infoMessage = "And this is an info message";

            logger.Debug(debugMessage);
            logger.Info(infoMessage);
            var logContents = File.ReadAllText(logfilename);
            Assert.IsFalse(logContents.Contains(debugMessage));
            Assert.IsTrue(logContents.Contains(infoMessage));

        }

        [TestMethod]
        public void LoggingExceptionsTest()
        {
            var logfilename = "testlogException.log";
            var logsettings = new NetStandardLoggingConfigSettings
            {
                ConsoleLogLevel = "Fatal",
                DaysToRetain = 1,
                FileLogLevel = "Info",
                LogDirectory = ".",
                LogEntryLayout = "{date:format=yyyy-MM-dd HH:mm:ss}\t{level}\t{message}",
                LogFileName = logfilename,
                LogToConsole = false
            };
            var logger = new NetStandardLogger(logsettings);

            if (File.Exists(logfilename))
            {
                File.Delete(logfilename);
            }
            var errorMessage = "This is an error, and an exception with it";
            var exceptionMessage = "My Exception";
            var stacktracetext = "at NetStandardLoggingTest.LogTests.LoggingExceptionsTest()";

            try
            {
                throw new ApplicationException(exceptionMessage);
            }
            catch (Exception ex)
            {
                logger.Error(errorMessage, ex);
            }

            var logContents = File.ReadAllText(logfilename);
            Assert.IsTrue(logContents.Contains(errorMessage));
            Assert.IsTrue(logContents.Contains(exceptionMessage));
            Assert.IsTrue(logContents.Contains(stacktracetext));

        }

    }

}
