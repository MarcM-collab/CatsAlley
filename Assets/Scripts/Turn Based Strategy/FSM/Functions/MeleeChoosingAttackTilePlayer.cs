using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeChoosingAttackTilePlayer : CombatPlayerBehaviour
{
    private void OnEnable()
    {
        MeleeChoosingAttackTileBehaviour.OnMeleeChoosingAttackTileUpdate += MeleeChoosingAttackTileUpadate;
    }
    private void OnDisable()
    {
        MeleeChoosingAttackTileBehaviour.OnMeleeChoosingAttackTileUpdate += MeleeChoosingAttackTileUpadate;
    }
    private void MeleeChoosingAttackTileUpadate(Animator animator)
    {
        var PointingNewTile = _currentGridPos != _lastGridPos;
        var PointingSpawnableTile = _uITilemap.GetTile(_currentGridPos) == _targetTile && _currentGridPos != _targetGridPosition && !IsEnemy();
        var LeavingSpawnableZone = _uITilemap.GetTile(_lastGridPos) == _attackingTile;
        var PointingNewSpawnableTile = _uITilemap.GetTile(_lastGridPos) == _attackingTile;

        if (PointingNewTile)
        {
            if (PointingSpawnableTile)
            {
                _uITilemap.SetTile(_currentGridPos, _attackingTile);
            }
            else
            {
                if (LeavingSpawnableZone)
                {
                    _uITilemap.SetTile(_lastGridPos, _targetTile);
                }
            }
            if (PointingNewSpawnableTile)
            {
                _uITilemap.SetTile(_lastGridPos, _targetTile);
            }
            _lastGridPos = _currentGridPos;
        }

        var SelectInput = InputManager.LeftMouseClick;
        if (SelectInput)
        {
            if (InTile(_currentGridPos + TileManager.CellSize) == (int)EntityType.EnemyHero || InTile(_currentGridPos + TileManager.CellSize) == (int)EntityType.EnemyCharacter)
            {
                Debug.Log(IsHeroMelee());
                if (IsExecutorMelee() || (IsHeroMelee() && InTile(_currentGridPos + TileManager.CellSize) == (int)EntityType.EnemyHero))
                {
                    _tileChosenGridPosition = _executorGridPosition;
                    _uITilemap.SetTile(_executorGridPosition, _allyTile);
                    animator.SetTrigger("TileChosen");
                    animator.SetBool("Attacking", true);
                }
            }
            else
            {
                var AttackTileNotSelcted = _attackingTile != _uITilemap.GetTile(_currentGridPos);
                if (AttackTileNotSelcted)
                {
                    animator.SetBool("PreparingAttack", false);
                }
                else
                {
                    _tileChosenGridPosition = _currentGridPos;
                    _uITilemap.SetTile(_currentGridPos, _allyTile);
                    animator.SetTrigger("TileChosen");
                    animator.SetBool("Attacking", true);
                }
            }
        }
    }
    private bool IsExecutorMelee()
    {
        for (int j = -1; j <= 1; j++)
        {
            for (int i = -1; i <= 1; i++)
            {
                var position = new Vector3Int(i, j, 0);
                var currentGridPosition = _targetGridPosition + position;

                if (currentGridPosition == _executorGridPosition)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool IsHeroMelee()
    {
        for (int j = -1; j <= 1; j++)
        {
            for (int i = -1; i <= 1; i++)
            {
                var position = new Vector3Int(i, j, 0);
                var currentGridPosition = _executorGridPosition + position;
                var currentGridCenterPosition = currentGridPosition + TileManager.CellSize;

                if (InTile(currentGridCenterPosition) == (int)EntityType.EnemyHero)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
