using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneNum : MonoBehaviour
{
    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        CustomSceneManager.SceneManagerCustom.LoadScene(index);
    }
    public void LoadAndUnlock(int index)
    {
        Time.timeScale = 1;
        CustomSceneManager.SceneManagerCustom.UnlockNextLevel();
        CustomSceneManager.SceneManagerCustom.LoadScene(index);
    }
}
