using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
    public void BackMenu() // ������� � ����
    {
        SceneManager.LoadScene(0);
    }
}
