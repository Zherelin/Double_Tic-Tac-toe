using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EnumGame;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Game GameScript; // Подключение скрипта игры
    [SerializeField] private CheckGame CheckScript;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CheckScript.StatusGame() == GameState.Continues && GameScript.move == true)
        {
            if (Menu.type == "tic")
            {
                //Cell = Null
                if (gameObject.GetComponent<Image>().sprite == null)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.Tic;
                    GameScript.move = false;
                }
                //Cell = Tac
                else if (gameObject.GetComponent<Image>().sprite == GameScript.Tac && GameScript.overlapsPlayer > 0)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.ClosingTic;
                    GameScript.overlapsPlayer--;
                    GameScript.move = false;
                }
                //Exception
                else
                    StartCoroutine(GameScript.ShowMessage("Невозможно добавить значение в ячейку!"));
            }
            else if (Menu.type == "tac")
            {
                //Cell = Null
                if (gameObject.GetComponent<Image>().sprite == null)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.Tac;
                    GameScript.move = false;
                }
                //Cell = Tic
                else if (gameObject.GetComponent<Image>().sprite == GameScript.Tic && GameScript.overlapsOpponent > 0)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.ClosingTac;
                    GameScript.overlapsOpponent--;
                    GameScript.move = false;
                }
                //Exception
                else
                    StartCoroutine(GameScript.ShowMessage("Невозможно добавить значение в ячейку!"));
            }
            else
                StartCoroutine(GameScript.ShowMessage("Тип игрока неопределён!"));
        }
    }
}
