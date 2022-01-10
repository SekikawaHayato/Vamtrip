using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.Scenario
{
    public class CharacterIndicator : MonoBehaviour
    {
        [SerializeField] Image _rinaImage;
        [SerializeField] Image _AdolfImage;
        [SerializeField] AssetBundleLoader _rinaAssetBundleLoader;
        [SerializeField] AssetBundleLoader _adolfAssetBundleLoader;

        ScenarioManager _scenarioManager;


        /// <summary>
        /// 画像データの読み込み
        /// イベントの追加
        /// </summary>
        void Awake()
        {
            _rinaAssetBundleLoader.LoadAssets();
            _adolfAssetBundleLoader.LoadAssets();

            if (TryGetComponent<ScenarioManager>(out _scenarioManager))
            {
                _scenarioManager.RinaFace
                    .Subscribe(t =>
                    {
                        RinaChanger(t);
                    })
                    .AddTo(this);
                _scenarioManager.AdolfFace
                    .Subscribe(t =>
                    {
                        AdolfChanger(t);
                    })
                    .AddTo(this);
                _scenarioManager.RinaActive
                    .Subscribe(t =>
                    {
                        RinaActive(t);
                    })
                    .AddTo(this);
                _scenarioManager.AdolfActive
                    .Subscribe(t =>
                    {
                        AdolfActive(t);
                    })
                    .AddTo(this);
            }
        }

        // TODO:キャラ数増えた時に対応できるようにメソッドを変更する

        /// <summary>
        /// リナの表情を変更するメソッド
        /// </summary>
        /// <param name="face">変更する表情名</param>
        void RinaChanger(string face)
        {
            _rinaImage.sprite = _rinaAssetBundleLoader.sprites["Rina_"+face];
        }

        /// <summary>
        /// アドルフの表情を変更するメソッド
        /// </summary>
        /// <param name="face">変更する表情名</param>
        void AdolfChanger(string face)
        {
            _AdolfImage.sprite = _adolfAssetBundleLoader.sprites["Adolf_"+face];
        }

        /// <summary>
        /// リナの立ち絵の表示を切り替えるメソッド
        /// </summary>
        /// <param name="flag">表示するかどうかのフラグ</param>
        void RinaActive(bool flag)
        {
            _rinaImage.gameObject.SetActive(flag);
        }

        /// <summary>
        /// アドルフの立ち絵の表示を切り替えるメソッド
        /// </summary>
        /// <param name="flag">表示するかどうかのフラグ</param>
        void AdolfActive(bool flag)
        {
            _AdolfImage.gameObject.SetActive(flag);
        }

        /// <summary>
        /// 全ての立ち絵を非表示にするメソッド
        /// </summary>
        public void CharaInactive()
        {
            RinaActive(false);
            AdolfActive(false);
        }
    }
}
