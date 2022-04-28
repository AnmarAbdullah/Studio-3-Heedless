using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public AudioSource source;


    PlayerController player;

    public GameObject ghoul;
    public GameObject abilityN;
    public GameObject abilityNRemove;
    float ParticleTimer;
    [SerializeField] float particletimerEnd;

    public TextMeshProUGUI subtitles;
    public float[] timers;
    public string[] myTexts;
    bool played;

    //public GameObject[] Triggers;

    public float speed;

    bool pageCollected;
    public bool isPage;
    public bool isStun;
    float timer;
    [SerializeField]int Index;

    [SerializeField]float TriggerTimer;
    bool TriggerTime;
    [SerializeField]GameObject TriggerObject;
    [SerializeField] bool abilityEarn;
    [SerializeField] bool particle = true;
    public AudioSource disable;

    Abilities ability;

    void Start()
    {
        source = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerController>();
        ability = FindObjectOfType<Abilities>();
    }
    void Update()
    {
        //THE TRIGGER OBJECT MUST BE THE PREVIOUSE ONE BEFORE THIS OBJECT
        if (TriggerObject != null)
        {
           // TriggerTimer += Time.deltaTime;
            if (!TriggerObject.gameObject.activeInHierarchy && particle)
            {
                // if (TriggerTime)
                //{
                //TriggerTimer += Time.deltaTime;
               // if (TriggerTimer >= player.GetComponent<AudioSource>().clip.length)
                //{
                    GetComponent<BoxCollider>().isTrigger = true;
                    ability.abilityEarn.Play();
                    particle = false;
                    abilityNRemove.SetActive(false);
                if (abilityNRemove != null) abilityNRemove.SetActive(false);
               // }
                if (TriggerTimer >= player.GetComponent<AudioSource>().clip.length - 2.5f && particle && abilityEarn)
                {
                    //ability.abilityEarn.Play();
                    particle = false;
                }
                // }
            }
        }
    }

    void TriggerEffects(ref AudioSource mainAudio, GameObject ob)
    {
        player.GetComponent<AudioSource>().clip = mainAudio.clip;
        player.GetComponent<AudioSource>().Play();
        ob.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !played)
        {
            played = true;
            TriggerTime = true;
            if (abilityN != null)
            {
                abilityN.SetActive(true);
            }
            if (isPage)
            {
                ghoul.gameObject.SetActive(true);
            }
            if (isStun)
            {
                speed = 0;
            }
            if(Index == 0)
            {
                ability.speedEarned = true;
            }
            if (Index == 1)
            {
                ability.TeleEarned = true;
                if (disable != null) disable.Stop();
            }
            if (Index == 2)
            {
                ability.TpEarned = true;
                if (disable != null) disable.Stop();
            }
            player.GetComponent<AudioSource>().clip = source.clip;
            player.GetComponent<AudioSource>().Play();
            if(myTexts != null)StartCoroutine(subtitleTime(myTexts, timers));
            //TriggerEffects(ref source, transform.gameObject);
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
        }
        subtitles.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
        abilityN.SetActive(false);
    }
}
