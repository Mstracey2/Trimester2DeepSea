using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{

    public float volume = 100;

    public Sprite mutedSprite;
    public Sprite unmutedSprite;

    public Image muteButton;


    [SerializeField] private TextMeshProUGUI volumeText;

    
    void Start()
    {
        volume = PlayerPrefs.GetFloat("VolumeValue") * 100;

        Debug.Log("Volume Muted: " + PlayerPrefs.GetInt("VolumeMuted"));

        if (PlayerPrefs.GetInt("VolumeMuted") == 1)
        {
            muteButton.sprite = unmutedSprite;
            volumeText.text = "*Muted*";
        }
        else
        {
            muteButton.sprite = mutedSprite;
            volumeText.text = volume.ToString() + "%";
        }
    }

    public void addVolume()
    {
        if (PlayerPrefs.GetInt("VolumeMuted") == 1)
        {
            toggleMute();
        }
        if (volume <= 190)
        {
            volume += 10;
        }

        PlayerPrefs.SetFloat("VolumeValue", volume / 100);
        PlayerPrefs.Save();
        volumeText.text = volume.ToString() + "%";
    }

    public void reduceVolume()
    {
        if (PlayerPrefs.GetInt("VolumeMuted") == 1)
        {
            toggleMute();
        }
        if (volume >= 10)
        {
            volume -= 10;
        }

        PlayerPrefs.SetFloat("VolumeValue", volume / 100);
        PlayerPrefs.Save();
        volumeText.text = volume.ToString() + "%";
    }

    public void toggleMute()
    {
        if (PlayerPrefs.GetInt("VolumeMuted") == 1)
        {
            muteButton.sprite = mutedSprite;
            volumeText.text = volume.ToString() + "%";
            PlayerPrefs.SetInt("VolumeMuted", 0);
        }

        else
        {
            muteButton.sprite = unmutedSprite;
            volumeText.text = "*Muted*";
            PlayerPrefs.SetInt("VolumeMuted", 1);
        }

        PlayerPrefs.Save();
    }
}
