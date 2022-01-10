using UnityEngine;
using UniRx;

namespace Vampire.AI {
    public class AIAnimation : MonoBehaviour
    {
        Animator _animator;
        IAIProvider _aiProvider;

        /// <summary>
        /// 変数の初期化
        /// イベントの追加
        /// </summary>
        void Start()
        {
            TryGetComponent<Animator>(out _animator);
            if(TryGetComponent<IAIProvider>(out _aiProvider))
            {
                _aiProvider.HitWind
                    .Subscribe(_ =>
                    {
                        _animator.SetTrigger("HitWind");
                    })
                    .AddTo(this);
                _aiProvider.PutOn
                    .Subscribe(_ =>
                    {
                        _animator.SetTrigger("PutOn");
                    })
                    .AddTo(this);
                _aiProvider.Damage
                    .Subscribe(_ =>
                    {
                        _animator.SetTrigger("Damage");
                    })
                    .AddTo(this);
            }
        }

        /// <summary>
        /// アニメーションのパラメータを更新
        /// </summary>
        void Update()
        {
            _animator.SetFloat("x",_aiProvider.Direction.x);
            _animator.SetBool("Stay",_aiProvider.State == AIState.Stay) ;
        }
    }
}
