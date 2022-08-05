using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSaver : MonoBehaviour
{
    Slider slider;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundSlider;

    public void VolumeSavedbutton()
    {
        PlayerPrefs.SetFloat("SavedMasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("SavedMusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SavedSoundVolume", soundSlider.value);
    }
}