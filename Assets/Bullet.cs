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
        Debug.Log(Mathf.Asin(FinalPosition.y - InitialPosition.y / FinalPosition.x - InitialPosition.x) * Mathf.Rad2Deg);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + Mathf.Asin(FinalPosition.y - InitialPosition.y / FinalPosition.x - InitialPosition.x) * Mathf.Rad2Deg, 0);
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
