using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.InGame
{
    public class GameClearManager : MonoBehaviour
    {
        [SerializeField] Button _backHomeButton;

        // Start is called before the first frame update
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

        // Update is called once per frame
        void Update()
        {

        }
    }
}
