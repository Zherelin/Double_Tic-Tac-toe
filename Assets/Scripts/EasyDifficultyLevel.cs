using UnityEngine;

public class EasyDifficultyLevel : MonoBehaviour
{
    [SerializeField] private Game GameScript;
    [SerializeField] CheckGame CheckScript;

    private Sprite _player;
    private Sprite _playerClosing;
    private Sprite _opponent;
    private int _playerOverlaps;

    public void PlayGame(string typePlayer)
    {
        // ������ ���� ��� ����� ������ ���������
        //
        if(typePlayer == "tic" || typePlayer == "tac")
        {
            AssigningSprites(typePlayer); // ���������� ��������

            if (CheckScript.StatusGame() == 0) // �������� �� ��, ��� ���� ������������
            {
                int winning = CheckScript.AttackStrategy(typePlayer);            // ��������� ����������� ����
                int protection = CheckScript.ProtectionStrategyEazy(typePlayer); // ��������� ������

                // ���������� ���
                if (winning != -1)
                {
                    if (GameScript.Cell[winning].sprite == null)
                    {
                        GameScript.Cell[winning].sprite = _player;
                    }
                    else
                    {
                        GameScript.Cell[winning].sprite = _playerClosing;
                        OverlapDeduction();
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
                        OverlapDeduction();
                    }
                }

                // ��������� ���
                else
                {
                    int randomCell = Random.Range(0, 9);
                    bool isThereAnEmptyCell = false; // ���� �� ������ ������
                    bool isThereAnOpponentCell = false; // ���� �� ������ ����������

                    for (int number = 0; number < 9; number++)
                    {
                        if (GameScript.Cell[number].sprite == null)
                            isThereAnEmptyCell = true;
                        if (GameScript.Cell[number].sprite == _opponent && _playerOverlaps > 0)
                            isThereAnOpponentCell = true;
                    }

                    if (isThereAnEmptyCell == true)
                    {
                        while (GameScript.Cell[randomCell].sprite != null)
                        {
                            randomCell = Random.Range(0, 9);
                        }
                         
                        GameScript.Cell[randomCell].sprite = _player;
                    }
                    else if (isThereAnOpponentCell == true)
                    {
                        while (GameScript.Cell[randomCell].sprite != _opponent)
                        {
                            randomCell = Random.Range(0, 9);
                        }

                        GameScript.Cell[randomCell].sprite = _playerClosing;
                        OverlapDeduction();
                    }
                    else 
                        GameScript.MessagePanel.text = "������� ����";
                }
            }

            // ����� ����������
            void OverlapDeduction()
            {
                if (typePlayer == "tic")
                    Game.overlapsTic--;
                else if (typePlayer == "tac")
                    Game.overlapsTac--;
            }  
        } 
        else
        {
            Debug.LogWarning("������� ��������� ������� �������� ������� 'PlayGame' ����� 'EasyDifficultyLevel.cs'.");
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
            _playerOverlaps = Game.overlapsTic;
        }
        else if (type == "tac")
        {
            _player = GameScript.Tac;
            _playerClosing = GameScript.ClosingTac;
            _opponent = GameScript.Tic;
            _playerOverlaps = Game.overlapsTac;
        }
    }
}
