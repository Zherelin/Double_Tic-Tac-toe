using UnityEngine;
using UnityEngine.UI;
using EnumGame;

public class ResultGamePanel : MonoBehaviour
{
    [SerializeField] private Game _gameScript;
    [SerializeField] private CheckGame _checkScript;
    [SerializeField] private EmergenceUI _emergenceUI;

    [SerializeField] private Image _resultPanel;
    [SerializeField] private GameObject _resultGame;
    [SerializeField] private GameObject _resultMatch;
    [SerializeField] private Text _textResultGame;
    [SerializeField] private Text _textResultMatch;
    [SerializeField] private Text _ScorePlayer;
    [SerializeField] private Text _ScoreOpponent;

    private Coroutine _emergencePanel;

    public void ShowResultGamePanel()
    {
        gameObject.SetActive(true);
        _resultGame.SetActive(true);

        if (_checkScript.StatusGame() == GameState.VictoryPlayer)
        {
            _textResultGame.text = "�� ��������!";
        }
        else if (_checkScript.StatusGame() == GameState.VictoryOpponent)
        {
            _textResultGame.text = "�� ���������!";
        }
        else if (_checkScript.StatusGame() == GameState.Draw)
        {
            _textResultGame.text = "�����!";
        }
        else
        {
            _textResultGame.text = "���� �����!";
        }

        _emergencePanel = StartCoroutine(_emergenceUI.EmergenceImage(_resultPanel));
    }

    public void ShowResultMatchPanel()
    {
        gameObject.SetActive(true);
        _resultMatch.SetActive(true);

        if (_gameScript.WinsPlayer == _gameScript.WinsFinal)
        {
            _textResultMatch.text = "������";
        }
        else if (_gameScript.WinsOpponent == _gameScript.WinsFinal)
        {
            _textResultMatch.text = "��������";
        }
        else
        {
            _textResultMatch.text = "���� �����";
        }

        _ScorePlayer.text = _gameScript.WinsPlayer.ToString();
        _ScoreOpponent.text = _gameScript.WinsOpponent.ToString();

        _emergencePanel = StartCoroutine(_emergenceUI.EmergenceImage(_resultPanel));
    }

    public void OnClickButton()
    {
        StopCoroutine(_emergencePanel);
        _resultGame.SetActive(false);
        _resultMatch.SetActive(false);
        gameObject.SetActive(false);
    }
}
