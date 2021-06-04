using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HealthSystem.TakeDamage(EntityManager.ExecutorCharacter.currentAttack);

        EntityManager.TargetCharacter.GetComponent<MeowAudio>().PlayAudioMew();
        animator.GetComponent<Entity>().ChangeHealth();

        SpriteRenderer[] r = animator.GetComponentsInChildren<SpriteRenderer>();
        if (r.Length > 1)
            r[1].color = Color.red;

        animator.GetComponent<Entity>().Hit = false;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Entity>().Hit = false;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SpriteRenderer[] r = animator.GetComponentsInChildren<SpriteRenderer>();
        if (r.Length > 1)
            r[1].color = Color.white;

        animator.GetComponent<Entity>().Hit = false;
        EntityManager.ExecutorCharacter.currentAttack = EntityManager.ExecutorCharacter.AttackPoints;
    }
}
