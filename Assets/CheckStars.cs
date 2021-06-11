using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStars : MonoBehaviour
{
    public int[] minScoreStars;
    public Animator[] stars;
    public ParticleSystem[] startsFX;
    private Hero player;
    private void Start()
    {
        player = EntityManager.GetHero(Team.TeamPlayer);
    }
    public void PlayStarFX(int index)
    {
        if (player.HP > minScoreStars[index])
        {
            print(index);
            stars[index].SetBool("Show", true);
            startsFX[index].Play();
            CustomSceneManager.SceneManagerCustom.SetStars(1);
        }
    }
}
