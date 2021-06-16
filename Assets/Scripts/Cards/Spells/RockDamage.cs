using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDamage : MonoBehaviour
{
    public int damage;
    public Vector2 tileSize = new Vector2(1, 1);
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, tileSize);
    }
    private void Awake()
    {
        Execute();
    }
    private void Update()
    {
            //Destroy(gameObject);
    }
    private void Execute()
    {
        Collider2D[] inTrigger = Physics2D.OverlapBoxAll(transform.position, tileSize, 360);
        print(inTrigger.Length);
        Damage(inTrigger);
    }
    private void Damage(Collider2D[] inTrigger)
    {
        List<Collider2D> damaged = new List<Collider2D>();
        for (int i = 0; i < inTrigger.Length; i++)
        {
            if (inTrigger[i].CompareTag("Character") && !damaged.Contains(inTrigger[i]))
            {
                HealthSystem.TakeDamage(damage, inTrigger[i].GetComponent<Character>());
            }
        }
    }
}

