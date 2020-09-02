using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsChanger : MonoBehaviour
{
    [SerializeField] private Dropdown _dropdownQuality;
    [SerializeField] private Dropdown _dropdownResolution;
    [SerializeField] private Toggle _fullScreenToggle;
   
    private Resolution[] _resolution;

    private void Start()
    {
        if (PlayerPrefs.HasKey("ScreenMode"))
        {
            if (PlayerPrefs.GetInt("ScreenMode") == 1)
            {
                Screen.fullScreen = true;
            }
            else if(PlayerPrefs.GetInt("ScreenMode") == 0)
            {
                Screen.fullScreen = false;
            }
        }
        _fullScreenToggle.isOn = Screen.fullScreen;

        _dropdownQuality.ClearOptions();
        _dropdownQuality.AddOptions(QualitySettings.names.ToList());
        _dropdownQuality.value = QualitySettings.GetQualityLevel();

        _resolution = Screen.resolutions;
        _resolution = _resolution.Distinct().ToArray();
        string[] strResolution = new string[_resolution.Length];
        for (int i = 0; i < _resolution.Length; i++)
        {
            strResolution[i] = _resolution[i].width.ToString() + " x " + _resolution[i].height.ToString();
        }

        _dropdownResolution.ClearOptions();
        _dropdownResolution.AddOptions(strResolution.ToList());

        if (PlayerPrefs.HasKey("Resolution"))
        {
            _dropdownResolution.value = PlayerPrefs.GetInt("Resolution");
            Screen.SetResolution(_resolution[_dropdownResolution.value].width, _resolution[_dropdownResolution.value].height, Screen.fullScreen);
        }
        else
        {
        _dropdownResolution.value = _resolution.Length - 1;
        Screen.SetResolution(_resolution[_resolution.Length - 1].width, _resolution[_resolution.Length - 1].height, Screen.fullScreen);
        }
        
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(_dropdownQuality.value);
    }

    public void SetResolution()
    {
        Screen.SetResolution(_resolution[_dropdownResolution.value].width, _resolution[_dropdownResolution.value].height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", _dropdownResolution.value);
    }

    public void SetScreenMode()
    {
        Screen.fullScreen = _fullScreenToggle.isOn;
        if(Screen.fullScreen == true)
        {
            PlayerPrefs.SetInt("ScreenMode", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ScreenMode", 0);
        }

    }




}
