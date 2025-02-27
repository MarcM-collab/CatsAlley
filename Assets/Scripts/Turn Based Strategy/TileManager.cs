﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [Header("UITiles")]
    public AnimatedTile PointingTile;
    public AnimatedTile TargetTile;
    public AnimatedTile AllyTile;
    public AnimatedTile SpawningTile;
    public AnimatedTile MovingTile;
    public AnimatedTile AttackingTile;
    public AnimatedTile CollisionAllyTile;

    [Header("HeroTiles")]
    public GameObject AIHeroTile;
    public GameObject PlayerHeroTile;

    [Header("TileMaps")]
    public Tilemap FloorTilemap;
    public Tilemap CollisionTilemap;
    public Tilemap UITilemap;

    private static Grid _grid;

    public static Vector3 CellSize => new Vector3(_grid.cellSize.x / 2, _grid.cellSize.y / 2);

    private void Start()
    {
        _grid = GetComponent<Grid>();
    }
    public static void ShowTilesInTilemap(Tilemap tilemapToLook, Tilemap tilemapToEdit, AnimatedTile tile, System.Func<Vector3Int, bool> function)
    {
        for (int x = tilemapToLook.cellBounds.min.x; x <= tilemapToLook.cellBounds.max.x; x++)
        {
            for (int y = tilemapToLook.cellBounds.min.y; y <= tilemapToLook.cellBounds.max.y; y++)
            {
                Vector3Int vector = new Vector3Int(x, y, 0);

                if (function(vector))
                    tilemapToEdit.SetTile(vector, tile);
            }
        }
    }
}
