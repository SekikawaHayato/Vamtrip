using UnityEngine;
using UnityEngine.UI;

namespace Vampire.Scenario
{
    public class LogText : MonoBehaviour
    {
        [SerializeField] Text _nameText;
        [SerializeField] Text _messageText;

        /// <summary>
        /// ログに表示する名前とメッセージを設定するメソッド
        /// </summary>
        /// <param name="name">表示する名前</param>
        /// <param name="message">表示するメッセージ</param>
        public void SetText(string name, string message)
        {
            _nameText.text = name;
            _messageText.text = message;
        }
    }
}
