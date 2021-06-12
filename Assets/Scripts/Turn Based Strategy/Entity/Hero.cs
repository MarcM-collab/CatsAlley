using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : Entity
{
    private bool Cast;
    public MenuPanel win, loose;
    public MenuPanel[] toHide;
    public Animator hit;
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

        if (Hit)
        {
            StartCoroutine(Shake());

            if (Team == Team.TeamPlayer)
            {
                hit.SetBool("Hit", true);
                StartCoroutine(DisHit());
            }
        }
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
    private IEnumerator DisHit() //triggers activated it 2 times
    {
        yield return new WaitForSeconds(0.5f);
        hit.SetBool("Hit", false);
    }
    private IEnumerator Shake() //triggers activated it 2 times
    {
        yield return new WaitForSeconds(0.45f);
        CameraShake.TriggerShake();
    }
}
