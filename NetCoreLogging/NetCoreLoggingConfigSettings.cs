namespace NetCoreLogging

{
    public class NetCoreLoggingConfigSettings
    {
        public string LogDirectory { get; set; }
        public string LogFileName { get; set; }
        public int DaysToRetain { get; set; }
        public bool LogToConsole { get; set; }
        public string LogEntryLayout { get; set; }
        public string FileLogLevel { get; set; }
        public string ConsoleLogLevel { get; set; }
    }
}
