using System;
using System.Collections.Generic;
using System.Text;

namespace NetStandardLogging
{
    public class LoggingData
    {
        public NetStandardLoggingConfigSettings ConfigurationSettings { get; set; }
        public LogLevel LogLevel { get; set; }

        private DateTime? _currentDateTime;

        public DateTime CurrentDateTime
        {
            get
            {
                DefaultDate();
                return _currentDateTime.Value;
            }
            set { _currentDateTime = value; }
        }

        public void DefaultDate()
        {
            if (!_currentDateTime.HasValue)
                _currentDateTime = DateTime.Now;
        }

        public string Message { get; set; }

    }
}
