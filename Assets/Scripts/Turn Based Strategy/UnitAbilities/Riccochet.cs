using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Riccochet : Abilty
{
    public int damage = 6;
    public float waitForNextTarget;
    public override void Excecute()
    {
        Use(Team.TeamAI);
    }
    public override void IAExecute()
    {
        Use(Team.TeamPlayer);
    }
    private void Use(Team targetTeam)
    {
        Character[] targets = EntityManager.GetCharacters(targetTeam);
        if(targets.Length > 0)
        {
            Character currentTarget = targets[UnityEngine.Random.Range(0, targets.Length)];

            StartCoroutine(Damage(currentTarget));
        }




        //RaycastHit2D hit2D = Physics2D.Raycast(GetMousePosition, Vector2.zero);

        //if (hit2D)
        //{
        //    if (hit2D.transform.CompareTag("Character"))
        //    {
        //        Character target = hit2D.collider.gameObject.GetComponent<Character>();
        //        if (target.Team == targetTeam)
        //        {
        //            HealthSystem.TakeDamage(damage, target);
        //            FindNewTargets(targetTeam, target);
        //        }
        //    }
        //}
    }
    private IEnumerator Damage(Character target)
    {
        yield return new WaitForSeconds(0.25f);

        EntityManager.SetTarget(target);
        Character executor = EntityManager.ExecutorCharacter.GetComponent<Character>();
        int currentAttack = executor.currentAttack;
        executor.currentAttack = damage;
        target.Hit = true;
        executed = true;

        executor.currentAttack = currentAttack;
    }
}
