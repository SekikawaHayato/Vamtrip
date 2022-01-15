using UnityEngine;
using UnityEngine.UI;

namespace Vampire.Select
{
    public class Section : MonoBehaviour
    {
        [SerializeField] Transform _parent;
        [SerializeField] GameObject _sectionPrefab;
        const int _deliveredNumber = 0;

        /// <summary>
        /// 節を表示するメソッドを呼び出す
        /// </summary>
        void Awake()
        {
            GenerateSection();
        }

        /// <summary>
        /// 進行度に合わせて節を表示するメソッド
        /// </summary>
        void GenerateSection()
        {
            foreach (Transform child in _parent)
            {
                Destroy(child.gameObject);
            }
            int solvedSection = SaveDataManager.Instance.SaveDataInstance.solvedSection;
            for (int i = 0; i <= solvedSection+1; i++)
            {
                GameObject obj = Instantiate(_sectionPrefab, _parent);
                if (i <= _deliveredNumber)
                {
                    obj.GetComponent<SectionText>().SetText(i, "初めての日本 第" + (i + 1) + "夜");
                }
                else
                {
                    obj.GetComponent<SectionText>().SetText(i, "Comming Soon...");
                    return;
                }
                if (i <= solvedSection) obj.GetComponent<Button>().interactable = true;
            }
        }
    }
}
