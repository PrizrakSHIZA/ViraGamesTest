using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Color lightColor, darkColor;

    [SerializeField] Slider musicSlider, SFXSlider;
    [SerializeField] Toggle darkModeToggle;

    Camera mainCam;
    bool darkMode = false;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        float temp;
        mixer.GetFloat("MusicVolume", out temp);
        musicSlider.value = temp;

        mixer.GetFloat("SFXVolume", out temp);
        SFXSlider.value = temp;

        darkModeToggle.isOn = darkMode;
    }

    public void OnMusicVolChanged(float val)
    {
        mixer.SetFloat("MusicVolume", val);
    }

    public void OnSFXVolChanged(float val)
    {
        mixer.SetFloat("SFXVolume", val);
    }

    public void DarkModeChanged(bool val)
    {
        darkMode = val;
        if (val)
            mainCam.backgroundColor = darkColor;
        else
            mainCam.backgroundColor = lightColor;
    }
}
