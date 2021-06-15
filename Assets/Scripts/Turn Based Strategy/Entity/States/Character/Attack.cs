using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EntityManager.TargetCharacter.Hit = true;
        var c = animator.GetComponent<Character>();
        c.Attack = false;
        if (c.Class == Class.Melee)
        {
            AudioSource AS = c.GetComponent<AudioSource>();
            AS.volume = 1;
            AS.clip = AudioManager.audioManager.GetRandomSwiss();
            AS.Play();
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
