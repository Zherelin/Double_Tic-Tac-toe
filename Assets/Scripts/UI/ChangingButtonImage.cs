using UnityEngine;
using UnityEngine.UI;

public class ChangingButtonImage : MonoBehaviour
{
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private Image buttonImage;
    private bool change = false;

    public void ChangingImage()
    {
        if(change == false)
        {
            buttonImage.sprite = sprite2;
        }
        else
        {
            buttonImage.sprite = sprite1;
        }

        change = !change;
    }
    
}
