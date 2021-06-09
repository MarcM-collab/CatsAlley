using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetHeroHealth : MonoBehaviour
{
    Hero h;
    TMP_Text t;
    void Start()
    {
        h = GetComponentInParent<Hero>();
        t = GetComponentInChildren<TMP_Text>();
    }
    void Update()
    {
        t.text = h.HP.ToString();
    }
}
