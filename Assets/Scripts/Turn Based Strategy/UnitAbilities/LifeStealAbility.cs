using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealAbility : Abilty
{
    public int damage;
    public override void Excecute()
    {
        DoAction(Team.TeamAI);
    }
    public override void IAExecute()
    {
        DoAction(Team.TeamPlayer);
    }
    private void DoAction(Team targetTeam)
    {
        Character[] characters = EntityManager.GetCharacters(targetTeam);
        if (characters.Length > 0)
        {
            HealthSystem.Heal(selfChar, damage);
            HealthSystem.TakeDamage(damage, characters[Random.Range(0, characters.Length)]);
            executed = true;
        }
    }
}
