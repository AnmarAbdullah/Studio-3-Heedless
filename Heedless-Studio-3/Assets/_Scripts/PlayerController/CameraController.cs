using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float rotationX;
    public float Sensitivity;
    public Transform Player;
    Animator anim;
    float jumpscare;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       /* float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * Sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * Sensitivity;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 70);
        transform.localEulerAngles = new Vector3(rotationX, 0, 0f);

        Player.Rotate(Vector3.up * mouseX);*/

        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
        {
            anim.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A))
        {
            anim.enabled = false;
        }
    }
    private void LateUpdate()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * Sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * Sensitivity;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 70);
        transform.localEulerAngles = new Vector3(rotationX, 0, 0f);

        Player.Rotate(Vector3.up * mouseX);
    }
}
