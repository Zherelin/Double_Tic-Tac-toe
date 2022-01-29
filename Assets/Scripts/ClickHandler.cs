using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public Game gameScript; // Подключение скрипта игры

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameScript.eazyTic.checkScript.StatusGame() == 0 && gameScript.move == true)
        {
            //Tic
            if (Menu.type == 1)
            {
                //Cell = Null
                if (gameObject.GetComponent<Image>().sprite == null)
                {
                    gameObject.GetComponent<Image>().sprite = gameScript.Tic;
                    gameScript.move = false;
                }
                //Cell = Tac
                else if (gameObject.GetComponent<Image>().sprite == gameScript.Tac && Game.overlapsTic > 0)
                {
                    gameObject.GetComponent<Image>().sprite = gameScript.ClosingTic;
                    Game.overlapsTic--;
                    gameScript.move = false;
                }
                //Exception
                else
                    Debug.Log("Невозможно добавить значение в ячейку!");
            }
            //Tac
            else if (Menu.type == 2)
            {
                //Cell = Null
                if (gameObject.GetComponent<Image>().sprite == null)
                {
                    gameObject.GetComponent<Image>().sprite = gameScript.Tac;
                    gameScript.move = false;
                }
                //Cell = Tic
                else if (gameObject.GetComponent<Image>().sprite == gameScript.Tic && Game.overlapsTac > 0)
                {
                    gameObject.GetComponent<Image>().sprite = gameScript.ClosingTac;
                    Game.overlapsTac--;
                    gameScript.move = false;
                }
                //Exception
                else
                    Debug.Log("Невозможно добавить значение в ячейку!");
            }
            else
                Debug.LogWarning("Тип игрока неопределён!");
        }
    }
}
