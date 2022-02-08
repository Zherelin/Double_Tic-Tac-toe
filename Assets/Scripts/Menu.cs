using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static string level = ""; // Уровень игры
    public static string type = ""; // Тип игры

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
