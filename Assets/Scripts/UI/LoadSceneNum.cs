using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneNum : MonoBehaviour
{
    private bool executed;
    private void Start()
    {
        executed = false;
    }
    public void LoadScene(int index)
    {
        if (!executed)
        {
            executed = true;
            Time.timeScale = 1;
            CustomSceneManager.SceneManagerCustom.LoadScene(index);
        }
    }
    public void LoadAndUnlock(int index)
    {
        if (!executed)
        {
            executed = true;
            Time.timeScale = 1;
            CustomSceneManager.SceneManagerCustom.UnlockNextLevel();
            CustomSceneManager.SceneManagerCustom.LoadScene(index);
        }
    }
}
