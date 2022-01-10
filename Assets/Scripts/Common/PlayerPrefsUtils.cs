using UnityEngine;

public static class PlayerPrefsUtils
{
    /// <summary>
    /// 指定されたオブジェクトの情報を保存
    /// </summary>
    /// <typeparam name="T">保存するオブジェクトの型</typeparam>
    /// <param name="key">保存するオブジェクトに対応するキー</param>
    /// <param name="obj">保存するオブジェクト</param>
    public static void SetObject<T>(string key, T obj)
    {
        var json = JsonUtility.ToJson(obj);
        PlayerPrefs.SetString(key, json);
    }

    /// <summary>
    /// 指定されたオブジェクトの情報を読み込む
    /// </summary>
    /// <typeparam name="T">読み込むオブジェクトの型</typeparam>
    /// <param name="key">読み込むオブジェクトに対応するキー</param>
    /// <returns>キーが存在する場合に対応するオブジェクト</returns>
    public static T GetObject<T>(string key)
    {
        var json = PlayerPrefs.GetString(key);
        var obj = JsonUtility.FromJson<T>(json);
        return obj;
    }
}