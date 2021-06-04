using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhausted : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Hero h = animator.GetComponent<Hero>();
        SpriteRenderer s;
        if (h)
        {
            s = animator.GetComponent<SpriteRenderer>();
            animator.GetComponent<Entity>().Exhausted = true;
        }
        else
        {
            animator.GetComponent<Character>().IsExhaustedAnim = true;
            s = animator.GetComponentsInChildren<SpriteRenderer>()[1];
        }

        s.color = Color.gray;

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Hero h = animator.GetComponent<Hero>();
        SpriteRenderer s;

        if (h)
        {
            s = animator.GetComponent<SpriteRenderer>();
            animator.GetComponent<Entity>().Exhausted = true;
        }
        else
        {
            s=animator.GetComponentsInChildren<SpriteRenderer>()[1];
            animator.GetComponent<Character>().IsExhaustedAnim = false;
        }


        s.color = Color.white;
    }
}
