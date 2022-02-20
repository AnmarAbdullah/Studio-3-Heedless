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

    public GameObject[] choices;

    float firstVoiceLine = 19;
    float SecondVoiceLine;
    float ThirdVoiceLine;
    bool inDialogue;

    //reworking all of this soon!


    private void Start()
    {
        inInteractionOne = true;
        inDialogue = true;
        Cursor.lockState = CursorLockMode.None;
        //this.GetComponent<AudioSource>()
    }
    private void Update()
    {
        if (inDialogue)
        {
            InterActionONE();
            GetComponentInParent<PlayerController>().enabled = false;
            GetComponent<CameraController>().enabled = false;
            GetComponent<CameraBobbing>().enabled = false;
        }
    }

    public void InterActionONE()
    {
        //inDialogue = true;
        //play voice line...set active first interaction choices at certain time...
        if (inInteractionOne)
        {
            InteractionTimerOne += Time.deltaTime;
            if (InteractionTimerOne >= firstVoiceLine)
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
        this.GetComponent<AudioSource>().clip = choiceObject.GetComponent<AudioSource>().clip;
        this.GetComponent<AudioSource>().Play();
        //invoke interaction two
        choices[0].gameObject.SetActive(false);
        choices[1].gameObject.SetActive(false);
        if (InteractionCount == 2)
        {
            InterActionTWO();
        }
        else
        {
            Invoke(nameof(endDialogue), choiceObject.GetComponent<AudioSource>().clip.length);
        }
    }
    public void playChoiceB(/*GameObject choice*/)
    {
        inInteractionOne = false;
        choiceObject = GameObject.Find("ChoiceB");
        choiceObject.GetComponent<AudioSource>().Play();
        //invoke interaction two if amount of interactions is 2 else remove player from cutscene mode
        choices[0].gameObject.SetActive(false);
        choices[1].gameObject.SetActive(false);
        if (InteractionCount > 2)
        {
            InterActionTWO();
        }
        else
        {
            Invoke(nameof(endDialogue), choiceObject.GetComponent<AudioSource>().clip.length);
        }
    }

    public void InterActionTWO()
    {
        //play voice line...second first interaction choice at certain time...
        InteractionTimerTwo += Time.deltaTime;
        if (InteractionTimerOne > SecondVoiceLine)
        {
            choices[2].SetActive(true);
            choices[3].SetActive(true);
        }
    }

    public void playChoiceAtwo(/*AudioSource choice*/)
    {
        choiceObject = GameObject.Find("ChoiceA2");
        choiceObject.GetComponent<AudioSource>().Play();
        //invoke interaction two
    }
    public void playChoiceBtwo(/*GameObject choice*/)
    {
        choiceObject = GameObject.Find("ChoiceB2");
        choiceObject.GetComponent<AudioSource>().Play();
        //invoke interaction two if amount of interactions is 2 else remove player from cutscene mode
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
        GetComponent<CameraBobbing>().enabled = true;
        GetComponent<CameraController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
