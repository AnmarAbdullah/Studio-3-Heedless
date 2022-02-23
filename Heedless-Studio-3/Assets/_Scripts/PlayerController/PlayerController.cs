using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    float moveForward;
    float moveAside;
    bool FlashLight;
    public Light flashlight;
    public Animator anim;

    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //flashlight = GetComponent<Light>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveForward = Input.GetAxis("Vertical") * speed;
        moveAside = Input.GetAxis("Horizontal") * speed;

        rb.velocity = (transform.forward * moveForward) + (transform.right * moveAside) + (transform.up * rb.velocity.y) * Time.deltaTime;
        rb.velocity = new Vector3(rb.velocity.x, -9.81f, rb.velocity.z);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A))
            {
            anim.SetBool("isRunning", false);
        }



       /* rb.velocity = (transform.forward * moveForward) + (transform.right * moveAside) + (transform.up * rb.velocity.y) * Time.deltaTime;
        rb.velocity = new Vector3 (rb.velocity.x, -9.81f, rb.velocity.z);*/
        if(Input.GetKeyDown(KeyCode.Space)) //if (Input.GetMouseButton(0))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
    [SerializeField]int collisions;
    [SerializeField] public bool isColliding;
    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
        collisions++;
    }
    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}
