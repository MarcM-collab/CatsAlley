using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    private const float waitToSpawn = 0.35f;
    public void Spawn()
    {
        StartCoroutine(DelaySpawn());
    }
    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(waitToSpawn);
        var bulletInstance = Instantiate(bullet, EntityManager.ExecutorCharacter.transform, EntityManager.ExecutorCharacter.transform);
        var bulletComponent = bulletInstance.GetComponent<Bullet>();
        bulletComponent.InitialPosition = transform.position;
        bulletComponent.FinalPosition = EntityManager.TargetCharacter.transform.position;
    }
}
