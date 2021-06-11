using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reminder : MonoBehaviour //this is used to remember the player to pass the turn which some will forget.
{
    private Animator anim;
    public MenuPanel p;
    public HandManager hm;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(TurnManager.TeamTurn == Team.TeamPlayer && !TurnManager.ExtraCards && hm.hand.Count > ChooseDrawableCardsPlayer.GetMaxHand)
        {
            p.Show();
        }
        else if (TurnManager.TeamTurn != Team.TeamPlayer)
        {
            anim.SetBool("HighLight", false);
        }

        if (EntityManager.GetActiveCharacters(Team.TeamPlayer).Length <= 0 && EntityManager.GetCharacters(Team.TeamPlayer).Length > 0)
        {
            if (TurnManager.currentMana <= 0 || hm.GetMaxHandCost() > TurnManager.currentMana)
            {
                anim.SetBool("HighLight", true);
            }
        }
    }

    //public GameObject g;
    //public float waiter;
    //private float timer;
    //private void Start()
    //{
    //    g.SetActive(false);
    //    timer = 0;
    //}
    //private void Update()
    //{
    //    if (TurnManager.TeamTurn == Team.TeamPlayer)
    //    {
    //        if (EntityManager.GetActiveCharacters(Team.TeamPlayer).Length <= 0 && EntityManager.GetCharacters(Team.TeamPlayer).Length > 0 && TurnManager.currentMana <= 0)
    //        {
    //            g.SetActive(true);
    //        }
    //        else if (timer > waiter)
    //        {
    //            g.SetActive(true);
    //        }
    //        else
    //            g.SetActive(false);
    //        timer +=Time.deltaTime;

    //        if (Input.GetMouseButtonDown(0))
    //            timer = 0;
    //    }
    //    else if (g.activeSelf)
    //        g.SetActive(false);
    //    else
    //        timer = 0;


    //}
}
