using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneNum : MonoBehaviour
{
    public void LoadScene(int index)
    {
        CustomSceneManager.SceneManagerCustom.LoadScene(index);
    }
}
