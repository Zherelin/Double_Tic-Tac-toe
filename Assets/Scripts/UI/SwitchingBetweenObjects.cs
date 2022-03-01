using UnityEngine;

public class SwitchingBetweenObjects : MonoBehaviour
{
    [SerializeField] private GameObject gameObject1;
    [SerializeField] private GameObject gameObject2;
    private bool switching = false;

    public void Switching()
    {
        if(switching == false)
        {
            gameObject1.SetActive(false);
            gameObject2.SetActive(true);
        }
        else
        {
            gameObject2.SetActive(false);
            gameObject1.SetActive(true);
        }

        switching = !switching;
    }
}
