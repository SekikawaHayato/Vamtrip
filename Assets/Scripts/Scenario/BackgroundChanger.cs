using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Vampire.Scenario
{
    public class BackgroundChanger : MonoBehaviour
    {
        [SerializeField] float _fadeTime;
        [SerializeField] float _interval;
        [SerializeField] Image _fadePanel;
        [SerializeField] Image _background;
        [SerializeField] AssetBundleLoader _assetBundleLoader;
        [SerializeField] CharacterIndicator _characterIndicator;
        Color _fadeColor;
        Sprite _useSprite;

        bool _isComplete = true;

        /// <summary>
        /// 画像データの読み込み
        /// 変数の初期化
        /// </summary>
        void Awake()
        {
            // 画像データを読み込む
            _assetBundleLoader.LoadAssets();
            _fadeColor = _fadePanel.color;
        }

        /// <summary>
        /// 背景を切り替えを実行するメソッド
        /// </summary>
        /// <param name="imageName">変更する背景の名前</param>
        /// <param name="action">コールバックアクション</param>
        public void ChangeBackground(string imageName, Action action = null)
        {
            if (!_isComplete) return;
            if (imageName != "Blackout") _useSprite = _assetBundleLoader.sprites[imageName];
            StartCoroutine(Change(action));
        }

        /// <summary>
        /// 指定した画像に背景を切り替えるメソッド
        /// </summary>
        /// <param name="onFinished">コールバックアクション</param>
        IEnumerator Change(Action onFinished = null)
        {
            _isComplete = false;
            float fadeSpeed = 1f / _fadeTime;
            while (_fadeColor.a <= 1)
            {
                _fadeColor.a += fadeSpeed * Time.deltaTime;
                _fadePanel.color = _fadeColor;
                yield return null;
            }
            _background.sprite = _useSprite;
            _characterIndicator.CharaInactive();
            
            yield return new WaitForSeconds(_interval);
            while (_fadeColor.a >= 0)
            {
                _fadeColor.a -= fadeSpeed * Time.deltaTime;
                _fadePanel.color = _fadeColor;
                yield return null;
            }
            yield return new WaitForSeconds(_interval);
            _isComplete = true;
            if(onFinished != null)onFinished();
        }
    }
}
