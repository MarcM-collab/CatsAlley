using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCanvasControl : MonoBehaviour
{
    public MenuPanel posPlayer, posIA, infoCanvas;

    private void OnEnable()
    {
        if (TurnManager.TeamTurn == Team.TeamPlayer)
        {
            //SELECT
            SelectingBehaviour.OnSelectingExit += HidePlayer;
            SelectingBehaviour.OnSelectingExit += HideIA;

            SelectingBehaviour.OnSelectingEnter += ShowPlayer;
            SelectingBehaviour.OnSelectingEnter += ShowIA;

            //Drawable cards
            ChooseDrawableCardsBehaviour.OnChooseDrawableCardsExit += HideIA;
            ChooseDrawableCardsBehaviour.OnChooseDrawableCardsExit += HideInfo;

            ChooseDrawableCardsBehaviour.OnChooseDrawableCardsEnter += ShowIA;
            ChooseDrawableCardsBehaviour.OnChooseDrawableCardsEnter += ShowInfo;

            //3 cards
            ThreeCardsBehaviour.OnThreeCardsExit += HideIA;
            ThreeCardsBehaviour.OnThreeCardsExit += HideInfo;

            ThreeCardsBehaviour.OnThreeCardsEnter += ShowIA;
            ThreeCardsBehaviour.OnThreeCardsEnter += ShowInfo;
        }
    }

    private void OnDisable()
    {
        if (TurnManager.TeamTurn == Team.TeamPlayer)
        {
            //SELECT
            SelectingBehaviour.OnSelectingExit -= HidePlayer;
            SelectingBehaviour.OnSelectingExit -= HideIA;

            SelectingBehaviour.OnSelectingEnter -= ShowPlayer;
            SelectingBehaviour.OnSelectingEnter -= ShowIA;

            //Drawable cards
            ChooseDrawableCardsBehaviour.OnChooseDrawableCardsExit -= HideIA;
            ChooseDrawableCardsBehaviour.OnChooseDrawableCardsExit -= HideInfo;

            ChooseDrawableCardsBehaviour.OnChooseDrawableCardsEnter -= ShowIA;
            ChooseDrawableCardsBehaviour.OnChooseDrawableCardsEnter -= ShowInfo;

            //3 cards
            ThreeCardsBehaviour.OnThreeCardsExit -= HideIA;
            ThreeCardsBehaviour.OnThreeCardsExit -= HideInfo;

            ThreeCardsBehaviour.OnThreeCardsEnter -= ShowIA;
            ThreeCardsBehaviour.OnThreeCardsEnter -= ShowInfo;
        }

    }
    private void HideIA()
    {
        posIA.Hide();
    }
    private void ShowIA(Animator a)
    {
        posIA.Show();
    }
    private void HidePlayer()
    {
        posPlayer.Hide();
    }
    private void ShowPlayer(Animator a)
    {
        posPlayer.Show();
    }
    private void HideInfo()
    {
        infoCanvas.Hide();
    }
    private void ShowInfo(Animator a)
    {
        if (TurnManager.TeamTurn == Team.TeamPlayer)
        {
            infoCanvas.Show();
        }

    }
}
