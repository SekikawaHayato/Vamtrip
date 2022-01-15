using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicator : MonoBehaviour
{
    Button _button;

    /// <summary>
    /// 変数の初期化
    /// ボタンを無効状態にする
    /// </summary>
    void Start()
    {
        _button = GetComponent<Button>();
        _button.interactable = false;
    }

    /// <summary>
    /// ボタンの有効無効を切り替えるメソッド
    /// </summary>
    /// <param name="flg">セットする値</param>
    public void SetActive(bool flg)
    {
        _button.interactable = flg;
    }
}
