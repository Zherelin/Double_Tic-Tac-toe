using UnityEngine;

public class NormTacGame : MonoBehaviour
{
    public Game gameScript;
    public CheckGame checkScript;

    public void PlayGame()
    {
        // ������ ��������� ��� ���������� ������ ���������
        //
        if (checkScript.StatusGame() == 0)
        {
            int winning = checkScript.AttackStrategy("tic");           // ���������� ��� �������� ���������� ����������
            int protection = checkScript.ProtectionStrategyNormal("tic"); // ������� �������� � ���������


            // ���������� ��� Tic
            if (winning != -1)
            {
                if (gameScript.Cell[winning].sprite == null)
                {
                    gameScript.Cell[winning].sprite = gameScript.Tic;
                }
                else
                {
                    Game.overlapsTic--;
                    gameScript.Cell[winning].sprite = gameScript.ClosingTic;
                }
            }

            // ������ Tic
            else if (protection != -1)
            {
                if (gameScript.Cell[protection].sprite == null)
                {
                    gameScript.Cell[protection].sprite = gameScript.Tic;
                }
                else
                {
                    gameScript.Cell[protection].sprite = gameScript.ClosingTic;
                    Game.overlapsTic--;
                }
            }

            // �������� ����������� ������� ���������
            else if (checkScript.LosingPosition("tic") != -1)
            {
                gameScript.Cell[checkScript.LosingPosition("tic")].sprite = gameScript.ClosingTic;
                Game.overlapsTic--;
            }

            // ���� �������� ������ ��� � ����� ����, �� ����������� ���
            else if (gameScript.Cell[4].sprite == gameScript.Tac && Game.overlapsTic > 0)
            {
                gameScript.Cell[4].sprite = gameScript.ClosingTic;
                Game.overlapsTic--;
            }

            // ��������� ��� Tic
            else
            {
                int randomCell = Random.Range(0, 8);
                bool cellNull = false;
                bool cellTac = false;
                int countNull = 0; // ���-�� ������ �����

                for (int i = 0; i < 9; i++)
                {
                    if (gameScript.Cell[i].sprite == null)
                    {
                        cellNull = true;
                        countNull++;
                    }
                    if (gameScript.Cell[i].sprite == gameScript.Tac && Game.overlapsTic > 0)
                        cellTac = true;
                }

                if (cellNull == true && countNull > 1)
                {
                    while (gameScript.Cell[randomCell].sprite != null || randomCell == 4)
                        randomCell = Random.Range(0, 8);
                    gameScript.Cell[randomCell].sprite = gameScript.Tic;
                }
                else if (cellTac == true)
                {
                    while (gameScript.Cell[randomCell].sprite != gameScript.Tac)
                        randomCell = Random.Range(0, 8);
                    gameScript.Cell[randomCell].sprite = gameScript.ClosingTic;
                    Game.overlapsTic--;
                }
                else if (cellNull == true && countNull == 1)
                {
                    while (gameScript.Cell[randomCell].sprite != null)
                        randomCell = Random.Range(0, 8);
                    gameScript.Cell[randomCell].sprite = gameScript.Tic;
                }
                else Debug.Log("������� ����");
            }
        }
    }
}
