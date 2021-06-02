using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorOnOver : MonoBehaviour
{
    public Color overColor;
    private Color startColor;
    private Image im;
    private void Start()
    {
        im = GetComponent<Image>();
        startColor = im.color;
    }
    public void OnMouseEnterCustom()
    {
        im.color = overColor;
    }
    public void OnMouseExitrCustom()
    {
        im.color = startColor;
    }
}
