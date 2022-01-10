using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.Scenario
{
    public class Skip : MonoBehaviour
    {
        [SerializeField] Button _skipPanelButton;
        [SerializeField] Button _skipButton;
        [SerializeField] Button _backButton;
        [SerializeField] GameObject _skipPanel;
        [SerializeField] ScenarioManager _scenarioManager;

        /// <summary>
        /// イベントの追加
        /// </summary>
        void Start()
        {
            _skipPanelButton
                .onClick
                .AsObservable()
                .Subscribe(t => SkipPanelButton())
                .AddTo(this);
            _skipButton.onClick
                .AsObservable()
                .Subscribe(t => SkipButton())
                .AddTo(this);
            _backButton.onClick
                .AsObservable()
                .Subscribe(t => BackButton())
                .AddTo(this);
        }

        /// <summary>
        /// スキップパネルを表示するメソッド
        /// </summary>
        void SkipPanelButton()
        {
            _skipPanel.SetActive(true);
        }

        /// <summary>
        /// シナリオをスキップして次のシーンを表示するメソッド
        /// </summary>
        void SkipButton()
        {
            _scenarioManager.NextScene();
        }

        /// <summary>
        /// スキップパネルを非表示にするメソッド
        /// </summary>
        void BackButton()
        {
            _skipPanel.SetActive(false);
        }
    }
}
