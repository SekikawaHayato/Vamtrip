using UnityEngine;

namespace Vampire
{
    public class SEManager : SingletonMonoBehaviour<SEManager>
    {
        AudioSource _audioSource;

        /// <summary>
        /// 変数の初期化
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// 効果音を再生するメソッド
        /// </summary>
        /// <param name="_clip">再生する音源ファイル</param>
        public void PlayOneShot(AudioClip _clip)
        {
            _audioSource.PlayOneShot(_clip);
        }
    }
}
