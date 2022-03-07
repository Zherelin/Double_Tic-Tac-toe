using UnityEngine;

public class SwitchingBetweenObjects : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject1;
    [SerializeField] private GameObject _gameObject2;
    private bool _toggle = false;

    public void Switching()
    {
        if(_toggle == false)
        {
            _gameObject1.SetActive(false);
            _gameObject2.SetActive(true);
        }
        else
        {
            _gameObject2.SetActive(false);
            _gameObject1.SetActive(true);
        }

        _toggle = !_toggle;
    }
}
