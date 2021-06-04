using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float time;
    public Vector3 InitialPosition;
    public Vector3 FinalPosition;
    private float timer;

    private void Start()
    {
        transform.eulerAngles = new Vector3(0, Vector3.Angle(InitialPosition, FinalPosition), 0);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(InitialPosition, FinalPosition, timer/ time);

        if (timer > time)
        {
            Destroy(gameObject);
        }

        timer += Time.deltaTime;
    }
}
