using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RockSpell : Spell
{
    public GameObject RockSpellPrefab;

    private Vector2[] fieldRange = new Vector2[] { new Vector2(3,1), new Vector2(-4, -2) };

    public bool activated;

    private List<int> puntuationList = new List<int>();
    private List<int> yPosList = new List<int>();

    public RockSpawner rockSpawner;

    private void Start()
    {
        prevPos = new Vector3Int(-5, -2, 0);
        rockSpawner = GameObject.Find("RockSpawner").GetComponent<RockSpawner>();
    }
    private void Update()
    {
        if (activated)
        {
            var mousePosInt = GetNearestCenterPoint();
            if (tileManager.FloorTilemap.HasTile(mousePosInt))
            {
                for (int i = (int)fieldRange[1].x; i <= fieldRange[0].x; i++)
                {
                    tileManager.UITilemap.SetTile(new Vector3Int(i, prevPos.y, 0), null);
                }

                prevPos = mousePosInt;

                for (int i = (int)fieldRange[1].x; i <= fieldRange[0].x; i++)
                {
                    tileManager.UITilemap.SetTile(new Vector3Int(i, mousePosInt.y, 0), tileManager.PointingTile);
                }
            }
            else
            {
                for (int i = (int)fieldRange[1].x; i <= fieldRange[0].x; i++)
                {
                    tileManager.UITilemap.SetTile(new Vector3Int(i, prevPos.y, 0), null);
                }
            }
        }
    }

    private Vector3Int GetNearestCenterPoint()
    {
        Vector2 mousePos = GetMousePosition;
        return new Vector3Int(Mathf.FloorToInt(mousePos.x), Mathf.FloorToInt(mousePos.y), 0);
    }
    private void OnDestroy()
    {
        activated = false;
    }
    public override void ExecuteSpell()
    {
        activated = false;
        Vector3Int mousePosInt = GetNearestCenterPoint();

        if (tileManager.FloorTilemap.HasTile(mousePosInt))
        {
            rockSpawner.StartSpawning(RockSpellPrefab, (int)mousePosInt.y);
            executed = true;
        }

        for (int i = (int)fieldRange[1].x; i <= fieldRange[0].x; i++)
        {
            tileManager.UITilemap.SetTile(new Vector3Int(i, mousePosInt.y, 0), null);
            tileManager.UITilemap.SetTile(new Vector3Int(i, prevPos.y, 0), null);
        }
    }
    public override void IAUse()
    {
        var yPos = GetBestGasPosition();
        rockSpawner.StartSpawning(RockSpellPrefab, yPos);
    }

    private int GetBestGasPosition()
    {
        int maxPuntuation = puntuationList[0];
        int maxPosition = 0;
        for (int i = 1; i < puntuationList.Count; i++)
        {
            var currentPuntuation = puntuationList[i];
            if (currentPuntuation > maxPuntuation)
            {
                maxPuntuation = currentPuntuation;
                maxPosition = i;
            }
        }
        return yPosList[maxPosition];
    }
    private void GeneratePuntuationList()
    {
        puntuationList = new List<int>();
        for (int y = (int)fieldRange[1].y; y <= (int)fieldRange[0].y; y++)
        {
            int currentPuntuation = 0;
            for (int x = (int)fieldRange[1].x; x <= (int)fieldRange[0].x; x++)
            {
                Vector3Int vector = new Vector3Int(x, y, 0);
                Vector3 centerGridPos = vector + TileManager.CellSize;

                var charcter = SelectCharacter(centerGridPos);
                if (1 == charcter)
                    currentPuntuation++;
                else if (2 == charcter)
                    currentPuntuation--;
            }
            yPosList.Add(y);
            puntuationList.Add(currentPuntuation);
        }
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

        return Mathf.Max(puntuationList.ToArray()) >= 2;
    }
}
