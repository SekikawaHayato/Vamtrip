using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

namespace Vampire.Scenario
{
    public class MenuMover : MonoBehaviour
    {
        [SerializeField] Button _menuButton;
        [SerializeField] RectTransform _menuObject;

        bool _canMove = true;
        float[] _posX = { 132,40 };
        float _moveTime = 0.5f;

        /// <summary>
        /// イベントの追加
        /// </summary>
        void Start()
        {
            _menuButton.onClick.AsObservable()
                .Where(_=>_canMove)
                .Select(_=>1)
                .Scan((acc,current)=>acc+current)
                .Subscribe(_ => {
                    _canMove = false;
                    _menuObject.DOAnchorPosX(_posX[_ % 2], _moveTime).OnComplete(()=>
                    {
                        _canMove = true;
                    });       
                })
                .AddTo(this);
        }
    }
}
