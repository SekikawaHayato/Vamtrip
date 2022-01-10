using System.Collections.Generic;

namespace Vampire.Scenario
{
    public class LogData
    {
        List<string> _logName;
        List<string> _logMessage;

        public LogData()
        {
            _logName = new List<string>();
            _logMessage = new List<string>();
        }

        public List<string> LogName
        {
            get { return _logName; }
        }

        public List<string> LogMessage
        {
            get { return _logMessage; }
        }

        public void AddName(string name)
        {
            _logName.Add(name);
        }

        public void AddMessage(string message)
        {
            _logMessage.Add(message);
        }
    }
}
