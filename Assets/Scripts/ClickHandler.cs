using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EnumGame;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Game GameScript;
    [SerializeField] private CheckGame CheckScript;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CheckScript.StatusGame() == GameState.Continues && GameScript.move == true)
        {
            if (Menu.type == "tic")
            {
                // Клетка пустая
                if (gameObject.GetComponent<Image>().sprite == null)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.Tic;
                    GameScript.move = false;
                }

                // Клетка противника
                else if (gameObject.GetComponent<Image>().sprite == GameScript.Tac && GameScript.OverlapsPlayer > 0)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.ClosingTic;
                    GameScript.OverlapsPlayer--;
                    GameScript.move = false;
                }

                else
                    StartCoroutine(GameScript.ShowMessage("Невозможно добавить значение в ячейку!"));
            }
            else if (Menu.type == "tac")
            {
                // Клетка пустая
                if (gameObject.GetComponent<Image>().sprite == null)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.Tac;
                    GameScript.move = false;
                }

                // Клетка противника
                else if (gameObject.GetComponent<Image>().sprite == GameScript.Tic && GameScript.OverlapsPlayer > 0)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.ClosingTac;
                    GameScript.OverlapsPlayer--;
                    GameScript.move = false;
                }

                else
                    StartCoroutine(GameScript.ShowMessage("Невозможно добавить значение в ячейку!"));
            }
            else
                StartCoroutine(GameScript.ShowMessage("Тип игрока неопределён!"));
        }
    }
}
