using UnityEngine;
using UniRx;

namespace Vampire.Players
{
    /// <summary>
    /// プレイヤーイベントのインターフェース
    /// </summary>
    public interface IPlayerInputEventProvider
    {
        IReadOnlyReactiveProperty<Vector3> MoveDirection{ get;}
        ReactiveCommand PutOn { get;}
        ReactiveCommand Fan { get; }
    }
}
