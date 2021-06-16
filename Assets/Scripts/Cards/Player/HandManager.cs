using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public static int HandLimit = 8;
    [Header ("Player")]
    public static List<Card> HandPlayer = new List<Card>();
    [SerializeField]
    private RectTransform HandCanvasPlayer;
    private float OffsetPlayer => Screen.height /2 - Screen.height / 2 / 3 * 2;

    [Header("AI")]
    public static List<Card> HandAI = new List<Card>();
    [SerializeField]
    private RectTransform HandCanvasAI;
    [SerializeField]
    private Sprite CardBackSprite;
    private float OffsetAI => Screen.height / 2 + Screen.height / 2 / 3 * 2;

    [SerializeField]
    private float _scaleCard = 1f;

    public float DivisionAngle;
    public float AngleScale;

    public static int GetMaxHandCost()
    {
        int max = 0;
        for (int i = 0; i < HandPlayer.Count; i++)
        {
            if (max < HandPlayer[i].Whiskas)
                max = HandPlayer[i].Whiskas;
        }
        return max;
    }
    public void AddCard(Card newCard, bool isAI)
    {
        var hand = isAI ? HandAI : HandPlayer;
        var handCanvas = isAI ? HandCanvasAI : HandCanvasPlayer;
        var offset = isAI ? OffsetAI : OffsetPlayer;

        Card cardInstance;

        if (isAI)
            cardInstance = CreateAICardInstance(newCard);
        else
            cardInstance = CreatePlayerCardInstance(newCard);

        hand.Add(cardInstance);

        RepositionCards(hand, handCanvas, offset, isAI);

        handCanvas.eulerAngles /= 2.0f;
    }

    public void RemoveCard(Card cardToRemove, bool isAI)
    {
        var hand = isAI ? HandAI : HandPlayer;
        var handCanvas = isAI ? HandCanvasAI : HandCanvasPlayer;
        var offset = isAI ? OffsetAI : OffsetPlayer;

        hand.Remove(cardToRemove);

        //for (int i = 0; i < hand.Count; i++) //en el if hay que buscar una solución menos compleja.
        //{
        //    if (hand[i].name.Substring(hand[i].name.Length - 1 - hand.Count / 10) == cardToRemove.name.Substring(cardToRemove.name.Length - 1 - hand.Count / 10)) //el .length con más de 10 cartas en la mano no funciona.
        //    {
        //        hand.Remove(hand[i]);
        //        break;
        //    }
        //}
        OrderHand();

        RepositionCards(hand, handCanvas, offset, isAI);

        handCanvas.eulerAngles /= 2.0f;
    }

    private void OrderHand() //remueve los numeros que se han colocado anteriomente, de nuevo coloca nuevos numeros ordenados.
    {
        for (int i = 0; i < HandPlayer.Count; i++)
        {
            HandPlayer[i].name = HandPlayer[i].name.Remove(HandPlayer[i].name.Length - 1 - HandPlayer.Count / 10);
            HandPlayer[i].name = HandPlayer[i].name + i;
        }
    }
    private void RepositionCards(List<Card> hand, RectTransform handCanvas, float offset, bool isAI)
    {
        handCanvas.eulerAngles = Vector3.zero;
        var scaledAngle = DivisionAngle - AngleScale * (hand.Count + 1);

        for (int i = 0; i < hand.Count; i++)
        {
            if (i != 0)
            {
                handCanvas.eulerAngles += new Vector3(0, 0, scaledAngle);
            }
            hand[i].transform.SetParent(null);
            hand[i].transform.eulerAngles = new Vector3(0, 0, 0);
            hand[i].transform.position = new Vector3(Screen.width / 2, offset, 0);
            hand[i].transform.SetParent(handCanvas);
        }
    }
    public void Reposition()
    {
        RepositionCards(HandPlayer, HandCanvasPlayer, OffsetPlayer, false);

        HandCanvasPlayer.eulerAngles /= 2.0f;
    }

    private Card CreatePlayerCardInstance(Card card)
    {
        Transform cardInstance = Instantiate(card, new Vector3(Screen.width / 2, OffsetPlayer, 0), Quaternion.identity).transform;
        cardInstance.name = cardInstance.name + HandPlayer.Count;

        cardInstance.SetParent(HandCanvasPlayer);
        cardInstance.localScale = new Vector3(_scaleCard, _scaleCard, _scaleCard);//escalamos las cartas que se ven en la mano.

        return cardInstance.GetComponent<Card>();
    }

    private Card CreateAICardInstance(Card card)
    {
        var cardInstance = Instantiate(card, new Vector3(Screen.width / 2, OffsetAI, 0), Quaternion.identity).transform;
        cardInstance.name = cardInstance.name + HandAI.Count;
        cardInstance.GetComponent<Button>().enabled = false; //Avoids interaction with player
        cardInstance.GetComponent<ScriptButton>().enabled = false;
        cardInstance.GetComponent<Image>().sprite = CardBackSprite;

        Transform[] stats = cardInstance.GetComponentsInChildren<Transform>();
        foreach (Transform t in stats)
        {
            if (t != cardInstance.transform)
                t.gameObject.SetActive(false);
        }

        cardInstance.SetParent(HandCanvasAI);
        cardInstance.localScale = new Vector3(_scaleCard * 0.8f, _scaleCard * 0.8f, _scaleCard * 0.8f);

        return cardInstance.GetComponent<Card>();
    }
}
