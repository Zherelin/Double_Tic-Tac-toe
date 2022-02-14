using UnityEngine;
using UnityEngine.UI;
using EnumGame;

public class ResultGamePanel : MonoBehaviour
{
    [SerializeField] private CheckGame CheckScript;
    [SerializeField] private Text ResultGame;

    public void ShowResultGamePanel()
    {
        gameObject.SetActive(true);

        if(CheckScript.StatusGame() == GameState.VictoryPlayer)
        {
            ResultGame.text = "�� ��������!";
        }
        else if (CheckScript.StatusGame() == GameState.VictoryOpponent)
        {
            ResultGame.text = "�� ���������!";
        }
        else if (CheckScript.StatusGame() == GameState.Draw)
        {
            ResultGame.text = "�����!";
        }
        else
        {
            ResultGame.text = "���� �����!";
        }
    }

    public void OnClickButton()
    {
        gameObject.SetActive(false);
    }
}
