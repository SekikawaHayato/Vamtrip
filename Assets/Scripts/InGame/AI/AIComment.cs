using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Vampire.InGame;

namespace Vampire.AI
{
    public class AIComment : MonoBehaviour
    {
        [SerializeField] Text _commentText;
        [SerializeField] GameManager _gameManager;
        [SerializeField] float _displayTime;
        IAIProvider _aiProvider;
        float _countTimer;

        /// <summary>
        /// イベントの追加
        /// </summary>
        private void Start()
        {
            if(TryGetComponent<IAIProvider>(out _aiProvider))
            {
                _aiProvider.CommentText
                    .Subscribe(t =>
                    {
                        DisplayText(t);
                    })
                    .AddTo(this);
            }

            _gameManager.CommentText
                .Subscribe(t =>
                {
                    DisplayText(t);
                })
                .AddTo(this);

            this.UpdateAsObservable()
                .Where(_=>_countTimer>0)
                .Subscribe(_ =>
                {
                    _countTimer -= Time.deltaTime;
                    if (_countTimer < 0) DeleteText();
                })
                .AddTo(this);            
        }

        /// <summary>
        /// リナのセリフを表示するメソッド
        /// </summary>
        /// <param name="text">表示するセリフ</param>
        void DisplayText(string text)
        {
            _commentText.text = text;
            _countTimer = _displayTime;
        }

        /// <summary>
        /// リナのセリフを非表示にするメソッド
        /// </summary>
        void DeleteText()
        {
            _commentText.text = string.Empty;
        }
    }
}
