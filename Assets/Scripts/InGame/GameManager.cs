using System.Collections;
using UnityEngine;
using UniRx;

namespace Vampire.InGame
{
    public class GameManager : MonoBehaviour
    {
        #region UniRx
        public IReadOnlyReactiveProperty<GameState> GameState => _gameState;
        public IReadOnlyReactiveProperty<string> CommentText => _commentText;

        // イベント発行に利用するReactiveProperty
        readonly ReactiveProperty<GameState> _gameState = new ReactiveProperty<GameState>();
        readonly ReactiveProperty<string> _commentText = new ReactiveProperty<string>();
        #endregion

        [SerializeField] float _startTime;

        /// <summary>
        /// 変数の初期化
        /// インゲームの開始
        /// </summary>
        void Start()
        {
            _gameState.Value = InGame.GameState.Opening;
            StartCoroutine(GameStart());
        }

        /// <summary>
        /// インゲームを開始するメソッド
        /// </summary>
        IEnumerator GameStart()
        {
            yield return new WaitForSeconds(1.5f);
            _commentText.Value = "さぁ、行きますわよ！";
            yield return new WaitForSeconds(_startTime);
            _gameState.Value = InGame.GameState.Gaming;
        }

        /// <summary>
        /// ゲームクリア処理を行うメソッド
        /// </summary>
        public void GameClear()
        {
            _gameState.Value = InGame.GameState.Clear;
            Scenario.ScenarioLoader.UpdateDegressOfProgress();
            SceneLoader.Instance.NextScene("Scenario");
        }

        /// <summary>
        /// ゲームオーバー処理を行うメソッド
        /// </summary>
        public void GameOver()
        {
            _gameState.Value = InGame.GameState.Clear;
            SceneLoader.Instance.NextScene("GameOver");
        }
    }
}
