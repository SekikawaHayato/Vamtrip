using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
    // 非同期動作で使用するAsyncOperation
    AsyncOperation _async;
    
    [SerializeField] Transform _maskImage; // 背景を切り抜くマスク画像
    [SerializeField] GameObject _parent; // シーン遷移時に利用するゲームオブジェクトの親
    [SerializeField] float _maxScale; //マスクをスケールする最大値
    [SerializeField] float _scaleTime;

    /// <summary>
    /// シナリオシーンでシーン切り替えを実行するメソッド
    /// </summary>
    public void NextScene()
    {
        if (_async == null)
        {
            StartCoroutine(NextSceneCoroutine("Scenario"));
        }
    }

    /// <summary>
    /// 指定したシーンでシーン切り替えを実行するメソッド
    /// </summary>
    /// <param name="name">読み込むシーン名</param>
    public void NextScene(string name){
        if(_async==null)StartCoroutine(NextSceneCoroutine(name));
    }

    /// <summary>
    /// 指定したシーンを読み込むメソッド(コルーチン)
    /// </summary>
    /// <param name="name"></param>
    IEnumerator NextSceneCoroutine(string name)
    {
        float currentScale = 0;
        float scaleSpeed =  _maxScale/_scaleTime;
        _parent.SetActive(true);
        while (currentScale <= _maxScale)
        {
            currentScale += scaleSpeed*Time.deltaTime;
            _maskImage.localScale = Vector3.one * currentScale;
            yield return null;
        }
        AssetBundle.UnloadAllAssetBundles(true);
        _async = SceneManager.LoadSceneAsync(name);
        while (!_async.isDone)
        {
            // LoadingEffect
            yield return null;
        }
        while (currentScale > 0 )
        {
            currentScale -= scaleSpeed * Time.deltaTime;
            if (currentScale < 0) currentScale = 0;
            _maskImage.localScale = Vector3.one * currentScale;
            yield return null;
        }
        _parent.SetActive(false);
        _async=null;
    }
}
