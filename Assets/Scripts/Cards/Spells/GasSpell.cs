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
    private Vector3Int mouseIntPos;

    private Vector2[] fieldRange = new Vector2[] { new Vector2(3, 1), new Vector2(-3, -1) };

    public bool activated;
    private void Start()
    {
        prevPos = new Vector3Int(-4, -1, 0);
    }
    private void Update()
    {
        if (activated)
        {
            mouseIntPos = GetNearestCenterPoint();
                    if (mouseIntPos.x <= fieldRange[0].x && mouseIntPos.x >= fieldRange[1].x && mouseIntPos.y <= fieldRange[0].y && mouseIntPos.y >= fieldRange[1].y && tileManager.FloorTilemap.HasTile(mouseIntPos))
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
                    else
                    {
                        tileManager.UITilemap.SetTile(prevPos, null);
                        tileManager.UITilemap.SetTile(prevPos + new Vector3Int(0, -1, 0), null);
                        tileManager.UITilemap.SetTile(prevPos + new Vector3Int(-1, 0, 0), null);
                        tileManager.UITilemap.SetTile(prevPos + new Vector3Int(-1, -1, 0), null);
                    }
            
        }

    }

    private Vector3Int GetNearestCenterPoint()
    {
        Vector2 mousePos = GetMousePosition;

        return new Vector3Int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y), 0);
    }
    private void OnDestroy()
    {
        activated = false;
    }
    public override void ExecuteSpell()
    {
        activated = false;
        mouseIntPos = GetNearestCenterPoint();

        if (tileManager.FloorTilemap.HasTile(mouseIntPos))
        {
            Instantiate(GasSpellPrefab, mouseIntPos, Quaternion.identity);
            executed = true;
        }

        tileManager.UITilemap.SetTile(mouseIntPos, null);
        tileManager.UITilemap.SetTile(mouseIntPos + new Vector3Int(0, -1, 0), null);
        tileManager.UITilemap.SetTile(mouseIntPos + new Vector3Int(-1, 0, 0), null);
        tileManager.UITilemap.SetTile(mouseIntPos + new Vector3Int(-1, -1, 0), null);
    }
    public override void IAUse()
    {

    }
    public override void Activate()
    {
        activated = true;
    }
}
   

