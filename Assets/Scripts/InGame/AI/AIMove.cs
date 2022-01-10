using UnityEngine;

namespace Vampire.AI
{
    public class AIMove : MonoBehaviour
    {
        [SerializeField] float _moveSpeed;
        IAIProvider _aIProvider;

        /// <summary>
        /// 変数の初期化
        /// </summary>
        void Start()
        {
            TryGetComponent<IAIProvider>(out _aIProvider);
        }

        /// <summary>
        /// 移動の処理
        /// </summary>
        void Update()
        {
            switch (_aIProvider.State)
            {
                case AIState.Move:
                    transform.Translate(_aIProvider.Direction.normalized * _moveSpeed * Time.deltaTime);
                    break;
            }
        }
    }
}
