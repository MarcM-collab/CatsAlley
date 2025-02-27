using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseDrawableCardsPlayer : MonoBehaviour
{
    private List<Card> _cards; //Baraja elegida por el player (8 cartas)
    [SerializeField]
    private DeckPlayer _deckPlayer;

    Card[] twoCardsRandom = new Card[2]; //lista para las dos cartas aleatorias.
    [SerializeField]
    private Image[] buttons;
    private MenuPanel[] buttonsPanel = new MenuPanel[2];
    private RectTransform[] cardInstancePos = new RectTransform[2];
    private GameObject[] cardsGO = new GameObject[2];

    [SerializeField]
    private HandManager HandManager;

    private bool cardSelected;

    public MenuPanel toShow;

    private void OnEnable()
    {
        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsEnter += ChooseDrawableCardsEnter;
        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsUpdate += ChooseDrawableCardsUpdate;
    }
    private void OnDisable()
    {
        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsEnter -= ChooseDrawableCardsEnter;
        ChooseDrawableCardsBehaviour.OnChooseDrawableCardsUpdate -= ChooseDrawableCardsUpdate;
    }
    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            cardInstancePos[i] = buttons[i].GetComponentInChildren<RectTransform>();
            buttonsPanel[i] = buttons[i].GetComponent<MenuPanel>();
        }
    }
    private void Start()
    {
        _cards = _deckPlayer.Cards;
    }
    private void ChooseDrawableCardsEnter(Animator animator)
    {
        if (HandManager.HandPlayer.Count < HandManager.HandLimit)
        {
            RemovePreviousCards();
            ChooseRandom();
            ShowRandomCards();
        }
        else
        {
            animator.SetBool("ChooseCard", false);
            cardSelected = false;
            TurnManager.CardDrawn = true;
        }
    }

    private void RemovePreviousCards()
    {
        for (int i = 0; i < twoCardsRandom.Length; i++)
        {
            Destroy(cardsGO[i]);
            cardsGO[i] = null;
        }
    }

    private void ChooseDrawableCardsUpdate(Animator animator)
    {
        if (cardSelected)
        {
            animator.SetBool("ChooseCard", false);
            cardSelected = false;
            TurnManager.CardDrawn = true;
        }
    }
    private void ShowRandomCards() //muestra las dos cartas random
    {
        for (int i = 0; i < twoCardsRandom.Length; i++) 
        {
            cardsGO[i] = Instantiate(twoCardsRandom[i].gameObject, buttons[i].transform.Find("Parent"));

            cardsGO[i].GetComponent<ScriptButton>().enabled = false;
            cardsGO[i].GetComponent<Button>().enabled = false;

            CursorUIShower ui = cardsGO[i].GetComponent<CursorUIShower>();
            if (ui)
                ui.use = true;

            RectTransform rt = cardsGO[i].GetComponent<RectTransform>();
            rt.position = cardInstancePos[i].position;
            rt.localScale = cardInstancePos[i].localScale;

            buttonsPanel[i].Show();
            toShow.Show();
        }

    }
    private void ChooseRandom() //salen dos cartas random y las guarda en una lista.
    {
        int random1 = UnityEngine.Random.Range(0, _cards.Count);
        int random2 = UnityEngine.Random.Range(0, _cards.Count);

        while (random1 == random2)
        {
            random2 = UnityEngine.Random.Range(0, _cards.Count);
        }
        twoCardsRandom[0] = _cards[random1];
        twoCardsRandom[1] = _cards[random2];
    }
    public void ConfirmAddCard(int number) //se pasa informaci�n de la carta escogida y se desactivan despu�s.
    {
        HandManager.AddCard(twoCardsRandom[number], false);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == number)
                buttonsPanel[i].VariantHide(false);
            else
                buttonsPanel[i].VariantHide(true);
        }
        cardSelected = true;
    }
}
