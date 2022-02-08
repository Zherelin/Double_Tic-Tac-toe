using UnityEngine;
using UnityEngine.UI;

public class OverlapsBar : MonoBehaviour
{
    public Game GameScript;
    public Image[] OverlapsBarPlayer;   // ����������� ���-�� ����������
    public Image[] OverlapsBarOpponent; // �� ������

    private Sprite _player;
    private Sprite _opponent;

    public void ConnectingOverlapPanel(string typePlayer)
    {
        if (typePlayer == "tic")
        {
            _player = GameScript.Tic;
            _opponent = GameScript.Tac;
        }
        else if (typePlayer == "tac")
        {
            _player = GameScript.Tac;
            _opponent = GameScript.Tic;
        }

        for (int number = 0; number < GameScript.startOverlaps; number++)
        {
            OverlapsBarPlayer[number].sprite = _player;
            OverlapsBarOpponent[number].sprite = _opponent;
        }
    }

    // �������� Update ������, � ����� �������� �� ������� �������?!
    void Update()
    {
        // �������� ��������� ���-�� ���������� � ���������� ������ �������� �� ������
        if (GameScript.overlapsPlayer != GameScript.startOverlaps)
            OverlapsBarPlayer[GameScript.overlapsPlayer].gameObject.SetActive(false);

        if (GameScript.overlapsOpponent != GameScript.startOverlaps)
            OverlapsBarOpponent[GameScript.overlapsOpponent].gameObject.SetActive(false);
    }
}
