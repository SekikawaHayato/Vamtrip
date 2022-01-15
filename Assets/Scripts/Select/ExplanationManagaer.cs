using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.Select
{
    public class ExplanationManagaer : MonoBehaviour
    {
        [SerializeField] GameObject _explanationPanel;
        [SerializeField] Button _explanationButton;
        [SerializeField] Button _exitButton;
        [SerializeField] Button _nextButton;
        [SerializeField] Button _backButton;
        [SerializeField] Image _image;
        [SerializeField] Text _text;
        [SerializeField] ExplanationData[] _explanationData;
        int _index;

        /// <summary>
        /// まだ操作説明を１度も表示していなければ表示
        /// イベントの追加
        /// </summary>
        void Start()
        {
            if (!SaveDataManager.Instance.SaveDataInstance.isExplained)
            {
                Open();
                SaveDataManager.Instance.SaveExplained();
            }
            _explanationButton.onClick.AsObservable()
                .Subscribe(t => Open())
                .AddTo(this);
            _exitButton.onClick.AsObservable()
                .Subscribe(t => Exit())
                .AddTo(this);
            _nextButton.onClick.AsObservable()
                .Subscribe(t => Change(1))
                .AddTo(this);
            _backButton.onClick.AsObservable()
                .Subscribe(t => Change(-1))
                .AddTo(this);
        }

        /// <summary>
        /// 操作説明画面を表示するメソッド
        /// </summary>
        void Open()
        {
            _index = 0;
            _explanationPanel.SetActive(true);
            SetData();
            SetActive();
        }

        /// <summary>
        /// 操作説明画面を非表示にするメソッド
        /// </summary>
        void Exit()
        {
            _explanationPanel.SetActive(false);
        }

        /// <summary>
        /// 操作説明画面のページを変更するメソッド
        /// </summary>
        /// <param name="number">何ページめくるか</param>
        void Change(int number)
        {
            _index += number;
            SetData();
            SetActive();
        }

        /// <summary>
        /// ページ数に合わせて操作説明を画面に表示するメソッド
        /// </summary>
        void SetData()
        {
            _image.sprite = _explanationData[_index].Image;
            _text.text = _explanationData[_index].Text;
        }

        /// <summary>
        /// 現在のページ数に合わせてボタンの有効無効を切り替えるメソッド
        /// </summary>
        void SetActive()
        {
            _backButton.interactable = _index != 0;
            _nextButton.interactable = _index != _explanationData.Length - 1;
        }
    }
}
