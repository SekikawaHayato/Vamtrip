using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int solvedSection; // 解放した節番号
    public bool isExplained; // 説明を表示したか

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="solvedSection">解放した節番号</param>
    /// <param name="isExplained">説明を表示したか</param>
    public SaveData(int solvedSection, bool isExplained = false)
    {
        this.solvedSection = solvedSection;
        this.isExplained = isExplained;
    }
}
