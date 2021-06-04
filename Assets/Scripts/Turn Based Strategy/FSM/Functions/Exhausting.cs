using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhausting : CombatBehaviour
{
    private bool _targetIsDead;
    private void OnEnable()
    {
        ExhaustingBehaviour.OnExhaustingEnter += ExhaustingEnter;
        ExhaustingBehaviour.OnExhaustingUpdate += ExhaustingUpdate;
        ExhaustingBehaviour.OnExhaustingExit += ExhaustingExit;
    }
    private void OnDisable()
    {
        ExhaustingBehaviour.OnExhaustingEnter -= ExhaustingEnter;
        ExhaustingBehaviour.OnExhaustingUpdate -= ExhaustingUpdate;
        ExhaustingBehaviour.OnExhaustingExit -= ExhaustingExit;
    }
    private void ExhaustingEnter(Animator animator)
    {
        ResetAnimatorParamaters(animator);

        if (_executorCharacter.Team == Team.TeamPlayer)
        {
            if (_executorCharacter.transform.eulerAngles.y == 0)
            {
                _executorCharacter.Turn = true;
            }
        }
        if (_executorCharacter.Team == Team.TeamAI)
        {
            if (_executorCharacter.transform.eulerAngles.y == 180)
            {
                _executorCharacter.Turn = true;
            }
        }

        _executorCharacter.Exhausted = true;

        Cursor.SetCursor(_cursorHand, Vector2.zero, CursorMode.Auto);
    }
    private void ResetAnimatorParamaters(Animator animator)
    {
        foreach (AnimatorControllerParameter paramater in animator.parameters)
        {
            if (paramater.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(paramater.name, false);
            }
        }
    }
    private void ExhaustingUpdate(Animator animator)
    {
        animator.SetTrigger("Exhausted");
    }
    private void ExhaustingExit(Animator animator)
    {
    }
}
