using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public MenuPanel win;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            win.Show();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            EntityManager.GetHero(Team.TeamPlayer).HP -= 5;
            EntityManager.GetHero(Team.TeamPlayer).Hit = true;
        }
    }
}
