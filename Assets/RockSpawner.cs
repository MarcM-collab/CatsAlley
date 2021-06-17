using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    private Vector2[] fieldRange = new Vector2[] { new Vector2(3, 1), new Vector2(-4, -2) };

    private IEnumerator Spawn(GameObject RockSpellPrefab, int yPos)
    {
        for (int i = (int)fieldRange[1].x; i <= fieldRange[0].x; i++)
        {
            Debug.Log(i);
            Instantiate(RockSpellPrefab, new Vector3(i + 0.5f, yPos + 0.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.25f));
        }
    }
    public void StartSpawning(GameObject RockSpellPrefab, int yPos)
    {
        StartCoroutine(Spawn(RockSpellPrefab, yPos));
    }
}
