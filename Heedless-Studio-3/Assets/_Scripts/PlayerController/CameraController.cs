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
        if (FindObjectOfType<IllusioOfChoice>().inDialogue)
        {
            anim.enabled = false;
        }
        if (mouseSens.MouseSens == 0)
        {
            mouseSens.MouseSens = 150;
        }
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens.MouseSens;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens.MouseSens;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 70);
        transform.localEulerAngles = new Vector3(rotationX, 0, 0f);

        Player.Rotate(Vector3.up * mouseX);

        if (GetComponentInParent<Rigidbody>().velocity.x > 0 || GetComponentInParent<Rigidbody>().velocity.x < 0)
        {
            anim.enabled = true;
        }
        
        else
        {
            anim.enabled = false;
        }
        
        if(FindObjectOfType<IllusioOfChoice>().inDialogue)
        {
            anim.enabled = false;
        }
    }
}
