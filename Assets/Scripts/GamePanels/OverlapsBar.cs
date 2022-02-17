using UnityEngine;
using UnityEngine.UI;

public class OverlapsBar : MonoBehaviour
{
    [SerializeField] private Game GameScript;
    [SerializeField] private Image[] OverlapsBarPlayer;   // Отображение кол-во перекрытий
    [SerializeField] private Image[] OverlapsBarOpponent; // на панели

    private Sprite _player;
    private Sprite _opponent;

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

        for (int number = 0; number < GameScript.startOverlaps; number++)
        {
            OverlapsBarPlayer[number].sprite = _player;
            OverlapsBarOpponent[number].sprite = _opponent;
        }
    }

    public void DisplayingOverlapsBar()
    {
        // Переделать логику !!!
        // 
        // Проверка изменения кол-ва перекрытий и отключения лишних спрайтов на панели
        if (GameScript.overlapsPlayer != GameScript.startOverlaps && GameScript.overlapsPlayer >= 0)
        {
            OverlapsBarPlayer[GameScript.overlapsPlayer].gameObject.SetActive(false);

            // TEST
            Debug.Log("OverlapsBarPlayer - 1");
            //
        }

        if (GameScript.overlapsOpponent != GameScript.startOverlaps && GameScript.overlapsOpponent >= 0)
        {
            OverlapsBarOpponent[GameScript.overlapsOpponent].gameObject.SetActive(false);

            // TEST
            Debug.Log("OverlapsBarOpponent - 1");
            //
        }
    }

        public void ResettingOverlapsBar()
    {
        for (int number = 0; number < GameScript.startOverlaps; number++)
        {
            OverlapsBarPlayer[number].gameObject.SetActive(true);
            OverlapsBarOpponent[number].gameObject.SetActive(true);
        }
    }
}
