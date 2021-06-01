using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GasSpell : Spell
{
    public GameObject GasSpellPrefab;
    public int damage;
    private Team currentTurn;
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
                    tileManager.UITilemap.SetTile(prevPos + new Vector3Int(0, -1, 0), null);
                    tileManager.UITilemap.SetTile(prevPos + new Vector3Int(-1, 0, 0), null);
                    tileManager.UITilemap.SetTile(prevPos + new Vector3Int(-1, -1, 0), null);
                    prevPos = mouseIntPos;

                    tileManager.UITilemap.SetTile(mouseIntPos, tileManager.PointingTile);
                    tileManager.UITilemap.SetTile(mouseIntPos + new Vector3Int(0, -1, 0), tileManager.PointingTile);
                    tileManager.UITilemap.SetTile(mouseIntPos + new Vector3Int(-1, 0, 0), tileManager.PointingTile);
                    tileManager.UITilemap.SetTile(mouseIntPos + new Vector3Int(-1, -1, 0), tileManager.PointingTile);

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
        base.ExecuteSpell();
        Vector3Int pos = GetIntPos(GetMousePosition);

        if (tileManager.FloorTilemap.HasTile(pos))
        {
            Instantiate(GasSpellPrefab, pos, Quaternion.identity);
            executed = true;
        }
        tileManager.UITilemap.SetTile(prevPos, null);
        tileManager.UITilemap.SetTile(prevPos + new Vector3Int(0, -1, 0), null);
        tileManager.UITilemap.SetTile(prevPos + new Vector3Int(-1, 0, 0), null);
        tileManager.UITilemap.SetTile(prevPos + new Vector3Int(-1, -1, 0), null);
    }
    public override void IAUse()
    {

    }
}
   

