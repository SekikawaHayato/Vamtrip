using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

namespace Vampire.Scenario
{
    public class ClickEventProvider : MonoBehaviour,IClickEventProvider
    {
        #region UniRx
        public IObservable<bool> IsClick => _isClickSubject;

        // イベントの発行に利用するSubject
        readonly Subject<bool> _isClickSubject=new Subject<bool>();
        #endregion

        /// <summary>
        /// イベントの追加
        /// </summary>
        void Start()
        {
            _isClickSubject.AddTo(this);
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Where(_ => !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                .Subscribe(_ =>
                {
                    _isClickSubject.OnNext(true);
                })
                .AddTo(this);
        }
    }
}
