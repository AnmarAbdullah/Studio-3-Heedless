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
    //Transform player;
    PlayerController player;

    public GameObject lookat;

    public GameObject choiceA;
    public GameObject choiceB;

    public GameObject nextProffesor;

    public bool inDialogue;
    public bool noChoice;
    //public AbilitiesManager ABManage;
    CameraController cam;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        choiceblay = GetComponent<AudioSource>();
        cam = FindObjectOfType<CameraController>();
    }
    private void Update()
    {
        //Debug.LogError(ABManage.SpeedBoost);
        if (inDialogue)
        {
            Vector3 scale = new Vector3(100, 95.90824f, 100);
            transform.localScale = Vector3.Lerp(transform.localScale, scale, 5 * Time.deltaTime);
            // transform.rotation = Quaternion.RotateTowards
            //anim.Play();
            //InterAction();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cam.GetComponent<Animator>().enabled = false;
            player.enabled = false;
            cam.enabled = false;
            //FindObjectOfType<CameraController>().GetComponent<Animation>().enabled = false;
            player.transform.LookAt(lookat.transform);
            //FindObjectOfType<CameraController>().Infuckingdialogue = true;
            InteractionTimer += Time.deltaTime;
            if (InteractionTimer >= choiceblay.clip.length && choiceB != null)
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
            }
            if (buttons == null&& InteractionTimer >= choiceblay.clip.length)
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
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if(noChoice && !choiceblay.isPlaying)
            {
                endDialogue();
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            Vector3 scale = new Vector3(100, -5, 100);
            transform.localScale = Vector3.Lerp(transform.localScale, scale, 5 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && inDialogue)
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
        player.enabled = true;
        player.speed = 20;
        FindObjectOfType<CameraController>().enabled = true;
        player.rb.isKinematic = false;
        //FindObjectOfType<CameraController>().GetComponent<Animation>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 scale = new Vector3(100, -5, 100);
        transform.localScale = Vector3.Lerp(transform.localScale, scale, 5 * Time.deltaTime);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);

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
            choiceblay.time += 5;
            InteractionTimer += 5;
    }



}
