using System;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Vampire.Scenario
{
    public class ScenarioManager : MonoBehaviour
    {
        #region UniRx
        public IReadOnlyReactiveProperty<string> MessageText => _messageText;
        public IObservable<string> NameText => _nameText;
        public IObservable<string> RinaFace => _rinaFace;
        public IObservable<string> AdolfFace => _adolfFace;
        public IObservable<bool> RinaActive => _rinaActive;
        public IObservable<bool> AdolfActive => _adolfActive;

        // イベント発行に利用するReactivePropertyなど
        readonly ReactiveProperty<string> _messageText = new ReactiveProperty<string>();
        readonly Subject<string> _nameText = new Subject<string>();
        readonly Subject<string> _rinaFace = new Subject<string>();
        readonly Subject<string> _adolfFace = new Subject<string>();
        readonly Subject<bool> _rinaActive = new Subject<bool>();
        readonly Subject<bool> _adolfActive = new Subject<bool>();
        #endregion
        [SerializeField] ScenarioData _scenarioData;

        // シナリオの進行を制御する変数
        int _currentStep = 0;
        int _currentLine = 0;
        int _currentLineTextCount = 0;
        int _lineCount = 0;
        string _previousLine = string.Empty;
        string _currentText = string.Empty;
        string[] _lineText;
        float _timeUntilDisplay = 0;
        float _timeElapsed = 1;
        float _lastUpdateCharacter = -1;
        float interval = 0.05f;
        bool _isDisplayComplete = true;
        bool _isTransitionCpmplete = true;

        // InputEvent
        IClickEventProvider _inputEventProvider;
        BackgroundChanger _backgroundChanger;
        LogData _logData;

        /// <value>シナリオのログデータ</value>
        public LogData LogData
        {
            get { return _logData; }
        }

        /// <summary>
        /// 変数の初期化
        /// イベントの追加
        /// </summary>
        void Start()
        {
            _scenarioData.LoadData();
            _logData = new LogData();

            _backgroundChanger = GetComponent<BackgroundChanger>();
            if (TryGetComponent<IClickEventProvider>(out _inputEventProvider))
            {
                _inputEventProvider.IsClick
                    .Subscribe(_ => ClickEvent())
                    .AddTo(this);
            }

            this.UpdateAsObservable()
                .Where(_ => _isTransitionCpmplete)
                .Where(_ => !_isDisplayComplete)
                .Subscribe(_ => UpdateText())
                .AddTo(this);

            SetNextStep();
        }

        /// <summary>
        /// 表示するテキストを更新するメソッド
        /// </summary>
        void UpdateText()
        {
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - _timeElapsed) / _timeUntilDisplay) * _currentLineTextCount);
            if (displayCharacterCount != _lastUpdateCharacter)
            {
                _messageText.Value = _previousLine + _lineText[_currentLine].Substring(0, displayCharacterCount);
                _lastUpdateCharacter = displayCharacterCount;
                if(_lastUpdateCharacter == _currentLineTextCount) CheckDisplayComplete();
            }
        }

        /// <summary>
        /// テキストの表示が完了しているか確認し、次の処理を呼び出すメソッド
        /// </summary>
        void CheckDisplayComplete()
        {
            if (_currentLine != _lineCount)
            {
                _previousLine += _lineText[_currentLine] + "\n";
                SetNextLine();
            }
            else
            {
                _logData.AddMessage(_messageText.Value);
                _isDisplayComplete = true;
            }
        }

        /// <summary>
        /// クリックイベントの処理を行うメソッド
        /// </summary>
        void ClickEvent()
        {
            if (!_isTransitionCpmplete) return;
            if (!_isDisplayComplete)
            {
                while (_currentLine < _lineCount)
                {
                    _previousLine += _lineText[_currentLine] + "\n";
                    _currentLine++;
                }
                _messageText.Value = _previousLine + _lineText[_currentLine];
                _logData.AddMessage(_messageText.Value);
                _isDisplayComplete = true;
            }
            else if (_currentStep < _scenarioData.Scenarios.Length)
            {
                SetNextStep();
            }
        }        

        /// <summary>
        /// 次のステップを読み込むメソッド
        /// </summary>
        void SetNextStep()
        {
            switch (_scenarioData.Scenarios[_currentStep].type)
            {
                case "Background": // 背景の変更
                    _isTransitionCpmplete = false;
                    _backgroundChanger.ChangeBackground(_scenarioData.Scenarios[_currentStep].option, BackgroundCallBack);
                    _messageText.Value = null;
                    _nameText.OnNext(null);
                    break;
                case "Words": // テキストを表示
                    _currentText = _scenarioData.Scenarios[_currentStep].message;
                    _nameText.OnNext(_scenarioData.CharacterName[_scenarioData.Scenarios[_currentStep].option]);
                    _logData.AddName(_scenarioData.CharacterName[_scenarioData.Scenarios[_currentStep].option]);
                    if (_scenarioData.Scenarios[_currentStep].rinaFace != "") _rinaFace.OnNext(_scenarioData.Scenarios[_currentStep].rinaFace);
                    if (_scenarioData.Scenarios[_currentStep].adolfFace != "") _adolfFace.OnNext(_scenarioData.Scenarios[_currentStep].adolfFace);
                    switch (_scenarioData.Scenarios[_currentStep].rinaActive)
                    {
                        case "Active":
                            _rinaActive.OnNext(true);
                            break;
                        case "Inactive":
                            _rinaActive.OnNext(false);
                            break;
                    }
                    switch (_scenarioData.Scenarios[_currentStep].adolfActive)
                    {
                        case "Active":
                            _adolfActive.OnNext(true);
                            break;
                        case "Inactive":
                            _adolfActive.OnNext(false);
                            break;
                    }
                    _previousLine = string.Empty;
                    _lineText = _currentText.Split('\\');
                    _lineCount = _lineText.Length - 1;
                    _currentLine = -1;
                    SetNextLine();
                    _isDisplayComplete = false;
                    break;
                case "Scene": // 画面遷移
                    NextScene();
                    break;
            }
            _currentStep++;
        }

        /// <summary>
        /// 次の行を読み込むメソッド
        /// </summary>
        void SetNextLine()
        {
            _currentLine++;
            _currentLineTextCount = _lineText[_currentLine].Length;
            _timeUntilDisplay = _currentLineTextCount * interval;
            _timeElapsed = Time.time;
            _lastUpdateCharacter = -1;
        }

        /// <summary>
        /// 背景の変更時のコールバックメソッド
        /// </summary>
        void BackgroundCallBack()
        {
            _isTransitionCpmplete = true;
            SetNextStep();
        }

        /// <summary>
        /// 次のシーンに遷移するメソッド
        /// </summary>
        public void NextScene()
        {
            SceneLoader.Instance.NextScene(_scenarioData.Scenarios[_scenarioData.Scenarios.Length - 1].option);
        }
    }
}
