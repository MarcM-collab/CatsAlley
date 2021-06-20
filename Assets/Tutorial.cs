using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrow;
    [SerializeField]
    private TileManager _tileManager;
    [SerializeField]
    private GameObject _enemyHero;
    [SerializeField]
    private GameObject _allyHero;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private MenuPanel _abilityPanel;
    [SerializeField]
    private Animator _rangedHeroPanel;
    private int _currentState => _animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
    private int _prevState = -1;

    private List<GameObject> _arrowList = new List<GameObject>();

    private bool _doneOnLoop;
    private bool _abiltyArrow;

    private bool UseAbilityCharacterTutorialDone;
    private bool UseAbilityHeroTutorialDone;
    private bool MeleeChoosingAttackTileHeroTutorialDone;
    private bool MeleeChoosingAttackTileCharacterTutorialDone;
    private bool RangedChoosingTileCharacterTutorialDone;
    private bool SelectingTutorialDone;
    private void OnEnable()
    {
        //one time
        SelectingPlayer.OnUseAbilityHeroTutorial += UseAbilityHeroTutorial;

        SelectingPlayer.OnSelectingTutorial += SelectingTutorial;
        SelectingPlayer.OnUseAbilityCharacterTutorial += UseAbilityCharacterTutorial;
        MeleeChoosingTilePlayer.OnUseAbilityCharacterTutorialEnd += UseAbilityCharacterTutorialEnd;
        RangedChoosingTilePlayer.OnUseAbilityCharacterTutorialEnd += UseAbilityCharacterTutorialEnd;
        MeleeChoosingTilePlayer.OnMeleeChoosingTileHeroTutorial += MeleeChoosingTileHeroTutorial;
        MeleeChoosingAttackTilePlayer.OnMeleeChoosingAttackTileHeroTutorial += MeleeChoosingAttackTileHeroTutorial;
        MeleeChoosingTilePlayer.OnMeleeChoosingTileCharacterTutorial += MeleeChoosingTileCharacterTutorial;
        MeleeChoosingAttackTilePlayer.OnMeleeChoosingAttackTileCharacterTutorial += MeleeChoosingAttackTileCharacterTutorial;
        RangedChoosingTilePlayer.OnRangedChoosingTileCharacterTutorial += RangedChoosingTileCharacterTutorial;

        //always
        RangedChoosingTilePlayer.OnCanNotAttackHeroTutorial += CanNotAttackHeroTutorial;
    }
    private void OnDisable()
    {
        //one time
        SelectingPlayer.OnSelectingTutorial -= SelectingTutorial;
        SelectingPlayer.OnUseAbilityCharacterTutorial -= UseAbilityCharacterTutorial;
        MeleeChoosingTilePlayer.OnUseAbilityCharacterTutorialEnd -= UseAbilityCharacterTutorialEnd;
        RangedChoosingTilePlayer.OnUseAbilityCharacterTutorialEnd -= UseAbilityCharacterTutorialEnd;
        MeleeChoosingTilePlayer.OnMeleeChoosingTileHeroTutorial -= MeleeChoosingTileHeroTutorial;
        MeleeChoosingAttackTilePlayer.OnMeleeChoosingAttackTileHeroTutorial -= MeleeChoosingAttackTileHeroTutorial;
        MeleeChoosingTilePlayer.OnMeleeChoosingTileCharacterTutorial -= MeleeChoosingTileCharacterTutorial;
        MeleeChoosingAttackTilePlayer.OnMeleeChoosingAttackTileCharacterTutorial -= MeleeChoosingAttackTileCharacterTutorial;
        RangedChoosingTilePlayer.OnRangedChoosingTileCharacterTutorial += RangedChoosingTileCharacterTutorial;

        //always
        RangedChoosingTilePlayer.OnCanNotAttackHeroTutorial -= CanNotAttackHeroTutorial;
    }
    private void Start()
    {
        OnEnable();
    }
    private void Update()
    {
        if (_abilityPanel != null)
        {
            if (_currentState != _prevState)
            {
                RemoveTutorialArrows();
                _doneOnLoop = false;
                _prevState = _currentState;

                _abilityPanel.Hide();
            }
        }
    }

    private void UseAbilityCharacterTutorial()
    {
        if (_abilityPanel != null)
        {
            if (!UseAbilityCharacterTutorialDone && !_doneOnLoop)
            {
                if (IsCharacterAbility())
                {
                    _arrowList.Add(Instantiate(_arrow, CharacterWithAbility().transform.position, Quaternion.identity));
                    _doneOnLoop = true;
                }
            }
        }
    }
    private void UseAbilityCharacterTutorialEnd()
    {
        if (_abilityPanel != null)
        {
            if (!UseAbilityCharacterTutorialDone)
            {
                if (!(EntityManager.ExecutorCharacter.GetComponent<UseAbility>() is null))
                {
                    _abilityPanel.Show();
                    UseAbilityCharacterTutorialDone = true;
                }
                else if (InputManager.LeftMouseClick)
                {
                    _abilityPanel.Hide();
                }
            }
        }
    }
    private bool IsCharacterAbility()
    {
        var list = EntityManager.GetActiveCharacters(Team.TeamPlayer);
        foreach(Character character in list)
        {
            if (!(character.GetComponent<UseAbility>() is null))
            {
                return true;
            }
        }
        return false;
    }
    private Character CharacterWithAbility()
    {
        var list = EntityManager.GetActiveCharacters(Team.TeamPlayer);
        foreach (Character character in list)
        {
            if (!(character.GetComponent<UseAbility>() is null))
            {
                return character;
            }
        }
        return null;
    }
    private void UseAbilityHeroTutorial()
    {
        if (_abilityPanel != null)
        {
            if (!UseAbilityHeroTutorialDone)
            {
                if (EntityManager.GetCharacters(Team.TeamAI).Length > 0)
                {
                    if (!_abiltyArrow)
                    {
                        _arrowList.Add(Instantiate(_arrow, _allyHero.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity));
                        _abiltyArrow = true;
                    }
                    else if (InputManager.LeftMouseClick && !_doneOnLoop)
                    {
                        _abilityPanel.Show();
                        _doneOnLoop = true;
                    }
                    else if (InputManager.LeftMouseClick)
                    {
                        _abilityPanel.Hide();
                        UseAbilityHeroTutorialDone = true;
                    }
                }
            }
        }
    }
    private void MeleeChoosingTileCharacterTutorial(Tilemap tilemap)
    {
        if (_abilityPanel != null)
        {
            if (!MeleeChoosingAttackTileCharacterTutorialDone && tilemap.ContainsTile(_tileManager.TargetTile) && !_doneOnLoop)
            {
                for (int x = tilemap.cellBounds.min.x; x <= tilemap.cellBounds.max.x; x++)
                {
                    for (int y = tilemap.cellBounds.min.y; y <= tilemap.cellBounds.max.y; y++)
                    {
                        Vector3Int vector = new Vector3Int(x, y, 0);
                        Vector3 vectorPos = vector + TileManager.CellSize;

                        if (tilemap.GetTile(vector) == _tileManager.TargetTile)
                        {
                            _arrowList.Add(Instantiate(_arrow, vectorPos, Quaternion.identity));
                        }
                    }
                }
                _doneOnLoop = true;
            }
        }
    }
    private void RemoveTutorialArrows()
    {
        foreach(GameObject go in _arrowList)
        {
            Destroy(go);
        }
        _arrowList.Clear();
    }
    private void MeleeChoosingAttackTileCharacterTutorial(Tilemap tilemap)
    {
        if (_abilityPanel != null)
        {
            if (!MeleeChoosingAttackTileCharacterTutorialDone)
            {
                for (int x = tilemap.cellBounds.min.x; x <= tilemap.cellBounds.max.x; x++)
                {
                    for (int y = tilemap.cellBounds.min.y; y <= tilemap.cellBounds.max.y; y++)
                    {
                        Vector3Int vector = new Vector3Int(x, y, 0);
                        Vector3 vectorPos = vector + TileManager.CellSize;

                        if (tilemap.GetTile(vector) == _tileManager.TargetTile && tilemap.WorldToCell(EntityManager.TargetCharacter.transform.position) != vector)
                        {
                            _arrowList.Add(Instantiate(_arrow, vectorPos, Quaternion.identity));
                        }
                    }
                }
                MeleeChoosingAttackTileCharacterTutorialDone = true;
            }
        }
    }
    private void MeleeChoosingTileHeroTutorial()
    {
        if (_abilityPanel != null)
        {
            if (!MeleeChoosingAttackTileHeroTutorialDone && !_doneOnLoop)
            {
                var go = Instantiate(_arrow, _enemyHero.transform);
                go.transform.position += new Vector3(0, 0.5f, 0);
                _arrowList.Add(go);
                _doneOnLoop = true;
            }
        }
    }
    private void MeleeChoosingAttackTileHeroTutorial(Tilemap tilemap)
    {
        if (_abilityPanel != null)
        {
            if (!MeleeChoosingAttackTileHeroTutorialDone)
            {
                for (int x = tilemap.cellBounds.min.x; x <= tilemap.cellBounds.max.x; x++)
                {
                    for (int y = tilemap.cellBounds.min.y; y <= tilemap.cellBounds.max.y; y++)
                    {
                        Vector3Int vector = new Vector3Int(x, y, 0);
                        Vector3 vectorPos = vector + TileManager.CellSize;

                        if (tilemap.GetTile(vector) == _tileManager.TargetTile)
                        {
                            _arrowList.Add(Instantiate(_arrow, vectorPos, Quaternion.identity));
                        }
                    }
                }
                MeleeChoosingAttackTileHeroTutorialDone = true;
            }
        }
    }

    private void RangedChoosingTileCharacterTutorial (Tilemap tilemap)
    {
        if (_abilityPanel != null)
        {
            if (!RangedChoosingTileCharacterTutorialDone && tilemap.ContainsTile(_tileManager.TargetTile))
            {
                for (int x = tilemap.cellBounds.min.x; x <= tilemap.cellBounds.max.x; x++)
                {
                    for (int y = tilemap.cellBounds.min.y; y <= tilemap.cellBounds.max.y; y++)
                    {
                        Vector3Int vector = new Vector3Int(x, y, 0);
                        Vector3 vectorPos = vector + TileManager.CellSize;

                        if (tilemap.GetTile(vector) == _tileManager.TargetTile)
                        {
                            _arrowList.Add(Instantiate(_arrow, vectorPos, Quaternion.identity));
                        }
                    }
                }
                RangedChoosingTileCharacterTutorialDone = true;
            }
        }
    }
    private void SelectingTutorial()
    {
        if (_abilityPanel != null)
        {
            var characterList = EntityManager.GetActiveCharacters(Team.TeamPlayer);
            if (characterList.Length > 0 && !SelectingTutorialDone)
            {
                _arrowList.Add(Instantiate(_arrow, characterList[0].transform.position, Quaternion.identity));
                SelectingTutorialDone = true;
            }
        }
    }
    private void CanNotAttackHeroTutorial()
    {
        if (_abilityPanel != null)
        {
            if (InputManager.LeftMouseClick)
            {
                Debug.Log("Ranged");
                _rangedHeroPanel.SetTrigger("Triggered");
            }
        }
    }
}
