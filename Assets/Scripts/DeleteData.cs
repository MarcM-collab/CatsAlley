using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteData : MonoBehaviour
{
    public void OnClick()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
