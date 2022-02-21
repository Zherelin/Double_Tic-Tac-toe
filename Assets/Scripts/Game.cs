using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnumGame;

public class Game : MonoBehaviour
{
    public Sprite Tic;        // Переменные для спрайтов
    public Sprite Tac;        //
    public Sprite ClosingTic; //
    public Sprite ClosingTac; // 
    public Image[] Cell; // Игровое поле

    [SerializeField] private Text MessagePanel;

    [HideInInspector] public bool move; // Разрешение на ход

    private int _startOverlaps;
    private int _overlapsPlayer;
    private int _overlapsOpponent;

    private string _typePlayer = "";
    private string _typeOpponent = "";

    private int _playerWins;   //
    private int _opponentWins; // Определение начального счёта и финальной границы матча
    private int _finalWins;    //

    private bool _isGameGoingOn;
    private Coroutine _startGame;
    private Coroutine _startMatch;

    // Свойства
    public int WinsPlayer
    {
        get { return _playerWins; }
        set { _playerWins = value; }
    }
    public int WinsOpponent
    {
        get { return _opponentWins; }
        set { _opponentWins = value; }
    }
    public int WinsFinal
    {
        get { return _finalWins; }
    }

    public string TypePlayer
    {
        get { return _typePlayer; }
    }
    public string TypeOpponent
    {
        get { return _typeOpponent; }
    }

    public int StartOverlaps
    {
        get { return _startOverlaps; }
    }
    public int OverlapsPlayer
    {
        get { return _overlapsPlayer; }
        set
        {
            if (value >= 0)
                _overlapsPlayer = value;
            else
                Debug.LogWarning("Получение недопустимого значения перекрытий 'Player'");
        }
    }
    public int OverlapsOpponent
    {
        get { return _overlapsOpponent; }
        set
        {
            if (value >= 0)
                _overlapsOpponent = value;
            else
                Debug.LogWarning("Получение недопустимого значения перекрытий 'Opponent'");
        }
    }
    //

    [SerializeField] private EasyDifficultyLevel EasyLevelScript;
    [SerializeField] private NormalDifficultyLevel NormalLevelScript;
    [SerializeField] private ChallengingDifficultyLevel ChallengLevelScript;
    [SerializeField] private CheckGame CheckScript;
    [SerializeField] private OverlapsBar OverlapsBarScript;
    [SerializeField] private MatchScorePanel MatchScorePanelScript;
    [SerializeField] private GameInformationPanel GameInformationPanelScript;
    [SerializeField] private ResultGamePanel ResultGamePanelScript;

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

            _startOverlaps = 3;

            OverlapsBarScript.ConnectingOverlapPanel();
            MatchScorePanelScript.ConnectionMatchScorePanel();
            GameInformationPanelScript.ConnectionGameInformationPanel(Menu.level, Menu.type);

            _startMatch = StartCoroutine(StartMatch());
        }
        else
            Debug.LogWarning("Тип игрока неопределён!");
    }

    private IEnumerator StartMatch()
    {
        // Определение начального счёта и финальной границы матча
        _playerWins = 0;
        _opponentWins = 0;
        _finalWins = 5;
        GameState gameState = CheckScript.StatusGame();

        //TEST
        int test = 0;
        //

        StartCoroutine(ShowMessage("Матч Начался!"));
        while(_playerWins != _finalWins && _opponentWins != _finalWins)
        {
            MatchScorePanelScript.ShowMatchScore();

            _isGameGoingOn = true;
            _startGame = StartCoroutine(StartGame());
            yield return new WaitUntil(() => _isGameGoingOn == false);

            // Подсчёт очков и их вывод
            if (CheckScript.StatusGame() == GameState.VictoryPlayer)
            {
                _playerWins++;
            }
            else if (CheckScript.StatusGame() == GameState.VictoryOpponent)
            {
                _opponentWins++;
            }
            MatchScorePanelScript.ShowMatchScore();

            // Отображение панели результата игры
            if (_playerWins < _finalWins && _opponentWins < _finalWins)
            {
                ResultGamePanelScript.ShowResultGamePanel();
            }
            else
            {
                ResultGamePanelScript.ShowResultMatchPanel();
            }
            yield return new WaitUntil(() => ResultGamePanelScript.gameObject.activeInHierarchy == false);

            // Очистка поля
            StopCoroutine(_startGame);
            ClearingField();
            OverlapsBarScript.ResettingOverlapsBar();

            // TEST
            test++;
            if(test > 10)
            {
                Debug.LogWarning("Ошибка в 'StartMatch'");
                break;
            }
            //
        }

        RestartMatch();
    }

    private IEnumerator StartGame()
    {
        _overlapsPlayer = _startOverlaps;
        _overlapsOpponent = _startOverlaps;

        // Первых ход крестиков
        if (_typeOpponent == "tic")
        {
            move = false;

            if (Menu.level == "easy")
            {
                yield return new WaitForSeconds(0.5f);

                Cell[Random.Range(0, 8)].sprite = Tic;
            }
            else if (Menu.level == "normal" || Menu.level == "challeng")
            {
                yield return new WaitForSeconds(0.5f);

                int randomCell = Random.Range(0, 8);
                while (randomCell == 4)
                    randomCell = Random.Range(0, 8);
                Cell[randomCell].sprite = Tic;
            }
        }

        int test = 0; // TEST

        while (CheckScript.StatusGame() == GameState.Continues)
        {
            bool isMovePlayer = IsPossibleToMakeMove(_typePlayer);
            bool isMoveOpponent = IsPossibleToMakeMove(_typeOpponent);

            if (isMovePlayer == true)
            {
                move = true;
                yield return new WaitUntil(() => move == false);
                OverlapsBarScript.DisplayingOverlapsBar();

                if (IsPossibleToMakeMove(_typeOpponent) == true)
                {
                    yield return new WaitForSeconds(0.5f);
                    SelectionGame();
                }
            }
            else if (isMoveOpponent == true)
            {
                yield return new WaitForSeconds(1f);
                SelectionGame();
            }

            OverlapsBarScript.DisplayingOverlapsBar();

            // TEST
            test++;
            if(test > 50)
            {
                Debug.LogWarning("Ошибка в StartGame");
                break;
            }
            //
        }

        _isGameGoingOn = false;
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
                playerOverlaps = _overlapsPlayer;
            }
            if (type == "tac")
            {
                opponent = Tic;
                playerOverlaps = _overlapsOpponent;
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

    private void ClearingField()
    {
        for (int i = 0; i < 9; i++)
            Cell[i].sprite = null;

        _overlapsPlayer = _startOverlaps;
        _overlapsOpponent = _startOverlaps;
    }

    public void RestartMatch()
    {
        StopAllCoroutines();
        ClearingField();
        OverlapsBarScript.ResettingOverlapsBar();
        _startMatch = StartCoroutine(StartMatch());
    }

    public IEnumerator ShowMessage(string message)
    {
        MessagePanel.text = message;
        yield return new WaitForSeconds(2f);
        MessagePanel.text = "";
    }
}
