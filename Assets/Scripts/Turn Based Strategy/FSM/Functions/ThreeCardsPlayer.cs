using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeCardsPlayer : MonoBehaviour
{
    private List<Card> _cards; //Baraja elegida por el player (8 cartas)
    [SerializeField]
    private DeckPlayer _deckPlayer;

    [SerializeField]
    private Image[] buttons;
    private Card[] randomCards; //lista de cartas random (se crea segun el numero de botones)
    private RectTransform[] cardInstancePos;
    private GameObject[] cardsGO;

    private List<Card> randomControl = new List<Card>();

    [SerializeField]
    private HandManager Hand;
    [SerializeField]
    private int maxCardInHand = 6;
    private bool currentTurn = false;

    private bool PressedFirst;

    private bool cardSelected;

    private List<Card> discardedCards = new List<Card>();

    [SerializeField]
    private GameObject xGO;

    private void OnEnable()
    {
        ThreeCardsBehaviour.OnThreeCardsEnter += ThreeCardsEnter;
        ThreeCardsBehaviour.OnThreeCardsUpdate += ThreeCardsUpdate;
    }
    private void OnDisable()
    {
        ThreeCardsBehaviour.OnThreeCardsEnter -= ThreeCardsEnter;
        ThreeCardsBehaviour.OnThreeCardsUpdate -= ThreeCardsUpdate;
    }
    private void Start()
    {
        randomCards = new Card[buttons.Length];
        cardInstancePos = new RectTransform[buttons.Length];
        cardsGO = new GameObject[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            cardInstancePos[i] = buttons[i].GetComponentInChildren<RectTransform>();
        }

        _cards = _deckPlayer.Cards;
    }
    private void ThreeCardsEnter(Animator animator)
    {
        if (Hand.hand.Count < maxCardInHand)
        {
            RemovePreviousCards();
            ChooseRandomInitial();
            ShowRandomCards();

            //AddCards() after confirm and animation
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
        for (int i = 0; i < randomCards.Length; i++)
        {
            Destroy(cardsGO[i]);
            cardsGO[i] = null;
        }
    }

    private void ThreeCardsUpdate(Animator animator)
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
        for (int i = 0; i < randomCards.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);

            cardsGO[i] = Instantiate(randomCards[i].gameObject, buttons[i].transform);
            Instantiate(xGO, buttons[i].transform.GetChild(1));

            cardsGO[i].GetComponent<ScriptButton>().enabled = false;
            cardsGO[i].GetComponent<Button>().enabled = false;

            CursorUIShower ui = cardsGO[i].GetComponent<CursorUIShower>();
            if (ui)
                ui.use = true;

            RectTransform rt = cardsGO[i].GetComponent<RectTransform>();
            rt.position = cardInstancePos[i].position;
            rt.localScale = cardInstancePos[i].localScale;
        }

    }
    private void ChooseRandomInitial() //salen dos cartas random y las guarda en una lista.
    {
        int[] listRandom = new int[randomCards.Length];
        bool differentToPrevious = false;
        for (int i = 0; i < randomCards.Length; i++)
        {
            int currentRandom = UnityEngine.Random.Range(0, _cards.Count);
            listRandom[i] = currentRandom;

            if (i > 0) 
            {
                while (differentToPrevious)
                {
                    differentToPrevious = true;
                    for (int j = i; j >= 0; j--)
                    {
                        if (currentRandom == listRandom[j])
                        {
                            differentToPrevious = false;
                            break;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < randomCards.Length; i++)
        {
            randomCards[i] = _cards[listRandom[i]];
        }
    }
    public void AddCards()
    {
        for (int i = 0; i < randomCards.Length; i++)
        {
            Hand.AddCard(randomCards[i]);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        cardSelected = true;
    }
    
    public void SelectCard(int number)
    {
        Card selectedCard = randomCards[number];
        Image xImage = buttons[number].transform.GetChild(1).GetChild(buttons[number].transform.GetChild(1).childCount - 1).GetComponent<Image>();
        if (xImage.enabled == true)
        {
            xImage.enabled = false;
            discardedCards.Remove(selectedCard);
        }
        else
        {
            xImage.enabled = true;
            discardedCards.Add(selectedCard);
        }
    }

    public void Confirm()
    {
        List<Card> canNotRepeatCards = new List<Card>();
        for (int i = 0; i < randomCards.Length; i++)
        {
            canNotRepeatCards.Add(randomCards[i]);
        }
    }
    private void ChooseRandomFinal() //salen dos cartas random y las guarda en una lista.
    {
        int[] listRandom = new int[randomCards.Length];
        bool differentToPrevious = false;
        for (int i = 0; i < randomCards.Length; i++)
        {
            int currentRandom = UnityEngine.Random.Range(0, _cards.Count);
            listRandom[i] = currentRandom;

            if (i > 0)
            {
                while (differentToPrevious)
                {
                    differentToPrevious = true;
                    for (int j = i; j >= 0; j--)
                    {
                        if (currentRandom == listRandom[j])
                        {
                            differentToPrevious = false;
                            break;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < randomCards.Length; i++)
        {
            randomCards[i] = _cards[listRandom[i]];
        }
    }
}