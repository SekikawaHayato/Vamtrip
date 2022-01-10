using UnityEngine;

namespace Vampire.Gimmick {
    public class Garlic : MonoBehaviour
    {
        GarlicGenerator _generator;
        SpriteRenderer _sprite;
        Color _color;
        float _moveSpeed;
        float _timer;
        bool _isFan = false;
        float _maxLife = 8.0f;
        float _life = 8.0f;

        /// <summary>
        /// 移動処理
        /// </summary>
        void Update()
        {
            //if (_isFan)
            //{
            //    _color.a -= Time.deltaTime / 2.0f;
            //    _sprite.color = _color;
            //    if (_color.a < 0) Destroy();
            //}
            //else
            //{ 
            transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _moveSpeed *= -1;
                _timer = Random.Range(3.0f, 5.0f);
            }
            //}
        }

        /// <summary>
        /// 変数を初期化するメソッド
        /// </summary>
        /// <param name="generator">ギミックを生成するゲームオブジェクト</param>
        /// <param name="moveSpeed">移動スピード</param>
        public void SetParameter(GarlicGenerator generator, float moveSpeed = 0.6f)
        {
            this._moveSpeed = moveSpeed;
            this._generator = generator;
            _sprite = GetComponent<SpriteRenderer>();
            _color = _sprite.color;
        }

        /// <summary>
        /// 扇がれた時のメソッド
        /// </summary>
        void Fan()
        {
            _life--;
            _color.a = _life / _maxLife;
            _sprite.color = _color;
            if (_life<= 0) Destroy();
        }

        /// <summary>
        /// 破壊と生成のメソッド
        /// </summary>
        void Destroy()
        {
            _generator.DestroyGarlic(transform.position);
            Destroy(this.gameObject);
        }
    }
}
