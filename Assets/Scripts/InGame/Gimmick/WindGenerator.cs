using UnityEngine;
using Vampire.InGame;

namespace Vampire.Gimmick
{
    public class WindGenerator : MonoBehaviour
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] Transform _target;
        [SerializeField] float _minInterval;
        [SerializeField] float _maxInterval;
        [SerializeField] float _minDistance;
        [SerializeField] float _maxDistance;
        [SerializeField] GameManager _gameManager;
        float _timer;

        /// <summary>
        /// ギミックの生成
        /// </summary>
        void Update()
        {
            if (_gameManager.GameState.Value == GameState.Gaming)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    GenerateWind();
                    _timer = Random.Range(_minInterval, _maxInterval) ;
                }
            }
        }

        /// <summary>
        /// 風を生成するメソッド
        /// </summary>
        void GenerateWind() {
            GameObject obj = Instantiate(_prefab, RandomPosition(), Quaternion.identity);
            obj.GetComponent<Wind>().SetParameter();
        }

        /// <summary>
        /// ランダムな座標を生成するメソッド
        /// </summary>
        /// <returns>ランダムな座標</returns>
        Vector3 RandomPosition()
        {

            float x = Random.Range(_minDistance, _maxDistance);
            float y = Random.Range(-10, 10);
            Vector3 position = _target.position + Vector3.right * x + Vector3.up * y;
            return position;
        }
    }
}
