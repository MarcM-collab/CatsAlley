using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using Random = UnityEngine.Random;
public enum MeowType
{
    women,
    men
}
public enum soundType
{
    SFX,
    music,
    global
}
public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;

    public float transitionWait = 1;
    public AudioClip[] musicClips;

    public static AudioManager audioManager;

    private AudioSource music;
    private AudioLowPassFilter passFilter;
    public AudioSource transition;
    private bool playerTurn = false;
    private float prevVolume;
    private bool transitioning;
    public AudioClip teleportSFX;

    public AudioClip[] meowWomen;
    public AudioClip[] meowMen;

    public AudioClip[] swissSounds;

    private AudioClip prevMeow;

    public float sfxVolume = 0.5f, musicVolume = 0.5f, globalVolume = 0.5f; //This is needed to initialitzate the value of the sliders

    public AudioClip GetRandomSwiss()
    {
        return swissSounds[Random.Range(0, swissSounds.Length)];
    }

    public AudioClip GetAudioClip(MeowType m)
    {
        if (m == MeowType.women)
            return GetRandomClip(meowWomen);

        return GetRandomClip(meowMen);
    }

    private AudioClip GetRandomClip(AudioClip[] meowArray)
    {
        AudioClip currentMeow;
        do
            currentMeow = meowArray[Random.Range(0, meowArray.Length)];
        while (currentMeow == prevMeow);

        prevMeow = currentMeow;
        return currentMeow;
    }
    private void Awake()
    {
        if (audioManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            audioManager = this;
        }
        DontDestroyOnLoad(gameObject);

        if (!PlayerPrefs.HasKey("sfxVol"))
        {
            ChangeVolume(sfxVolume, soundType.SFX);
            ChangeVolume(musicVolume, soundType.music);
            ChangeVolume(globalVolume, soundType.global);
        }
        else
        {
            ChangeVolume(PlayerPrefs.GetFloat("sfxVol"), soundType.SFX);
            ChangeVolume(PlayerPrefs.GetFloat("musicVol"), soundType.music);
            ChangeVolume(PlayerPrefs.GetFloat("globalVol"), soundType.global);
        }

        passFilter = GetComponent<AudioLowPassFilter>();
    }
    private void Start()
    {
        music = GetComponent<AudioSource>();
        music.outputAudioMixerGroup = musicMixer;
        music.loop = true;
        music.clip = musicClips[0];
        music.Play();
    }


    //private IEnumerator FadeIn(float duration, float targetVolume)
    //{
    //    float currentTime = 0;
    //    float start = 0;

    //    while (currentTime < duration)
    //    {
    //        currentTime += Time.deltaTime;
    //        music.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);

    //        if (start != prevVolume) //someone change the settings transitioning
    //            prevVolume = start;
    //        yield return null;
    //    }
    //    yield break;
    //}
    //private IEnumerator FadeOut(float duration)
    //{
    //    float start = music.volume;
    //    while (music.volume > 0)
    //    {
    //        music.volume -= start * Time.deltaTime / duration;
    //        if (start != prevVolume) //someone change the settings transitioning
    //            prevVolume = start;
    //        yield return null;
    //    }
    //    yield break;
    //}
    public void ChangeVolume(float value, soundType s)
    {
        switch (s)
        {
            case soundType.SFX:
                PlayerPrefs.SetFloat("sfxVol", value);
                sfxVolume = value;
                SetVolume("SFXVol", value);
                break;
            case soundType.music:
                PlayerPrefs.SetFloat("musicVol", value);
                musicVolume = value;
                SetVolume("MusicVol", value);
                break;
            case soundType.global:
                PlayerPrefs.SetFloat("globalVol", value);
                globalVolume = value;
                SetVolume("MasterVol", value);
                break;
        }
        PlayerPrefs.Save();
    }
    private void SetVolume(string parameter, float value)
    {
        if (value < 0.1f)
        {
            mixer.SetFloat(parameter, -80); //avoids errors that when 0 log10 is 0 and should be -80 to mute.
            return;
        }

        if (mixer)
            mixer.SetFloat(parameter, Mathf.Log10(value) * 20);
    }

    public void ApplyFilter()
    {
        if (passFilter)
            passFilter.enabled = true;
    }
    public void StopFilter()
    {
        if (passFilter)
            passFilter.enabled = false;
    }
    private void OnLevelWasLoaded(int level)
    {
        StopFilter();
        if (level == 1)
        {
            if (music)
            {
                music.clip = musicClips[0];
                music.Play();
            }
        }
        else
        {
            music.clip = musicClips[1];
            music.Play();
        }
    }
    public void StartBattle()
    {
        music.clip = musicClips[2];
        music.Play();
    }
}
