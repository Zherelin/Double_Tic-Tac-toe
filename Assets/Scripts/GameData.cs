using UnityEngine;

public class GameData : MonoBehaviour
{
    public void SaveAudioStatus(bool status)
    {
        if(status == true)
        {
            PlayerPrefs.SetString("AudioStatus", "true");
            PlayerPrefs.Save();
        }
        else if(status == false)
        {
            PlayerPrefs.SetString("AudioStatus", "false");
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("Статус аудио неопределён 'SaveAudioStatus'");
        }
    }

    public bool LoadAudioStatus()
    {
        if(PlayerPrefs.HasKey("AudioStatus"))
        {
            if(PlayerPrefs.GetString("AudioStatus") == "true")
            {
                return true;
            }
            else if (PlayerPrefs.GetString("AudioStatus") == "false")
            {
                return false;
            }
            else
            {
                Debug.LogWarning("Статус аудио неопределён 'LoadAudioStatus'");
                return true;
            }
        }
        else
        {
            Debug.Log("Состояние аудио ещё не было сохранено");
            return true;
        }
    }
}
