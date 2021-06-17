using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CinematicaText : MonoBehaviour
{
    public TMP_Text CanvasText;
    public string[] texts = new string[15];

    public int index = 0;

    private void Update()
    {
        Show();
    }
    public void Show()
    {
        CanvasText.text = texts[index];
    }
}
