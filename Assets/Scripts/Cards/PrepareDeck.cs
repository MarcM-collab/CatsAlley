using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class PrepareDeck : MonoBehaviour
{
    public Image[] Slots;
    public DeckPlayer CardDeck;
    private List<Card> currentCards = new List<Card>();
    private int index= 0;
    private int DeckLimitation = 8;

    [SerializeField]
    private List<SelectableCardButton> cardDisplays;

    private Sprite emptyImage;
    private CanvasGroup canvas;

    private int slot;
    private GameObject displayC;
    //private Vector3 backDesplacement;
    private bool desplace = false;
    public float desplaceTime = 0.5f;
    public MenuPanel[] toShow;
    private void Start()
    {
        emptyImage = Slots[0].sprite;

        for (int i = index; i < Slots.Length; i++)
        {
           // Slots[i].color = new Color(Slots[i.color.r, Slots[i].color.g, Slots[i].color.b, 0);
            Slots[i].sprite = emptyImage;
        }

        canvas = GetComponent<CanvasGroup>();
        cardDisplays = FindObjectsOfType<SelectableCardButton>().ToList();
    }

    private void Update()
    {
        DesplaceCard();
    }
    private void DesplaceCard()
    {
        if (desplace)
        {
            //displayC es la carta q se ha seleccionado.
            displayC.transform.position = Vector2.Lerp(displayC.transform.position, Slots[slot].transform.position, desplaceTime);

            if (displayC.transform.position == Slots[slot].transform.position)
            {
                desplace = false;
                displayC.gameObject.SetActive(false);
                //displayC.transform.position = backDesplacement;
            }
        }
    }
    private void OnDisable()
    {
        SelectableCardButton.displayCard -= CardDisplayChoosen;
    }

    private void OnEnable()
    {
        SelectableCardButton.displayCard += CardDisplayChoosen;
    }

    public void CardDisplayChoosen(Image cardDisplay, Card card, GameObject displayCard)
    {
        if (index < Slots.Length)
        {
            
            Slots[index].sprite = cardDisplay.sprite;
            Slots[index].color = new Color(Slots[index].color.r, Slots[index].color.g, Slots[index].color.b, 1);
            currentCards.Add(card);
            if (index >= Slots.Length)
                index = 0;

            cardDisplay.gameObject.SetActive(false);
            slot = index;
            displayC = displayCard;
            //backDesplacement = displayCard.transform.position; //guardamos su posicion inicial
            desplace = true;

            index++; 
        }
    }

    

    public void RemoveChosenCard(int _index)
    {
        if(Slots[_index].sprite != emptyImage)
        {
            for (int i = _index; i < Slots.Length - 1; i++)
            {
                if (Slots[i + 1].sprite != null)
                    Slots[i].sprite = Slots[i + 1].sprite;

            }
            for (int i = 0; i < cardDisplays.Count; i++)
            {

                if (cardDisplays[i].GetComponent<SelectableCardButton>().card.name == currentCards[_index].name)
                    cardDisplays[i].gameObject.SetActive(true);
            }

            //Slots[Slots.Length-1].color = new Color(Slots[_index].color.r, Slots[_index].color.g, Slots[_index].color.b, 0);
            Slots[Slots.Length - 1].sprite = emptyImage;
            currentCards.RemoveAt(_index);
            index--;
        }
        
    }

    public void DeckIsFinished()
    {
        if (currentCards.Count == DeckLimitation)
        {
            for (int i = 0; i < currentCards.Count; i++)
            {
                CardDeck.Cards[i] = currentCards[i];
            }

            canvas.alpha = 0;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;

            gameObject.SetActive(true);
            GetComponent<MenuPanel>().Hide();
            //TurnManager.NextTurn();

            for (int i = 0; i < toShow.Length; i++)
            {
                toShow[i].Show();
            }
        }
    }
}
