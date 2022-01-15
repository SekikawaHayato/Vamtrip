using System.Collections.Generic;

namespace Vampire.Scenario
{
    public class LogData
    {
        List<string> _logName;
        List<string> _logMessage;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogData()
        {
            _logName = new List<string>();
            _logMessage = new List<string>();
        }

        /// <value>ログに表示する名前のリスト</value>
        public List<string> LogName
        {
            get { return _logName; }
        }

        /// <value>ログに表示するメッセージのリスト</value>
        public List<string> LogMessage
        {
            get { return _logMessage; }
        }

        /// <summary>
        /// ログに表示する名前を新しく追加するメソッド
        /// </summary>
        /// <param name="name">追加する名前</param>
        public void AddName(string name)
        {
            _logName.Add(name);
        }

        /// <summary>
        /// ログに表示するメッセージを新しく追加するメソッド
        /// </summary>
        /// <param name="message">追加するメッセージ</param>
        public void AddMessage(string message)
        {
            _logMessage.Add(message);
        }
    }
}
