using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCanvasControl : MonoBehaviour
{
    public MenuPanel posPlayer, posIA, infoCanvas;
    private void Start()
    {
        posPlayer.Show();
        posIA.Show();
    }
    private void OnEnable()
    {            //SELECT
                 //SelectingBehaviour.OnSelectingExit += HidePlayer;
                 //SelectingBehaviour.OnSelectingExit += HideIA;

        //SelectingBehaviour.OnSelectingEnter += ShowPlayer;
        //SelectingBehaviour.OnSelectingEnter += ShowIA;

        HideRangeBehaviour.OnHideRangeEnter += ShowPlayerAnim;
        HideRangeBehaviour.OnHideRangeEnter += ShowIAAniamtor;

        MeleeShowRangeBehaviour.OnMeleeShowRangeEnter += HidePlayer;
        MeleeShowRangeBehaviour.OnMeleeShowRangeEnter += HideIA;

        RangedShowRangeBehaviour.OnRangedShowRangeEnter += HidePlayer;
        RangedShowRangeBehaviour.OnRangedShowRangeEnter += HideIA;

        HideRangeBehaviour.OnHideRangeEnter += ShowPlayerAnim;
        HideRangeBehaviour.OnHideRangeEnter += ShowIAAniamtor;

        HideUIBehaviour.OnHideUIEnter += ShowPlayer;
        HideUIBehaviour.OnHideUIEnter += ShowIA;


        //Drawable cards
        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsExit += ShowIA;
        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsExit += ShowInfo;

        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsEnter += HideIAAnimator;
        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsEnter += HideInfo;

        //3 cards
        ThreeCardsBehaviour.OnThreeCardsEnter += HideIAAnimator;
        ThreeCardsBehaviour.OnThreeCardsEnter += HideInfo;

        ThreeCardsBehaviour.OnThreeCardsExit += ShowIA;
        ThreeCardsBehaviour.OnThreeCardsExit += ShowInfo;
        //if (TurnManager.TeamTurn == Team.TeamPlayer)
        //{

        //}
    }

    private void OnDisable()
    {
        //SELECT
        //SelectingBehaviour.OnSelectingExit -= HidePlayer;
        //SelectingBehaviour.OnSelectingExit -= HideIA;

        //SelectingBehaviour.OnSelectingEnter -= ShowPlayer;
        //SelectingBehaviour.OnSelectingEnter -= ShowIA;

        HideRangeBehaviour.OnHideRangeEnter -= ShowPlayerAnim;
        HideRangeBehaviour.OnHideRangeEnter -= ShowIAAniamtor;

        MeleeShowRangeBehaviour.OnMeleeShowRangeEnter -= HidePlayer;
        MeleeShowRangeBehaviour.OnMeleeShowRangeEnter -= HideIA;

        HideRangeBehaviour.OnHideRangeEnter -= ShowPlayerAnim;
        HideRangeBehaviour.OnHideRangeEnter -= ShowIAAniamtor;


        HideUIBehaviour.OnHideUIEnter -= ShowPlayer;
        HideUIBehaviour.OnHideUIEnter -= ShowIA;
        //Drawable cards
        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsExit -= ShowIA;
        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsExit -= ShowInfo;

        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsEnter -= HideIAAnimator;
        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsEnter -= HideInfo;

        //3 cards
        ThreeCardsBehaviour.OnThreeCardsEnter -= HideIAAnimator;
        ThreeCardsBehaviour.OnThreeCardsEnter -= HideInfo;

        ThreeCardsBehaviour.OnThreeCardsExit -= ShowIA;
        ThreeCardsBehaviour.OnThreeCardsExit -= ShowInfo;

    }
    private void HideIAAnimator(Animator a)
    {
        HideIA();
    }
    private void HideIA()
    {
        if (TurnManager.TeamTurn == Team.TeamPlayer)
        {
            posIA.Hide();
        }
    }

    private void ShowIA()
    {
        if (TurnManager.TeamTurn == Team.TeamPlayer)
        {
            posIA.Show();
        }
    }
    private void ShowIAAniamtor(Animator a)
    {
        ShowIA();
    }
    private void HidePlayer()
    {
        if (TurnManager.TeamTurn == Team.TeamPlayer)
        {
            posPlayer.Hide();
        }
    }
    private void ShowPlayerAnim(Animator a)
    {
        ShowPlayer();
    }
    private void ShowPlayer()
    {
        if (TurnManager.TeamTurn == Team.TeamPlayer)
        {
            posPlayer.Show();
        }
    }
    private void HideInfo(Animator a)
    {
        if (TurnManager.TeamTurn == Team.TeamPlayer)
        {
            infoCanvas.Hide();
        }
    }
    private void ShowInfo()
    {
        if (TurnManager.TeamTurn == Team.TeamPlayer)
        {
            infoCanvas.Show();
        }
    }
}
