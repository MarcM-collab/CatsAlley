using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTurn : MonoBehaviour
{
    private MenuPanel p;
    private HandManager h;
    public MenuPanel checkSteal;
    private bool hided = false;
    private void Start()
    {
        h = FindObjectOfType<HandManager>();
        p = GetComponent<MenuPanel>();
    }
    private void Update()
    {
        if (TurnManager.TeamTurn != Team.TeamPlayer && !p.isHided)
        {
            p.Hide();
        }
        if (TurnManager.TeamTurn == Team.TeamPlayer && h.GetCurrentHandLength <= ChooseDrawableCardsPlayer.GetMaxHand && p.isHided && checkSteal.isHided)
        {
            StartCoroutine(ReCheck());
        }
    }
    private IEnumerator ReCheck()
    {
        yield return new WaitForSeconds(0.5f);

        if (checkSteal.isHided)
            p.Show();
    }
}
