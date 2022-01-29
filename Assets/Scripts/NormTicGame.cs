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

        // Логика ноликов при нормальном уровне сложности
        //
        if (CheckScript.StatusGame() == 0)
        {
            int winning = CheckScript.AttackStrategy(type);           // Переменные для хранения результата выполнения
            int protection = CheckScript.ProtectionStrategyNormal(type); // функций выигрыша и проигрыша


            // Выигрышный ход
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

            // Защита
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

            // Заведома проигрышная позиция соперника
            else if (CheckScript.LosingPosition(type) != -1)
            {
                GameScript.Cell[CheckScript.LosingPosition(type)].sprite = _playerClosing;
                Game.overlapsTac--;
            }

            // Если соперник сделал ход в центр поля, то перекрываем его
            else if (GameScript.Cell[4].sprite == _opponent && Game.overlapsTac > 0)
            {
                GameScript.Cell[4].sprite = _playerClosing;
                Game.overlapsTac--;
            }

            // Случайный ход
            else
            {
                int randomCell = Random.Range(0, 8);
                bool areThereOpponentCells = false; // Есть ли ячейки противника
                bool isThereNonLosingCell = false; // Есть ли хотя бы одна не проигрышная позиция
                int numberOfEmptyCells = 0; // Кол-во пустых ячеек

                List<int> losingPositions = new List<int>(); // Проигрышные позиции
                CheckScript.ProtectionFromLosingPosition(type, ref losingPositions, GameScript.Cell);

                for (int i = 0; i < 9; i++) // Анализ поля
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

                if (numberOfEmptyCells > 0 && isThereNonLosingCell == true) // Если есть пустые и не проигрышные позиции
                {
                    while (GameScript.Cell[randomCell].sprite != null || IsThisLosingPosition(randomCell) == true)
                    {
                        randomCell = Random.Range(0, 8);
                    }
                    GameScript.Cell[randomCell].sprite = _player;
                }

                else if (areThereOpponentCells == true) // Если же нет пустых и не проигрышных позиций, то перекрывать противника
                {
                    while (GameScript.Cell[randomCell].sprite != _opponent)
                        randomCell = Random.Range(0, 8);
                    GameScript.Cell[randomCell].sprite = _playerClosing;
                    Game.overlapsTac--;
                }

                else if (numberOfEmptyCells > 0) // Если же не осталось других ходов
                {
                    while (GameScript.Cell[randomCell].sprite != null)
                        randomCell = Random.Range(0, 8);
                    GameScript.Cell[randomCell].sprite = _player;
                }

                else Debug.Log("Пропуск хода");


                // Является ли позиция проигрышной
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

    // Функция назначение спрайтов
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
            Debug.LogWarning("Неверно указанный входной параметр функции 'AssigningSprites' в файле 'NormTicGame.cs'.");
        }
    }
}
