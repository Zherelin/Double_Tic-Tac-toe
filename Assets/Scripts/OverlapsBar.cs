using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlapsBar : MonoBehaviour
{
    public Game GameScript;
    public Image[] OverlapsBarPlayer;   // ќтображение кол-во перекрытий
    public Image[] OverlapsBarOpponent; // на панели

    private Sprite _player;
    private Sprite _opponent;

    public void ConnectingOverlapPanel()
    {
        if(Menu.type == 1)
        {
            _player = GameScript.Tic;
            _opponent = GameScript.Tac;
        }
        else if (Menu.type == 2)
        {
            _player = GameScript.Tac;
            _opponent = GameScript.Tic;
        }

        for(int number = 0; number < GameScript.startOverlaps; number++)
        {
            OverlapsBarPlayer[number].gameObject.SetActive(true);
            OverlapsBarOpponent[number].gameObject.SetActive(true);

            OverlapsBarPlayer[number].sprite = _player;
            OverlapsBarOpponent[number].sprite = _opponent;
        }
    }

    void Update()
    {
        // ѕроверка изменени€ кол-ва перекрытий и отключени€ лишних спрайтов на панели
        if (Menu.type == 1)
        {
            if (GameScript.startOverlaps != Game.overlapsTic)
                OverlapsBarPlayer[Game.overlapsTic].gameObject.SetActive(false);
            if (GameScript.startOverlaps != Game.overlapsTac)
                OverlapsBarOpponent[Game.overlapsTac].gameObject.SetActive(false);
        }

        if (Menu.type == 2)
        {
            if (GameScript.startOverlaps != Game.overlapsTac)
                OverlapsBarPlayer[Game.overlapsTac].gameObject.SetActive(false);
            if (GameScript.startOverlaps != Game.overlapsTic)
                OverlapsBarOpponent[Game.overlapsTic].gameObject.SetActive(false);
        }
    }
}
