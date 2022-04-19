using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtitles : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitles;
    IllusioOfChoice dialogue;
    public string[] myTexts;

    public string[] myTextsA;
    public float[] timersA;
    bool choiceCalled;
    public string[] myTextsB;
    public float[] timersB;
    bool done;

    public float[] timers;
    bool called;
    AudioSource audio;
    void Start()
    {
        dialogue = GetComponent<IllusioOfChoice>();
        subtitles.gameObject.SetActive(true);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogue.inDialogue)
        {
            subtitles.gameObject.SetActive(false);
        }
        if (dialogue.inDialogue && !called)
        {
            StartCoroutine(subtitleTime(myTexts, timers));
            called = true;
        }
        if (dialogue.choicea && !choiceCalled)
        {
            StartCoroutine(subtitleTime(myTextsA, timersA));
            choiceCalled = true;
        }
        if (dialogue.choiceb && !choiceCalled)
        {
            StartCoroutine(subtitleTime(myTextsB, timersB));
            choiceCalled = true;
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
            if (!audio.isPlaying)
            {
                break;
            }
        }
       subtitles.gameObject.SetActive(false);
    }
}
