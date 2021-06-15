using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MeowAudio : MonoBehaviour
{
    public MeowType meowType;
    private AudioSource a;
    private void Awake()
    {
        a = GetComponent<AudioSource>();
        a.outputAudioMixerGroup = AudioManager.audioManager.sfxMixer;
    }
    public void PlayAudioMew()
    {
        a.volume = 0.6f;
        a.PlayOneShot(AudioManager.audioManager.GetAudioClip(meowType));
    }
}
