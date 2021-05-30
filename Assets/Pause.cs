using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public MenuPanel pause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
                UnPause();
            else
                OnClick();
        }

    }
    public void OnClick()
    {
        if (AudioManager.audioManager)
            AudioManager.audioManager.ApplyFilter();

        pause.Show();
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        if (AudioManager.audioManager)
            AudioManager.audioManager.StopFilter();
        
        pause.Hide();
        Time.timeScale = 1;
    }
}
