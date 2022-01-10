using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.Select
{
    public class SectionText : MonoBehaviour
    {
        [SerializeField] Text _numberText;
        [SerializeField] Text _titleText;
        int _sectionID;

        public void SetText(int number,string title)
        {
            _numberText.text = "第" + (number+1) + "節";
            _titleText.text = title;
            _sectionID = number;
            GetComponent<Button>().onClick.AsObservable()
                .Subscribe(t => SelectSection())
                .AddTo(this);
        }

        void SelectSection()
        {
            Scenario.ScenarioData.SetSectionID(_sectionID);
            SceneLoader.Instance.NextScene();
        }
    }
}
