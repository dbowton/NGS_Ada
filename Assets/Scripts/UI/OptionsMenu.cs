using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TMP_Dropdown screenDropdown;
    [SerializeField] TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

	void Start()
	{
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResloutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
		{
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(resolutions[i].width + "x" + resolutions[i].height);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
			{
                currentResloutionIndex = i;
			}
		}
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResloutionIndex;
        resolutionDropdown.RefreshShownValue();
	}

	public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetFullscreen()
    {
        Debug.Log(screenDropdown.value);
        if (screenDropdown.value == 0) Screen.fullScreen = true;
        else if (screenDropdown.value == 1) Screen.fullScreenMode = FullScreenMode.Windowed;
        else if (screenDropdown.value == 2) Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    public void SetQuality(int qualityIndex)
	{
        QualitySettings.SetQualityLevel(qualityIndex);
	}

    public void SetResolution(int resolutionQualityIndex)
	{
        Resolution resolution = resolutions[resolutionQualityIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}
}
