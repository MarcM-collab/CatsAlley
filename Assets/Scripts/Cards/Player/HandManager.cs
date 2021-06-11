using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<Card> hand;
    [SerializeField]
    private float _scaleCard = 1f;
    public RectTransform HandCanvas;

    public float DivisionAngle;
    public float AngleScale;
    private float _randomOffset = 1950;
    public int GetMaxHandCost()
    {
        int max = 0;
        for (int i = 0; i < hand.Count; i++)
        {
            if (max < hand[i].Whiskas)
                max = hand[i].Whiskas;
        }
        return max;
    }
    public void AddCard(Card newCard)
    {
        var scaledAngle = DivisionAngle - AngleScale * (hand.Count + 1);

        RepositionCards();

        HandCanvas.eulerAngles += new Vector3(0, 0, scaledAngle);

        var cardInstance = CreateCardInstance(newCard);

        hand.Add(cardInstance);

        HandCanvas.eulerAngles /= 2.0f;
    }

    public void RemoveCard(Card cardToRemove)
    {
        if (hand.Count > 0)
        {
            HandCanvas.eulerAngles -= new Vector3(0, 0, DivisionAngle);
        }
        for (int i = 0; i < hand.Count; i++) //en el if hay que buscar una solución menos compleja.
        {
            if(hand[i].name.Substring(hand[i].name.Length - 1 - hand.Count / 10) == cardToRemove.name.Substring(cardToRemove.name.Length - 1-hand.Count/10)) //el .length con más de 10 cartas en la mano no funciona.
            {
                hand.Remove(hand[i]);
                break;
            }
        }
        RepositionCards();
        OrderHand();

        HandCanvas.eulerAngles /= 2.0f;
    }

    private void OrderHand() //remueve los numeros que se han colocado anteriomente, de nuevo coloca nuevos numeros ordenados.
    {
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].name = hand[i].name.Remove(hand[i].name.Length - 1 - hand.Count / 10);
            hand[i].name = hand[i].name + i;
        }
    }
    private void RepositionCards()
    {
        HandCanvas.eulerAngles = Vector3.zero;
        var scaledAngle = DivisionAngle - AngleScale * (hand.Count + 1);

        for (int i = 0; i < hand.Count; i++)
        {
            if (i != 0)
            {
                HandCanvas.eulerAngles += new Vector3(0, 0, scaledAngle);
            }
            hand[i].transform.SetParent(null);
            hand[i].transform.position = HandCanvas.position + new Vector3(0, _randomOffset, 0);
            hand[i].transform.eulerAngles = new Vector3(0, 0, 0);
            hand[i].transform.SetParent(HandCanvas);
        }
    }

    private Card CreateCardInstance(Card card)
    {
        Transform cardInstance = Instantiate(card, HandCanvas.position + new Vector3(0, _randomOffset, 0), Quaternion.identity).transform;
        cardInstance.name = cardInstance.name + hand.Count;

        cardInstance.SetParent(HandCanvas);
        cardInstance.localScale = new Vector3(_scaleCard, _scaleCard, _scaleCard);//escalamos las cartas que se ven en la mano.

        return cardInstance.GetComponent<Card>();
    }
}
