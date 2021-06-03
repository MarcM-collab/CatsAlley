using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedChoosingTileAI : CombatAIBehaviour
{
    Character[] characters;
    [SerializeField]
    private float delayTime = 1.0f;
    private float timer;

    private bool _goingToAttack;

    private void OnEnable()
    {
        RangedChoosingTileBehaviour.OnRangedChoosingTileEnter += RangedChoosingTileEnter;
        RangedChoosingTileBehaviour.OnRangedChoosingTileUpdate += RangedChoosingTileUpdate;
    }
    private void OnDisable()
    {
        RangedChoosingTileBehaviour.OnRangedChoosingTileEnter -= RangedChoosingTileEnter;
        RangedChoosingTileBehaviour.OnRangedChoosingTileUpdate -= RangedChoosingTileUpdate;
    }
    private void RangedChoosingTileEnter(Animator animator)
    {
        characters = EntityManager.GetLivingCharacters(Team.TeamPlayer);
        if (characters.Length > 0)
        {
            _goingToAttack = true;
            timer = 0.0f;
        }
        else
        {
            _executorCharacter.Exhausted = true;
            foreach (AnimatorControllerParameter paramater in animator.parameters)
            {
                if (paramater.type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(paramater.name, false);
                }
            }
        }
    }
    private void RangedChoosingTileUpdate(Animator animator)
    {
        if (timer >= delayTime)
        {
            AttackEnemy();
            animator.SetBool("Attacking", true);
            animator.SetTrigger("TileChosen");
            _goingToAttack = false;
        }
        timer += Time.deltaTime;
    }
    private void AttackEnemy()
    {
        EntityManager.SetTarget(characters[0]);
        _targetGridPosition = _floorTilemap.WorldToCell(characters[0].transform.position);
        _uITilemap.SetTile(_targetGridPosition, _targetTile);

        _tileChosenGridPosition = _executorGridPosition;
    }
}
