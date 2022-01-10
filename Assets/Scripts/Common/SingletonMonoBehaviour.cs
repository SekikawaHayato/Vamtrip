using System;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T:MonoBehaviour
{
    static T _instance;

    public static T Instance {
        get
        {
            if(_instance == null)
            {
                Type t = typeof(T);

                _instance = (T)FindObjectOfType(t);
                if (_instance == null)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }
            return _instance;
        }
    }

    virtual protected void Awake()
    {
        if (CheckInstance())
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    virtual protected bool CheckInstance()
    {
        return this != Instance;
    }
}
