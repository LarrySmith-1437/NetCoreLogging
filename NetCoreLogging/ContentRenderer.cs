using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NetCoreLogging
{
    public class ContentRenderer
    {
        public LoggingData LayoutData { get; set; }

        public ContentRenderer()
        {
        }
              
        private string _defaultDateFormat = "yyyyMMdd";
        public string Render(string input)
        {           
            input = RenderDate(input);
            input = RenderMessage(input);
            input = RenderLevel(input);
            return input;
        }
        
        public string RenderDate(string input)
        {
            StringBuilder buildOutput = new StringBuilder(input);

            DateTime workingDate = LayoutData.CurrentDateTime;

            var regex = new Regex(@"\{date.*?\}", RegexOptions.Compiled);

            var matches = regex.Matches(input);
            for (int i = matches.Count - 1; i > -1; i-- )
            {
                var dateFormat = _defaultDateFormat;
                var match = matches[i];
                int posFormat =  match.Value.IndexOf(":format=");
                if (posFormat > 0)
                {
                    dateFormat = match.Value.Substring(posFormat + 8,
                        (match.Length - 1  - (posFormat + 8)));
                }
                buildOutput.Remove(match.Index, match.Length);
                buildOutput.Insert(match.Index, workingDate.ToString(dateFormat));
            }

            return buildOutput.ToString();
        }

        public string RenderMessage(string input)
        {
            StringBuilder buildOutput = new StringBuilder(input);

            var regex = new Regex(@"\{message\}", RegexOptions.Compiled);

            var matches = regex.Matches(input);
            for (int i = matches.Count - 1; i > -1; i--)
            {
                var match = matches[i];
                buildOutput.Remove(match.Index, match.Length);
                buildOutput.Insert(match.Index, LayoutData.Message);
            }
            return buildOutput.ToString();
        }

        public string RenderLevel(string input)
        {
            StringBuilder buildOutput = new StringBuilder(input);

            var regex = new Regex(@"\{level\}", RegexOptions.Compiled);

            var matches = regex.Matches(input);
            for (int i = matches.Count - 1; i > -1; i--)
            {
                var match = matches[i];
                buildOutput.Remove(match.Index, match.Length);
                buildOutput.Insert(match.Index, LayoutData.LogLevel.ToString());
            }
            return buildOutput.ToString();
        }
    }
}
