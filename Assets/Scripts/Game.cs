using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour
{
    public static int overlapsTic; // Кол-во возможных перекрытий
    public static int overlapsTac; //
    int startOverlaps; // Стартовое кол-во перекрытий для проверки изменений (отобр. на панели)

    public Sprite Tic;        // Переменные для спрайтов
    public Sprite Tac;        //
    public Sprite ClosingTic; //
    public Sprite ClosingTac; //
    public Image[] Cell; // Игровое поле
    public Image[] OverlapsBarPlayer;   // Отображение кол-во перекрытий
    public Image[] OverlapsBarOpponent; // на панели

    public Text panel; // ???
    public bool move; // Разрешение на ход
    private int typePlayer; //
    private int typeEnemy;  // Какой тип игрока и соперника

    public EazyTicGame eazyTic; // Подключение скриптов
    public EazyTacGame eazyTac; //
    public NormTicGame normTic; //
    public NormTacGame normTac; //
    public HardTicGame hardTic; //

    public void Start()
    {
        startOverlaps = 3; // Начальное кол-во отображений перекрытий на панели
        overlapsTic = startOverlaps; // Начальное кол-во перекрытий
        overlapsTac = startOverlaps; //
        panel.text = "Игра началась!";

        if (Menu.type == 2 && Menu.level == 1)
            Cell[4].sprite = Tic;
        if(Menu.type == 2 && Menu.level == 2)
        {
            int randomCell = Random.Range(0, 8);
            while(Cell[randomCell].sprite != null || randomCell == 4)
                randomCell = Random.Range(0, 8);
            Cell[randomCell].sprite = Tic;
        }

        // !!! Дописать логику постановки крестика на сложных уровнях !!!

        // Назначение типа отображения перекрытий
        if(Menu.type == 1)
        {
            for (int i = 0; i < startOverlaps; i++)
            {
                OverlapsBarPlayer[i].sprite = Tic;
                OverlapsBarOpponent[i].sprite = Tac;
            }

            typePlayer = 1;
            typeEnemy = 2;
        }

        if(Menu.type == 2)
        {
            for(int i = 0; i < startOverlaps; i++)
            {
                OverlapsBarPlayer[i].sprite = Tac;
                OverlapsBarOpponent[i].sprite = Tic;
            }

            typePlayer = 2;
            typeEnemy = 1;
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
        while (eazyTic.checkScript.StatusGame() == 0)
        {
            if (CheckMove(typePlayer) == true)
            {
                move = true;
                yield return new WaitUntil(() => move == false);

                if (CheckMove(typeEnemy) == true)
                {
                    Invoke("SelectionGame", 0.5f);
                }

                PrintStatusGame();
            }
            else if (CheckMove(typeEnemy) == true)
            {
                Invoke("SelectionGame", 0.5f);
            }
        }
        // Логика игры для соперника при отсутствии хода игрока!!!
    }

    private void SelectionGame() // Выбор типа и уровня игры
    {
        //Tic
        if (Menu.type == 1)
        {
            if (Menu.level == 1)
            {
                eazyTic.PlayGame();
            }
            else if (Menu.level == 2)
            {
                normTic.PlayGame();
            }
            else if (Menu.level == 3)
            {
                hardTic.PlayGame();
            }
            else
                Debug.Log("Error: Level?");
        }
        //Tac
        else if (Menu.type == 2)
        {
            if (Menu.level == 1)
            {
                eazyTac.PlayGame();
            }
            else if (Menu.level == 2)
            {
                normTac.PlayGame();
            }
            else if (Menu.level == 3)
            {
                Debug.Log("He!");
            }
            else
                Debug.Log("Error: Level?");
        }
        else
            Debug.Log("Error: Type Player?");


        PrintStatusGame();
    }

    private void PrintStatusGame() // Вывод на панель статуса игры
    {
        // Победа TIC
        if (eazyTic.checkScript.StatusGame() == 1)
        {
            panel.text = "Победа Крестиков!";
        }
        // Победа TAC
        else if (eazyTic.checkScript.StatusGame() == 2)
        {
            panel.text = "Победа Ноликов!";
        }
        // Ничья
        else if (eazyTic.checkScript.StatusGame() == 3)
        {
            panel.text = "Ничья!";
        }
    }

    public void BackMenu() // Переход в меню
    { 
        SceneManager.LoadScene(0); 
    }

    public void SetGame() // ??? Временно !!!
    {
        if (Menu.type == 1 || Menu.type == 2)
            panel.text = "Type = " + Menu.type + ": Level = " + Menu.level;
        else
            panel.text = "Error: The type of player is not defined";
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
}
