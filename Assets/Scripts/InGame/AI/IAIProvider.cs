using System;
using UnityEngine;
using UniRx;

namespace Vampire.AI
{
    /// <summary>
    /// AIのインターフェース
    /// </summary>
    public interface IAIProvider
    {
        AIState State { get; }
        Transform Target { get;}
        Vector3 Direction { get; }
        IReadOnlyReactiveProperty<string> CommentText { get; }
        IObservable<Unit> HitWind { get; }
        IObservable<Unit> PutOn { get; }
        IObservable<Unit> Damage { get; }
    }
}