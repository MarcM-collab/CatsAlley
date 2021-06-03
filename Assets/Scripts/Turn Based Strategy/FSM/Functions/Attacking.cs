using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : CombatBehaviour
{
    private void OnEnable()
    {
        AttackingBehaviour.OnAttackingEnter += AttackingEnter;
        AttackingBehaviour.OnAttackingUpdate += AttackingUpdate;
        AttackingBehaviour.OnAttackingExit += AttackingExit;
    }
    private void OnDisable()
    {
        AttackingBehaviour.OnAttackingEnter -= AttackingEnter;
        AttackingBehaviour.OnAttackingUpdate -= AttackingUpdate;
        AttackingBehaviour.OnAttackingExit -= AttackingExit;
    }
    private void AttackingEnter(Animator animator)
    {
        var deltaX = _targetGridPosition.x - _tileChosenGridPosition.x;
        _executorCharacter.TurningExecutor(deltaX);
        _executorCharacter.Attack = true;


        if (animator.GetBool("Ranged"))
        {
            _executorCharacter.GetComponentInChildren<BulletSpawner>().Spawn();
        }
    }
    private void AttackingUpdate(Animator animator)
    {
        animator.SetBool("Attacking", false);
    }
    private void AttackingExit()
    {
    }
}
