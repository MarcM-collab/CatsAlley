using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeCardsPlayer : MonoBehaviour
{
    public MenuPanel threeCardPanel;
    public MenuPanel cardCanvas;
    public MenuPanel infoManager;

    private List<Card> _cards = new List<Card>(); //Baraja elegida por el player (8 cartas)
    [SerializeField]
    private DeckPlayer _deckPlayer;

    [SerializeField]
    private Image[] buttons;
    private Card[] randomCards; //lista de cartas random (se crea segun el numero de botones)
    private RectTransform[] cardInstancePos;
    private GameObject[] cardsGO;

    [SerializeField]
    private HandManager Hand;
    [SerializeField]
    private int maxCardInHand = 6;

    private List<int> discardedCards = new List<int>();

    [SerializeField]
    private GameObject xGO;

    private int[] listRandom;

    private bool _buttonPressed;

    private GameObject canvasGO;

    private float _timer;
    [SerializeField]
    private float _timeAfterConfirm = 1.5f;
    private bool _startTimer;

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
        canvasGO = buttons[0].transform.parent.gameObject;

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
            threeCardPanel.Show();
            cardCanvas.Show();
            infoManager.Hide();
            randomCards = new Card[buttons.Length];
            RemovePreviousCards();
            ChooseRandomInitial();
            ShowRandomCards();
        }
        else
        {
            animator.SetBool("CardsDrawn", false);
            TurnManager.ExtraCards = true;
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
        if (_buttonPressed)
        {
            threeCardPanel.Hide();
            _buttonPressed = false;
            _startTimer = true;
            _timer = 0;
        }
        //Debug.Log(_timer);
        if (_startTimer && _timer >= _timeAfterConfirm)
        {
            animator.SetBool("CardsDrawn", false);
            TurnManager.ExtraCards = true;
            TurnManager.CardDrawn = true;

            AddCards();
            HideRandomCards();

            infoManager.Show();
            _startTimer = false;
        }
        _timer += Time.deltaTime;
    }
    private void ShowRandomCards() //muestra las dos cartas random
    {
        for (int i = 0; i < randomCards.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);

            cardsGO[i] = Instantiate(randomCards[i].gameObject, buttons[i].transform);

            cardsGO[i].GetComponent<ScriptButton>().enabled = false;
            cardsGO[i].GetComponent<Button>().enabled = false;

            CursorUIShower ui = cardsGO[i].GetComponent<CursorUIShower>();
            if (ui)
                ui.use = true;

            RectTransform rt = cardsGO[i].GetComponent<RectTransform>();
            rt.position = cardInstancePos[i].position;
            rt.localScale = cardInstancePos[i].localScale;

            var a = Instantiate(xGO, cardsGO[i].transform);
        }

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
            Hand.AddCard(randomCards[i]);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }
    
    public void SelectCard(int number)
    {
        Image xImage = buttons[number].transform.GetChild(1).GetChild(buttons[number].transform.GetChild(1).childCount - 1).GetComponent<Image>();
        if (xImage.enabled == true)
        {
            xImage.enabled = false;
            discardedCards.Remove(number);
        }
        else
        {
            xImage.enabled = true;
            discardedCards.Add(number);
        }
    }

    public void Confirm()
    {
        List<int> canNotRepeatCards = new List<int>();
        for (int i = 0; i < randomCards.Length; i++)
        {
            canNotRepeatCards.Add(listRandom[i]);
        }
        if (canNotRepeatCards.Count > 0)
        {
            ChooseRandomFinal(canNotRepeatCards);
            RemovePreviousCards();
            ShowRandomCards();
        }

        _buttonPressed = true;
    }
    private void ChooseRandomFinal(List<int> canNotRepeatCards) //salen dos cartas random y las guarda en una lista.
    {
        int[] previousListRandom = new int[listRandom.Length];
        listRandom.CopyTo(previousListRandom, 0);
        listRandom = new int[randomCards.Length];
        for (int i = 0; i < randomCards.Length; i++)
        {
            if (discardedCards.Contains(i))
            {
                int currentRandom = UnityEngine.Random.Range(0, _cards.Count);
                listRandom[i] = currentRandom;

                while (canNotRepeatCards.Contains(currentRandom))
                {
                    currentRandom = UnityEngine.Random.Range(0, _cards.Count);
                    listRandom[i] = currentRandom;
                }

                canNotRepeatCards.Add(currentRandom);
            }
            else 
            {
                listRandom[i] = previousListRandom[i];
            }
        }

        for (int i = 0; i < randomCards.Length; i++)
        {
            randomCards[i] = _cards[listRandom[i]];
        }
    }

    private void HideRandomCards()
    {
        for (int i = 0; i > buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }
}