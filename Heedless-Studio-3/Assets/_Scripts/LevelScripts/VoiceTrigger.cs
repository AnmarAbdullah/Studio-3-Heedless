using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoiceTrigger : MonoBehaviour
{

    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource audio;
    public bool alreadyPlayed = false;
    public TextMeshProUGUI subtitles;
    public float[] timers;
    public string[] myTexts;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider Player)
    {
        if (!alreadyPlayed & Player.tag == "Player")
        {
            StartCoroutine(subtitleTime(myTexts, timers));
            audio.PlayOneShot(SoundToPlay, Volume);
            alreadyPlayed = true;
        }
    }

    IEnumerator subtitleTime(string[] text, float[] time)
    {
        subtitles.gameObject.SetActive(true);
        for (int i = 0; i < text.Length; i++)
        {
            subtitles.text = null;
            subtitles.text = text[i];
            yield return new WaitForSeconds(time[i]);
            /* if (!audio.isPlaying)
             {
                 break;
             }*/
        }
        subtitles.gameObject.SetActive(false);
    }
}