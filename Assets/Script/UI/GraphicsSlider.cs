using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;    //For Textmeshpro.

/// <summary>
/// Creates a list of graphics resolution, quality, etc. and handle Video
/// option part.
/// </summary>
public class GraphicsSlider : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown; //Textmesh dropdown.
    //Will store an array of all available resolutions.
    Resolution[] resolutions;
    public Button applyButton;

    private int tempQualityIndex;
    private int tempSetResolution;
    private bool tempSetFullscreen;


    // Start is called before the first frame update
    void Start()
    {
        //Store an array of all available resolutions.
        resolutions = Screen.resolutions;
        //Will clear all previous saved resolution options.
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            //Change scale of value to eliminate amount in dropbar.
            if (resolutions[i].width >= 800)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                if (!options.Contains(option))
                {
                    options.Add(option);
                }

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        //Refreshes screen to display current resolution.
        resolutionDropdown.RefreshShownValue();
    }

    public void SetQuality(int qualityIndex)
    {
        tempQualityIndex = qualityIndex;
    }

    public void SetFullScreen(bool screen)
    {
        Screen.fullScreen = screen;
        if (!screen)
        {
            Resolution resolution = Screen.currentResolution;
            Screen.SetResolution(resolution.width, resolution.height, screen);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        tempSetResolution = resolutionIndex;
    }

    //Saves the video settings.
    public void SaveSetting()
    {
        QualitySettings.SetQualityLevel(tempQualityIndex);

        Resolution resolution = resolutions[tempSetResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
