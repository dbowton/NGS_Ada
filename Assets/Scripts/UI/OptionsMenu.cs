using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TMP_Dropdown dropdown;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen()
    {
        Debug.Log(dropdown.value);
        if (dropdown.value == 0) Screen.fullScreen = true;
        else if (dropdown.value == 1) Screen.fullScreenMode = FullScreenMode.Windowed;
        else if (dropdown.value == 2) Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }
}
