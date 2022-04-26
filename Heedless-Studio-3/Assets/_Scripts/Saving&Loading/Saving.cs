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
        player = FindObjectOfType<PlayerController>();
       /* SaveLoadSystem.BeginSave("/game.data");
        SaveLoadSystem.Insert(health);
        SaveLoadSystem.Insert(damage);
        SaveLoadSystem.Insert(transform.position);
        SaveLoadSystem.Insert(transform.rotation);
        SaveLoadSystem.EndSave();


        SaveLoadSystem.BeginLoad("/game.data");
        health = SaveLoadSystem.Load<int>();
        damage = SaveLoadSystem.Load<int>();
        transform.position = SaveLoadSystem.Load<Vector3>();
        transform.rotation = SaveLoadSystem.Load<Quaternion>();
        SaveLoadSystem.EndLoad();*/
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
        SaveLoadSystem.BeginSave("/game.data");
        SaveLoadSystem.Insert(player.PlayerLifes);
        SaveLoadSystem.Insert(player.level);
        SaveLoadSystem.Insert(player.pageCounter);
        SaveLoadSystem.EndSave();
        SaveLoadSystem.Insert(transform.position);
        SaveLoadSystem.EndSave();
    }
    
    public void Load()
    {
        SaveLoadSystem.BeginLoad("/game.data");
        player.PlayerLifes = SaveLoadSystem.Load<int>();
        player.level = SaveLoadSystem.Load<int>();
        SaveLoadSystem.EndLoad();
        player.loading();
        transform.position = SaveLoadSystem.Load<Vector3>();
        transform.rotation = SaveLoadSystem.Load<Quaternion>();
    }
}
