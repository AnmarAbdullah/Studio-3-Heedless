using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IllusioOfChoice : MonoBehaviour
{
    public GameObject[] choiceObject;
    public AudioSource choiceblay;

    public GameObject[] buttons;

    public float InteractionTimer;

    [SerializeField]bool choicechosen;
    [SerializeField] bool abilityChoicer;
    Transform player;

    public GameObject lookat;

    public GameObject choiceA;
    public GameObject choiceB;

    public GameObject nextProffesor;
    float disappearTimer;

    public bool inDialogue;
    public bool noChoice;
    public bool hasTp;
    //public AbilitiesManager ABManage;
    Animation anim;
    public GameObject cam;
    public ParticleSystem particle;
    float particleTimer;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        choiceblay = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void Update()
    {
        //Debug.LogError(ABManage.SpeedBoost);
        if (inDialogue)
        {
            Vector3 scale = new Vector3(100, 95.90824f, 100);
            transform.localScale = Vector3.Lerp(transform.localScale, scale, 5 * Time.deltaTime);
            //anim.Play();
            //InterAction();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            FindObjectOfType<PlayerController>().enabled = false;
            FindObjectOfType<CameraController>().enabled = false;
            //FindObjectOfType<CameraController>().GetComponent<Animation>().enabled = false;
            player.transform.LookAt(lookat.transform);
            FindObjectOfType<CameraController>().Infuckingdialogue = true;
            InteractionTimer += Time.deltaTime;
            if (InteractionTimer >= choiceblay.clip.length && choiceB != null)
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
            }
            else if (buttons == null&& InteractionTimer >= choiceblay.clip.length)
            {
                endDialogue();
            }

           // cam.GetComponent<Animator>().enabled = false; 
            if (choicechosen)
            {
                InteractionTimer = 0;
            }
            if (choicechosen && !choiceblay.isPlaying)
            {
                endDialogue();
                Destroy(transform.gameObject, 2);
                player.rotation = Quaternion.Euler(0, 0, 0);
            }
            if(noChoice && !choiceblay.isPlaying)
            {
                endDialogue();
                player.rotation = Quaternion.Euler(0, 0, 0);
            }
            if(abilityChoicer && choicechosen)
            {
                particleTimer += Time.deltaTime;
                if(particleTimer >= 8)
                {
                    particle.Play();
                }
            }

        }
        else
        {
            Vector3 scale = new Vector3(100, -5, 100);
            transform.localScale = Vector3.Lerp(transform.localScale, scale, 5 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Tab) && inDialogue)
        {
            SkipDialogue();
        }
        if (!choiceblay.isPlaying)
        {
            //CAEWendDialogue();
        }
    }

    public void InterAction()
    {
        choiceblay.Play();
    }
    public void playChoiceA()
    {
        choicechosen = true;
        InteractionTimer = 0;
        choiceblay.clip = choiceA.GetComponent<AudioSource>().clip;
        choiceblay.Play();
        removeButtons();
        /*if (abilityChoicer) { ABManage.SpeedBoost = true; }
        if (ABManage.SpeedBoost && hasTp) ABManage.Teleport = true;*/
    }
    public void playChoiceB()
    {
        choicechosen = true;
        InteractionTimer = 0;
        choiceblay.clip = choiceB.GetComponent<AudioSource>().clip;
        choiceblay.Play();
        removeButtons();
        /*if(abilityChoicer)ABManage.Telekenesis = true;
        if(ABManage.Telekenesis && hasTp) ABManage.Teleport = true;*/
    }
    //create new function to unlock tp
    void endDialogue()
    {
        inDialogue = false;
        FindObjectOfType<PlayerController>().enabled = true;
        FindObjectOfType<PlayerController>().speed = 20;
        FindObjectOfType<CameraController>().enabled = true;
        FindObjectOfType<PlayerController>().rb.isKinematic = false;
        //FindObjectOfType<CameraController>().GetComponent<Animation>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 scale = new Vector3(100, -5, 100);
        transform.localScale = Vector3.Lerp(transform.localScale, scale, 5 * Time.deltaTime);
        player.rotation = Quaternion.Euler(0, 0, 0);

        nextProffesor.gameObject.SetActive(true);
        //Debug.Log("heloo????????????");
    }
    void removeButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }
    void SkipDialogue()
    {
        // if (choicebla.isPlaying)
        // {
            //GetComponent<AudioSource>().time += 5;
            choiceblay.time += 5;
            InteractionTimer += 5;
            particleTimer += 5;
            //GetComponent<AudioSource>().time += 5;  
        //}
    }



}
