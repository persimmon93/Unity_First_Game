using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script will go inside the volume slider. The AudioManager audio channel channel
/// is the name of audiomixer. So Master/Music/Sound.
/// </summary>
public class VolumeSlider : MonoBehaviour
{
    public AudioManager.AudioChannel channel;
    public Text soundLevelText;
    public string channelName;

    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        switch (slider.name)
        {
            case "Master Volume":
                slider.value = PlayerPrefs.GetFloat("SavedMasterVolume");
                soundLevelText.text = channelName + ": " + (int)(PlayerPrefs.GetFloat("SavedMasterVolume") * 100) + " / " + "100";
                break;

            case "Sound Volume":
                slider.value = PlayerPrefs.GetFloat("SavedSoundVolume");
                soundLevelText.text = channelName + ": " + (int)(PlayerPrefs.GetFloat("SavedSoundVolume") * 100) + " / " + "100";
                break;

            case "Music Volume":
                slider.value = PlayerPrefs.GetFloat("SavedMusicVolume");
                soundLevelText.text = channelName + ": " + (int)(PlayerPrefs.GetFloat("SavedMusicVolume") * 100) + " / " + "100";
                break;
        }
    }

    public void UpdateVolumeSlider()
    {
        if (slider != null)
        {
            int sliderValue = (int)(slider.value * 100);
            AudioManager.Instance.SetVolume(channel, sliderValue);
            soundLevelText.text = channelName + ": " + sliderValue + " / " + "100";
        } else
        {
            Debug.LogWarning("The " + slider + "does not exist.");
        }
        
    }
}
