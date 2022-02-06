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
    public static int overlapsTic; // Кол-во возможных перекрытий
    public static int overlapsTac; //

    private string typePlayer;    //
    private string typeOpponent;  // Какой тип игрока и соперника

    [SerializeField] private EasyDifficultyLevel EasyLevelScript;
    [SerializeField] private NormalDifficultyLevel NormalLevelScript;
    [SerializeField] private ChallengingDifficultyLevel ChallengLevelScript;
    [SerializeField] private CheckGame CheckScript;
    [SerializeField] private OverlapsBar OverlapsBar;

    public void Start()
    {
        startOverlaps = 3; // Начальное кол-во отображений перекрытий
        overlapsTic = startOverlaps; // Начальное кол-во перекрытий
        overlapsTac = startOverlaps; //

        OverlapsBar.ConnectingOverlapPanel(); // 

        MessagePanel.text = "Игра Началась!";
        ShowLevel();

        // Первых ход крестиков
        if (Menu.type == 2)
        {
            if (Menu.level == 1)
            {
                Cell[Random.Range(0, 8)].sprite = Tic;
            }
            else if (Menu.level == 2 || Menu.level == 3)
            {
                int randomCell = Random.Range(0, 8);
                while (randomCell == 4)
                    randomCell = Random.Range(0, 8);
                Cell[randomCell].sprite = Tic;
            }
        }

        // Определение типа игроков
        if (Menu.type == 1)
        {
            typePlayer = "tic";
            typeOpponent = "tac";
        }

        if(Menu.type == 2)
        {
            typePlayer = "tac";
            typeOpponent = "tic";
        }

        if (Menu.type == 1 || Menu.type == 2)
        { 
            StartCoroutine(StartGame()); 
        }
        else
            Debug.LogWarning("Тип игрока неопределён!");
    }

    private IEnumerator StartGame()
    {
        int test = 0; // TEST

        while (CheckScript.StatusGame() == 0)
        {
            bool isMovePlayer = IsPossibleToMakeMove(typePlayer);
            bool isMoveOpponent = IsPossibleToMakeMove(typeOpponent);

            if (isMovePlayer == true)
            {
                move = true;
                yield return new WaitUntil(() => move == false);

                if (IsPossibleToMakeMove(typeOpponent) == true)
                {
                    //yield return new WaitForSeconds(0.5f);
                    SelectionGame();
                }

                PrintStatusGame();
            }
            else if (isMoveOpponent == true)
            {
                //yield return new WaitForSeconds(0.5f);
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

    private void SelectionGame() // Выбор типа и уровня игры
    {
        //Пользователь за 'Tic', а программа за 'Tac'
        if (Menu.type == 1)
        {
            if (Menu.level == 1)
            {
                EasyLevelScript.PlayGame("tac");
            }
            else if (Menu.level == 2)
            {
                NormalLevelScript.PlayGame("tac");
            }
            else if (Menu.level == 3)
            {
                ChallengLevelScript.PlayGame("tac");
            }
            else
                Debug.LogWarning("Уровень не определён");
        }
        // Пользователь за 'Tac', а программа за 'Tic'
        else if (Menu.type == 2)
        {
            if (Menu.level == 1)
            {
                EasyLevelScript.PlayGame("tic");
            }
            else if (Menu.level == 2)
            {
                NormalLevelScript.PlayGame("tic");
            }
            else if (Menu.level == 3)
            {
                ChallengLevelScript.PlayGame("tic");
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
                playerOverlaps = overlapsTic;
            }
            if (type == "tac")
            {
                opponent = Tic;
                playerOverlaps = overlapsTac;
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

    private void ShowLevel()
    {
        if (Menu.level == 1)
            GameTypePanel.text = "Легкий";
        else if (Menu.level == 2)
            GameTypePanel.text = "Нормальный";
        else if (Menu.level == 3)
            GameTypePanel.text = "Сложный";
        else
            GameTypePanel.text = "?";
    }
}
