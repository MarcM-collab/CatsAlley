using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealSpell : Spell
{
   
    public GameObject FX;
    public bool activated;

    private Character[] chars;
    private void Update()
    {
        if (activated)
        {
            Vector3Int mouseIntPos = GetIntPos(GetMousePosition);

            if (tileManager.FloorTilemap.HasTile(mouseIntPos))
            {
                if (prevPos != mouseIntPos)
                {
                    tileManager.UITilemap.SetTile(prevPos, null);
                    prevPos = mouseIntPos;
                    tileManager.UITilemap.SetTile(mouseIntPos, tileManager.PointingTile);

                }
            }
        }
    }

    private void OnDestroy()
    {
        activated = false;
    }
    public override void ExecuteSpell()
    {
        activated = false;
        RaycastHit2D hit2D = Physics2D.Raycast(GetMousePosition, Vector2.zero);

        if (hit2D)
        {
            if (hit2D.transform.CompareTag("Character"))
            {
                Character target = hit2D.collider.gameObject.GetComponent<Character>();
                chars = FindObjectsOfType<Character>();

                if (FX)
                    Instantiate(FX, hit2D.transform.position, Quaternion.identity);

                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i].Team == Team.TeamPlayer)
                        Healing(chars[i]);
                    else
                        ReduceHealth(chars[i]);
                }
                executed = true;
            }
        }
        tileManager.UITilemap.SetTile(prevPos, null);
    }
    public override void IAUse()
    {
        chars = FindObjectsOfType<Character>();
        IAUsingSpell(chars);
    }

    private void IAUsingSpell(Character[] Allcharacters)
    {
        for (int i = 0; i < Allcharacters.Length; i++)
        {
            if (Allcharacters[i].Team == Team.TeamPlayer)
            {

                ReduceHealth(Allcharacters[i]);

            }
            else
            {
                Healing(Allcharacters[i]);
            }
        }
    }
    private void ReduceHealth(Character target)
    {
        EntityManager.SetTarget(target);
        HealthSystem.TakeDamage(1);
    }

    private void Healing(Character target)
    {
        if (target.HP != target.MaxHP)
        {
            HealthSystem.Heal(target,1); //"takedamage"
        }
    }
    public override void Activate()
    {
        activated = true;
    }
}
