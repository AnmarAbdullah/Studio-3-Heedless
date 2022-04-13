using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGhoul : MonoBehaviour
{
    CameraController player;
    PlayerController pplayer;
    public GameObject RespawnGhoul;
    public GameObject RespawnPlayer;

    float speed = 20; // Original Value = 6
    float dist;

    private void Start()
    {
        player = FindObjectOfType<CameraController>();
        pplayer = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist <= 3)
        {
            pplayer.transform.position = RespawnPlayer.transform.position;
            transform.position = RespawnGhoul.transform.position;
        }
        
        if (pplayer.isStunned)
        {
            speed = 0;
        }
    }
}
