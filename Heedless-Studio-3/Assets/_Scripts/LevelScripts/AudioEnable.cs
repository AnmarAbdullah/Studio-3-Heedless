using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEnable : MonoBehaviour
{
    public AudioClip Music;
    private AudioSource Audio;

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = Music;
            Audio.loop = true;
            Audio.volume = 0.4f;
            Audio.Play();
        }
    }
}
