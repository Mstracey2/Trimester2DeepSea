using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Settings : MonoBehaviour
{

    public float volume = 100; //Percentage that the volume is set as

    public Sprite mutedSprite; //Sprite to show the audio is muted
    public Sprite unmutedSprite; //Sprite to show the audio is un muted

    public Image muteButton; 


    [SerializeField] private TextMeshProUGUI volumeText; //Displays the current volume percentage

    
    void Start()
    {
        volume = PlayerPrefs.GetFloat("VolumeValue") * 100; //Get the current volume from player prefs

        if (PlayerPrefs.GetInt("VolumeMuted") == 1) //If the player muted the volume...
        {
            muteButton.sprite = unmutedSprite; 
            volumeText.text = "*Muted*"; //Set the text to Muted
        }
        else //Else the volume isn't muted
        {
            muteButton.sprite = mutedSprite;
            volumeText.text = volume.ToString() + "%"; //Set the text to the current volume
        }
    }
    /// <summary>
    /// Add to the current volume, ran by the button on the settings page
    /// </summary>
    public void addVolume()
    {
        if (PlayerPrefs.GetInt("VolumeMuted") == 1) //If is currently muted...
        {
            toggleMute(); //Unmute it
        }
        if (volume <= 190) //If the volume is less than 190...
        {
            volume += 10; //Add 10
        }

        PlayerPrefs.SetFloat("VolumeValue", volume / 100); //Save the new value
        PlayerPrefs.Save();
        volumeText.text = volume.ToString() + "%"; //Set the text to current value
    }
    /// <summary>
    /// Reduce the current volume, ran by the button on the settings page
    /// </summary>
    public void reduceVolume()
    {
        if (PlayerPrefs.GetInt("VolumeMuted") == 1) //If is currently muted...
        {
            toggleMute(); //Unmute it
        }
        if (volume >= 10) //If bigger than 10
        {
            volume -= 10; //Remove 10
        }

        PlayerPrefs.SetFloat("VolumeValue", volume / 100); //Save it to player prefs
        PlayerPrefs.Save();
        volumeText.text = volume.ToString() + "%"; //Set the text to current value
    }

    /// <summary>
    /// Toggles between muted and unmuted
    /// </summary>
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

    /// <summary>
    /// Clear the playerprefs and restart the scene. Ran by a button in settings.
    /// </summary>
    public void resetSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("ActiveMechColour", 1000);
        PlayerPrefs.SetInt("ActiveDeskToy", 1000);
        SceneManager.LoadScene(0);
    }
}
