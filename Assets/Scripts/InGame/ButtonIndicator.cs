using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicator : MonoBehaviour
{
    Image _image;
    Button _button;

    void Start()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.interactable = false;
    }

    public void SetActive(bool flg)
    {
        //Color color = _image.color;
        //color.a = flg ? 1 : 0.6f;
        //_image.color = color;
        _button.interactable = flg;
    }
}
