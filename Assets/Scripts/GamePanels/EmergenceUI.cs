using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmergenceUI : MonoBehaviour
{
    [SerializeField] private float _alpha = 0f;
    [SerializeField] private float _alphaFinal = 0.6f;
    [SerializeField] private float _timeUpdateAlpha = 0.01f;
    [SerializeField] private float _stepForAlpha = 0.02f;

    public IEnumerator EmergenceImage(Image image)
    {
        _alpha = 0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, _alpha);

        while(image.color.a < _alphaFinal)
        {
            _alpha += _stepForAlpha;
            image.color = new Color(image.color.r, image.color.g, image.color.b, _alpha);
            yield return new WaitForSeconds(_timeUpdateAlpha);
        }
        yield return null;
    }
}
