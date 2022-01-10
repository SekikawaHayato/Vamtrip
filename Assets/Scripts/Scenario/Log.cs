using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.Scenario
{
    public class Log : MonoBehaviour
    {
        [SerializeField] Button _logButton;
        [SerializeField] Button _backButton;
        [SerializeField] GameObject _logPrefab;
        [SerializeField] Transform _parent;
        [SerializeField] ScenarioManager _scenarioManager;
        [SerializeField] GameObject _logPanel;

        /// <summary>
        /// イベントの追加
        /// </summary>
        void Awake()
        {
            _logButton.onClick.AsObservable()
                .Subscribe(t => LogButton())
                .AddTo(this);

            _backButton.onClick.AsObservable()
                .Subscribe(t => BackButton())
                .AddTo(this);
        }

        /// <summary>
        /// シナリオのログを表示するメソッド
        /// </summary>
        void LogButton()
        {
            foreach (Transform child in _parent)
            {
                Destroy(child.gameObject);
            }

            List<string> name = _scenarioManager.LogData.LogName;
            List<string> message = _scenarioManager.LogData.LogMessage;

            for(int i = 0; i < message.Count; i++)
            {
                GameObject obj = Instantiate(_logPrefab, _parent);
                obj.GetComponent<LogText>().SetText(name[i], message[i]);
            }
            _logPanel.SetActive(true);
        }

        /// <summary>
        /// ログを非表示にするメソッド
        /// </summary>
        void BackButton()
        {
            _logPanel.SetActive(false);
        }
    }
}
