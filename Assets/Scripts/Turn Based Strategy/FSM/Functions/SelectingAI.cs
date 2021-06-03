using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingAI : CombatAIBehaviour
{
    private bool _spawned;
    [SerializeField]
    private float delayTime = 1.0f;
    private float timer;
    private void OnEnable()
    {
        SelectingBehaviour.OnSelectingEnter += SelectingEnter;
        SelectingBehaviour.OnSelectingUpdate += SelectingUpdate;
    }
    private void OnDisable()
    {
        SelectingBehaviour.OnSelectingEnter -= SelectingEnter;
        SelectingBehaviour.OnSelectingUpdate -= SelectingUpdate;
    }
    private void SelectingEnter(Animator animator)
    {
        timer = 0.0f;
    }
    private void SelectingUpdate(Animator animator)
    {
        var CharactersActive = EntityManager.GetActiveCharacters(Team.TeamAI).Length > 0;
        if (!TurnManager.ExtraCards)
        {
            animator.SetBool("CardsDrawn", true);
        }
        else if (!TurnManager.CardDrawn)
        {
            animator.SetBool("ChooseCard", true);
        }
        else if (!TurnManager.Spawned)
        {
            animator.SetBool("IsDragging", true);
        }
        else
        {
            if (timer >= delayTime)
            {
                if (CharactersActive)
                {
                    var characters = EntityManager.GetActiveCharacters(Team.TeamAI);

                    TeamAILength = EntityManager.GetCharacters(Team.TeamAI).Length;

                    var _currentGridPos = _floorTilemap.WorldToCell(characters[0].transform.position);

                    if (characters[0].Class == Class.Ranged)
                    {
                        animator.SetBool("Ranged", true);
                    }
                    else
                    {
                        animator.SetBool("Ranged", false);
                    }
                    animator.SetBool("Selected", true);

                    _executorGridPosition = _currentGridPos;
                    _uITilemap.SetTile(_executorGridPosition, _allyTile);
                    EntityManager.SetExecutor(characters[0]);
                    _notPossibleTarget.Clear();
                }
                else
                {
                    TurnManager.NextTurn();
                }
            }
        }
        
        timer += Time.deltaTime;
    }
}
