using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour
{
    public Sprite Tic;        // ���������� ��� ��������
    public Sprite Tac;        //
    public Sprite ClosingTic; //
    public Sprite ClosingTac; // 
    public Image[] Cell; // ������� ����

    public Text MessagePanel;
    public Text GameTypePanel;

    [HideInInspector] public bool move; // ���������� �� ���
    [HideInInspector] public int startOverlaps; // ��������� ���-�� ���������� ��� �������� ���������
    [HideInInspector] public int overlapsPlayer; // ���-�� ��������� ����������
    [HideInInspector] public int overlapsOpponent; //

    private string _typePlayer = "";    //
    private string _typeOpponent = "";  // ����� ��� ������ � ���������

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

            startOverlaps = 3; // ��������� ���-�� ����������
            OverlapsBarScript.ConnectingOverlapPanel(_typePlayer);

            ShowLevelPanel();

            //// ������ ��� ���������
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
            Debug.LogWarning("��� ������ ����������!");
    }

    private void StartMatch()
    {
        // ����������� ���������� ����� � ��������� ������� �����
        int playerWins = 0;
        int opponentWins = 0;
        int finalWins = 5;

        while(playerWins != finalWins || opponentWins != finalWins)
        {
            // ����� ������� ����������� ����� ����� !!!

            _startGame = StartCoroutine(StartGame());

            // ������ ���������� ���� !!!
        }
        // � � ����� �������� �!

        if(playerWins == finalWins)
        {
            //
        }
        else if(opponentWins == finalWins)
        {
            //
        }

        // ��������� ������ ������� ����� !!!
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


        PrintStatusGame();
    }

    private void PrintStatusGame() // ����� �� ������ ������� ����
    {
        // ������ TIC
        if (CheckScript.StatusGame() == 1)
        {
            MessagePanel.text = "������ ���������!";
        }
        // ������ TAC
        else if (CheckScript.StatusGame() == 2)
        {
            MessagePanel.text = "������ �������!";
        }
        // �����
        else if (CheckScript.StatusGame() == 3)
        {
            MessagePanel.text = "�����!";
        }
    }

    public void BackMenu() // ������� � ����
    { 
        SceneManager.LoadScene(0); 
    }

    public void RestartGame()
    {
        StopCoroutine(_startGame);

        // ������� ����
        for (int i = 0; i < 9; i++)
            Cell[i].sprite = null;

        Start();
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

    private void ShowLevelPanel()
    {
        if (Menu.level == "easy")
        {
            GameTypePanel.text = "������";
        }
        else if (Menu.level == "normal")
        {
            GameTypePanel.text = "����������";
        }
        else if (Menu.level == "challeng")
        {
            GameTypePanel.text = "�������";
        }
        else
            GameTypePanel.text = "?";
    }
}
