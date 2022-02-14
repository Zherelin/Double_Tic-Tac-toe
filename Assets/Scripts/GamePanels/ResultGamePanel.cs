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
            ResultGame.text = "¬€ œŒ¡≈ƒ»À»!";
        }
        else if (CheckScript.StatusGame() == GameState.VictoryOpponent)
        {
            ResultGame.text = "¬€ œ–Œ»√–¿À»!";
        }
        else if (CheckScript.StatusGame() == GameState.Draw)
        {
            ResultGame.text = "Õ»◊‹ﬂ!";
        }
        else
        {
            ResultGame.text = "’–≈Õ «Õ¿≈“!";
        }
    }

    public void OnClickButton()
    {
        gameObject.SetActive(false);
    }
}
