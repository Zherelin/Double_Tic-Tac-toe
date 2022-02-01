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
    public Image[] OverlapsBarPlayer;   // ����������� ���-�� ����������
    public Image[] OverlapsBarOpponent; // �� ������

    public Text MessagePanel;
    public Text GameTypePanel;

    [HideInInspector] public bool move; // ���������� �� ���
    public static int overlapsTic; // ���-�� ��������� ����������
    public static int overlapsTac; //
    private int startOverlaps; // ��������� ���-�� ���������� ��� �������� ��������� (�����. �� ������)

    private int typePlayer;    //
    private int typeOpponent;  // ����� ��� ������ � ���������

    [SerializeField] private EasyDifficultyLevel EasyLevelScript;
    [SerializeField] private NormalDifficultyLevel NormalLevelScript;
    [SerializeField] private ChallengingDifficultyLevel ChallengLevelScript;
    [SerializeField] private CheckGame CheckScript;

    public void Start()
    {
        startOverlaps = 3; // ��������� ���-�� ����������� ���������� �� ������
        overlapsTic = startOverlaps; // ��������� ���-�� ����������
        overlapsTac = startOverlaps; //

        MessagePanel.text = "���� ��������!";
        ShowLevel();

        // ������ ��� ���������
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

        // ���������� ���� ����������� ����������
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
        // �������� ��������� ���-�� ���������� � ���������� ������ �������� �� ������
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
        // ������ ���� ��� ��������� ��� ���������� ���� ������!!!
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

        // ��������� �������� �� ������
        for (int i = 0; i < startOverlaps; i++)
        {
            OverlapsBarPlayer[i].gameObject.SetActive(true);
            OverlapsBarOpponent[i].gameObject.SetActive(true);
        }

        Start();
    }

    private bool CheckMove(int type) // �������� �� ����������� ������� ���
    {
        Sprite enemy = null; 
        int playerOverlaps = 0;
        bool cellNull = false; // ���������� ��������� ����� ��� ����
        bool cellEnemy = false; // ���������� ������� ����� ��� ����

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
            GameTypePanel.text = "������";
        else if (Menu.level == 2)
            GameTypePanel.text = "����������";
        else if (Menu.level == 3)
            GameTypePanel.text = "�������";
        else
            GameTypePanel.text = "?";
    }
}
