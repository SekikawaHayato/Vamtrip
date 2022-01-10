using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssetBundleLoader
{
    // 読み込むデータ
    public string[] _imageName;
    public string _assetName;

    // 読み込んだ画像を保管する変数
    public Dictionary<string, Sprite> sprites;

    /// <summary>
    /// 設定したデータを元にアセットバンドルから読み込むメソッド
    /// </summary>
    public void LoadAssets()
    {
        string path = Application.streamingAssetsPath + "/" + _assetName + ".assetbundle";
        AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
        sprites = new Dictionary<string, Sprite>();
        foreach (string name in _imageName)
        {
            sprites.Add(name, assetBundle.LoadAsset<Sprite>(name));
        }
    }
}
