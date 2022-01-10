using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vampire
{
    public class SliderIndicator : MonoBehaviour
    {
        Slider _slider;
        [SerializeField] Text debugText;

        void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void SetParameter(float maxValue,float currentValue)
        {
            _slider.maxValue = maxValue;
            _slider.value = currentValue;
        }

        public void UpdateParameter(float value)
        {
            _slider.value = value;
        }
    }
}
