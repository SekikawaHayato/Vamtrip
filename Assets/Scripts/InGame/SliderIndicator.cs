using UnityEngine;
using UnityEngine.UI;

namespace Vampire
{
    public class SliderIndicator : MonoBehaviour
    {
        Slider _slider;
        [SerializeField] Text debugText;

        /// <summary>
        /// 変数の初期化
        /// </summary>
        void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        /// <summary>
        /// パラメータの初期値を設定するメソッド
        /// </summary>
        /// <param name="maxValue">パラメータの最大値</param>
        /// <param name="currentValue">パラメータの現在値</param>
        public void SetParameter(float maxValue,float currentValue)
        {
            _slider.maxValue = maxValue;
            _slider.value = currentValue;
        }

        /// <summary>
        /// パラメータを更新するメソッド
        /// </summary>
        /// <param name="value"></param>
        public void UpdateParameter(float value)
        {
            _slider.value = value;
        }
    }
}
