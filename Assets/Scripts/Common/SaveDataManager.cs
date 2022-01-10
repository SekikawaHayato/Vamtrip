using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
{
    const string KEY_SAVE_DATA = "SaveData";

    SaveData _saveData;
    public SaveData SaveDataInstance
    {
        get { return _saveData; }
    }

    protected override void Awake()
    {
        base.Awake();
        if (CheckInstance()) return;
        _saveData = PlayerPrefsUtils.GetObject<SaveData>(KEY_SAVE_DATA);
        if (_saveData == null)
        {
            _saveData = new SaveData(0);
            PlayerPrefsUtils.SetObject<SaveData>(KEY_SAVE_DATA,_saveData);
        }
    }

    public void SaveSolvedSection()
    {
        int sectionID = Vampire.Scenario.ScenarioData.SelectSectionID;
        if (_saveData.solvedSection <= sectionID)
        {
            _saveData.solvedSection = sectionID + 1;
            PlayerPrefsUtils.SetObject<SaveData>(KEY_SAVE_DATA, _saveData);
        }
    }
}
