using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderSet : MonoBehaviour
{
    public soundType soundT;
    void Start()
    {
        switch (soundT)
        {
            case soundType.SFX:
                GetComponent<Slider>().value = AudioManager.audioManager.sfxVolume;
                break;
            case soundType.music:
                GetComponent<Slider>().value = AudioManager.audioManager.musicVolume;
                break;
            case soundType.global:
                GetComponent<Slider>().value = AudioManager.audioManager.globalVolume;
                break;
        }
    }
}
