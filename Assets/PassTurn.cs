using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTurn : MonoBehaviour
{
    private MenuPanel p;
    private HandManager h;
    private bool hided = false;
    private void Start()
    {
        h = FindObjectOfType<HandManager>();
        p = GetComponent<MenuPanel>();
    }
    private void Update()
    {
        if (TurnManager.TeamTurn != Team.TeamPlayer && !hided)
        {
            hided = true;
            p.Hide();
        }
        else if (TurnManager.TeamTurn == Team.TeamPlayer && hided)
        {
            hided = false;
            if (h.GetCurrentHandLength <= ChooseDrawableCardsPlayer.GetMaxHand)
                p.Show();
        }
    }
}
