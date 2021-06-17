using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DialogueIA : MonoBehaviour
{
    public string[] answers;
    private string answer = "";
    private string currentAnswer = "";
    private float speed = 0.05f;

    public TMP_Text bocadillo;

    private void OnEnable()
    {
        ThreeCardsPlayer.dialogueIA += Talk;
    }

    private void OnDisable()
    {
        ThreeCardsPlayer.dialogueIA -= Talk;
    }

    private void Start()
    {
        answer = answers[0];
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Talk()
    {
        RandomAnswer();
        StartCoroutine(Text());
    }

   

    private void RandomAnswer()
    {
        answer = answers[Random.Range(1, answers.Length)];
    }

    private IEnumerator Text()
    {
        yield return new WaitForSeconds(2f);
        transform.GetChild(0).gameObject.SetActive(true);

        for (int i = 0; i < answer.Length; i++)
        {
            currentAnswer = answer.Substring(0, i);
            bocadillo.text = currentAnswer;

            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(2f);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
