using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static float shakeDuration = 0f;
    public float strength = 0.7f;
    public float duration = 1.0f;

    Vector3 pos;
    void OnEnable()
    {
        pos = transform.localPosition;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TriggerShake();
        }
        if (shakeDuration > 0)
        {
            transform.localPosition = pos + Random.insideUnitSphere * strength;

            shakeDuration -= Time.deltaTime * duration;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = pos;
        }
    }
    public static void TriggerShake()
    {
        Shake(0.1f);
    }
    private static void Shake(float duration)
    {
        shakeDuration = duration;
    }
}
