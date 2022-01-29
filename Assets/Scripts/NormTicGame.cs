using UnityEngine;
using System.Collections.Generic;

public class NormTicGame : MonoBehaviour
{
    public Game GameScript;
    public CheckGame CheckScript;

    private string type = "tac";
    private Sprite _player;
    private Sprite _playerClosing;
    private Sprite _opponent;

    public void PlayGame()
    {
        AssigningSprites(type);

        // ������ ������� ��� ���������� ������ ���������
        //
        if (CheckScript.StatusGame() == 0)
        {
            int winning = CheckScript.AttackStrategy(type);           // ���������� ��� �������� ���������� ����������
            int protection = CheckScript.ProtectionStrategyNormal(type); // ������� �������� � ���������


            // ���������� ���
            if (winning != -1)
            {
                if (GameScript.Cell[winning].sprite == null)
                {
                    GameScript.Cell[winning].sprite = _player;
                }
                else
                {
                    Game.overlapsTac--;
                    GameScript.Cell[winning].sprite = _playerClosing;
                }
            }

            // ������
            else if (protection != -1)
            {
                if (GameScript.Cell[protection].sprite == null)
                {
                    GameScript.Cell[protection].sprite = _player;
                }
                else
                {
                    GameScript.Cell[protection].sprite = _playerClosing;
                    Game.overlapsTac--;
                }
            }

            // �������� ����������� ������� ���������
            else if (CheckScript.LosingPosition(type) != -1)
            {
                GameScript.Cell[CheckScript.LosingPosition(type)].sprite = _playerClosing;
                Game.overlapsTac--;
            }

            // ���� �������� ������ ��� � ����� ����, �� ����������� ���
            else if (GameScript.Cell[4].sprite == _opponent && Game.overlapsTac > 0)
            {
                GameScript.Cell[4].sprite = _playerClosing;
                Game.overlapsTac--;
            }

            // ��������� ���
            else
            {
                int randomCell = Random.Range(0, 8);
                bool areThereOpponentCells = false; // ���� �� ������ ����������
                bool isThereNonLosingCell = false; // ���� �� ���� �� ���� �� ����������� �������
                int numberOfEmptyCells = 0; // ���-�� ������ �����

                List<int> losingPositions = new List<int>(); // ����������� �������
                CheckScript.ProtectionFromLosingPosition(type, ref losingPositions, GameScript.Cell);

                for (int i = 0; i < 9; i++) // ������ ����
                {
                    if (GameScript.Cell[i].sprite == null)
                    {
                        numberOfEmptyCells++;
                        
                        if (IsThisLosingPosition(i) == false)
                            isThereNonLosingCell = true;
                    }
                    if (GameScript.Cell[i].sprite == _opponent && Game.overlapsTac > 0)
                        areThereOpponentCells = true;
                }

                if (numberOfEmptyCells > 0 && isThereNonLosingCell == true) // ���� ���� ������ � �� ����������� �������
                {
                    while (GameScript.Cell[randomCell].sprite != null || IsThisLosingPosition(randomCell) == true)
                    {
                        randomCell = Random.Range(0, 8);
                    }
                    GameScript.Cell[randomCell].sprite = _player;
                }

                else if (areThereOpponentCells == true) // ���� �� ��� ������ � �� ����������� �������, �� ����������� ����������
                {
                    while (GameScript.Cell[randomCell].sprite != _opponent)
                        randomCell = Random.Range(0, 8);
                    GameScript.Cell[randomCell].sprite = _playerClosing;
                    Game.overlapsTac--;
                }

                else if (numberOfEmptyCells > 0) // ���� �� �� �������� ������ �����
                {
                    while (GameScript.Cell[randomCell].sprite != null)
                        randomCell = Random.Range(0, 8);
                    GameScript.Cell[randomCell].sprite = _player;
                }

                else Debug.Log("������� ����");


                // �������� �� ������� �����������
                bool IsThisLosingPosition(int position)
                {
                    bool isThisLosingPosition = false;

                    if(losingPositions.Count > 0)
                    {
                        foreach (int number in losingPositions)
                        {
                            if (position == number || position == 4)
                                isThisLosingPosition = true;
                        }
                    }
                    else
                    {
                        if(position == 4)
                            isThisLosingPosition = true;
                    }

                    return isThisLosingPosition;
                }
            }
        }
    }

    // ������� ���������� ��������
    //
    private void AssigningSprites(string type)
    {
        if (type == "tic")
        {
            _player = GameScript.Tic;
            _playerClosing = GameScript.ClosingTic;
            _opponent = GameScript.Tac;
        }
        else if (type == "tac")
        {
            _player = GameScript.Tac;
            _playerClosing = GameScript.ClosingTac;
            _opponent = GameScript.Tic;
        }
        else
        {
            Debug.LogWarning("������� ��������� ������� �������� ������� 'AssigningSprites' � ����� 'NormTicGame.cs'.");
        }
    }
}
