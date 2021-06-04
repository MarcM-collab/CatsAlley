using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;

    public void Spawn()
    {
        var bulletInstance = Instantiate(bullet, EntityManager.ExecutorCharacter.transform, EntityManager.ExecutorCharacter.transform);
        var bulletComponent = bulletInstance.GetComponent<Bullet>();
        bulletComponent.InitialPosition = transform.position;
        bulletComponent.FinalPosition = EntityManager.TargetCharacter.transform.position;
    }
}
