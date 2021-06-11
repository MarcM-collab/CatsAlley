using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using TMPro;

public class PrepareDeck : MonoBehaviour
{
    public GameObject vol;
    private MenuPanel panel;
    public Transform parent;
    public Image[] Slots;
    public DeckPlayer CardDeck;
    private List<Card> currentCards = new List<Card>();
    private int index= 0;
    private int DeckLimitation = 8;

    [SerializeField]
    private List<SelectableCardButton> cardDisplays;
    [SerializeField]
    private GameObject[] lockDisplays;

    private Sprite emptyImage;
    private CanvasGroup canvas;

    private List<float> _WhiskasAverage = new List<float>();
    private float whiskasAverage = 0;
    public TMP_Text WhiskasAverageText;

    //private int slot;
    //private GameObject displayC;
    //private Vector3 backDesplacement;
    //private bool desplace = false;

    public float desplaceTime = 0.5f;
    private GameObject temporalDisplay;
    private bool hasEnded = false;

    public Animator toHighLight;
    private void Start()
    {
        if (vol)
        {
            vol.SetActive(true);
        }

        emptyImage = Slots[0].sprite;

        for (int i = index; i < Slots.Length; i++)
        {
           // Slots[i].color = new Color(Slots[i.color.r, Slots[i].color.g, Slots[i].color.b, 0);
            Slots[i].sprite = emptyImage;
        }

        for (int i = 0; i < GetCardsUnlocked(); i++)
        {
            if (i >= cardDisplays.Count)
            {
                Debug.LogWarning("Not enought cards");
                break;
            }
            cardDisplays[i].gameObject.SetActive(true);

            if (i > 8)
            {
                CheckLock(2);
            }
            else if (i > 10)
            {
                CheckLock(4);
            }
        }


        canvas = GetComponent<CanvasGroup>();
        cardDisplays = FindObjectsOfType<SelectableCardButton>().ToList();
    }

    private int GetCardsUnlocked()
    {
        return (CustomSceneManager.SceneManagerCustom.GetLevelsUnlocked()) switch
        {
            0 => 8,
            1 => 10,
            _ => 12,
        };
    }

    private void Enter()
    {
        panel = GetComponent<MenuPanel>();
        panel.Show();
    }

    private void SetFinished(Animator a)
    {
        if (hasEnded)
        {
            a.SetBool("PreparingDeck", false);
        }
    }

    private void OnDisable()
    {
        SelectableCardButton.displayCard -= CardDisplayChoosen;
        PrepareDeckBehaviour.OnPrepareDeckEnter -= Enter;
        PrepareDeckBehaviour.OnPrepareDeckUpdate -= SetFinished;
    }

    private void OnEnable()
    {
        SelectableCardButton.displayCard += CardDisplayChoosen;
        PrepareDeckBehaviour.OnPrepareDeckEnter += Enter;
        PrepareDeckBehaviour.OnPrepareDeckUpdate += SetFinished;
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
            //slot = index;
            //displayC = displayCard;

            //desplace = true;

            //temporalDisplay = Instantiate(displayC, displayC.transform.position, Quaternion.identity,displayC.transform.parent);
            //temporalDisplay.SetActive(true);
            //temporalDisplay.transform.SetParent(parent);
            //temporalDisplay.GetComponent<Button>().enabled = false;

            _WhiskasAverage.Add(card.Whiskas);
            whiskasAverage = 0;
            for (int i = 0; i < _WhiskasAverage.Count; i++)
            {
                whiskasAverage += _WhiskasAverage[i];
            }
            whiskasAverage /= _WhiskasAverage.Count;
            whiskasAverage = Mathf.Round(whiskasAverage * 100) / 100.0f;

            if (whiskasAverage <= 0)
                whiskasAverage = 0;


            WhiskasAverageText.text = whiskasAverage.ToString();


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

            //lista que guarda los wishkas de las cartas para realizar la media
            _WhiskasAverage.RemoveAt(_index);
            whiskasAverage = 0;
            for (int i = 0; i < _WhiskasAverage.Count; i++)
            {
                whiskasAverage += _WhiskasAverage[i];
            }
            whiskasAverage /= _WhiskasAverage.Count;
            whiskasAverage = Mathf.Round(whiskasAverage * 100) / 100.0f;

            if (whiskasAverage <= 0)
                whiskasAverage = 0;


            WhiskasAverageText.text = whiskasAverage.ToString();
            currentCards.RemoveAt(_index);

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
            panel.Hide();

            hasEnded = true;
            //TurnManager.NextTurn();
            AudioManager.audioManager.StartBattle();

            if (vol)
            {
                vol.SetActive(false);
            }
        }
    }
    private void CheckLock(int length)
    {
        for (int i = 0; i < length; i++)
        {
            lockDisplays[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (currentCards.Count == DeckLimitation)
        {
            toHighLight.SetBool("HighLigh", true);
        }
        else
        {
            toHighLight.SetBool("HighLigh", false);
        }
    }
    //private void Update()
    //{
    //    DesplaceCard();
    //}
    //private void DesplaceCard()
    //{

    //    if (desplace)
    //    {
    //        //displayC y temporalDisplay es la carta q se ha seleccionado.
    //        if (temporalDisplay)
    //        {
    //            //print(Slots[slot].transform.name + "///" + Slots[slot].GetComponent<RectTransform>().position + "....." + Slots[slot].transform.position);
    //            //temporalDisplay.transform.position += Vector3.Lerp(displayC.transform.position, Slots[slot].transform.position, Time.deltaTime/desplaceTime);

    //            if (temporalDisplay.transform.position == Slots[slot].transform.position)
    //            {
    //                desplace = false;


    //                displayC.gameObject.SetActive(false);
    //                Destroy(temporalDisplay);
    //            }
    //        }
    //    }

    //}
}
