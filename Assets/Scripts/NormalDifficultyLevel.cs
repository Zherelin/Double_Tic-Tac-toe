using System.Collections.Generic;
using UnityEngine;

public class NormalDifficultyLevel : MonoBehaviour
{
    [SerializeField] private Game GameScript;
    [SerializeField] private CheckGame CheckScript;

    private Sprite _player;
    private Sprite _playerClosing;
    private Sprite _opponent;
    private int _playerOverlaps;

    public void PlayGame(string typePlayer)
    {
        // Логика игры при лёгком уровне сложности
        //
        if (typePlayer == "tic" || typePlayer == "tac")
        {
            AssigningSprites(typePlayer); // Назначение спрайтов

            if (CheckScript.StatusGame() == 0) // Проверка на то, что игра продолжается
            {
                int winning = CheckScript.AttackStrategy(typePlayer);            // Результат выигрышного хода
                int protection = CheckScript.ProtectionStrategyNormal(typePlayer); // Результат защиты

                // Выигрышный ход
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
                        OverlapDeduction();
                    }
                }

                // Заведома проигрышная позиция соперника
                else if (CheckScript.LosingPosition(typePlayer) != -1)
                {
                    GameScript.Cell[CheckScript.LosingPosition(typePlayer)].sprite = _playerClosing;
                    OverlapDeduction();
                }

                // Если соперник сделал ход в центр поля, то перекрываем его
                else if (GameScript.Cell[4].sprite == _opponent && _playerOverlaps > 0)
                {
                    GameScript.Cell[4].sprite = _playerClosing;
                    OverlapDeduction();
                }

                // Случайный ход
                else
                {
                    int randomCell = Random.Range(0, 8);
                    bool isThereAnEmptyCell = false; // Есть ли пустая ячейка
                    bool isThereAnOpponentCell = false; // Есть ли ячейка противника
                    bool isThereNonLosingCell = false; // Есть ли хотя бы одна не проигрышная позиция

                    List<int> losingPositions = new List<int>(); // Проигрышные позиции
                    CheckScript.ProtectionFromLosingPosition(typePlayer, ref losingPositions, GameScript.Cell);

                    for (int number = 0; number < 9; number++) // Анализ поля
                    {
                        if (GameScript.Cell[number].sprite == null)
                        {
                            isThereAnEmptyCell = true;

                            if (IsThisLosingPosition(number) == false)
                                isThereNonLosingCell = true;
                        }
                        if (GameScript.Cell[number].sprite == _opponent && _playerOverlaps > 0)
                            isThereAnOpponentCell = true;
                    }

                    if (isThereAnEmptyCell == true && isThereNonLosingCell == true) // Если есть пустые и не проигрышные позиции
                    {
                        while (GameScript.Cell[randomCell].sprite != null || IsThisLosingPosition(randomCell) == true)
                        {
                            randomCell = Random.Range(0, 8);
                        }
                        GameScript.Cell[randomCell].sprite = _player;
                    }

                    else if (isThereAnOpponentCell == true) // Если же нет пустых и не проигрышных позиций, то перекрывать противника
                    {
                        while (GameScript.Cell[randomCell].sprite != _opponent)
                        {
                            randomCell = Random.Range(0, 8);
                        }
                        GameScript.Cell[randomCell].sprite = _playerClosing;
                        OverlapDeduction();
                    }

                    else if (isThereAnEmptyCell == true) // Если же не осталось других ходов
                    {
                        while (GameScript.Cell[randomCell].sprite != null)
                        {
                            randomCell = Random.Range(0, 8);
                        }
                        GameScript.Cell[randomCell].sprite = _player;
                    }
                    else Debug.Log("Пропуск хода");


                    // Является ли позиция проигрышной
                    //
                    bool IsThisLosingPosition(int position)
                    {
                        bool isThisLosingPosition = false;

                        if (losingPositions.Count > 0)
                        {
                            foreach (int number in losingPositions)
                            {
                                if (position == number || position == 4)
                                    isThisLosingPosition = true;
                            }
                        }
                        else
                        {
                            if (position == 4)
                                isThisLosingPosition = true;
                        }

                        return isThisLosingPosition;
                    }
                }
            }

            // Вычет перекрытия
            //
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
            Debug.LogWarning("Неверно указанный входной параметр функции 'PlayGame' файла 'EasyDifficultyLevel.cs'.");
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
