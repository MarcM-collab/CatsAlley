using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeCardsBehaviour : StateMachineBehaviour
{
    public delegate void ThreeCardsEnterDelegate(Animator animator);
    public static ThreeCardsEnterDelegate OnThreeCardsEnter;
    public delegate void ThreeCardsUpdateDelegate(Animator animator);
    public static ThreeCardsUpdateDelegate OnThreeCardsUpdate;
    public delegate void ThreeCardsExitDelegate();
    public static ThreeCardsExitDelegate OnThreeCardsExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnThreeCardsEnter?.Invoke(animator);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnThreeCardsUpdate?.Invoke(animator);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnThreeCardsExit?.Invoke();
    }
}
