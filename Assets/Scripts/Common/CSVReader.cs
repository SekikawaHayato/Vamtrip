using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Vampire.Scenario;

public class CSVReader
{
    /// <summary>
    /// 名前の情報を記録するCSVファイルのデータをリストにして取得するメソッド
    /// </summary>
    /// <param name="csvFile">読み込むCSVファイル</param>
    /// <returns>読み込んだデータのリスト</returns>
    public static List<string[]> LoadName(TextAsset csvFile)
    {
        List<string[]> _csvDatas = new List<string[]>();
        StringReader _reader = new StringReader(csvFile.text);

        while(_reader.Peek() != -1)
        {
            string line = _reader.ReadLine();
            _csvDatas.Add(line.Split(','));
        }

        return _csvDatas;
    }

    /// <summary>
    /// シナリオの情報を記録するCSVファイルのデータをリストにして取得するメソッド
    /// </summary>
    /// <param name="csvFile">読み込むCSVファイル</param>
    /// <returns>読み込んだデータのリスト</returns>
    public static ScenarioInfo[] LoadScenario(TextAsset csvFile)
    {
        List<ScenarioInfo> csvDatas = new List<ScenarioInfo>();
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string[] line = reader.ReadLine().Split(',');
            csvDatas.Add(new ScenarioInfo(line));
        }

        return csvDatas.ToArray();
    }
}
