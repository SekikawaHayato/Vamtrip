using UniRx;

namespace Vampire.InGame
{
    /// <summary>
    /// GameStateのインターフェース
    /// </summary>
    public interface IGameStateProvider
    {
        IReadOnlyReactiveProperty<GameState> GameState { get; }
    }
}
