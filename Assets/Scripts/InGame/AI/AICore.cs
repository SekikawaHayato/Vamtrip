using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Vampire.InGame;
using Vampire.Gimmick;

namespace Vampire.AI
{
    public class AICore : MonoBehaviour,IAIProvider
    {
        
        #region UniRx
        public IReadOnlyReactiveProperty<string> CommentText => _commentText;
        public IObservable<Unit> HitWind => _hitWind;
        public IObservable<bool> HitGarlic => _hitGarlic;
        public IObservable<Unit> PutOn => _putOn;
        public IObservable<Unit> Damage => _damage;

        // イベントの発行に利用するSubject
        readonly ReactiveProperty<string> _commentText = new ReactiveProperty<string>();
        readonly Subject<Unit> _hitWind = new Subject<Unit>();
        readonly Subject<bool> _hitGarlic = new Subject<bool>();
        readonly Subject<Unit> _putOn = new Subject<Unit>();
        readonly Subject<Unit> _damage = new Subject<Unit>();
        #endregion
        [SerializeField] List<TargetInfo> _allTargets;
        [SerializeField] TargetInfo _goal;
        [SerializeField] GameManager _gameManager;

        AIState _state = AIState.Stay;
        TargetInfo _target;
        float _damageTimer;

        /// <value>キャラクターの状態</value>
        public AIState State
        {
            get { return _state; }
        }

        /// <value>目的地のゲームオブジェクトのトランスフォーム</value>
        public Transform Target
        {
            get { return _target.transform; }
        }

        /// <value>移動する向き</value>
        public Vector3 Direction
        {
            get { return _target.transform.position - this.gameObject.transform.position; }
        }

        /// <summary>
        /// 変数の初期化
        /// イベントの追加
        /// </summary>
        void Start()
        {
            ChangeTarget();
            _state = AIState.Stay;
            _gameManager.GameState
                .Where(_ => _ == GameState.Gaming)
                .Take(1)
                .Subscribe(_ => { _state = AIState.Move; })
                .AddTo(this);

            this.OnTriggerEnter2DAsObservable()
                .Where(_ => _state == AIState.Move)
                .Subscribe(_ =>
                {
                    if (_.gameObject == _target.gameObject)
                    {
                        Arrival();
                    }
                    else if (_.gameObject.tag == "Wind")
                    {
                        Wind();
                    }
                    else if (_.gameObject.tag == "Garlic")
                    {
                        Garlic();
                    }
                })
                .AddTo(this);
            this.OnTriggerExit2DAsObservable()
                .Where(_ => _.gameObject.tag == "Garlic")
                .Subscribe(_ =>
                {
                    _hitGarlic.OnNext(false);
                    _state = AIState.Move;
                })
                .AddTo(this);

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    switch (_state)
                    {
                        case AIState.Stay:

                            break;
                        case AIState.Endure:
                            _damageTimer -= Time.deltaTime;
                            if (_damageTimer <= 0)
                            {
                                _damage.OnNext(Unit.Default);
                                _state = AIState.Damage;
                                _damageTimer = 2;
                            }
                            break;
                        case AIState.Damage:
                            _damageTimer -= Time.deltaTime;
                            if (_damageTimer <= 0)
                            {
                                // ゲームオーバー処理
                                _gameManager.GameOver();
                                _state = AIState.Stay;
                            }
                            break;
                        case AIState.Move:

                            break;
                    }
                })
                .AddTo(this);
        }

        /// <summary>
        /// 次の目的地を設定するメソッド
        /// ゴール時はゴールの処理
        /// </summary>
        void Arrival()
        {
            _state = AIState.Stay;
            if (_target != _goal)
            {
                StartCoroutine(ChangeTargetCoroutine());
            }
            else
            {
                _gameManager.GameClear();
            }
        }

        /// <summary>
        /// ステートを耐久にし、風に当たったことを通知するメソッド
        /// </summary>
        void Wind()
        {
            _damageTimer = 0.5f;
            _state = AIState.Endure;
            _hitWind.OnNext(Unit.Default);

        }

        void Garlic()
        {
            _state = AIState.Discomfort;
            _hitGarlic.OnNext(true);
        }

        /// <summary>
        /// 風によるダメージを回復するメソッド
        /// </summary>
        void PutOnHat()
        {
            if(_state == AIState.Endure||_state == AIState.Damage)
            {
                _state = AIState.Move;
                _putOn.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// 目的地を変更し、移動可能にするメソッド
        /// </summary>
        IEnumerator ChangeTargetCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            ChangeTarget();
            _commentText.Value = _target.Comment;
            yield return new WaitForSeconds(1.0f);
            _state = AIState.Move;
        }

        /// <summary>
        /// 目的地を変更するメソッド
        /// </summary>
        void ChangeTarget()
        {
            if (_allTargets.Count == 0)
            {
                _target = _goal;
            }
            else
            {
                int index = UnityEngine.Random.Range(0, _allTargets.Count);
                _target = _allTargets[index];
                _allTargets.RemoveAt(index);
            }
        }
    }
}
