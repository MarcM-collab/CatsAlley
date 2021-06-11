using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    private Animator[] linesActive;
    private void Awake()
    {
        linesActive = GetComponentsInChildren<Animator>();
    }
    public void ShowPath()
    {
        for (int i = 0; i < linesActive.Length; i++)
        {
            linesActive[i].SetBool("Show_insta", true);
        }
    }
    public void CreatePath()
    {
        StartCoroutine(Create());
    }
    private IEnumerator Create()
    {
        for (int i = 0; i < linesActive.Length; i++)
        {
            linesActive[i].SetBool("Show", true);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
