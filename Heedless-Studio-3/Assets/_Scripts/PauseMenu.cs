using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public bool isPaused;
    AudioSource[] allAudios;
    IllusioOfChoice illusion;

    void Start()
    {
        allAudios = FindObjectsOfType<AudioSource>();
        illusion = FindObjectOfType<IllusioOfChoice>();
    }
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausemenu.SetActive(false);
        for (int i = 0; i < allAudios.Length; i++)
        {
            allAudios[i].UnPause();
        }
        if (illusion != null)
        {
            if (!illusion.inDialogue) 
            { 
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            pausemenu.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            for (int i = 0; i < allAudios.Length; i++)
            {
                allAudios[i].Pause();
            }
        }
        if (!isPaused)
        {
            Resume();
        }
    }
}
