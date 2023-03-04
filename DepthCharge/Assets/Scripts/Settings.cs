using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{

    public int volume = 100;
    public bool muted;

    public Sprite mutedSprite;
    public Sprite unmutedSprite;

    public Image muteButton;


    [SerializeField] private TextMeshProUGUI volumeText;

    
    void Start()
    {
        volumeText.text = volume.ToString() + "%";
    }

    public void addVolume()
    {
        if (muted)
        {
            toggleMute();
        }
        if (volume <= 190)
        {
            volume += 10;
        }

        volumeText.text = volume.ToString() + "%";
    }
    public void reduceVolume()
    {
        if (muted)
        {
            toggleMute();
        }
        if (volume >= 10)
        {
            volume -= 10;
        }
        volumeText.text = volume.ToString() + "%";
    }

    public void toggleMute()
    {
        if (muted)
        {
            muteButton.sprite = mutedSprite;
            muted = false;
            volumeText.text = volume.ToString() + "%";
        }
        else
        {
            muteButton.sprite = unmutedSprite;
            muted = true;
            volumeText.text = "*Muted*";
        }
    }
}
