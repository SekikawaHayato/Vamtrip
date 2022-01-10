using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Vampire.Select
{
    public class Section : MonoBehaviour
    {
        [SerializeField] Transform _parent;
        [SerializeField] GameObject _sectionPrefab;

        void Awake()
        {
            GenerateSection();
        }

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
                obj.GetComponent<SectionText>().SetText(i,"初めての日本 第"+(i+1)+"夜");
                if (i <= solvedSection) obj.GetComponent<Button>().interactable = true;
            }
        }

        void SelectSection()
        {

        }
    }
}
