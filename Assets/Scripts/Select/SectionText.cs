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

        /// <summary>
        /// 節の番号と題名を表示するメソッド
        /// </summary>
        /// <param name="number">節番号</param>
        /// <param name="title">題名</param>
        public void SetText(int number,string title)
        {
            _numberText.text = "第" + (number+1) + "節";
            _titleText.text = title;
            _sectionID = number;
            GetComponent<Button>().onClick.AsObservable()
                .Take(1)
                .Subscribe(t => SelectSection())
                .AddTo(this);
        }

        /// <summary>
        /// 節を選択した時の処理を行うメソッド
        /// </summary>
        void SelectSection()
        {
            Scenario.ScenarioLoader.SetSectionID(_sectionID);
            SceneLoader.Instance.NextScene();
        }
    }
}
