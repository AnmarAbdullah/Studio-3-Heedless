using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float rotationX;
    public float Sensitivity;
    public Transform Player;
    Animator anim;
    public PlayerController pplayer;
    float jumpscare;
    public AbilitiesManager mouseSens;
    public bool Infuckingdialogue;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        pplayer = FindObjectOfType<PlayerController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens.MouseSens;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens.MouseSens;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 70);
        transform.localEulerAngles = new Vector3(rotationX, 0, 0f);
        if (mouseSens.MouseSens == 0)
        {
            mouseSens.MouseSens = 150;
        }

        Player.Rotate(Vector3.up * mouseX);


        Player.Rotate(Vector3.up * mouseX);

        if (GetComponentInParent<Rigidbody>().velocity.x > 0 || GetComponentInParent<Rigidbody>().velocity.x < 0)
        {
            //if (!Infuckingdialogue) 
           // { 
                anim.enabled = true;
                //anim.SetBool("inDialogue", false);
           // };
            //anim.enabled = true;
        }       
        else
        {
            anim.enabled = false;
           // anim.SetBool("inDialogue", true);
        }
        
        /*if(FindObjectOfType<IllusioOfChoice>().inDialogue)
        {
            Infuckingdialogue = true;
            anim.enabled = false;
            anim.SetBool("inDialogue", true);
        }
        else
        {
            Infuckingdialogue = false;
        }*/
    }
}
