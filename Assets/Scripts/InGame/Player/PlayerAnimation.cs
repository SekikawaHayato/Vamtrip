using UnityEngine;

namespace Vampire.Players {
    public class PlayerAnimation : MonoBehaviour
    {
        Animator _animator;
        IPlayerInputEventProvider _inputEventProvider;

        /// <summary>
        /// 変数の初期化
        /// </summary>
        void Start()
        {
            TryGetComponent<Animator>(out _animator);
            TryGetComponent<IPlayerInputEventProvider>(out _inputEventProvider);
        }

        /// <summary>
        /// アニメーションのパラメータを更新
        /// </summary>
        void Update()
        {
            _animator.SetFloat("x",_inputEventProvider.MoveDirection.Value.x);
            _animator.speed = _inputEventProvider.MoveDirection.Value==Vector3.zero ? 0 : 1;
        }
    }
}
