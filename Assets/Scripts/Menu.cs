using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static int level = 0; // ������� ����
    public static int type = 0; // ��� ���� (�������� ��� ������)

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
        type = 1;
    }
    public void ClickTypeTac()
    {
        type = 2;
    }
    public void ClickLevelEazy()
    {
        level = 1;
    }
    public void ClickLevelNormal()
    {
        level = 2;
    }
    public void ClickLevelHard()
    {
        level = 3;
    }
}
