using UnityEngine;
using UniRx;

namespace Vampire.Players{
    public sealed class PlayerMove : MonoBehaviour
    {
        [SerializeField] SliderIndicator _sliderIndicator;
        [SerializeField] float _defaultSpeed = 2;
        [SerializeField] float _maxStamina;
        [SerializeField] float _staminaDownValue;
        [SerializeField] float _staminaUpValue;
        
        float _currentStamina;
        float[] _countTimer;

        PlayerCore _playerCore;
        Rigidbody2D _rigidbody2D;

        /// <summary>
        /// 変数の初期化
        /// イベントの追加
        /// </summary>
        void Start()
        {
            
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerCore = GetComponent<PlayerCore>();
            _currentStamina = _maxStamina;
            _sliderIndicator.SetParameter(_maxStamina, _currentStamina);
            _countTimer = new float[2];
            _playerCore.MoveObservable
                .Subscribe(t =>
                {
                    StaminaChange(t == Vector3.zero);
                    Move(t);
                })
                .AddTo(this);
        }

        /// <summary>
        /// プレイヤーを移動させるメソッド
        /// </summary>
        /// <param name="moveVector">移動する方向</param>
        void Move(Vector3 moveVector){
            _rigidbody2D.velocity = moveVector * _defaultSpeed * GetMagnification();
        }

        /// <summary>
        /// スタミナを変更するメソッド
        /// </summary>
        /// <param name="flg">増やすかどうか</param>
        void StaminaChange(bool flg)
        {
            int index = flg ? 0 : 1;
            _countTimer[index] += Time.deltaTime;
            if (_countTimer[index] >= 2)
            {
                _countTimer[index] = 0;
                _currentStamina += flg ? _staminaUpValue : -_staminaDownValue;
                _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
                _sliderIndicator.UpdateParameter(_currentStamina);
            }
        }

        /// <summary>
        /// スタミナによる移動制限の値を取得するメソッド
        /// </summary>
        /// <returns>移動速度の倍率</returns>
        float GetMagnification()
        {
            if (_currentStamina == 0) return 0;
            if (_currentStamina <= _maxStamina / 3.0f) return 0.6f;
            if (_currentStamina <= _maxStamina * 2 / 3.0f) return 0.9f;
            return 1;
        }
    }
}
