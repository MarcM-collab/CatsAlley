using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoAbilty : Abilty
{
    public Character littleSkeleton;

    private int nSpawn = 32;
    Vector2 positionSpawn;
    private bool CanSpawn;
    TileManager tileManager;
    CardSpawner _cardSpawner;
    List<Vector2> positions;



    public override void Excecute()
    {
        if (!tileManager)
            tileManager = FindObjectOfType<TileManager>();
        if (!_cardSpawner)
            _cardSpawner = FindObjectOfType<CardSpawner>();

        positions = new List<Vector2>();

        CanSpawn = false;
        int i = 0;
        while (!CanSpawn)
        {
            if (i > nSpawn)
                return;
            Spawn();
            positions.Add(positionSpawn);
            i++;
        }

        if (CanSpawn)
        {
            _cardSpawner.SpawnCharacter(littleSkeleton.gameObject, GetTilePosition(positionSpawn), Team.TeamPlayer);
            executed = true;
            //Character e = Instantiate(littleSkeleton, GetTilePosition(positionSpawn), Quaternion.identity).GetComponent<Character>();
            //e.Team = Team.TeamPlayer;
            //e.Exhausted = true;
            //e.GetComponentInChildren<HealthBar>().color = Color.green;
            //e.ChangeHealth();


        }
    }
    private Vector2 GetTilePosition(Vector2 pos)
    {
        return new Vector2(pos.x, pos.y);
    }

    //private void GetTilePosition(Transform toPosition, Vector2 pos)
    //{

    //    toPosition.transform.position = new Vector3(pos.x + (TileManager.CellSize.x / 2), pos.y + (TileManager.CellSize.y / 2), 0);
    //}


    protected Vector3Int GetIntPos(Vector2 pos)
    {
        return (Vector3Int)tileManager.FloorTilemap.WorldToCell(pos);
    }
    private void Spawn()
    {
        do
        {
            positionSpawn = new Vector2(Random.Range(-4,3), Random.Range(1,-2));
        }
        while (positions.Contains(positionSpawn));
       
        print(positionSpawn);

        var vector = new Vector3(positionSpawn.x, positionSpawn.y);
        RaycastHit2D rayCast = Physics2D.Raycast(vector, Vector3.zero, Mathf.Infinity);

        if (rayCast)
        {
            if ((rayCast.collider.CompareTag("Character")))
            {
                print("no spawn");
                CanSpawn = false;
                return;
            }
        }
        else if (tileManager.CollisionTilemap.HasTile(GetIntPos(positionSpawn)))
        {
            CanSpawn = false;
            return;
        }
        else
        {
            print("puede spawnear");
            CanSpawn = true;
        }
    }
}
