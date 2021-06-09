using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoManager : MonoBehaviour
{
    public CanvasGroup abilityDiplay;
    public Image spriteShower;
    public Image buttonUse;
    public Image whiskasIm;
    private Color buttonUseColor;
    private Color whiskasColor;
    public TMP_Text attackText;
    public TMP_Text abilityInfo;
    public TMP_Text abilityCost;
    private Camera mainCamera;
    private Character targetChar;
    private Hero targetHero;
    private Hero[] heroes = new Hero[2];
    private UseAbility currentAbility;
    private int currentAttack = 0;
    private Vector2 GetMousePosition
    {
        get { return mainCamera.ScreenToWorldPoint(Input.mousePosition); }
    }

    private void Start()
    {
        spriteShower.color = new Color(spriteShower.color.r, spriteShower.color.g, spriteShower.color.b, 0);
        buttonUseColor = buttonUse.color;
        whiskasColor = whiskasIm.color;
        mainCamera = Camera.main; //This will avoid extra iterations searching for a Game Object with tag in the whole scene.
        HideAbilityInfo();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayCast = Physics2D.Raycast(GetMousePosition, Vector2.zero);

            if (rayCast)
            {
                if (rayCast.transform.CompareTag("Character"))
                {
                    targetHero = null;
                    targetChar = rayCast.transform.gameObject.GetComponent<Character>();
                    currentAttack = targetChar.AttackPoints;
                    ShowSprite(targetChar.gameObject);
                    ShowBasicInfo();
                    if (targetChar.Team == Team.TeamPlayer)
                    {
                        if (!targetChar.Exhausted)
                            ShowAbility(targetChar.gameObject);
                    }
                }
                else if (rayCast.transform.CompareTag("Hero"))
                {
                    targetChar = null;
                    targetHero = rayCast.transform.gameObject.GetComponent<Hero>();

                    ShowSprite(targetHero.gameObject);

                    //ShowSprite(targetHero.GetComponentsInChildren<Transform>()[1].gameObject);

                    if (targetHero.Team == Team.TeamPlayer)
                    {
                        if (!targetHero.Exhausted)
                            ShowAbility(targetHero.gameObject);
                    }
                }
            }
        }
        if (targetChar)
        {
            if (currentAttack != targetChar.AttackPoints)
            {
                currentAttack = targetChar.AttackPoints;
                ShowBasicInfo();
            }
            if (targetChar.Team == Team.TeamAI || targetChar.Exhausted || TurnManager.TeamTurn != Team.TeamPlayer)
            {
                HideAbilityInfo();
            }
            else if (TurnManager.TeamTurn == Team.TeamPlayer)
            {
                ShowAbility(targetChar.gameObject);
            }
        }
        if (targetHero)
        {
            if (targetHero.Team == Team.TeamAI || targetHero.Exhausted || TurnManager.TeamTurn != Team.TeamPlayer)
            {
                HideAbilityInfo();
            }
            else if (TurnManager.TeamTurn == Team.TeamPlayer)
            {
                ShowAbility(targetHero.gameObject);
            }
        }
        if (TurnManager.TeamTurn != Team.TeamPlayer)
            HideAbilityInfo();

        if (currentAbility)
            if (currentAbility.hasBeenUsed)
                HideAbilityInfo();

    }
    private void Hide()
    {
        HideAbilityInfo();
        spriteShower.color = new Color(spriteShower.color.r, spriteShower.color.g, spriteShower.color.b, 0);
        attackText.text = "";
    }
    private void InitSpriteShower()
    {
        if (spriteShower.color.a <= 0)
        {
            spriteShower.color = new Color(spriteShower.color.r, spriteShower.color.g, spriteShower.color.b, 1);
        }
    }
    private void ShowSprite(GameObject toshow)
    {
        spriteShower.sprite = toshow.GetComponent<SpriteRenderer>().sprite;
        InitSpriteShower();
    }
    private void ShowBasicInfo()
    {
        attackText.text = targetChar.AttackPoints.ToString();

        HideAbilityInfo();
    }

    private void HideAbilityInfo()
    {
        abilityInfo.text = "";
        abilityDiplay.alpha = 0;
        abilityDiplay.interactable = false;
        abilityDiplay.blocksRaycasts = false;
        currentAbility = null; //resets and avoids casting when another char selected.
    }

    private void ShowAbility(GameObject target)
    {
        currentAbility = target.GetComponentInChildren<UseAbility>();

        if (currentAbility)
        {
            abilityDiplay.alpha = 1;
            abilityDiplay.interactable = true;
            abilityDiplay.blocksRaycasts = true;
            abilityInfo.text = currentAbility.ability.textExplain;
            abilityCost.text = currentAbility.ability.whiskasCost.ToString();
        }
    }
    public void UseAbility()
    {
        if (TurnManager.currentMana < currentAbility.ability.whiskasCost)
        {
            StartCoroutine(NotEnough(buttonUse, buttonUseColor));
            StartCoroutine(NotEnough(whiskasIm, whiskasColor));
        }
        else if (currentAbility)
        {
            currentAbility.Use();
        }
    }
    private IEnumerator NotEnough(Image i, Color startColor)
    {
        i.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        i.color = startColor;
    }
}
