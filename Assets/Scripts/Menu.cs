using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static int level = 0; // Уровень игры
    public static int type = 0; // Тип игры (крестики или нолики)

    public void ExitGame() // Выход из игры
    {
        Application.Quit();
    }

    public void StartGame() // Переход к игры
    {
        SceneManager.LoadScene(1);
    }

    // Выбор уровня и типа игры
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
