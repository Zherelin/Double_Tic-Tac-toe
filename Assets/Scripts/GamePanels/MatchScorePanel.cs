using UnityEngine;
using UnityEngine.UI;

public class MatchScorePanel : MonoBehaviour
{
    [SerializeField] private Game GameScript;

    [SerializeField] private Image Player;
    [SerializeField] private Image Opponent;
    [SerializeField] private Text ScorePlayer;
    [SerializeField] private Text ScoreOpponent;

    public void ConnectionMatchScorePanel()
    {
        if (GameScript.TypePlayer == "tic")
        {
            Player.sprite = GameScript.Tic;
            Opponent.sprite = GameScript.Tac;
        }
        else if (GameScript.TypePlayer == "tac")
        {
            Player.sprite = GameScript.Tac;
            Opponent.sprite = GameScript.Tic;
        }

        ShowMatchScore();
    }

    public void ShowMatchScore()
    {
        ScorePlayer.text = GameScript.WinsPlayer.ToString();
        ScoreOpponent.text = GameScript.WinsOpponent.ToString();
    }
}
