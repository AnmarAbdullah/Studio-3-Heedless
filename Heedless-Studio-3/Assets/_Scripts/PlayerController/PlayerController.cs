using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    float moveForward;
    float moveAside;

    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveForward = Input.GetAxis("Vertical") * speed;
        moveAside = Input.GetAxis("Horizontal") * speed;

        // transform.Translate(Vector3.down * 9.81f * Time.deltaTime);

        

        rb.velocity = (transform.forward * moveForward) + (transform.right * moveAside) + (transform.up * rb.velocity.y) * Time.deltaTime;
        rb.velocity = new Vector3 (rb.velocity.x, -9.81f, rb.velocity.z);
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
