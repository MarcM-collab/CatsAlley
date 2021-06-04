using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : Entity
{
    private bool Cast;
    public MenuPanel win, loose;
    public MenuPanel[] toHide;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        HP = MaxHP;
        OnChangeHealth?.Invoke(HP / MaxHP);
    }
    private void Update()
    {
        _animator.SetBool("Exhausted", Exhausted);
        _animator.SetBool("Hit", Hit);
        _animator.SetBool("Dead", Dead);
        _animator.SetBool("Cast", Cast);

        if (Dead)
        {
            for (int i = 0; i < toHide.Length; i++)
            {
                toHide[i].Hide();
            }
            if (Team == Team.TeamAI)
            {
                win.Show();

            }
            else
            {
                loose.Show();
            }

        }

        if (TurnManager.TeamTurn != Team.TeamPlayer)
        {
            Exhausted = false;
        }
    }
}
