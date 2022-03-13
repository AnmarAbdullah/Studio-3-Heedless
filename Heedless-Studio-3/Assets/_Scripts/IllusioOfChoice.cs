using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IllusioOfChoice : MonoBehaviour
{
    public GameObject choiceObject;
    public AudioSource choicebla;
    [SerializeField] int InteractionCount;


    [SerializeField] float InteractionTimerOne;
    [SerializeField] float InteractionTimerTwo;
    [SerializeField] float InteractionTimerThree;
    [SerializeField] bool inInteractionOne;
    [SerializeField] bool inInteractionTwo;

    public GameObject[] choices;

    float firstVoiceLine = 19;
    float SecondVoiceLine = 74;
    float ThirdVoiceLine;
    bool inDialogue;
    public AbilitiesManager ABManage;

    //reworking all of this soon!


    private void Start()
    {
        inInteractionOne = true;
        inDialogue = true;
        Cursor.lockState = CursorLockMode.None;
        choicebla = GetComponent<AudioSource>();
        GetComponentInParent<PlayerController>().enabled = false;
        GetComponent<CameraController>().enabled = false;
    }
    private void Update()
    {
        if (inDialogue)
        {
            InterActionONE();
            if (inInteractionTwo)
            {
                InterActionTWO();
            }
            GetComponentInParent<PlayerController>().enabled = false;
            GetComponent<CameraController>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SkipDialogue();
        }
        if (!choicebla.isPlaying && !inInteractionTwo && !inInteractionOne)
        {
            endDialogue();
        }
    }

    public void InterActionONE()
    {
        //inDialogue = true;
        //play voice line...set active first interaction choices at certain time...
        if (inInteractionOne)
        {
            InteractionTimerOne += Time.deltaTime;
            if (InteractionTimerOne >= GetComponent<AudioSource>().clip.length)
            {
                choices[0].SetActive(true);
                choices[1].SetActive(true);
            }
        }
    }
    public void playChoiceA(/*AudioSource choice*/)
    {
        inInteractionOne = false;
        choiceObject = GameObject.Find("ChoiceA");
        choicebla.clip = choiceObject.GetComponent<AudioSource>().clip;
        choicebla.Play();
        removeButtons();
        if (InteractionCount == 2)
        {
            InterActionTWO();
        }
        else if (!choicebla.isPlaying)
        {
            endDialogue();
        }
    }
    public void playChoiceB(/*GameObject choice*/)
    {
        inInteractionOne = false;
        choiceObject = GameObject.Find("ChoiceB");
        choicebla.clip = choiceObject.GetComponent<AudioSource>().clip;
        choicebla.Play();
        //invoke interaction two if amount of interactions is 2 else remove player from cutscene mode
        removeButtons();
        if (InteractionCount == 2)
        {
            InterActionTWO();
        }
        else if(!choicebla.isPlaying)
        {
            endDialogue();
        }
    }

    public void InterActionTWO()
    {
        inInteractionTwo = true;
        InteractionTimerTwo += Time.deltaTime;
        if (InteractionTimerTwo > choiceObject.GetComponent<AudioSource>().clip.length)
        {
            choices[2].SetActive(true);
            choices[3].SetActive(true);
        }  
    }

    public void playChoiceAtwo(/*AudioSource choice*/)
    {
        removeButtons();
        inInteractionTwo = false;
        choiceObject = GameObject.Find("ChoiceA2");
        choicebla.clip = choiceObject.GetComponent<AudioSource>().clip;
        choicebla.Play();
        GetComponentInParent<PlayerController>().TelekenesisEarn = true;
        ABManage.Telekenesis = true;
        removeButtons();
        if (InteractionCount == 3)
        {
            InterActionThree();
        }
        else if (!choicebla.isPlaying)
        {
            endDialogue();
        }
    }
    public void playChoiceBtwo(/*GameObject choice*/)
    {
        removeButtons();
        inInteractionTwo = false;
        choiceObject = GameObject.Find("ChoiceB2");
        choicebla.clip = choiceObject.GetComponent<AudioSource>().clip;
        choicebla.Play();
        //GetComponentInParent<PlayerController>().SpeedEarn = true;
        ABManage.SpeedBoost = true;
        //invoke interaction two if amount of interactions is 2 else remove player from cutscene mode
        removeButtons();
        if (InteractionCount == 3)
        {
            InterActionThree();
        }
         else if(!choicebla.isPlaying)
        {
            endDialogue();
        }
    }

    public void InterActionThree()
    {
        //play voice line...second first interaction choice at certain time...

    }
    public void playChoiceAthree(/*AudioSource choice*/)
    {
        choiceObject = GameObject.Find("ChoiceA3");
        choiceObject.GetComponent<AudioSource>().Play();
        //invoke interaction two
    }
    public void playChoiceBthree(/*GameObject choice*/)
    {
        choiceObject = GameObject.Find("ChoiceB3");
        choiceObject.GetComponent<AudioSource>().Play();
        //invoke interaction two if amount of interactions is 2 else remove player from cutscene mode
    }
    void endDialogue()
    {
        inDialogue = false;
        GetComponentInParent<PlayerController>().enabled = true;
        GetComponent<CameraController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("heloo????????????");
    }
    void removeButtons()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }
    void SkipDialogue()
    {
        // if (choicebla.isPlaying)
        // {
            //GetComponent<AudioSource>().time += 5;
            choicebla.time += 5;
            if(inInteractionOne) InteractionTimerOne += 5;
            if(inInteractionTwo) InteractionTimerTwo += 5;
            //GetComponent<AudioSource>().time += 5;  
        //}
    }

}
