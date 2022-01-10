using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int solvedSection; // 解放した節番号

    public SaveData(int solvedSection)
    {
        this.solvedSection = solvedSection;
    }
}
