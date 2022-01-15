using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.InGame
{
    public class GameClearManager : MonoBehaviour
    {
        [SerializeField] Button _backHomeButton;

        /// <summary>
        /// クリア情報の更新
        /// イベントの発行
        /// </summary>
        void Start()
        {
            SaveDataManager.Instance.SaveSolvedSection();
            _backHomeButton.onClick.AsObservable()
                .Take(1)
                .Subscribe(t =>
                {
                    SceneLoader.Instance.NextScene("Select");
                })
                .AddTo(this);
        }
    }
}
