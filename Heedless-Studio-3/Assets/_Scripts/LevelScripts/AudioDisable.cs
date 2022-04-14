using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDisable : MonoBehaviour
{
    public AudioClip Music;
    public AudioSource Audio;

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = Music;
            Audio.Stop();
        }
    }
}
