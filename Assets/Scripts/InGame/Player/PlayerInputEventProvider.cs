using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.Players.InputImpls
{
    public sealed class PlayerInputEventProvider : MonoBehaviour,IPlayerInputEventProvider
    {
        #region IInputEventProvider
        public IReadOnlyReactiveProperty<Vector3> MoveDirection => _move;
        public ReactiveCommand PutOn => _putOn;
        public ReactiveCommand Fan => _fan;

        // イベント発行に利用するSubjectやReactiveProperty
        readonly ReactiveProperty<Vector3> _move = new ReactiveProperty<Vector3>();
        readonly ReactiveCommand _putOn = new ReactiveCommand();
        readonly ReactiveCommand _fan = new ReactiveCommand();
        #endregion

        [SerializeField] Joystick joystick = null;
        [SerializeField] Button _putOnButton;
        [SerializeField] Button _fanButton;
        float _moveLimit = 0.9f;

        /// <summary>
        /// イベントの追加
        /// </summary>
        void Awake()
        {
            _putOn.BindTo(_putOnButton)
                .AddTo(this);
            _move.AddTo(this);
            _fan.BindTo(_fanButton)
                .AddTo(this);
        }

        /// <summary>
        /// 移動方向の通知
        /// </summary>
        void Update()
        {
            Vector3 input = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
            _move.SetValueAndForceNotify(input.magnitude < _moveLimit ? Vector3.zero : input);
        }
    }
}