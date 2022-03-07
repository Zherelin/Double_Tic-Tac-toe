using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private SwitchingBetweenImage _switchImageSound;
    [SerializeField] private GameData _gameData;
    private bool _status = true;

    private void Start()
    {
        _status = _gameData.LoadAudioStatus();
        _switchImageSound.ImageSwitch(_status);

        AudioStatus();
    }

    public void SoundSwitch()
    {
        _status = !_status;

        _switchImageSound.ImageSwitch(_status);
        AudioStatus();

        _gameData.SaveAudioStatus(_status);
    }

    private void AudioStatus()
    {
        if(_status == true)
        {
            _mixer.audioMixer.SetFloat("VolumeSounds", 0);
        }
        else if(_status == false)
        {
            _mixer.audioMixer.SetFloat("VolumeSounds", -80);
        }
        else
        {
            Debug.LogWarning("Статус аудио неопределён 'AudioStatus'");
        }
    }
}
