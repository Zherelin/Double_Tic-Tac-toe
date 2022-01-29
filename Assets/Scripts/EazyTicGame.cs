using UnityEngine;

public class EazyTicGame : MonoBehaviour
{
    public Game gameScript;       // Подключение скриптов
    public CheckGame checkScript; //
   
    public void PlayGame()
    {
        // Логика ноликов при лёгком уровне сложности
        //
        if(checkScript.StatusGame() == 0) // Проверка на то, что игра продолжается
        {
            int winning = checkScript.AttackStrategy("tac");       // Переменные для хранения результата выполнения
            int protection = checkScript.ProtectionStrategyEazy("tac"); // функций выигрыша и проигрыша


            // Ход в центр поля, если ячейка пуста
            if (gameScript.Cell[4].sprite == null)
                gameScript.Cell[4].sprite = gameScript.Tac;

            // Выигрышный ход Tac
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
            // Защита Tac
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
            // Случайный ход Tac
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
                else Debug.Log("Пропуск хода");
            }
        }
    }
}
