using UnityEngine;
using System.Collections.Generic;

public class HardTicGame : MonoBehaviour
{
    public Game gameScript;
    public CheckGame checkScript;

    public void PlayGame()
    {
        // Логика ноликов при сложном уровне сложности
        //
        if (checkScript.StatusGame() == 0)
        {
            int winning = checkScript.AttackStrategy("tac");           // Переменные для хранения результата выполнения
            int protection = checkScript.ProtectionStrategyHard("tac"); // функций выигрыша и проигрыша

            List<int> positionsCrossingPaths = new List<int>();
            checkScript.CrossingPaths("tic", ref positionsCrossingPaths);

            List<int> losingPositionsProtect = new List<int>();
            checkScript.ProtectionFromLosingPosition("tac", ref losingPositionsProtect, gameScript.Cell);

            // Выигрышный ход Tac
            if (winning != -1) 
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
                Debug.Log("Выигрыш");
            }

            // Защита Tac
            else if (protection != -1)
            {
                if (gameScript.Cell[protection].sprite == null)
                {
                    gameScript.Cell[protection].sprite = gameScript.Tac;
                }
                else
                {
                    gameScript.Cell[protection].sprite = gameScript.ClosingTac;
                    Game.overlapsTac--;
                }
                Debug.Log("Защита");
            }

            //// Заведома проигрышная позиция соперника
            //else if (checkScript.LosingPositionsTac() == true) // Исправить ошибку!!!
            //{
            //    bool flag = false;
            //    int pos = 0;
            //    for (int i = 0; i < positionsCrossingPaths.Count; i++)
            //        for (int j = 0; j < checkScript.losingPositionsTac.Count; j++)
            //            if (positionsCrossingPaths[i] == checkScript.losingPositionsTac[j])
            //            {
            //                pos = j;
            //                flag = true;
            //            }

            //    if (flag)
            //    {
            //        gameScript.Cell[checkScript.losingPositionsTac[pos]].sprite = gameScript.ClosingTac;
            //        Game.overlapsTac--;
            //        checkScript.losingPositionsTac.RemoveAt(pos);
            //    }
            //    else
            //    {
            //        gameScript.Cell[checkScript.losingPositionsTac[0]].sprite = gameScript.ClosingTac;
            //        Game.overlapsTac--;
            //        checkScript.losingPositionsTac.RemoveAt(0);
            //    }

            //}

            // Заведома проигрышная позиция соперника
            else if (checkScript.LosingPosition("tac") != -1)
            {
                gameScript.Cell[checkScript.LosingPosition("tac")].sprite = gameScript.ClosingTac;
                Game.overlapsTac--;
                Debug.Log("Проигрышная позиция");
            }

            // Если соперник сделал ход в центр поля, то перекрываем его
            else if(gameScript.Cell[4].sprite == gameScript.Tic && Game.overlapsTac > 0)
            {
                gameScript.Cell[4].sprite = gameScript.ClosingTac;
                Game.overlapsTac--;
                Debug.Log("Центр");
            }

            // Случайный ход Tac !!!!!!!!!
            else
            {
                int randomCell = Random.Range(0, 8);
                bool cellNull = false; // Содержит ли поле хотя бы одну пустую ячейку
                bool cellTic = false; // Содержит ли поле хотя бы одну ячейку для перекрытия
                bool notLosingPosProtect = true; //
                int countNull = 0; // Кол-во пустых ячеек

                if (losingPositionsProtect.Count > 0)
                    notLosingPosProtect = false;
                else
                    notLosingPosProtect = true;

                for (int i = 0; i < 9; i++)
                {
                    if (gameScript.Cell[i].sprite == null)
                    {
                        cellNull = true;
                        countNull++;
                        for(int j = 0; j < losingPositionsProtect.Count; j++)
                        {

                        }
                    }
                    if (gameScript.Cell[i].sprite == gameScript.Tic && Game.overlapsTac > 0)
                        cellTic = true;
                }

                if (cellNull == true && countNull > 1)
                {
                    while (gameScript.Cell[randomCell].sprite != null || randomCell == 4)
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
                else if (cellNull == true && countNull == 1)
                {
                    while (gameScript.Cell[randomCell].sprite != null)
                        randomCell = Random.Range(0, 8);
                    gameScript.Cell[randomCell].sprite = gameScript.Tac;
                }
                else Debug.Log("Пропуск хода");

                Debug.Log("Случайный ход");
            }
        }
    }
}
