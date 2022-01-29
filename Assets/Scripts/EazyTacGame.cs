using UnityEngine;

public class EazyTacGame : MonoBehaviour
{
    public Game gameScript;       // Подключение скриптов
    public CheckGame checkScript; //

    public void PlayGame()
    {
        if (checkScript.StatusGame() == 0) // Game continues
        {
            int winning = checkScript.AttackStrategy("tic");       // Переменные для хранения результата выполнения
            int protection = checkScript.ProtectionStrategyEazy("tic"); // функций выигрыша и проигрыша


            // Выигрышный ход Tic
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
            // Защита Tic
            else if (protection != -1)
            {
                if (gameScript.Cell[protection].sprite == null)
                {
                    gameScript.Cell[protection].sprite = gameScript.Tic;
                }
                else
                {
                    Game.overlapsTic--;
                    gameScript.Cell[protection].sprite = gameScript.ClosingTic;
                }
            }
            // Случайный ход Tic
            else
            {
                int randomCell = Random.Range(0, 8);
                bool cellNull = false;
                bool cellTac = false;

                for (int i = 0; i < 9; i++)
                {
                    if (gameScript.Cell[i].sprite == null)
                        cellNull = true;
                    if (gameScript.Cell[i].sprite == gameScript.Tac && Game.overlapsTic > 0)
                        cellTac = true;
                }

                if (cellNull == true)
                {
                    while (gameScript.Cell[randomCell].sprite != null)
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
                else Debug.Log("Пропуск хода");
            }
        }
    }
}
