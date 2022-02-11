using UnityEngine;
using UnityEngine.UI;

public class GameInformationPanel : MonoBehaviour
{
    [SerializeField] private Text LevelGame;
    [SerializeField] private Text TypeGame;

    public void ConnectionGameInformationPanel(string level, string type)
    {
        // Level
        if (level == "easy")
        {
            LevelGame.text = "Легкий";
        }
        else if (level == "normal")
        {
            LevelGame.text = "Нормальный";
        }
        else if (level == "challeng")
        {
            LevelGame.text = "Сложный";
        }
        else
            LevelGame.text = "?";

        // Type
        if (type == "tic")
        {
            TypeGame.text = "X";
        }
        else if (type == "tac")
        {
            TypeGame.text = "O";
        }
        else
        {
            TypeGame.text = "?";
        }
    }
}
