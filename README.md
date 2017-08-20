# NetCoreLogging

To use this, add this section to your config.json or applicationsettings.json, or create a separate NetStandardLogging.json file.
After you get it up and running, tailor the settings to your liking.  I modeled the date formatting sections to work like NLog.



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
