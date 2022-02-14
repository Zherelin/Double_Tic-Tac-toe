using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnumGame;

public class Game : MonoBehaviour
{
    public Sprite Tic;        // ���������� ��� ��������
    public Sprite Tac;        //
    public Sprite ClosingTic; //
    public Sprite ClosingTac; // 
    public Image[] Cell; // ������� ����

    [SerializeField] private Text MessagePanel;

    [HideInInspector] public bool move; // ���������� �� ���
    [HideInInspector] public int startOverlaps; // ��������� ���-�� ���������� ��� �������� ���������
    [HideInInspector] public int overlapsPlayer; // ���-�� ��������� ����������
    [HideInInspector] public int overlapsOpponent; //

    private string _typePlayer = "";    //
    private string _typeOpponent = "";  // ����� ��� ������ � ���������
    private int _playerWins;   //
    private int _opponentWins; // ����������� ���������� ����� � ��������� ������� �����
    private Coroutine _startGame;
    private Coroutine _startMatch;

    // ��������
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

    public string TypePlayer
    {
        get { return _typePlayer; }
    }
    public string TypeOpponent
    {
        get { return _typeOpponent; }
    }

    public IEnumerator ShowMessage(string message)
    {
        MessagePanel.text = message;
        yield return new WaitForSeconds(2f);
        MessagePanel.text = "";
    }

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
            // ����������� ���� �������
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

            startOverlaps = 3;

            OverlapsBarScript.ConnectingOverlapPanel();
            MatchScorePanelScript.ConnectionMatchScorePanel();
            GameInformationPanelScript.ConnectionGameInformationPanel(Menu.level, Menu.type);

            _startMatch = StartCoroutine(StartMatch());
        }
        else
            Debug.LogWarning("��� ������ ����������!");
    }

    private IEnumerator StartMatch()
    {
        // ����������� ���������� ����� � ��������� ������� �����
        _playerWins = 0;
        _opponentWins = 0;
        int finalWins = 5;
        GameState gameState = CheckScript.StatusGame();

        //TEST
        int test = 0;
        //

        while(_playerWins != finalWins && _opponentWins != finalWins)
        {
            MatchScorePanelScript.ShowMatchScore();

            _startGame = StartCoroutine(StartGame());
            yield return new WaitUntil(() => CheckScript.StatusGame() != GameState.Continues);

            if (CheckScript.StatusGame() == GameState.VictoryPlayer)
            {
                _playerWins++;
                // Test
                Debug.Log("Victory Player: playerWins = " + _playerWins);
                //
            }
            else if (CheckScript.StatusGame() == GameState.VictoryOpponent)
            {
                _opponentWins++;
                // TEST
                Debug.Log("Victory Opponent: opponentWins = " + _opponentWins);
                //
            }

            ResultGamePanelScript.ShowResultGamePanel();
            yield return new WaitUntil(() => ResultGamePanelScript.gameObject.activeInHierarchy == false);

            //------------
            // ������� ����
            StopCoroutine(_startGame);

            for (int i = 0; i < 9; i++)
                Cell[i].sprite = null;

            overlapsPlayer = startOverlaps;
            overlapsOpponent = startOverlaps;
            OverlapsBarScript.ResettingOverlapsBar();
            //------------

            // TEST
            test++;
            if(test > 10)
            {
                Debug.LogWarning("������ � 'StartMatch'");
                break;
            }
            //
        }
        MatchScorePanelScript.ShowMatchScore();

        if (_playerWins == finalWins)
        {
            StartCoroutine(ShowMessage("� ����� ������� �����"));
        }
        else if(_opponentWins == finalWins)
        {
            StartCoroutine(ShowMessage("� ����� ������� ��������"));
        }

        // ��������� ������ ������� ����� !!!
    }

    private IEnumerator StartGame()
    {
        overlapsPlayer = startOverlaps;
        overlapsOpponent = startOverlaps;

        // ������ ��� ���������
        if (_typeOpponent == "tic")
        {
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
                Debug.LogWarning("������ � StartGame");
                break;
            }
            //
        }
    }

    private void SelectionGame() // ����� ������ ����
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
                Debug.LogWarning("������� �� ��������");
        }
        else
            Debug.LogWarning("��� ������ �� ��������");
    }

    private bool IsPossibleToMakeMove(string type) // �������� �� ����������� ������� ���
    {
        Sprite opponent = null; 
        int playerOverlaps = 0;
        bool isEmptyCell = false; // ���������� ��������� ����� ��� ����
        bool isOpponentCell = false; // ���������� ������� ����� ��� ����

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
            Debug.LogWarning("������ �� ������� ��������� 'IsPossibleToMakeMove'. ������� ��������: " + type);
            return false;
        }
    }
}
