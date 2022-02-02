using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControls : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider masterSlider, bgmSlider, sfxSlider;

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Vol_Master", 0.5f);
        bgmSlider.value = PlayerPrefs.GetFloat("Vol_BGM", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("Vol_SFX", 0.7f);
    }

    public void OnBGMSliderValueChanged(float value)
    {
        mixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20f); //Refrence: https://www.youtube.com/watch?v=C1gCOoDU29M
        PlayerPrefs.SetFloat("Vol_BGM", value);
        PlayerPrefs.Save();
    }

    public void OnSFXSliderValueChanged(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("Vol_SFX", value);
        PlayerPrefs.Save();
    }

    public void OnMasterSliderValueChanged(float value)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("Vol_Master", value);
        PlayerPrefs.Save();
    }


}
