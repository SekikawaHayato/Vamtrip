using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.Scenario
{
    public class ScenarioIndicator : MonoBehaviour
    {
        // テキストを表示するUI
        [SerializeField] Text _messageWindowText;
        [SerializeField] Text _nameText;

        ScenarioManager _scenarioManager;

        /// <summary>
        /// イベントの追加
        /// </summary>
        void Awake()
        {
            if(TryGetComponent<ScenarioManager>(out _scenarioManager))
            {
                _scenarioManager.MessageText.Subscribe(t =>
                {
                    SetText(t);
                });
                _scenarioManager.NameText.Subscribe(t =>
                {
                    SetName(t);
                });
            }
        }

        /// <summary>
        /// メッセージを表示するメソッド
        /// </summary>
        /// <param name="text">表示するメッセージ</param>
        void SetText(string text)
        {
            _messageWindowText.text = text;
        }

        /// <summary>
        /// 名前を表示するメソッド
        /// </summary>
        /// <param name="name">表示する名前</param>
        void SetName(string name)
        {
            _nameText.text = name;
        }
    }
}
