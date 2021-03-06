using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LibraryOfPathPositions;
using EnumGame;

public class CheckGame : MonoBehaviour
{
    public Game GameScript;

    private Sprite _player;
    private Sprite _playerClosing;
    private Sprite _opponent;
    private Sprite _opponentClosing;
    private int _playerOverlaps;
    private int _opponentOverlaps;

    // ??????? ?????????? ????????
    //
    private void AssigningSprites(string type) // !!!!!!!
    {
        if(type == "tic")
        {
            _player = GameScript.Tic;
            _playerClosing = GameScript.ClosingTic;
            _opponent = GameScript.Tac;
            _opponentClosing = GameScript.ClosingTac;

            if(GameScript.TypePlayer == type)
            {
                _playerOverlaps = GameScript.OverlapsPlayer;
                _opponentOverlaps = GameScript.OverlapsOpponent;
            }
            else
            {
                _playerOverlaps = GameScript.OverlapsOpponent;
                _opponentOverlaps = GameScript.OverlapsPlayer;
            }
        }
        else if(type == "tac")
        {
            _player = GameScript.Tac;
            _playerClosing = GameScript.ClosingTac;
            _opponent = GameScript.Tic;
            _opponentClosing = GameScript.ClosingTic;

            if (GameScript.TypePlayer == type)
            {
                _playerOverlaps = GameScript.OverlapsPlayer;
                _opponentOverlaps = GameScript.OverlapsOpponent;
            }
            else
            {
                _playerOverlaps = GameScript.OverlapsOpponent;
                _opponentOverlaps = GameScript.OverlapsPlayer;
            }
        }
        else
        {
            Debug.LogWarning("??????? ????????? ??????? ???????? ??????? 'AssigningSprites' ? ????? 'CheckGame.cs'.");
        }
    }

    //
    // ??????? ??????? ????
    //

    public GameState StatusGame() // ???????? ??????? ????
    {
        string player = GameScript.TypePlayer;
        string opponent = GameScript.TypeOpponent;

        // ???????? ?????? Player
        if (CheckingTheFieldForVictory(0, 1, 2, player))
            return GameState.VictoryPlayer;
        else if (CheckingTheFieldForVictory(3, 4, 5, player))
            return GameState.VictoryPlayer;
        else if (CheckingTheFieldForVictory(6, 7, 8, player))
            return GameState.VictoryPlayer;

        else if (CheckingTheFieldForVictory(0, 3, 6, player))
            return GameState.VictoryPlayer;
        else if (CheckingTheFieldForVictory(1, 4, 7, player))
            return GameState.VictoryPlayer;
        else if (CheckingTheFieldForVictory(2, 5, 8, player))
            return GameState.VictoryPlayer;

        else if (CheckingTheFieldForVictory(0, 4, 8, player))
            return GameState.VictoryPlayer;
        else if (CheckingTheFieldForVictory(2, 4, 6, player))
            return GameState.VictoryPlayer;

        //???????? ?????? Opponent
        else if (CheckingTheFieldForVictory(0, 1, 2, opponent))
            return GameState.VictoryOpponent;
        else if (CheckingTheFieldForVictory(3, 4, 5, opponent))
            return GameState.VictoryOpponent;
        else if (CheckingTheFieldForVictory(6, 7, 8, opponent))
            return GameState.VictoryOpponent;

        else if (CheckingTheFieldForVictory(0, 3, 6, opponent))
            return GameState.VictoryOpponent;
        else if (CheckingTheFieldForVictory(1, 4, 7, opponent))
            return GameState.VictoryOpponent;
        else if (CheckingTheFieldForVictory(2, 5, 8, opponent))
            return GameState.VictoryOpponent;

        else if (CheckingTheFieldForVictory(0, 4, 8, opponent))
            return GameState.VictoryOpponent;
        else if (CheckingTheFieldForVictory(2, 4, 6, opponent))
            return GameState.VictoryOpponent;

        // ???????? ?? ?????
        else if (GameScript.OverlapsPlayer < 1 && GameScript.OverlapsOpponent < 1 && IsFieldEmpty() == false)
            return GameState.Draw;

        // ??????????? ????
        else return GameState.Continues;

        // ???????? ???? ?? ??????
        bool CheckingTheFieldForVictory(int number1, int number2, int number3, string type)
        {
            AssigningSprites(type);

            // ???????? 3 ????? ?? ??????? ?????? ??????
            if ((GameScript.Cell[number1].sprite == _player || GameScript.Cell[number1].sprite == _playerClosing)
                && (GameScript.Cell[number2].sprite == _player || GameScript.Cell[number2].sprite == _playerClosing)
                && (GameScript.Cell[number3].sprite == _player || GameScript.Cell[number3].sprite == _playerClosing))
                return true;
            else
                return false;
        }

        // ???????? ?? ???? ??????
        bool IsFieldEmpty()
        {
            bool flag = false;
            for (int i = 0; i < 9; i++)
            {
                if (GameScript.Cell[i].sprite == null)
                    flag = true;
            }

            return flag;
        }
    }

    //
    // ??????? ????????
    //

    int FindingPositionToAttack(int number1, int number2, int number3, string type) // ????? ??????? ??? ?????
    {
        AssigningSprites(type);

        // ???????? ????? ?? ?????????? ??? ??????
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
            return -1; // ??????? ?? ???????
    }

    public int AttackStrategy(string type) // ????????? ?????
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
                return -1; // ??????? ?? ???????
        }
        else
        {
            Debug.LogWarning("??????? ????????? ??????? ???????? ??????? 'AttackStrategy'.");
            return -1;
        }
    }

    //
    // ??????? ?????? 
    //

    void FindingPositionsToProtection(int number1, int number2, int number3, string type, ref List<int> protect) // ????? ??????? ??? ??????
    {
        AssigningSprites(type);

        // ???????? ????? ?? ????????? ??????
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

    void EnumerationOfProtectionPositions(string type, ref List<int> protect) // ??????? ??????? ??????
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

    public int ProtectionStrategyEazy(string type) // ????????? ?????? 'Eazy'
    {
        if (type == "tic" || type == "tac")
        {
            List<int> protect = new List<int>(); // ?????? ?????? ????? ??????

            // ?????? ?? ???????? ????, ???????? ?? ????????? ??????
            EnumerationOfProtectionPositions(type, ref protect);

            // ????? ????????? ?????? ??? ??????
            if (protect.Count > 0)
            {
                return protect[Random.Range(0, protect.Count)];
            }
            else
                return -1;
        }
        else
        {
            Debug.LogWarning("??????? ????????? ??????? ???????? ??????? 'ProtectionStrategyEazy'.");
            return -1;
        }
    }

    public int ProtectionStrategyNormal(string type) //????????? ?????? 'Normal'
    {
        if(type == "tic" || type == "tac")
        {
            List<int> protect = new List<int>(); // ?????? ?????? ????? ??????

            EnumerationOfProtectionPositions(type, ref protect);

            // ????? ????????? ?????? ??? ??????
            if (protect.Count > 0)
            {
                bool isThereBestPosition = false; // ???? ?? ????????? ??????? ??????
                int bestPosition = 0; // ????????? ??????? ??????

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
            Debug.LogWarning("??????? ????????? ??????? ???????? ??????? 'ProtectionStrategyNormal'.");
            return -1;
        }
    }

    public int ProtectionStrategyHard(string type) //????????? ?????? 'Hard'
    {
        if(type == "tic" || type == "tac")
        {
            List<int> protect = new List<int>(2); // ?????? ?????? ????? ??????

            // ?????? ?? ???????? ????, ???????? ?? ????????? ??????
            EnumerationOfProtectionPositions(type, ref protect);

            // ????? ????????? ?????? ??? ??????
            if (protect.Count > 0)
            {
                bool isThereBestPosition = false; // ???? ?? ????????? ??????? ??????
                int bestPosition = 0; // ????????? ??????? ??????

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
            Debug.LogWarning("??????? ????????? ??????? ???????? ??????? 'ProtectionStrategyHard'.");
            return -1;
        }
    }

    //
    // ??????? ??????????? ???????
    //

    private void FindingLosingPosition(int number1, int number2, int number3, string type, ref List<int> positions, ref Image[] cell) // ????? ??????????? ???????
    {
        AssigningSprites(type);

        if (cell[number1].sprite == _playerClosing && cell[number2].sprite == _opponent && cell[number3].sprite != _opponentClosing) 
        {
            positions.Add(number2);
        }
        else if (cell[number1].sprite == _playerClosing && cell[number3].sprite == _opponent && cell[number2].sprite != _opponentClosing) 
        {
            positions.Add(number3);
        }

        else if (cell[number2].sprite == _playerClosing && cell[number1].sprite == _opponent && cell[number3].sprite != _opponentClosing)
        {
            positions.Add(number1);
        }
        else if (cell[number2].sprite == _playerClosing && cell[number3].sprite == _opponent && cell[number1].sprite != _opponentClosing) 
        {
            positions.Add(number3);
        }

        else if (cell[number3].sprite == _playerClosing && cell[number1].sprite == _opponent && cell[number2].sprite != _opponentClosing) 
        {
            positions.Add(number1);
        }
        else if (cell[number3].sprite == _playerClosing && cell[number2].sprite == _opponent && cell[number1].sprite != _opponentClosing)
        {
            positions.Add(number2);
        }

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

    public int LosingPosition(string type) // ??????????? ???????
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
            Debug.LogWarning("??????? ????????? ??????? ???????? ??????? 'LosingPosition'.");
            return -1;
        }
    }

    public void ProtectionFromLosingPosition(string type, ref List<int> losingPositionsProtect, Image[] cell) // ?????? ?? ??????????? ???????
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
            Debug.LogWarning("??????? ????????? ??????? ???????? ??????? 'ProtectionFromLosingPosition'.");
        }
    }

    ///
    /// ??????? ?????????? ????? ? ?? ???????????
    ///

    private void FindingPaths(int number1, int number2, int number3, string type, ref List<PathPositions> positions, ref Image[] cell) // ????? ????????? ?????
    {
        AssigningSprites(type);

        if (cell[number1].sprite == _player 
            && (cell[number2].sprite == null || cell[number2].sprite == _player || cell[number2].sprite == _opponent) 
            && (cell[number3].sprite == null || cell[number3].sprite == _player || cell[number3].sprite == _opponent))
        {
            positions.Add(new PathPositions(number1, number2));
            positions.Add(new PathPositions(number1, number3));
        }
        if (cell[number2].sprite == _player
            && (cell[number1].sprite == null || cell[number1].sprite == _player || cell[number1].sprite == _opponent)
            && (cell[number3].sprite == null || cell[number3].sprite == _player || cell[number3].sprite == _opponent))
        {
            positions.Add(new PathPositions(number2, number1));
            positions.Add(new PathPositions(number2, number3));
        }
        if (cell[number3].sprite == _player
            && (cell[number1].sprite == null || cell[number1].sprite == _player || cell[number1].sprite == _opponent)
            && (cell[number2].sprite == null || cell[number2].sprite == _player || cell[number2].sprite == _opponent))
        {
            positions.Add(new PathPositions(number3, number1));
            positions.Add(new PathPositions(number3, number2));
        }
    }

    private void SearchCrossingPaths(string type, ref List<int> crossingPositions, ref List<int> startPositions, ref Image[] cell) // ?????????? ??????? ??????????? ????? ? ?????????? ?? ? ??????
    {
        List<PathPositions> pathPositions = new List<PathPositions>(); // ??????? ????? (??????, ????)
        int numberOfRepetitions; // ???-?? ?????????? ????? ???????

        FindingPaths(0, 1, 2, type, ref pathPositions, ref cell);
        FindingPaths(3, 4, 5, type, ref pathPositions, ref cell);
        FindingPaths(6, 7, 8, type, ref pathPositions, ref cell);

        FindingPaths(0, 3, 6, type, ref pathPositions, ref cell);
        FindingPaths(1, 4, 7, type, ref pathPositions, ref cell);
        FindingPaths(2, 5, 8, type, ref pathPositions, ref cell);

        FindingPaths(0, 4, 8, type, ref pathPositions, ref cell);
        FindingPaths(2, 4, 6, type, ref pathPositions, ref cell);

        for (int i = 0; i < pathPositions.Count; i++)
        {
            numberOfRepetitions = 0;
            for (int j = 0; j < pathPositions.Count; j++)
            {
                if (pathPositions[i].Path == pathPositions[j].Path)
                    numberOfRepetitions++;
            }

            if (numberOfRepetitions > 1)
            {
                crossingPositions.Add(pathPositions[i].Path);
                startPositions.Add(pathPositions[i].Beginning);
            }
        }
    }

    public int CrossingAttack(string type, ref Image[] cell)
    {
        if(type == "tic" || type == "tac")
        {
            List<int> crossingPositions = new List<int>();
            List<int> startPositions = new List<int>();

            SearchCrossingPaths(type, ref crossingPositions, ref startPositions, ref cell);

            if (crossingPositions.Count > 0)
            {
                int attackPosition = -1;
                foreach (int number in crossingPositions)
                {
                    if (GameScript.Cell[number].sprite == _opponent)
                        attackPosition = number;
                }
                return attackPosition;
            }
            else
                return -1;
        }
        else
        {
            Debug.LogWarning("??????? ????????? ??????? ???????? ??????? 'CrossingAttack'.");
            return -1;
        }
    }

    public int CrossingProtection(string type, ref Image[] cell)
    {
        if (type == "tic" || type == "tac")
        {
            string typeOpponent;
            List<int> crossingPositions = new List<int>();
            List<int> startPositions = new List<int>();

            if (type == "tic")
                typeOpponent = "tac";
            else
                typeOpponent = "tic";

            SearchCrossingPaths(typeOpponent, ref crossingPositions, ref startPositions, ref cell);

            AssigningSprites(type);

            if (crossingPositions.Count > 0)
            {
                int protectionPosition = -1;
                foreach (int number in crossingPositions)
                {
                    if (cell[number].sprite == _player)
                        protectionPosition = startPositions[Random.Range(0, startPositions.Count)];
                }
                return protectionPosition;
            }
            else
                return -1;
        }
        else
        {
            Debug.LogWarning("??????? ????????? ??????? ???????? ??????? 'CrossingAttack'.");
            return -1;
        }
    }

    public void ProtectionFromCrossing(string type, ref List<int> protectionFromCrossing, Image[] cell)
    {
        if(type == "tic" || type == "tac")
        {
            AssigningSprites(type);

            for(int number = 0; number < 9; number++)
                if(cell[number].sprite == null)
                {
                    cell[number].sprite = _player;

                    if (CrossingProtection(type, ref cell) != -1 && _opponentOverlaps > 0)
                        protectionFromCrossing.Add(number);

                    cell[number].sprite = null;
                }
        }
        else
        {
            Debug.LogWarning("??????? ????????? ??????? ???????? ??????? 'ProtectionFromCrossing'.");
        }
    }
}

