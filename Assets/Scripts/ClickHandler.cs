using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Game GameScript; // Подключение скрипта игры
    [SerializeField] private CheckGame CheckScript;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CheckScript.StatusGame() == 0 && GameScript.move == true)
        {
            //Tic
            if (Menu.type == 1)
            {
                //Cell = Null
                if (gameObject.GetComponent<Image>().sprite == null)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.Tic;
                    GameScript.move = false;
                }
                //Cell = Tac
                else if (gameObject.GetComponent<Image>().sprite == GameScript.Tac && Game.overlapsTic > 0)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.ClosingTic;
                    Game.overlapsTic--;
                    GameScript.move = false;
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
                    gameObject.GetComponent<Image>().sprite = GameScript.Tac;
                    GameScript.move = false;
                }
                //Cell = Tic
                else if (gameObject.GetComponent<Image>().sprite == GameScript.Tic && Game.overlapsTac > 0)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.ClosingTac;
                    Game.overlapsTac--;
                    GameScript.move = false;
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
