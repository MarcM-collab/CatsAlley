using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFX : MonoBehaviour
{
    private ParticleSystem s;
    public float timeWait = 0;
    public int index = 0;
    private AudioSource AS;
    void Start()
    {
        s = GetComponent<ParticleSystem>();
        AS = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        CustomSceneManager.OnUnlock += Play;
    }
    private void OnDisable()
    {
        CustomSceneManager.OnUnlock -= Play;
    }
    private void Play(int i)
    {
        if (index == 0 ||i == index)
            StartCoroutine(StartDelay());
    }
    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(timeWait);
        AS.Play();
        s.Play();
    }
}
