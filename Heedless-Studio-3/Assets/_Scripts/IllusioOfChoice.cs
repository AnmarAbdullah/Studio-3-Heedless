using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IllusioOfChoice : MonoBehaviour
{
    public GameObject[] choiceObject;
    public AudioSource choiceblay;

    public GameObject[] buttons;

    [SerializeField] float InteractionTimer;

    [SerializeField]bool choicechosen;
    [SerializeField] bool abilityChoicer;
    Transform player;

    public GameObject lookat;

    public GameObject choiceA;
    public GameObject choiceB;

    public GameObject proffesor;

    public bool inDialogue;
    public AbilitiesManager ABManage;
    Animation anim;
    private void Start()
    {
        player = player = GameObject.FindWithTag("Player").transform;
        choiceblay = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
    }
    private void Update()
    {
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
            player.transform.LookAt(lookat.transform);
            InteractionTimer += Time.deltaTime;
            if (InteractionTimer >= choiceblay.clip.length)
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
            }
            if (choicechosen)
            {
                InteractionTimer = 0;
            }
            if (choicechosen && !choiceblay.isPlaying)
            {
                endDialogue();
                player.rotation = Quaternion.Euler(0, 0, 0);
            }

        }

        if (Input.GetKeyDown(KeyCode.Tab))
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
        if (abilityChoicer) ABManage.SpeedBoost = true;
    }
    public void playChoiceB()
    {
        choicechosen = true;
        InteractionTimer = 0;
        choiceblay.clip = choiceB.GetComponent<AudioSource>().clip;
        choiceblay.Play();
        removeButtons();
        if(abilityChoicer)ABManage.Telekenesis = true;
    }
    void endDialogue()
    {
        inDialogue = false;
        FindObjectOfType<PlayerController>().enabled = true;
        FindObjectOfType<PlayerController>().speed = 25;
        FindObjectOfType<CameraController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 scale = new Vector3(100, -5, 100);
        transform.localScale = Vector3.Lerp(transform.localScale, scale, 5 * Time.deltaTime);

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
            //GetComponent<AudioSource>().time += 5;  
        //}
    }



}
