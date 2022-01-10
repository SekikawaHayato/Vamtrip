using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Vampire.InGame;

namespace Vampire.Players{
    public class PlayerCore : MonoBehaviour
    {
        #region UniRx
        public IObservable<Vector3> MoveObservable => _moveSubject;
        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;

        // イベントの発行に利用するSubject
        readonly ReactiveProperty<bool> _isDead = new ReactiveProperty<bool>();
        readonly Subject<Vector3> _moveSubject= new Subject<Vector3>();
        #endregion

        IPlayerInputEventProvider _inputEventProvider;
        [SerializeField] GameManager _gameManager;
        [SerializeField] ButtonIndicator _putOnIndicator;
        [SerializeField] ButtonIndicator _fanIndicator;

        /// <summary>
        /// 変数の初期化
        /// イベントの追加
        /// </summary>
        void Start()
        {
            _isDead.AddTo(this);
            _inputEventProvider = GetComponent<IPlayerInputEventProvider>();
            _inputEventProvider.PutOn
                .Subscribe(t =>
                {
                    RaycastHit2D[] hitInfo = Physics2D.CircleCastAll(transform.position, 1, Vector2.zero, 0);
                    foreach(RaycastHit2D hit in hitInfo)
                    {
                        if(hit.collider.gameObject.tag == "Rina")
                        {
                            hit.collider.gameObject.SendMessage("PutOnHat");
                        }
                    }
                })
                .AddTo(this);

            _inputEventProvider.Fan
                .Subscribe(t =>
                {
                    RaycastHit2D[] hitInfo = Physics2D.CircleCastAll(transform.position, 1, Vector2.zero, 0);
                    foreach (RaycastHit2D hit in hitInfo)
                    {
                        if (hit.collider.gameObject.tag == "Garlic")
                        {
                            hit.collider.gameObject.SendMessage("Fan");
                        }
                    }
                })
                .AddTo(this);

            if (_putOnIndicator != null)
            {
                this.OnTriggerEnter2DAsObservable()
                    .Where(_ => _.gameObject.tag == "Rina")
                    .Subscribe(_ =>
                    {
                        _putOnIndicator.SetActive(true);
                    })
                    .AddTo(this);

                this.OnTriggerExit2DAsObservable()
                    .Where(_ => _.gameObject.tag == "Rina")
                    .Subscribe(_ =>
                    {
                        _putOnIndicator.SetActive(false);
                    })
                    .AddTo(this);
            }

            if (_fanIndicator != null)
            {
                this.OnTriggerEnter2DAsObservable()
                    .Where(_ => _.gameObject.tag == "Garlic")
                    .Subscribe(_ =>
                    { 
                        _fanIndicator.SetActive(true);
                    })
                    .AddTo(this);

                this.OnTriggerExit2DAsObservable()
                    .Where(_ => _.gameObject.tag == "Garlic")
                    .Subscribe(_ =>
                    {
                        _fanIndicator.SetActive(false);
                    })
                    .AddTo(this);
            }

            _inputEventProvider.MoveDirection
                .Where(_ => _gameManager.GameState.Value == GameState.Gaming)
                .Subscribe(_ =>
                {
                    _moveSubject.OnNext(_);
                })
                .AddTo(this);
        }
    }
}
