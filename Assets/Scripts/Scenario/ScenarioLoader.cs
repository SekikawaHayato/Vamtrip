using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vampire.Scenario
{
    [Serializable]
    public class ScenarioLoader
    {
        // シナリオデータ
        [SerializeField] TextAsset[] _scenarioTextAssets;
        [SerializeField] TextAsset _characterNameTextAssets;

        // シナリオを選択する変数
        static int _selectSectionID;
        static int _degreeOfProgress;

        ScenarioData[] _scenarios;
        Dictionary<string, string> _characterName;

        /// <value>シナリオデータの配列</value>
        public ScenarioData[] Scenarios
        {
            get { return _scenarios; }
        }

        /// <value>シナリオデータと対応する表示名の辞書</value>
        public Dictionary<string, string> CharacterName
        {
            get { return _characterName; }
        }

        /// <value>選択した節のID
        /// </value>
        public static int SelectSectionID
        {
            get { return _selectSectionID; }
        }

        /// <summary>
        /// シナリオデータの読み込み
        /// 読み込んだデータをScenarioManagerへセット
        /// </summary>
        public void LoadData()
        {
            // データの読み込み
            _scenarios = CSVReader.LoadScenario(_scenarioTextAssets[_degreeOfProgress]);

            List<string[]> characterNameSource = CSVReader.LoadName(_characterNameTextAssets);
            _characterName = new Dictionary<string, string>();

            foreach (string[] character in characterNameSource)
            {
                _characterName.Add(character[0], character[1]);
            }
        }

        /// <summary>
        /// シナリオの進行度を初期化するメソッド
        /// </summary>
        public static void InitDegressOfProgress()
        {
            _degreeOfProgress = _selectSectionID * 2;
        }

        /// <summary>
        /// シナリオの進行度を設定するメソッド
        /// </summary>
        /// <param name="value">設定する進行度</param>
        public static void SetSectionID(int value)
        {
            _selectSectionID = value;
            InitDegressOfProgress();
        }

        /// <summary>
        /// シナリオの進行度を１進めるメソッド
        /// </summary>
        public static void UpdateDegressOfProgress()
        {
            _degreeOfProgress++;
        }
    }
}
