using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class MainMenu : MonoBehaviour
{
    PlayerLocomotion playerLocomotion;
    Volume volume;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1;

    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 30;
    public int mainControllerSen = 30;
    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 0.2f;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    void Start()
    {
        volume = FindObjectOfType<Volume>();

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void PlayMap(string map)
    {
        SceneManager.LoadScene(map);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetControllerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void SettingsApply()
    {
        PlayerPrefs.SetInt("masterSen", mainControllerSen);
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        QualitySettings.SetQualityLevel(_qualityLevel);
        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        volume.profile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.postExposure.value = _brightnessLevel;

        playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        playerLocomotion.xSensitivity = mainControllerSen;
        playerLocomotion.ySensitivity = mainControllerSen;


        /*         StartCoroutine(ConfirmationBox());
         */
    }

    public void BackBtn(GameObject promtp)
    {

    }

    public void SetBrightness(float brightness)
    {

        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void ResetButton()
    {
        //Gameplay Settings
        controllerSenTextValue.text = defaultSen.ToString("0");
        controllerSenSlider.value = defaultSen;
        mainControllerSen = defaultSen;
        SettingsApply();
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
