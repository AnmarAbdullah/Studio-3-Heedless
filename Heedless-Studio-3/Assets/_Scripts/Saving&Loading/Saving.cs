using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serielization;
using UnityEngine.SceneManagement;

public class Saving : MonoBehaviour
{
    Player playeh;// = new Player();
    PlayerData hehe = new PlayerData(new Player());
    PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        hehe.pageCount = player.pageCounter;
        hehe.level = SceneManager.GetActiveScene().buildIndex;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Save();
            Debug.Log("Hello");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Load();
            Debug.Log("Loaded");
        }
    }
    public void Save()
    {
        playeh.SavePlayer();
    }
    
    public void Load()
    {
        playeh.LoadPlayer();
    }
}
