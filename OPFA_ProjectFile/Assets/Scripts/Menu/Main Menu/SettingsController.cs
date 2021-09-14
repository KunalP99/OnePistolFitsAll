using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    public Slider sensitivitySlider;

    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;

        // Clears all current options on the drop down object
        resolutionDropdown.ClearOptions();

        // List of strings that are our resolutions (size of list can be changed while arrays can't)
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        // Loop through each element in our resolutions array
        for (int i =0; i < resolutions.Length; i++)
        {
            // Create a formatted string that displays the resolution
            string option = resolutions[i].width + "x" + resolutions[i].height;
            // Add each formatted string to the list
            options.Add(option);

            // Matches the resolution of the players system resolution (in my case 1920 x 1080)
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Add options list to the resolution dropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMouseSensitivity(float val)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        PlayerPrefs.SetFloat("Sensitivity", val);
        Debug.Log("Set sensitivity to " + val);
    }

    public void SetVolume(float volume)
    {
        // MAKE SURE TO CHANGE ALL AUDIO SOURCES "Output" TO "MasterMixer"
        audioMixer.SetFloat("volume", volume);
        Debug.Log("Volume is: " + volume);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
