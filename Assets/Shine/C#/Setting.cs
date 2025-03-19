using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class Setting : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioMixer AudioMixerObj;
    public TMP_Dropdown ScreenDropdown;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

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
    public void ChangeScreenSize() {
        switch (ScreenDropdown.value) {
            case 0:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 1:
                Screen.SetResolution(800, 480, false);
                break;
        }
    }
}
