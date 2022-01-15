using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Vampire.InGame;

namespace Vampire.AI
{
    public class AIDiscomfort : MonoBehaviour
    {
        [SerializeField] Image _image;
        [SerializeField] Sprite[] _discomfortSprite;
        [SerializeField] GameManager _gameManager;
        IAIProvider _aiProvider;
        bool _hitGarlic = false;
        int _index = 2;
        float _timer = 3.0f;

        /// <summary>
        /// 変数の初期化
        /// イベントの発行
        /// </summary>
        void Start()
        {
            _aiProvider = GetComponent<IAIProvider>();
            _aiProvider.HitGarlic
                .Subscribe(t =>
                {
                    _hitGarlic = t;
                    _image.enabled = t;
                    _image.sprite = _discomfortSprite[_index];
                })
                .AddTo(this);

            this.UpdateAsObservable()
                .Where(_ => _gameManager.GameState.Value == GameState.Gaming)
                .Subscribe(t =>
                {
                    if (_hitGarlic)
                    {
                        _timer -= Time.deltaTime;
                        if (_timer < _index)
                        {
                            _index--;
                            if (_index == -1)
                            {
                                _gameManager.GameOver();
                                return;
                            }
                            _image.sprite = _discomfortSprite[_index];
                        }
                    }
                    else if (_timer < 3.0f)
                    {
                        _timer += Time.deltaTime * 0.5f;
                        if (_timer >= _index + 1 && _index < 2)
                        {
                            _index++;
                        }
                    }
                })
                .AddTo(this);
        }
    }
}
