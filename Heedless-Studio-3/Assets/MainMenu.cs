using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider MouseSensSlider;
    public AbilitiesManager mouseSen;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        AudioListener.volume = volumeSlider.value;
        mouseSen.MouseSens = MouseSensSlider.value;
    }

}
