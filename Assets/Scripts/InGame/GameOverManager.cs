using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.InGame
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] Button _retryButton;
        [SerializeField] Button _backHomeButton;
        bool _isSelected = false;

        // Start is called before the first frame update
        void Start()
        {
            _retryButton.onClick.AsObservable()
                .Where(t => !_isSelected)
                .Subscribe(t =>
                {
                    Scenario.ScenarioData.InitDegressOfProgress();
                    SceneLoader.Instance.NextScene();
                    _isSelected = true;
                })
                .AddTo(this);
            _backHomeButton.onClick.AsObservable()
                .Where(t => !_isSelected)
                .Subscribe(t =>
                {
                    SceneLoader.Instance.NextScene("Select");
                    _isSelected = true;
                })
                .AddTo(this);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
