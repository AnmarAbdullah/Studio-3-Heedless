using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    [SerializeField]AudioSource calmMusic;
    [SerializeField] AudioSource chaseMusic;
    [SerializeField]PlayerController player;
    AudioSource music;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        if(player.dist <= 30)
        {
            calmMusic.volume = 0;
            chaseMusic.volume = 1;
        }
        else
        {
            calmMusic.volume = 1;
            chaseMusic.volume = 0;
        }
    }
}
