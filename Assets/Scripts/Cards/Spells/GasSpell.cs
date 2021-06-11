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

    private List<int> puntuationList = new List<int>();
    private List<Vector3> positionList = new List<Vector3>();

    private void Start()
    {
        prevPos = new Vector3Int(-4, -1, 0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            IAUse();
        }
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
        var vector = GetBestGasPosition();
        vector = new Vector3(vector.x + 0.5f, vector.y - 0.5f, 0);
        Instantiate(GasSpellPrefab, vector, Quaternion.identity);
    }
    private Vector3 GetBestGasPosition()
    {        
        int maxPuntuation = puntuationList[0];
        Vector3 maxPosition = positionList[0];
        for (int i = 1; i < puntuationList.Count; i++)
        {
            var currentPuntuation = puntuationList[i];
            if (currentPuntuation > maxPuntuation)
            {
                maxPuntuation = currentPuntuation;
                maxPosition = positionList[i];
            }
        }
        return maxPosition;
    }
    private Vector3 GetBestRockPosition()
    {
        List<int> puntuationList = new List<int>();
        List<Vector3> positionList = new List<Vector3>();
        for (int x = (int)fieldRange[1].x; x <= (int)fieldRange[0].x; x++)
        {
            int currentPuntuation = 0;
            for (int y = (int)fieldRange[1].y; y <= (int)fieldRange[0].y; y++)
            {
                Vector3Int vector = new Vector3Int(x, y, 0);
                Vector3 centerGridPos = vector + TileManager.CellSize;

                if (x == 0)
                    positionList.Add(centerGridPos);

                var charcter = SelectCharacter(centerGridPos);
                if (1 == charcter)
                    currentPuntuation++;
                else if (2 == charcter)
                    currentPuntuation--;
            }
            puntuationList.Add(currentPuntuation);
            Debug.Log(currentPuntuation);
        }

        int maxPuntuation = puntuationList[0];
        Vector3 maxPosition = positionList[0];
        for (int i = 1; i < puntuationList.Count; i++)
        {
            var currentPuntuation = puntuationList[i];
            if (currentPuntuation > maxPuntuation)
            {
                maxPuntuation = currentPuntuation;
                maxPosition = positionList[1];
            }
        }

        return maxPosition;
    }
    protected int SelectCharacter(Vector3 vector) //1 Player / 2 AI / 0 nothing
    {
        var postion = new Vector2(vector.x, vector.y);
        RaycastHit2D hit = Physics2D.Raycast(postion, Vector2.zero, Mathf.Infinity);
        var hitCollider = hit.collider;
        if (hitCollider != null)
        {
            var gameObject = hitCollider.gameObject;
            if (!(gameObject.GetComponent("Character") as Character is null))
            {
                var character = gameObject.GetComponent("Character") as Character;
                if (character.Team == Team.TeamPlayer)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
        return 0;
    }
    public override void Activate()
    {
        activated = true;
    }
    public override bool CanBeUsed()
    {
        GeneratePuntuationList();

        return Mathf.Max(puntuationList.ToArray()) >= 1;
    }
    private void GeneratePuntuationList()
    {
        puntuationList = new List<int>();
        positionList = new List<Vector3>();
        for (int x = (int)fieldRange[1].x - 1; x <= (int)fieldRange[0].x - 1; x++)
        {
            for (int y = (int)fieldRange[1].y; y <= (int)fieldRange[0].y; y++)
            {
                int currentPuntuation = 0;
                for (int i = x; i <= x + 1; i++)
                {
                    for (int j = y; j >= y - 1; j--)
                    {
                        Vector3Int vector = new Vector3Int(i, j, 0);
                        Vector3 centerGridPos = vector + TileManager.CellSize;

                        if (i == x && j == y)
                            positionList.Add(centerGridPos);

                        var charcter = SelectCharacter(centerGridPos);
                        if (1 == charcter)
                            currentPuntuation++;
                        else if (2 == charcter)
                            currentPuntuation--;
                    }
                }
                puntuationList.Add(currentPuntuation);
            }
        }
    }
}
   

