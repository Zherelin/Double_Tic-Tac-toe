using UnityEngine;

public class EazyTicGame : MonoBehaviour
{
    public Game gameScript;       // ����������� ��������
    public CheckGame checkScript; //
   
    public void PlayGame()
    {
        // ������ ������� ��� ����� ������ ���������
        //
        if(checkScript.StatusGame() == 0) // �������� �� ��, ��� ���� ������������
        {
            int winning = checkScript.AttackStrategy("tac");       // ���������� ��� �������� ���������� ����������
            int protection = checkScript.ProtectionStrategyEazy("tac"); // ������� �������� � ���������


            // ��� � ����� ����, ���� ������ �����
            if (gameScript.Cell[4].sprite == null)
                gameScript.Cell[4].sprite = gameScript.Tac;

            // ���������� ��� Tac
            else if (winning != -1)
            {
                if (gameScript.Cell[winning].sprite == null)
                {
                    gameScript.Cell[winning].sprite = gameScript.Tac;
                }
                else
                {
                    Game.overlapsTac--;
                    gameScript.Cell[winning].sprite = gameScript.ClosingTac;
                }
            }
            // ������ Tac
            else if (protection != -1)
            {
                if (gameScript.Cell[protection].sprite == null)
                {
                    gameScript.Cell[protection].sprite = gameScript.Tac;
                }
                else
                {
                    Game.overlapsTac--;
                    gameScript.Cell[protection].sprite = gameScript.ClosingTac;
                }
            }
            // ��������� ��� Tac
            else
            {
                int randomCell = Random.Range(0, 8);
                bool cellNull = false;
                bool cellTic = false;

                for (int i = 0; i < 9; i++)
                {
                    if (gameScript.Cell[i].sprite == null)
                        cellNull = true;
                    if (gameScript.Cell[i].sprite == gameScript.Tic && Game.overlapsTac > 0)
                        cellTic = true;
                }

                if (cellNull == true)
                {
                    while (gameScript.Cell[randomCell].sprite != null)
                        randomCell = Random.Range(0, 8);
                    gameScript.Cell[randomCell].sprite = gameScript.Tac;
                }
                else if (cellTic == true)
                {
                    while (gameScript.Cell[randomCell].sprite != gameScript.Tic)
                        randomCell = Random.Range(0, 8);
                    gameScript.Cell[randomCell].sprite = gameScript.ClosingTac;
                    Game.overlapsTac--;
                }
                else Debug.Log("������� ����");
            }
        }
    }
}
