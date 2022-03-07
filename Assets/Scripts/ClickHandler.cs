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
                // ������ ������
                if (gameObject.GetComponent<Image>().sprite == null)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.Tic;
                    GameScript.move = false;
                }

                // ������ ����������
                else if (gameObject.GetComponent<Image>().sprite == GameScript.Tac && GameScript.OverlapsPlayer > 0)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.ClosingTic;
                    GameScript.OverlapsPlayer--;
                    GameScript.move = false;
                }

                else
                    StartCoroutine(GameScript.ShowMessage("���������� �������� �������� � ������!"));
            }
            else if (Menu.type == "tac")
            {
                // ������ ������
                if (gameObject.GetComponent<Image>().sprite == null)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.Tac;
                    GameScript.move = false;
                }

                // ������ ����������
                else if (gameObject.GetComponent<Image>().sprite == GameScript.Tic && GameScript.OverlapsPlayer > 0)
                {
                    gameObject.GetComponent<Image>().sprite = GameScript.ClosingTac;
                    GameScript.OverlapsPlayer--;
                    GameScript.move = false;
                }

                else
                    StartCoroutine(GameScript.ShowMessage("���������� �������� �������� � ������!"));
            }
            else
                StartCoroutine(GameScript.ShowMessage("��� ������ ����������!"));
        }
    }
}
