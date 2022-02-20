using UnityEngine;
using UnityEngine.UI;

public class OverlapsBar : MonoBehaviour
{
    [SerializeField] private Game GameScript;
    [SerializeField] private Image[] OverlapsBarPlayer;   // ����������� ���-�� ����������
    [SerializeField] private Image[] OverlapsBarOpponent; // �� ������

    private Sprite _player;
    private Sprite _opponent;

    private int _currentOverlapsPlayer;
    private int _currentOverlapsOpponent;

    public void ConnectingOverlapPanel()
    {
        if (GameScript.TypePlayer == "tic")
        {
            _player = GameScript.Tic;
            _opponent = GameScript.Tac;
        }
        else if (GameScript.TypePlayer == "tac")
        {
            _player = GameScript.Tac;
            _opponent = GameScript.Tic;
        }

        _currentOverlapsPlayer = GameScript.StartOverlaps;
        _currentOverlapsOpponent = GameScript.StartOverlaps;

        for (int number = 0; number < GameScript.StartOverlaps; number++)
        {
            OverlapsBarPlayer[number].sprite = _player;
            OverlapsBarOpponent[number].sprite = _opponent;
        }
    }

    // �������� ��������� ���-�� ���������� � ���������� ������ �������� �� ������
    //
    public void DisplayingOverlapsBar()
    {
        if (GameScript.OverlapsPlayer != _currentOverlapsPlayer && GameScript.OverlapsPlayer >= 0)
        {
            OverlapsBarPlayer[GameScript.OverlapsPlayer].gameObject.SetActive(false);
            _currentOverlapsPlayer--;
        }

        if (GameScript.OverlapsOpponent != _currentOverlapsOpponent && GameScript.OverlapsOpponent >= 0)
        {
            OverlapsBarOpponent[GameScript.OverlapsOpponent].gameObject.SetActive(false);
            _currentOverlapsOpponent--;
        }
    }

    public void ResettingOverlapsBar()
    {
        _currentOverlapsPlayer = GameScript.StartOverlaps;
        _currentOverlapsOpponent = GameScript.StartOverlaps;

        for (int number = 0; number < GameScript.StartOverlaps; number++)
        {
            OverlapsBarPlayer[number].gameObject.SetActive(true);
            OverlapsBarOpponent[number].gameObject.SetActive(true);
        }
    }
}
