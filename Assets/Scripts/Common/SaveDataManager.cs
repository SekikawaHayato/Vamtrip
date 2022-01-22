public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
{
    const string KEY_SAVE_DATA = "SaveData";
    SaveData _saveData;

    /// <value>シングルトンパターンのインスタンス</value>
    public SaveData SaveDataInstance
    {
        get { return _saveData; }
    }

    /// <summary>
    /// データの読み込み
    /// 保存データがなければ作成
    /// </summary>
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

    /// <summary>
    /// 解放した節番号を更新して保存するメソッド
    /// </summary>
    public void SaveSolvedSection()
    {
        int sectionID = Vampire.Scenario.ScenarioLoader.SelectSectionID;
        if (_saveData.solvedSection < sectionID)
        {
            _saveData.solvedSection = sectionID;
            Save();
        }
    }

    /// <summary>
    /// 説明を表示したかどうかを更新して保存するメソッド
    /// </summary>
    public void SaveExplained()
    {
        _saveData.isExplained = true;
        Save();
    }

    // データを保存するメソッド
    void Save()
    {
        PlayerPrefsUtils.SetObject<SaveData>(KEY_SAVE_DATA, _saveData);
    }
}
