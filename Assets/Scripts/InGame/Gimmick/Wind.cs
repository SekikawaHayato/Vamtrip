using UnityEngine;

namespace Vampire.Gimmick
{
    public class Wind : MonoBehaviour
    {
        float _moveSpeed;
        float _lifeTime;

        /// <summary>
        /// 移動処理
        /// 破壊処理
        /// </summary>
        void Update()
        {
            transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0) Destroy(this.gameObject);
        }

        /// <summary>
        /// 変数を初期化するメソッド
        /// </summary>
        /// <param name="moveSpeed">移動スピード</param>
        /// <param name="lifeTime">生存時間</param>
        public void SetParameter(float moveSpeed = 5, float lifeTime = 2)
        {
            this._moveSpeed = moveSpeed;
            this._lifeTime = lifeTime;
        }
    }
}
