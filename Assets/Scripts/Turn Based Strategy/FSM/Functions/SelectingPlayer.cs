using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingPlayer : CombatPlayerBehaviour
{
    public delegate void UseAbilityCharacterTutorialDelegate();
    public static UseAbilityCharacterTutorialDelegate OnUseAbilityCharacterTutorial;
    public delegate void OnSelectingTutorialDelegate();
    public static OnSelectingTutorialDelegate OnSelectingTutorial;
    public delegate void OnUseAbilityHeroTutorialDelegate();
    public static OnUseAbilityHeroTutorialDelegate OnUseAbilityHeroTutorial;

    private bool _buttonEnabled;
    private void OnEnable()
    {
        SelectingBehaviour.OnSelectingEnter += SelectingEnter;
        SelectingBehaviour.OnSelectingUpdate += SelectingUpdate;
        SelectingBehaviour.OnSelectingExit += SelectingExit;
    }
    private void OnDisable()
    {
        SelectingBehaviour.OnSelectingEnter -= SelectingEnter;
        SelectingBehaviour.OnSelectingUpdate -= SelectingUpdate;
        SelectingBehaviour.OnSelectingExit -= SelectingExit;
    }
    private void SelectingEnter(Animator animator)
    {
        EnablePassButton(true);
    }
    private void SelectingUpdate(Animator animator)
    {
        if (!TurnManager.ExtraCards)
        {
            animator.SetBool("CardsDrawn", true);
        }
        else if (!TurnManager.CardDrawn)
        {
            animator.SetBool("ChooseCard", true);
            _uITilemap.SetTile(_currentGridPos, null);
        }
        else
        {
            if (!CardUsage.isDragging)
            {
                //TileHighlighting(animator);

                Selected(animator);

            }
            else
            {
                animator.SetBool("IsDragging", true);
                _uITilemap.SetTile(_currentGridPos, null);
            }
        }
    }
    private void SelectingExit()
    {
        EnablePassButton(false);
    }
    private void EnablePassButton(bool _bool)
    {
        _buttonEnabled = _bool;
    }
    public void PassTurnButton()
    {
        if (_buttonEnabled && TurnManager.CardDrawn)
        {
            if (TurnManager.TeamTurn == Team.TeamPlayer)
            {
                TurnManager.NextTurn();
            }
        }
    }
    private void TileHighlighting(Animator animator)
    {
        var PointingNewFloorTile = _floorTilemap.HasTile(_currentGridPos) && _currentGridPos != _lastGridPos;
        var PointingOutOfFloor = !_floorTilemap.HasTile(_currentGridPos);

        if (!animator.GetBool("Selected"))
        {
            if (PointingNewFloorTile)
            {
                _uITilemap.SetTile(_lastGridPos, null);
                _uITilemap.SetTile(_currentGridPos, _pointingTile);
                _lastGridPos = _currentGridPos;
            }
            else if (PointingOutOfFloor)
            {
                _uITilemap.SetTile(_lastGridPos, null);
            }
        }
    }
    private void Selected(Animator animator)
    {
        var TileSelected = InputManager.LeftMouseClick;

        OnUseAbilityHeroTutorial?.Invoke();
        OnUseAbilityCharacterTutorial?.Invoke();
        OnSelectingTutorial?.Invoke();

        if (TileSelected)
        {
            var tempCharacter = SelectCharacter();
            var PointingACharacter = !(tempCharacter is null);

            if (PointingACharacter)
            {
                var CharacterOfTeamPlayer = EntityManager.IsEntityInList(EntityManager.GetActiveCharacters(Team.TeamPlayer), tempCharacter);

                if (CharacterOfTeamPlayer)
                {
                    EntityManager.SetExecutor(tempCharacter);

                    TeamPlayerLength = EntityManager.GetCharacters(Team.TeamPlayer).Length;

                    _executorGridPosition = _currentGridPos;

                    var CharacterIsRanged = tempCharacter.Class == Class.Ranged;

                    if (CharacterIsRanged)
                    {
                        animator.SetBool("Ranged", true);
                    }
                    else
                    {
                        animator.SetBool("Ranged", false);
                    }
                    animator.SetBool("Selected", true);
                    _uITilemap.SetTile(_currentGridPos, null);
                }
            }
        }
    }
}
