using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public AudioSource source;


    PlayerController player;

    public GameObject ghoul;
    public GameObject abilityN;
    public GameObject abilityNRemove;

    //public GameObject[] Triggers;

    public float speed;

    bool pageCollected;
    public bool isPage;
    public bool isStun;
    float timer;

    [SerializeField]float TriggerTimer;
    bool TriggerTime;
    [SerializeField]GameObject TriggerObject;
    void Start()
    {
        source = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        //THE TRIGGER OBJECT MUST BE THE PREVIOUSE ONE BEFORE THIS OBJECT
        if (!TriggerObject.gameObject.activeInHierarchy)
        {
           // if (TriggerTime)
            //{
                TriggerTimer += Time.deltaTime;
                if (TriggerTimer >= player.GetComponent<AudioSource>().clip.length)
                {
                    GetComponent<BoxCollider>().isTrigger = true;
                    abilityNRemove.SetActive(false);
                }
           // }
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
        if (other.gameObject.CompareTag("Player"))
        {
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
            TriggerEffects(ref source, transform.gameObject);
        }
    }
}
