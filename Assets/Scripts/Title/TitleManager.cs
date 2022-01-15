using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

namespace Vampire.Title
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] Button _startButton;
        [SerializeField] float _duration;
        [SerializeField] Ease _easeType;
        [SerializeField] AudioClip _playSE;
        Tweener _tweener;

        /// <summary>
        /// ボタンのアニメーションとクリック時の処理を設定する
        /// </summary>
        void Start()
        {
            _startButton.onClick.AsObservable()
                .Take(1)
                .Subscribe(_ =>
                {
                    SceneLoader.Instance.NextScene("Select");
                    SEManager.Instance.PlayOneShot(_playSE);
                    _tweener.Kill();
                })
                .AddTo(this);
            _tweener = _startButton.image
                .DOFade(0.0f, _duration)
                .SetEase(_easeType)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
