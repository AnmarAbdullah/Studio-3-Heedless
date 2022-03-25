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
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        pplayer = FindObjectOfType<PlayerController>();
    }
 
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * Sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * Sensitivity;
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
        
        else
        {
            anim.enabled = true;
        }
    }
}
