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
        if(player.dist <= 35)
        {
            calmMusic.volume = 0;
            chaseMusic.volume = 0.4f;
        }
        else
        {
            calmMusic.volume = 0.4f;
            chaseMusic.volume = 0;
        }
    }
}
