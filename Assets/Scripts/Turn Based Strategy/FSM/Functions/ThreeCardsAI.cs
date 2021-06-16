using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeCardsAI : MonoBehaviour
{
    private List<Card> _cards = new List<Card>(); //Baraja elegida por el player (8 cartas)
    [SerializeField]
    private DeckAI _deckAI;

    private Card[] randomCards; //lista de cartas random (se crea segun el numero de botones)
    [SerializeField]
    private int numberOfCards;

    private int[] listRandom;

    public Transform IAHandCanvas;

    public float scale = 1f;

    public HandManager HandManager;

    private void OnEnable()
    {
        ThreeCardsBehaviour.OnThreeCardsEnter += ThreeCardsEnter;
    }
    private void OnDisable()
    {
        ThreeCardsBehaviour.OnThreeCardsEnter -= ThreeCardsEnter;
    }
    private void Start()
    {
        randomCards = new Card[numberOfCards];

        for (int i = 0; i < _deckAI.IADeck.Count; i++)
        {
            _cards.Add(_deckAI.IADeck[i].card);
        }
    }
    private void ThreeCardsEnter(Animator animator)
    {
        ChooseRandomInitial();
        AddCards();
        animator.SetBool("CardsDrawn", false);
        TurnManager.ExtraCards = true;
        TurnManager.CardDrawn = true;
    }

    private void ChooseRandomInitial() //salen dos cartas random y las guarda en una lista.
    {
        listRandom = new int[randomCards.Length];
        for (int i = 0; i < randomCards.Length; i++)
        {
            int currentRandom = UnityEngine.Random.Range(0, _cards.Count);
            listRandom[i] = currentRandom;
            bool differentToPrevious = false;

            if (i > 0)
            {
                while (!differentToPrevious)
                {
                    currentRandom = UnityEngine.Random.Range(0, _cards.Count);
                    listRandom[i] = currentRandom;

                    differentToPrevious = true;

                    for (int j = i - 1; j >= 0; j--)
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
            if (HandManager.HandAI.Count < HandManager.HandLimit)
                HandManager.AddCard(randomCards[i], true);
        }
    }
}