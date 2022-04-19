using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serielization;
using UnityEngine.SceneManagement;

public class Saving : MonoBehaviour
{
    //Player playeh;// = new Player();
    //PlayerData hehe = new PlayerData();
    PlayerController player;

    int health;
    int damage;

    void Start()
    {

        SaveLoadSystem.BeginSave("game.data");
        SaveLoadSystem.Insert(health);
        SaveLoadSystem.Insert(damage);
        SaveLoadSystem.Insert(transform.position);
        SaveLoadSystem.Insert(transform.rotation);
        SaveLoadSystem.EndSave();


        SaveLoadSystem.BeginLoad("game.data");
        health = SaveLoadSystem.Load<int>();
        damage = SaveLoadSystem.Load<int>();
        transform.position = SaveLoadSystem.Load<Vector3>();
        transform.rotation = SaveLoadSystem.Load<Quaternion>();
        SaveLoadSystem.EndLoad();



        player = FindObjectOfType<PlayerController>();
        /*hehe.pageCount = player.pageCounter;
        hehe.level = SceneManager.GetActiveScene().buildIndex;*/
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
        //playeh.SavePlayer();
    }
    
    public void Load()
    {
        //playeh.LoadPlayer();
    }
}
