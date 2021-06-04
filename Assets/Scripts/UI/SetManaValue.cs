using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetManaValue : MonoBehaviour
{
    public GameObject whiskas;
    public GameObject maxWhiskas;

    private Image[] whiskas_array;
    private Image[] disactiveWhiskas_array;

    private Slider manaSlider;
    private Text manaText;

    private int _currentMana;
    private int _maxMana;
    private void OnEnable()
    {
        TurnManager.setDisplay += ChangeValue;
        ScriptButton.whiskasNecessary += CourutineMana;
    }
    private void OnDisable()
    {
        TurnManager.setDisplay -= ChangeValue;
        ScriptButton.whiskasNecessary -= CourutineMana;
    }
    private void Start()
    {
        ChangeValue(2, 2); //init
    }
    private void ChangeValue(int currentAmount, int maxMana)
    {
        _currentMana = currentAmount;
        _maxMana = maxMana;
        if (whiskas_array == null) //As it's called from the awake method this avoids 
            Init();

        for (int i = 0; i < currentAmount; i++)
        {
            whiskas_array[i].gameObject.SetActive(true);
        }
        for (int i = currentAmount; i < whiskas_array.Length; i++)
        {
            whiskas_array[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < maxMana; i++)
        {
            disactiveWhiskas_array[i].gameObject.SetActive(true);
        }
        //manaSlider.value = amount;
        manaText.text = currentAmount.ToString();
    }
    private void Init()
    {
        //manaSlider = GetComponent<Slider>();
        manaText = GetComponentInChildren<Text>();
        whiskas_array = whiskas.GetComponentsInChildren<Image>();
        disactiveWhiskas_array = maxWhiskas.GetComponentsInChildren<Image>();

        for (int i = 0; i < disactiveWhiskas_array.Length; i++)
        {
            disactiveWhiskas_array[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < whiskas_array.Length; i++)
        {
            whiskas_array[i].gameObject.SetActive(false);
        }
    }

    private void CourutineMana(Card currentCardMana)
    {
        StartCoroutine(ManaNecessary(currentCardMana));
    }
    public IEnumerator ManaNecessary(Card current)
    {
        Color temp = disactiveWhiskas_array[0].color;

        for (int i = _currentMana; i < current.Whiskas; i++)
        {

            disactiveWhiskas_array[i].color = Color.red;
            disactiveWhiskas_array[i].gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = current.Whiskas; i >= _currentMana; i--)
        {

            disactiveWhiskas_array[i].color = temp;
            if (i>=_maxMana)//para q no retire las espacios de mana utilizado:)
                disactiveWhiskas_array[i].gameObject.SetActive(false);
        }
    }
}
