using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeShowAttackTilePlayer : CombatPlayerBehaviour
{
    private void OnEnable()
    {
        MeleeShowAttackRangeBehaviour.OnMeleeShowAttackRangeEnter += MeleeShowAttackRangeEnter;
    }
    private void OnDisable()
    {
        MeleeShowAttackRangeBehaviour.OnMeleeShowAttackRangeEnter -= MeleeShowAttackRangeEnter;
    }
    private void MeleeShowAttackRangeEnter()
    {
        var cellSize = TileManager.CellSize;

        TileManager.ShowTilesInTilemap(_uITilemap, _uITilemap, _collisionAllyTile, IsPath);

        var IsCharacter = !(_targetEntity.GetComponent("Character") as Entity is null);
        if (IsCharacter)
        {
            for (int j = -1; j <= 1; j++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    var position = new Vector3Int(i, j, 0);
                    var currentGridPosition = _targetGridPosition + position;
                    var currentGridCenterPosition = currentGridPosition + cellSize;

                    if (_uITilemap.HasTile(currentGridPosition))
                    {
                        if (InTile(currentGridCenterPosition) == (int)EntityType.Nothing && _uITilemap.GetTile(currentGridPosition) != _collisionAllyTile)
                        {
                            _uITilemap.SetTile(currentGridPosition, _targetTile);
                        }
                    }
                }
            }
            HideHeroTiles();
        }
        else
        {
            for (int x = 0; x < _enemyHeroAttackableTiles.Count; x++)
            {
                var currentGridPosition = _enemyHeroAttackableTiles[x];
                var currentGridCenterPosition = currentGridPosition + cellSize;

                var IsNothingOrIsEnemy = InTile(currentGridCenterPosition) == (int)EntityType.Nothing ||
                    InTile(currentGridCenterPosition) == (int)EntityType.EnemyCharacter || InTile(currentGridCenterPosition) == (int)EntityType.EnemyHero;

                if (_floorTilemap.HasTile(currentGridPosition) && _uITilemap.GetTile(currentGridPosition) == _allyTile)
                {
                    if (IsNothingOrIsEnemy)
                    {
                        _uITilemap.SetTile(currentGridPosition, _targetTile);
                    }
                }
            }
            //!(i == 0 && j == 0) && _uITilemap.HasTile(currentGridPosition) && (currentGridPosition == _executorGridPos || !(InTile(currentGridCenterPosition) == (int)EntityType.AllyCharacter)) && !_collisionTilemap.HasTile(currentGridPosition)
        }
    }
    private bool IsPath(Vector3Int vector)
    {
        return _uITilemap.GetTile(vector) == _targetTile && vector != _targetGridPosition;
    }
}
