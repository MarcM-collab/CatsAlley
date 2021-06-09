using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoAbilty : Abilty
{
    private TileManager tileManager;
    private bool activated = false;

    public int damage = 1;
    private Vector3Int mouseIntPos;
    private Vector2[] fieldRange = new Vector2[] { new Vector2(3, 1), new Vector2(-3, -1) };
    private Vector3Int prevPos;
    public GameObject fx;
    public Vector2 tileSize;

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();


    }
    public override void Excecute()
    {
        //tileManager.
        print("tus muertos");

    }

    private void Update()
    {
        if (activated)
        {
            if (mouseIntPos.x <= fieldRange[0].x && mouseIntPos.x >= fieldRange[1].x && mouseIntPos.y <= fieldRange[0].y && mouseIntPos.y >= fieldRange[1].y && tileManager.FloorTilemap.HasTile(mouseIntPos))
            {
                tileManager.UITilemap.SetTile(prevPos, null);
                prevPos = mouseIntPos;
                tileManager.UITilemap.SetTile(mouseIntPos, tileManager.PointingTile);
            }
            else
            {
                tileManager.UITilemap.SetTile(prevPos, null);
            }
            if (Input.GetMouseButtonDown(0) && !IsOccupied())
            {
                Excecute();
            }
        }

    }

    private bool IsOccupied()
    {
        activated = false;
        RaycastHit2D hit2D = Physics2D.Raycast(GetMousePosition, Vector2.zero);

        if (hit2D)
        {
            if (hit2D.transform.CompareTag("Character"))
            {

                return true;
            }
        }

        return false;
    }
}
