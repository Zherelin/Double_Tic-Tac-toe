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
    public Image[] OverlapsBarPlayer;   // Отображение кол-во перекрытий
    public Image[] OverlapsBarOpponent; // на панели

    public Text MessagePanel;
    public Text GameTypePanel;

    [HideInInspector] public bool move; // Разрешение на ход
    public static int overlapsTic; // Кол-во возможных перекрытий
    public static int overlapsTac; //
    private int startOverlaps; // Стартовое кол-во перекрытий для проверки изменений (отобр. на панели)

    private int typePlayer;    //
    private int typeOpponent;  // Какой тип игрока и соперника

    [SerializeField] private EasyDifficultyLevel EasyLevelScript;
    [SerializeField] private NormalDifficultyLevel NormalLevelScript;
    [SerializeField] private ChallengingDifficultyLevel ChallengLevelScript;
    [SerializeField] private CheckGame CheckScript;

    public void Start()
    {
        startOverlaps = 3; // Начальное кол-во отображений перекрытий на панели
        overlapsTic = startOverlaps; // Начальное кол-во перекрытий
        overlapsTac = startOverlaps; //

        MessagePanel.text = "Игра Началась!";
        ShowLevel();

        // Первых ход крестиков
        if (Menu.type == 2)
        {
            if(Menu.level == 1)
            {
                Cell[Random.Range(0, 8)].sprite = Tic;
            }
            else if(Menu.level == 2 || Menu.level == 3)
            {
                int randomCell = Random.Range(0, 8);
                while (randomCell == 4)
                    randomCell = Random.Range(0, 8);
                Cell[randomCell].sprite = Tic;
            }
        }

        // Назначение типа отображения перекрытий
        if(Menu.type == 1)
        {
            for (int i = 0; i < startOverlaps; i++)
            {
                OverlapsBarPlayer[i].sprite = Tic;
                OverlapsBarOpponent[i].sprite = Tac;
            }

            typePlayer = 1;
            typeOpponent = 2;
        }

        if(Menu.type == 2)
        {
            for(int i = 0; i < startOverlaps; i++)
            {
                OverlapsBarPlayer[i].sprite = Tac;
                OverlapsBarOpponent[i].sprite = Tic;
            }

            typePlayer = 2;
            typeOpponent = 1;
        }

        StartCoroutine(StartGame());
    }

    public void Update()
    {
        // Проверка изменения кол-ва перекрытий и отключения лишних спрайтов на панели
        if(Menu.type == 1)
        {
            if (startOverlaps != overlapsTic)
                OverlapsBarPlayer[overlapsTic].gameObject.SetActive(false);
            if (startOverlaps != overlapsTac)
                OverlapsBarOpponent[overlapsTac].gameObject.SetActive(false);
        }

        if (Menu.type == 2)
        {
            if (startOverlaps != overlapsTac)
                OverlapsBarPlayer[overlapsTac].gameObject.SetActive(false);
            if (startOverlaps != overlapsTic)
                OverlapsBarOpponent[overlapsTic].gameObject.SetActive(false);
        }
    }

    private IEnumerator StartGame()
    {
        while (CheckScript.StatusGame() == 0)
        {
            if (CheckMove(typePlayer) == true)
            {
                move = true;
                yield return new WaitUntil(() => move == false);

                if (CheckMove(typeOpponent) == true)
                {
                    Invoke("SelectionGame", 0.5f);
                }

                PrintStatusGame();
            }
            else if (CheckMove(typeOpponent) == true)
            {
                Invoke("SelectionGame", 0.5f);
            }
        }
        // Логика игры для соперника при отсутствии хода игрока!!!
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

        // Включение спрайтов на панели
        for (int i = 0; i < startOverlaps; i++)
        {
            OverlapsBarPlayer[i].gameObject.SetActive(true);
            OverlapsBarOpponent[i].gameObject.SetActive(true);
        }

        Start();
    }

    private bool CheckMove(int type) // Проверка на возможность сделать ход
    {
        Sprite enemy = null; 
        int playerOverlaps = 0;
        bool cellNull = false; // Показатель свободных ячеек для хода
        bool cellEnemy = false; // Показатель занятых ячеек для хода

        if(type == 1) // Tic
        {
            enemy = Tac;
            playerOverlaps = overlapsTic;
        }
        if(type == 2) // Tac
        {
            enemy = Tic;
            playerOverlaps = overlapsTac;
        }

        for(int i = 0; i < 9; i++)
        {
            if (Cell[i].sprite == null)
                cellNull = true;
            if (Cell[i].sprite == enemy && playerOverlaps > 0)
                cellEnemy = true;
        }

        if (cellNull == true || cellEnemy == true)
            return true;
        else
            return false;
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
