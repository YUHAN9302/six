using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.Rendering.Universal;

public class Setting : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioMixer AudioMixerObj;
    public TMP_Dropdown ScreenDropdown;
    public UniversalRenderPipelineAsset urpAsset;
    public TMP_Dropdown RatioDropdown;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        urpAsset.renderScale = 2f;
    }
   
    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeBGMSlider()
    {
        AudioMixerObj.SetFloat("BGMSound", BGMSlider.value);
    }
    public void ChangeSFXSlider()
    {
        AudioMixerObj.SetFloat("SFXSound", SFXSlider.value);
    }
    public void ChangeScreenSize()
    {
        switch (ScreenDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 1:
                Screen.SetResolution(800, 480, false);
                break;
        }
    }

    public void SetRenderRatio()
    {
        switch (RatioDropdown.value)
        {
            case 0:
                urpAsset.renderScale = 2f;
                break;
            case 1:
                urpAsset.renderScale = 1f;
                break;
            case 2:
                urpAsset.renderScale = 0.5f;
                break;
        }
    }
}
