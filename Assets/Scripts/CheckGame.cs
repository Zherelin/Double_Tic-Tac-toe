using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckGame : MonoBehaviour
{
    public Game GameScript;

    private Sprite _player;
    private Sprite _playerClosing;
    private Sprite _opponent;
    private Sprite _opponentClosing;
    private int _playerOverlaps;
    private int _opponentOverlaps;

    // ������� ���������� ��������
    //
    private void AssigningSprites(string type)
    {
        if(type == "tic")
        {
            _player = GameScript.Tic;
            _playerClosing = GameScript.ClosingTic;
            _opponent = GameScript.Tac;
            _opponentClosing = GameScript.ClosingTac;
            _playerOverlaps = Game.overlapsTic;
            _opponentOverlaps = Game.overlapsTac;
        }
        else if(type == "tac")
        {
            _player = GameScript.Tac;
            _playerClosing = GameScript.ClosingTac;
            _opponent = GameScript.Tic;
            _opponentClosing = GameScript.ClosingTic;
            _playerOverlaps = Game.overlapsTac;
            _opponentOverlaps = Game.overlapsTic;
        }
        else
        {
            Debug.LogWarning("������� ��������� ������� �������� ������� 'AssigningSprites' � ����� 'CheckGame.cs'.");
        }
    }

    //
    // ������� ������� ����
    //

    public int StatusGame() // �������� ������� ����
    {
        // �������� ������ TIC
        if (CheckingTheFieldForVictory(0, 1, 2, "tic"))
            return 1;
        else if (CheckingTheFieldForVictory(3, 4, 5, "tic"))
            return 1;
        else if (CheckingTheFieldForVictory(6, 7, 8, "tic"))
            return 1;

        else if (CheckingTheFieldForVictory(0, 3, 6, "tic"))
            return 1;
        else if (CheckingTheFieldForVictory(1, 4, 7, "tic"))
            return 1;
        else if (CheckingTheFieldForVictory(2, 5, 8, "tic"))
            return 1;

        else if (CheckingTheFieldForVictory(0, 4, 8, "tic"))
            return 1;
        else if (CheckingTheFieldForVictory(2, 4, 6, "tic"))
            return 1;

        //�������� ������ TAC
        else if (CheckingTheFieldForVictory(0, 1, 2, "tac"))
            return 2;
        else if (CheckingTheFieldForVictory(3, 4, 5, "tac"))
            return 2;
        else if (CheckingTheFieldForVictory(6, 7, 8, "tac"))
            return 2;

        else if (CheckingTheFieldForVictory(0, 3, 6, "tac"))
            return 2;
        else if (CheckingTheFieldForVictory(1, 4, 7, "tac"))
            return 2;
        else if (CheckingTheFieldForVictory(2, 5, 8, "tac"))
            return 2;

        else if (CheckingTheFieldForVictory(0, 4, 8, "tac"))
            return 2;
        else if (CheckingTheFieldForVictory(2, 4, 6, "tac"))
            return 2;

        // �������� �� ����� !!! �������� �������� �� ��������� ���� !!
        else if (Game.overlapsTic < 1 && Game.overlapsTac < 1 && IsFieldEmpty() == false)
            return 3;

        // ����������� ����
        else return 0;

        // �������� ���� �� ������
        bool CheckingTheFieldForVictory(int number1, int number2, int number3, string type)
        {
            AssigningSprites(type);

            // �������� 3 ����� �� ������� ������ ������
            if ((GameScript.Cell[number1].sprite == _player || GameScript.Cell[number1].sprite == _playerClosing)
                && (GameScript.Cell[number2].sprite == _player || GameScript.Cell[number2].sprite == _playerClosing)
                && (GameScript.Cell[number3].sprite == _player || GameScript.Cell[number3].sprite == _playerClosing))
                return true;
            else
                return false;
        }

        // �������� �� ���� ������
        bool IsFieldEmpty()
        {
            bool flag = false;
            for (int i = 0; i < 9; i++)
                if (GameScript.Cell[i].sprite == null)
                    flag = true;

            return flag;
        }
    }

    //
    // ������� ��������
    //

    int FindingPositionToAttack(int number1, int number2, int number3, string type) // ����� ������� ��� �����
    {
        AssigningSprites(type);

        // �������� ����� �� ���������� ��� ������
        if ((GameScript.Cell[number1].sprite == _player || GameScript.Cell[number1].sprite == _playerClosing)
                && (GameScript.Cell[number2].sprite == _player || GameScript.Cell[number2].sprite == _playerClosing)
                && (GameScript.Cell[number3].sprite == null || (GameScript.Cell[number3].sprite == _opponent && _playerOverlaps > 0)))
            return number3;

        else if ((GameScript.Cell[number1].sprite == _player || GameScript.Cell[number1].sprite == _playerClosing)
                && (GameScript.Cell[number3].sprite == _player || GameScript.Cell[number3].sprite == _playerClosing)
                && (GameScript.Cell[number2].sprite == null || (GameScript.Cell[number2].sprite == _opponent && _playerOverlaps > 0)))
            return number2;

        else if ((GameScript.Cell[number2].sprite == _player || GameScript.Cell[number2].sprite == _playerClosing)
                && (GameScript.Cell[number3].sprite == _player || GameScript.Cell[number3].sprite == _playerClosing)
                && (GameScript.Cell[number1].sprite == null || (GameScript.Cell[number1].sprite == _opponent && _playerOverlaps > 0)))
            return number1;

        else
            return -1; // ������� �� �������
    }

    public int AttackStrategy(string type) // ��������� �����
    {
        if(type == "tic" || type == "tac")
        {
            if (FindingPositionToAttack(0, 1, 2, type) != -1)
                return FindingPositionToAttack(0, 1, 2, type);
            else if (FindingPositionToAttack(3, 4, 5, type) != -1)
                return FindingPositionToAttack(3, 4, 5, type);
            else if (FindingPositionToAttack(6, 7, 8, type) != -1)
                return FindingPositionToAttack(6, 7, 8, type);

            else if (FindingPositionToAttack(0, 3, 6, type) != -1)
                return FindingPositionToAttack(0, 3, 6, type);
            else if (FindingPositionToAttack(1, 4, 7, type) != -1)
                return FindingPositionToAttack(1, 4, 7, type);
            else if (FindingPositionToAttack(2, 5, 8, type) != -1)
                return FindingPositionToAttack(2, 5, 8, type);

            else if (FindingPositionToAttack(0, 4, 8, type) != -1)
                return FindingPositionToAttack(0, 4, 8, type);
            else if (FindingPositionToAttack(2, 4, 6, type) != -1)
                return FindingPositionToAttack(2, 4, 6, type);

            else
                return -1; // ������� �� �������
        }
        else
        {
            Debug.LogWarning("������� ��������� ������� �������� ������� 'AttackStrategy'.");
            return -1;
        }
    }

    //
    // ������� ������ 
    //

    void FindingPositionsToProtection(int number1, int number2, int number3, string type, ref List<int> protect) // ����� ������� ��� ������
    {
        AssigningSprites(type);

        // �������� ����� �� ��������� ������
        //
        if ((GameScript.Cell[number1].sprite == _opponent || GameScript.Cell[number1].sprite == _opponentClosing)
                && (GameScript.Cell[number2].sprite == _opponent || GameScript.Cell[number2].sprite == _opponentClosing)
                && (GameScript.Cell[number3].sprite == null || (GameScript.Cell[number3].sprite == _player && _opponentOverlaps > 0)))
        {
            if(_playerOverlaps > 0 && GameScript.Cell[number1].sprite == _opponent) 
            { 
                protect.Add(number1); 
            }
            if (_playerOverlaps > 0 && GameScript.Cell[number2].sprite == _opponent)
            {
                protect.Add(number2);
            }
            if (GameScript.Cell[number3].sprite == null)
            {
                protect.Add(number3);
            }
        }

        else if ((GameScript.Cell[number1].sprite == _opponent || GameScript.Cell[number1].sprite == _opponentClosing)
                && (GameScript.Cell[number3].sprite == _opponent || GameScript.Cell[number3].sprite == _opponentClosing)
                && (GameScript.Cell[number2].sprite == null || (GameScript.Cell[number2].sprite == _player && _opponentOverlaps > 0)))
        {
            if (_playerOverlaps > 0 && GameScript.Cell[number1].sprite == _opponent)
            {
                protect.Add(number1);
            }
            if (_playerOverlaps > 0 && GameScript.Cell[number3].sprite == _opponent)
            {
                protect.Add(number3);
            }
            if (GameScript.Cell[number2].sprite == null)
            {
                protect.Add(number2);
            }
        }

        else if ((GameScript.Cell[number2].sprite == _opponent || GameScript.Cell[number2].sprite == _opponentClosing)
                && (GameScript.Cell[number3].sprite == _opponent || GameScript.Cell[number3].sprite == _opponentClosing)
                && (GameScript.Cell[number1].sprite == null || (GameScript.Cell[number1].sprite == _player && _opponentOverlaps > 0)))
        {
            if (_playerOverlaps > 0 && GameScript.Cell[number2].sprite == _opponent)
            {
                protect.Add(number2);
            }
            if (_playerOverlaps > 0 && GameScript.Cell[number3].sprite == _opponent)
            {
                protect.Add(number3);
            }
            if (GameScript.Cell[number1].sprite == null)
            {
                protect.Add(number1);
            }
        }
    }

    void EnumerationOfProtectionPositions(string type, ref List<int> protect) // ������� ������� ������
    {
        FindingPositionsToProtection(0, 1, 2, type, ref protect);
        FindingPositionsToProtection(3, 4, 5, type, ref protect);
        FindingPositionsToProtection(6, 7, 8, type, ref protect);

        FindingPositionsToProtection(0, 3, 6, type, ref protect);
        FindingPositionsToProtection(1, 4, 7, type, ref protect);
        FindingPositionsToProtection(2, 5, 8, type, ref protect);

        FindingPositionsToProtection(0, 4, 8, type, ref protect);
        FindingPositionsToProtection(2, 4, 6, type, ref protect);
    }

    public int ProtectionStrategyEazy(string type) // ��������� ������ 'Eazy'
    {
        if (type == "tic" || type == "tac")
        {
            List<int> protect = new List<int>(); // ������ ������ ����� ������

            // ������ �� �������� ����, �������� �� ��������� ������
            EnumerationOfProtectionPositions(type, ref protect);

            // ����� ��������� ������ ��� ������
            if (protect.Count > 0)
            {
                return protect[Random.Range(0, protect.Count)];
            }
            else
                return -1;
        }
        else
        {
            Debug.LogWarning("������� ��������� ������� �������� ������� 'ProtectionStrategyEazy'.");
            return -1;
        }
    }

    public int ProtectionStrategyNormal(string type) //��������� ������ 'Normal'
    {
        if(type == "tic" || type == "tac")
        {
            List<int> protect = new List<int>(); // ������ ������ ����� ������

            EnumerationOfProtectionPositions(type, ref protect);

            // ����� ��������� ������ ��� ������
            if (protect.Count > 0)
            {
                // �������� ��������� ������ !!!

                bool isThereBestPosition = false; // ���� �� ��������� ������� ������
                int bestPosition = 0; // ��������� ������� ������

                for (int i = 0; i < protect.Count; i++)
                    if (GameScript.Cell[protect[i]].sprite == _opponent)
                    {
                        isThereBestPosition = true;
                        bestPosition = protect[i];
                    }

                if (isThereBestPosition)
                    return bestPosition;
                else
                    return protect[Random.Range(0, protect.Count)];
            }
            else
                return -1;
        }
        else
        {
            Debug.LogWarning("������� ��������� ������� �������� ������� 'ProtectionStrategyNormal'.");
            return -1;
        }
    }

    public int ProtectionStrategyHard(string type) //��������� ������ 'Hard'
    {
        if(type == "tic" || type == "tac")
        {
            List<int> protect = new List<int>(2); // ������ ������ ����� ������

            // ������ �� �������� ����, �������� �� ��������� ������
            EnumerationOfProtectionPositions(type, ref protect);

            // ����� ��������� ������ ��� ������
            if (protect.Count > 0)
            {
                // �������� ��������� ������ !!!

                bool isThereBestPosition = false; // ���� �� ��������� ������� ������
                int bestPosition = 0; // ��������� ������� ������

                for (int i = 0; i < protect.Count; i++)
                    if (GameScript.Cell[protect[i]].sprite == _opponent)
                    {
                        isThereBestPosition = true;
                        bestPosition = protect[i];
                    }

                if (isThereBestPosition)
                    return bestPosition;
                else
                    return protect[Random.Range(0, protect.Count)];
            }
            else
                return -1;
        }
        else
        {
            Debug.LogWarning("������� ��������� ������� �������� ������� 'ProtectionStrategyHard'.");
            return -1;
        }
    }

    //
    // ������� ����������� �������
    //

    private void FindingLosingPosition(int number1, int number2, int number3, string type, ref List<int> positions, ref Image[] cell) // ����� ����������� �������
    {
        AssigningSprites(type);

        if (cell[number1].sprite == _playerClosing && cell[number2].sprite == _opponent && cell[number3] != _opponentClosing)
            positions.Add(number2);
        else if (cell[number1].sprite == _playerClosing && cell[number3].sprite == _opponent && cell[number2] != _opponentClosing)
            positions.Add(number3);

        else if (cell[number2].sprite == _playerClosing && cell[number1].sprite == _opponent && cell[number3] != _opponentClosing)
            positions.Add(number1);
        else if (cell[number2].sprite == _playerClosing && cell[number3].sprite == _opponent && cell[number1] != _opponentClosing)
            positions.Add(number3);

        else if (cell[number3].sprite == _playerClosing && cell[number1].sprite == _opponent && cell[number2] != _opponentClosing)
            positions.Add(number1);
        else if (cell[number3].sprite == _playerClosing && cell[number2].sprite == _opponent && cell[number1] != _opponentClosing)
            positions.Add(number2);

        //
        if (cell[number1].sprite == _opponent && cell[number2].sprite == _opponent && cell[number3].sprite == null)
        {
            positions.Add(number1);
            positions.Add(number2);
        }
        else if (cell[number1].sprite == _opponent && cell[number3].sprite == _opponent && cell[number2].sprite == null)
        {
            positions.Add(number1);
            positions.Add(number3);
        }
        else if (cell[number2].sprite == _opponent && cell[number3].sprite == _opponent && cell[number1].sprite == null)
        {
            positions.Add(number2);
            positions.Add(number3);
        }
    }

    public int LosingPosition(string type) // ����������� �������
    {
        if(type == "tic" || type == "tac")
        {
            List<int> losingPositions = new List<int>();

            FindingLosingPosition(0, 1, 2, type, ref losingPositions, ref GameScript.Cell);
            FindingLosingPosition(3, 4, 5, type, ref losingPositions, ref GameScript.Cell);
            FindingLosingPosition(6, 7, 8, type, ref losingPositions, ref GameScript.Cell);

            FindingLosingPosition(0, 3, 6, type, ref losingPositions, ref GameScript.Cell);
            FindingLosingPosition(1, 4, 7, type, ref losingPositions, ref GameScript.Cell);
            FindingLosingPosition(2, 5, 8, type, ref losingPositions, ref GameScript.Cell);

            FindingLosingPosition(0, 4, 8, type, ref losingPositions, ref GameScript.Cell);
            FindingLosingPosition(2, 4, 6, type, ref losingPositions, ref GameScript.Cell);

            if (losingPositions.Count > 0 && _playerOverlaps > 1)
                return losingPositions[0];
            else
                return -1;
        }
        else
        {
            Debug.LogWarning("������� ��������� ������� �������� ������� 'LosingPosition'.");
            return -1;
        }
    }

    public void ProtectionFromLosingPosition(string type, ref List<int> losingPositionsProtect, Image[] cell) // ������ �� ����������� �������
    {
        if(type == "tic" || type == "tac")
        {
            Sprite player = null;
            string typeOpponent = "";

            if (type == "tic")
            {
                player = GameScript.Tic;
                typeOpponent = "tac";
            }
            if (type == "tac")
            {
                player = GameScript.Tac;
                typeOpponent = "tic";
            }

            for (int i = 0; i < 9; i++)
                if (cell[i].sprite == null)
                {
                    cell[i].sprite = player;
                    FindingLosingPosition(0, 1, 2, typeOpponent, ref losingPositionsProtect, ref cell);
                    FindingLosingPosition(3, 4, 5, typeOpponent, ref losingPositionsProtect, ref cell);
                    FindingLosingPosition(6, 7, 8, typeOpponent, ref losingPositionsProtect, ref cell);

                    FindingLosingPosition(0, 3, 6, typeOpponent, ref losingPositionsProtect, ref cell);
                    FindingLosingPosition(1, 4, 7, typeOpponent, ref losingPositionsProtect, ref cell);
                    FindingLosingPosition(2, 5, 8, typeOpponent, ref losingPositionsProtect, ref cell);

                    FindingLosingPosition(0, 4, 8, typeOpponent, ref losingPositionsProtect, ref cell);
                    FindingLosingPosition(2, 4, 6, typeOpponent, ref losingPositionsProtect, ref cell);
                    cell[i].sprite = null;
                }
        }
        else
        {
            Debug.LogWarning("������� ��������� ������� �������� ������� 'ProtectionFromLosingPosition'.");
        }
    }

    ///
    /// ������� ���������� ����� � �� �����������
    ///

    private void FindingPaths(int number1, int number2, int number3, string type, ref List<int> positions) // ����� ��������� �����
    {
        AssigningSprites(type);

        if(GameScript.Cell[number1].sprite == _player 
            && (GameScript.Cell[number2].sprite == null || GameScript.Cell[number2].sprite == _player || GameScript.Cell[number2].sprite == _opponent) 
            && (GameScript.Cell[number3].sprite == null || GameScript.Cell[number3].sprite == _player || GameScript.Cell[number3].sprite == _opponent))
        {
            positions.Add(number2);
            positions.Add(number3);
        }
        if (GameScript.Cell[number2].sprite == _player
            && (GameScript.Cell[number1].sprite == null || GameScript.Cell[number1].sprite == _player || GameScript.Cell[number1].sprite == _opponent)
            && (GameScript.Cell[number3].sprite == null || GameScript.Cell[number3].sprite == _player || GameScript.Cell[number3].sprite == _opponent))
        {
            positions.Add(number1);
            positions.Add(number3);
        }
        if (GameScript.Cell[number3].sprite == _player
            && (GameScript.Cell[number1].sprite == null || GameScript.Cell[number1].sprite == _player || GameScript.Cell[number1].sprite == _opponent)
            && (GameScript.Cell[number2].sprite == null || GameScript.Cell[number2].sprite == _player || GameScript.Cell[number2].sprite == _opponent))
        {
            positions.Add(number1);
            positions.Add(number2);
        }
    }

    public void CrossingPaths(string type, ref List<int> positionsCrossingPaths) // ���������� ������� ����������� ����� � ���������� �� � ������
    {
        if(type == "tic" || type == "tac")
        {
            List<int> positionsPaths = new List<int>(); // ������� �������� �����
            int numberOfRepetitions; // ���-�� ���������� ����� �������

            FindingPaths(0, 1, 2, type, ref positionsPaths);
            FindingPaths(3, 4, 5, type, ref positionsPaths);
            FindingPaths(6, 7, 8, type, ref positionsPaths);

            FindingPaths(0, 3, 6, type, ref positionsPaths);
            FindingPaths(1, 4, 7, type, ref positionsPaths);
            FindingPaths(2, 5, 8, type, ref positionsPaths);

            FindingPaths(0, 4, 8, type, ref positionsPaths);
            FindingPaths(2, 4, 6, type, ref positionsPaths);

            for (int i = 0; i < positionsPaths.Count; i++)
            {
                numberOfRepetitions = 0;
                for (int j = 0; j < positionsPaths.Count; j++)
                {
                    if (positionsPaths[i] == positionsPaths[j])
                        numberOfRepetitions++;
                }

                if (numberOfRepetitions > 1)
                    positionsCrossingPaths.Add(positionsPaths[i]);
            }
        }
        else
        {
            Debug.LogWarning("������� ��������� ������� �������� ������� 'CrossingPaths'.");
        }
    }
}
