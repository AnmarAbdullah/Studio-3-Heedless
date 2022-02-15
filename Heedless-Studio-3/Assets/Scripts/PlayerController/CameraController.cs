using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float rotationX;
    public float Sensitivity;
    public Transform Player;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * Sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * Sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        transform.localEulerAngles = new Vector3(rotationX, 0, 0f);

        Player.Rotate(Vector3.up * mouseX);

    }
}
