using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
    public void BackMenu() // Переход в меню
    {
        SceneManager.LoadScene(0);
    }
}
