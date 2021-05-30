using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown resolutionsDropdown;
    public Toggle t;
    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int current = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string o = resolutions[i].width + " x " + resolutions[i].height;

            if(true)//!options.Contains(o))
            {
                options.Add(o);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    current = i;
                }
            }
        }
        resolutionsDropdown.AddOptions(options);

        if (!PlayerPrefs.HasKey("fullscreen"))
        {
            ToggleFullScreen(true);
            t.isOn = true;
        }
        else if (PlayerPrefs.GetInt("fullscreen") == 1)
        {
            ToggleFullScreen(true);
            t.isOn = true;
        }
        else
        {
            ToggleFullScreen(false);
            t.isOn = false;
        }


        if (!PlayerPrefs.HasKey("resolutionIndex"))
        {
            SetResolution(current);
            resolutionsDropdown.value = current;
        }
        else
        {
            SetResolution(PlayerPrefs.GetInt("resolutionIndex"));
            resolutionsDropdown.value = PlayerPrefs.GetInt("resolutionIndex");
        }
        resolutionsDropdown.RefreshShownValue();

    }
    public void SetResolution(int resIndex)
    {
        Resolution r = resolutions[resIndex];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);

        PlayerPrefs.SetInt("resolutionIndex", resIndex);
        PlayerPrefs.Save();
    }
    public void ToggleFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;

        if (fullScreen)
            PlayerPrefs.SetInt("fullscreen", 1);
        else
            PlayerPrefs.SetInt("fullscreen", 0);

        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.audioManager.ChangeVolume(value, soundType.SFX);
    }
    public void SetMusicVolume(float value)
    {
        AudioManager.audioManager.ChangeVolume(value, soundType.music);
    }
    public void SetGlobalVolume(float value)
    {
        AudioManager.audioManager.ChangeVolume(value, soundType.global);
    }
}
