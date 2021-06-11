using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TierraAbility : Abilty
{
    public int damage = 2;
    private bool activated = false;
    private Vector3Int mouseIntPos;
    private Vector2[] fieldRange = new Vector2[] { new Vector2(0, 0), new Vector2(4, 0) };
    private Vector3Int prevPos;
    private TileManager tileManager;
    public GameObject fx;
    public Vector2 tileSize;
    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }
    public override void Excecute()
    {
        if (TurnManager.TeamTurn == Team.TeamPlayer)
        {
            activated = true;
            prevPos = new Vector3Int(-4, -1, 0);
        }
    }
    private void Update()
    {
        if (activated)
        {
            if (mouseIntPos.x <= fieldRange[0].x && mouseIntPos.x >= fieldRange[1].x && mouseIntPos.y <= fieldRange[0].y && mouseIntPos.y >= fieldRange[1].y && tileManager.FloorTilemap.HasTile(mouseIntPos))
            {
                tileManager.UITilemap.SetTile(prevPos, null);
                tileManager.UITilemap.SetTile(prevPos + new Vector3Int(+1, 0, 0), null);
                tileManager.UITilemap.SetTile(prevPos + new Vector3Int(+2, 0, 0), null);
                tileManager.UITilemap.SetTile(prevPos + new Vector3Int(+3, 0, 0), null);

                prevPos = mouseIntPos;

                tileManager.UITilemap.SetTile(mouseIntPos, tileManager.PointingTile);
                tileManager.UITilemap.SetTile(mouseIntPos + new Vector3Int(+1, 0, 0), tileManager.PointingTile);
                tileManager.UITilemap.SetTile(mouseIntPos + new Vector3Int(+2, 0, 0), tileManager.PointingTile);
                tileManager.UITilemap.SetTile(mouseIntPos + new Vector3Int(+3, 0, 0), tileManager.PointingTile);
            }
            else
            {
                tileManager.UITilemap.SetTile(prevPos, null);
                tileManager.UITilemap.SetTile(prevPos + new Vector3Int(+1, 0, 0), null);
                tileManager.UITilemap.SetTile(prevPos + new Vector3Int(+2, 0, 0), null);
                tileManager.UITilemap.SetTile(prevPos + new Vector3Int(+3, 0, 0), null);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Use();
            }
        }

    }

    private void Use()
    {
        activated = false;

        if (tileManager.FloorTilemap.HasTile(mouseIntPos))
        {
            executed = true;
            Instantiate(fx, mouseIntPos, Quaternion.identity);

            Vector3Int[] positions = new Vector3Int[4];
            positions[0] = mouseIntPos;
            positions[1] = mouseIntPos + new Vector3Int(+1, 0, 0);
            positions[2] = mouseIntPos + new Vector3Int(+2, 0, 0);
            positions[3] = mouseIntPos + new Vector3Int(+3, 0, 0);

            for (int i = 0; i < positions.Length; i++)
            {
                tileManager.UITilemap.SetTile(prevPos, null);
                tileManager.UITilemap.SetTile(positions[i], null);
            }
            List<Collider2D> damaged = new List<Collider2D>();
            Debug.Log("using");
            Collider2D[] inTrigger = Physics2D.OverlapBoxAll(new Vector2(mouseIntPos.x, mouseIntPos.y), tileSize, 360);

            for (int i = 0; i < inTrigger.Length; i++)
            {
                if (inTrigger[i].CompareTag("Character") && !damaged.Contains(inTrigger[i]))
                {
                    HealthSystem.TakeDamage(damage, inTrigger[i].GetComponent<Character>());
                    damaged.Add(inTrigger[i]);
                }

            }
        }
    }
    
}
