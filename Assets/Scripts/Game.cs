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
    public static int overlapsTic; // ���-�� ��������� ����������
    public static int overlapsTac; //

    private string typePlayer;    //
    private string typeOpponent;  // ����� ��� ������ � ���������

    [SerializeField] private EasyDifficultyLevel EasyLevelScript;
    [SerializeField] private NormalDifficultyLevel NormalLevelScript;
    [SerializeField] private ChallengingDifficultyLevel ChallengLevelScript;
    [SerializeField] private CheckGame CheckScript;
    [SerializeField] private OverlapsBar OverlapsBar;

    public void Start()
    {
        startOverlaps = 3; // ��������� ���-�� ����������� ����������
        overlapsTic = startOverlaps; // ��������� ���-�� ����������
        overlapsTac = startOverlaps; //

        OverlapsBar.ConnectingOverlapPanel(); // 

        MessagePanel.text = "���� ��������!";
        ShowLevel();

        // ������ ��� ���������
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

        // ����������� ���� �������
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
            Debug.LogWarning("��� ������ ����������!");
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
                Debug.LogWarning("������ � StartGame");
                break;
            }
            //
        }
    }

    private void SelectionGame() // ����� ���� � ������ ����
    {
        //������������ �� 'Tic', � ��������� �� 'Tac'
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
                Debug.LogWarning("������� �� ��������");
        }
        // ������������ �� 'Tac', � ��������� �� 'Tic'
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
            Debug.LogWarning("������ �� ������� ��������� 'IsPossibleToMakeMove'. ������� ��������: " + type);
            return false;
        }
    }

    private void ShowLevel()
    {
        if (Menu.level == 1)
            GameTypePanel.text = "������";
        else if (Menu.level == 2)
            GameTypePanel.text = "����������";
        else if (Menu.level == 3)
            GameTypePanel.text = "�������";
        else
            GameTypePanel.text = "?";
    }
}
