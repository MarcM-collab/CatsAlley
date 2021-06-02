using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevels : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CustomSceneManager.SceneManagerCustom.UnlockNextLevel();
            CustomSceneManager.SceneManagerCustom.LoadScene(0);
        }
    }
}
