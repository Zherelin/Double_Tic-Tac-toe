using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static string level = ""; // ������� ����
    public static string type = ""; // ��� ����

    public void ExitGame() // ����� �� ����
    {
        Application.Quit();
    }

    public void StartGame() // ������� � ����
    {
        SceneManager.LoadScene(1);
    }

    // ����� ������ � ���� ����
    public void ClickTypeTic()
    {
        type = "tic";
    }
    public void ClickTypeTac()
    {
        type = "tac";
    }
    public void ClickLevelEasy()
    {
        level = "easy";
    }
    public void ClickLevelNormal()
    {
        level = "normal";
    }
    public void ClickLevelChalleng()
    {
        level = "challeng";
    }
}
