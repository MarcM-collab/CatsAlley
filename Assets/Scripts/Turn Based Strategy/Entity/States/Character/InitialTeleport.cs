using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialTeleport : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var character = animator.GetComponent<Character>();
        AudioSource AS = character.GetComponent<AudioSource>();
        AS.volume = 1;
        AS.clip = AudioManager.audioManager.teleportSFX;
        AS.Play();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var character = animator.GetComponent<Character>();
        animator.transform.position = character.TeleportPoint;
    }
}
