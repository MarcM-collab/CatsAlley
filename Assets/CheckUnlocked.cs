using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUnlocked : MonoBehaviour
{
    void Start()
    {
        LevelChecker[] levels = GetComponentsInChildren<LevelChecker>();

        for (int i = 0; i < levels.Length; i++)
        {
            SetLevelActive(levels[i], i <= CustomSceneManager.SceneManagerCustom.GetLevelsUnlocked());
        }
    }
    private void SetLevelActive(LevelChecker l, bool active)
    {
        l.active.SetActive(active);
        l.disactive.SetActive(!active);
    }
}
