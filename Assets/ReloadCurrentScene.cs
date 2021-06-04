using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCurrentScene : MonoBehaviour
{
    public void Reload()
    {
        Time.timeScale = 1;
        CustomSceneManager.SceneManagerCustom.LoadScene(CustomSceneManager.SceneManagerCustom.currentBuildIndex);
    }
}
