using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbility : MonoBehaviour
{
    public Abilty ability;
    public bool isHero = false;
    [HideInInspector] public bool hasBeenUsed = false; //avoids bugs
    public void Use()
    {
        if (ability.whiskasCost <= TurnManager.currentMana && TurnManager.TeamTurn == Team.TeamPlayer && !hasBeenUsed)
        {
            ability.selfChar = GetComponent<Character>();
            ability.Excecute();
        }
    }

    public void IAUse()
    {
        if (TurnManager.TeamTurn == Team.TeamAI && !hasBeenUsed)
        {
            print("abilidad iaaaaaa");
            ability.selfChar = GetComponent<Character>();
            ability.IAExecute();
        }
    }
    private void Update()
    {
        if (ability.executed)
        {
            if (isHero)
            {
                GetComponentInParent<Hero>().Exhausted = true;
            }
            else
            {
                EntityManager.ExecutorCharacter.Exhausted = true;
            }
            TurnManager.SubstractMana(ability.whiskasCost);
            ability.executed = false;
            hasBeenUsed = true;
        }

        if (TurnManager.TeamTurn == Team.TeamAI)
        {
            hasBeenUsed = false;
        }
    }
}
