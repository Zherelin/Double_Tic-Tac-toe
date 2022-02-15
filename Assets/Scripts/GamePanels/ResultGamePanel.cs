using UnityEngine;
using UnityEngine.UI;
using EnumGame;
using System.Collections;

public class ResultGamePanel : MonoBehaviour
{
    [SerializeField] private CheckGame CheckScript;
    [SerializeField] private Text ResultGame;
    [SerializeField] private Image BackgroundPanel;

    private float _alpha = 0f;
    private float _alphaFinal = 0.6f;
    private float _timeUpdateAlpha = 0.01f;
    private float _stepForAlpha = 0.02f;
    private Color _background—olor;

    private Coroutine _emergencePanel;

    private void Awake()
    {
        _background—olor = BackgroundPanel.color;
        _background—olor.a = _alpha;
    }

    public void ShowResultGamePanel()
    {
        gameObject.SetActive(true);

        _alpha = 0f;
        BackgroundPanel.color = _background—olor;

        if (CheckScript.StatusGame() == GameState.VictoryPlayer)
        {
            ResultGame.text = "¬€ œŒ¡≈ƒ»À»!";
        }
        else if (CheckScript.StatusGame() == GameState.VictoryOpponent)
        {
            ResultGame.text = "¬€ œ–Œ»√–¿À»!";
        }
        else if (CheckScript.StatusGame() == GameState.Draw)
        {
            ResultGame.text = "Õ»◊‹ﬂ!";
        }
        else
        {
            ResultGame.text = "’–≈Õ «Õ¿≈“!";
        }

        _emergencePanel = StartCoroutine(EmergencePanel());
    }

    private IEnumerator EmergencePanel()
    {
        while(BackgroundPanel.color.a < _alphaFinal)
        {
            _alpha += _stepForAlpha;
            BackgroundPanel.color = new Color(_background—olor.r, _background—olor.g, _background—olor.b, _alpha);
            yield return new WaitForSeconds(_timeUpdateAlpha);
        }
    }

    public void OnClickButton()
    {
        StopCoroutine(_emergencePanel);
        gameObject.SetActive(false);
    }
}
