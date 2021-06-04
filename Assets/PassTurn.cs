using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTurn : MonoBehaviour
{
    private MenuPanel p;
    private bool hided = false;
    private void Start()
    {
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
        }

    }
}
