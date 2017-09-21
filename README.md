# NetCoreLogging

With all the loggers in all the world, why write this?  Good question.

I started working on a .Net Core 2.0 project that I intended to deploy to the Raspbery Pi running Debian and/or Win 10 IOT. When you do that, there's no fudging with libraries that contain references other than pure .Net Standard 2.0.  Log4Net was nowhere to be found, though it was my first choice.  NLog actually loaded, but it raised some reference errors and warnings, and on the Pi, it would not write out to the log file, nor could I get any of it's internal logging to give me a clue why.  

Not finding anything else to fill the immediate need, I banged this library out with pure .Net Standard calls and it worked great for me.  I suspecteded there might be other folks in the same boat, and so I published it as a Nuget package.

https://www.nuget.org/packages/NetStandardLogging/

To use the logger, add this example section below to your config.json or applicationsettings.json, or create a separate NetStandardLogging.json file with just this section in it.

After you get it up and running, tailor the settings to your liking.  I modeled the settings and the date formatting to work like NLog.



```json
  "NetStandardLogger": {
    "DaysToRetain": 3,
    "ConsoleLogLevel": "Debug",
    "FileLogLevel": "Info",
    "LogDirectory": "Log",
    "LogEntryLayout": "{date:format=yyyy-MM-dd HH:mm:ss}\t{level}\t{message}",
    "LogFileName": "svclog_{date:format=yyyy-MM-dd}.log",
    "LogToConsole": "true"
  }
```

Like other loggers this one uses discreet log levels in this order of precedence:

```
    public enum LogLevel
    {
        Debug = 0,
        Info = 1,
        Warn = 2,
        Error = 3,
        Fatal = 4
    }
```

Log entries are written by calling the method with the name of the log level. Example:

```
    logger.Debug("This is a debug message.");
    logger.Info("And this is an info message");
    logger.Info("This is an error, and an exception with it", new ApplicationException("My Exception"));
```
