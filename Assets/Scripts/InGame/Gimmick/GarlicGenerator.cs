using System.Collections;
using UnityEngine;

namespace Vampire.Gimmick
{
    public class GarlicGenerator : MonoBehaviour
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] Transform[] _generatePosition;

        /// <summary>
        /// 初期位置にギミックを生成する
        /// </summary>
        void Start()
        {
            foreach(Transform pos in _generatePosition)
            {
                GameObject obj = Instantiate(_prefab, pos.position, Quaternion.identity);
                obj.GetComponent<Garlic>().SetParameter(this, Random.Range(0.5f, 0.7f));
            }
        }

        /// <summary>
        /// 新しいギミックを生成する処理を呼び出すメソッド
        /// </summary>
        /// <param name="pos">生成する位置</param>
        public void DestroyGarlic(Vector2 pos)
        {
            StartCoroutine(GenerateGarlic(pos));
        }

        /// <summary>
        /// 新しいギミックを生成する処理
        /// </summary>
        /// <param name="pos">生成する位置</param>
        /// <returns></returns>
        IEnumerator GenerateGarlic(Vector2 pos) {
            yield return new WaitForSeconds(5.0f);
            GameObject obj = Instantiate(_prefab, pos, Quaternion.identity);
            obj.GetComponent<Garlic>().SetParameter(this,Random.Range(0.5f,0.7f));
        }
    }
}
