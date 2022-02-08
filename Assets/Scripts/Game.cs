using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour
{
    public Sprite Tic;        // Переменные для спрайтов
    public Sprite Tac;        //
    public Sprite ClosingTic; //
    public Sprite ClosingTac; // 
    public Image[] Cell; // Игровое поле

    public Text MessagePanel;
    public Text GameTypePanel;

    [HideInInspector] public bool move; // Разрешение на ход
    [HideInInspector] public int startOverlaps; // Стартовое кол-во перекрытий для проверки изменений
    [HideInInspector] public int overlapsPlayer; // Кол-во возможных перекрытий
    [HideInInspector] public int overlapsOpponent; //

    private string _typePlayer = "";    //
    private string _typeOpponent = "";  // Какой тип игрока и соперника

    [SerializeField] private EasyDifficultyLevel EasyLevelScript;
    [SerializeField] private NormalDifficultyLevel NormalLevelScript;
    [SerializeField] private ChallengingDifficultyLevel ChallengLevelScript;
    [SerializeField] private CheckGame CheckScript;
    [SerializeField] private OverlapsBar OverlapsBarScript;

    private Coroutine _startGame;

    public void Start()
    {
        if (Menu.type == "tic" || Menu.type == "tac")
        {
            // Определение типа игроков
            if (Menu.type == "tic")
            {
                _typePlayer = "tic";
                _typeOpponent = "tac";
            }
            else if (Menu.type == "tac")
            {
                _typePlayer = "tac";
                _typeOpponent = "tic";
            }

            startOverlaps = 3; // Начальное кол-во перекрытий
            OverlapsBarScript.ConnectingOverlapPanel(_typePlayer);

            ShowLevelPanel();

            //// Первых ход крестиков
            //if (Menu.type == 2)
            //{
            //    if (Menu.level == 1)
            //    {
            //        Cell[Random.Range(0, 8)].sprite = Tic;
            //    }
            //    else if (Menu.level == 2 || Menu.level == 3)
            //    {
            //        int randomCell = Random.Range(0, 8);
            //        while (randomCell == 4)
            //            randomCell = Random.Range(0, 8);
            //        Cell[randomCell].sprite = Tic;
            //    }
            //}

            StartMatch();
        }
        else
            Debug.LogWarning("Тип игрока неопределён!");
    }

    private void StartMatch()
    {
        // Определение начального счёта и финальной границы матча
        int playerWins = 0;
        int opponentWins = 0;
        int finalWins = 5;

        while(playerWins != finalWins || opponentWins != finalWins)
        {
            // Нужна функция отображения счёта матча !!!

            _startGame = StartCoroutine(StartGame());

            // Анализ пройденной игры !!!
        }
        // и в конец добавить её!

        if(playerWins == finalWins)
        {
            //
        }
        else if(opponentWins == finalWins)
        {
            //
        }

        // Придумать способ повтора матча !!!
    }

    private IEnumerator StartGame()
    {
        int test = 0; // TEST

        while (CheckScript.StatusGame() == 0)
        {
            bool isMovePlayer = IsPossibleToMakeMove(_typePlayer);
            bool isMoveOpponent = IsPossibleToMakeMove(_typeOpponent);

            if (isMovePlayer == true)
            {
                move = true;
                yield return new WaitUntil(() => move == false);

                if (IsPossibleToMakeMove(_typeOpponent) == true)
                {
                    yield return new WaitForSeconds(0.5f);
                    SelectionGame();
                }

                PrintStatusGame();
            }
            else if (isMoveOpponent == true)
            {
                yield return new WaitForSeconds(0.5f);
                SelectionGame();
            }

            // TEST
            test++;
            if(test > 50)
            {
                Debug.LogWarning("Ошибка в StartGame");
                break;
            }
            //
        }
    }

    private void SelectionGame() // Выбор уровня игры
    {
        if (_typePlayer == "tic" || _typePlayer == "tac")
        {
            if (Menu.level == "easy")
            {
                EasyLevelScript.PlayGame(_typeOpponent);
            }
            else if (Menu.level == "normal")
            {
                NormalLevelScript.PlayGame(_typeOpponent);
            }
            else if (Menu.level == "challeng")
            {
                ChallengLevelScript.PlayGame(_typeOpponent);
            }
            else
                Debug.LogWarning("Уровень не определён");
        }
        else
            Debug.LogWarning("Тип игрока не определён");


        PrintStatusGame();
    }

    private void PrintStatusGame() // Вывод на панель статуса игры
    {
        // Победа TIC
        if (CheckScript.StatusGame() == 1)
        {
            MessagePanel.text = "Победа Крестиков!";
        }
        // Победа TAC
        else if (CheckScript.StatusGame() == 2)
        {
            MessagePanel.text = "Победа Ноликов!";
        }
        // Ничья
        else if (CheckScript.StatusGame() == 3)
        {
            MessagePanel.text = "Ничья!";
        }
    }

    public void BackMenu() // Переход в меню
    { 
        SceneManager.LoadScene(0); 
    }

    public void RestartGame()
    {
        StopCoroutine(_startGame);

        // Очистка поля
        for (int i = 0; i < 9; i++)
            Cell[i].sprite = null;

        Start();
    }

    private bool IsPossibleToMakeMove(string type) // Проверка на возможность сделать ход
    {
        Sprite opponent = null; 
        int playerOverlaps = 0;
        bool isEmptyCell = false; // Показатель свободных ячеек для хода
        bool isOpponentCell = false; // Показатель занятых ячеек для хода

        if(type == "tic" || type == "tac")
        {
            if (type == "tic")
            {
                opponent = Tac;
                playerOverlaps = overlapsPlayer;
            }
            if (type == "tac")
            {
                opponent = Tic;
                playerOverlaps = overlapsOpponent;
            }

            for (int number = 0; number < 9; number++)
            {
                if (Cell[number].sprite == null)
                {
                    isEmptyCell = true;
                }
                if (Cell[number].sprite == opponent && playerOverlaps > 0)
                {
                    isOpponentCell = true;
                }
            }

            if (isEmptyCell == true || isOpponentCell == true)
                return true;
            else
                return false;
        }
        //
        else
        {
            Debug.LogWarning("Ошибка во входном параметре 'IsPossibleToMakeMove'. Входной параметр: " + type);
            return false;
        }
    }

    private void ShowLevelPanel()
    {
        if (Menu.level == "easy")
        {
            GameTypePanel.text = "Легкий";
        }
        else if (Menu.level == "normal")
        {
            GameTypePanel.text = "Нормальный";
        }
        else if (Menu.level == "challeng")
        {
            GameTypePanel.text = "Сложный";
        }
        else
            GameTypePanel.text = "?";
    }
}
