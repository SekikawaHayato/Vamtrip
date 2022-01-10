using System;

namespace Vampire.Scenario
{
    /// <summary>
    /// クリックイベントのインターフェース
    /// </summary>
    public interface IClickEventProvider
    {
        IObservable<bool> IsClick { get; }
    }
}
