using UnityEngine;
using UnityEngine.UI;

public class SwitchingBetweenImage : MonoBehaviour
{
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Image _buttonImage;
    private bool _toggle = false;

    public void ImageSwitch()
    {
        if(_toggle == false)
        {
            _buttonImage.sprite = _sprite2;
            _toggle = !_toggle;
        }
        else if(_toggle == true)
        {
            _buttonImage.sprite = _sprite1;
            _toggle = !_toggle;
        }
        else
        {
            Debug.LogWarning("Статус тумблера неопределён 'ImageSwitch()'");
        }
    }

    public void ImageSwitch(bool toggle)
    {
        if (toggle == true)
        {
            _buttonImage.sprite = _sprite1;
        }
        else if(toggle == false)
        {
            _buttonImage.sprite = _sprite2;
        }
        else
        {
            Debug.LogWarning("Статус тумблера неопределён 'ImageSwitch(bool)'");
        }
    }
}
