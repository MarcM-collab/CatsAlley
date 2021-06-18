using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUnlocked : MonoBehaviour
{
    static int prevLevelsUnlocked = 0;
    public PathCreator[] paths;

    private void Awake()
    {
        prevLevelsUnlocked = 0;
    }
    void Start()
    {
        LevelChecker[] levels = GetComponentsInChildren<LevelChecker>();


        int currentLevelsUnlocked = CustomSceneManager.SceneManagerCustom.GetLevelsUnlocked();

        for (int i = 0; i < levels.Length; i++)
        {
            if (currentLevelsUnlocked != prevLevelsUnlocked && i == currentLevelsUnlocked && i != 0) //avoids unlocking a scene each time you win even if you complete the same level twice
            {
                levels[i].active.Show();
                levels[i].disactive.Hide();
                paths[i-1].CreatePath();
                prevLevelsUnlocked = currentLevelsUnlocked;
            }
            else if (i <= CustomSceneManager.SceneManagerCustom.GetLevelsUnlocked())
            {
                levels[i].active.VariantHide(true);
                levels[i].disactive.gameObject.SetActive(false);

                if(i!=0)
                    paths[i-1].ShowPath();
            }
            else
            {
                levels[i].disactive.Show();
            }
        }
    }
}
