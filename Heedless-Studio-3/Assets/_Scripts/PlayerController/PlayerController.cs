using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float moveForward;
    float moveAside;
    bool FlashLight;
    public Light flashlight;
    public Animator anim;
    public Camera cam;

    //------
    [SerializeField] float TeleTime;
    [SerializeField] public bool Magnet;
   public bool TelekenesisEarn;
    //-----
    public float speedTimer;
    public bool SpeedEarn;
    public bool speedBoost;
    //-----
    [SerializeField]bool Teleporting = true;
    public GameObject tpObject;
    bool TPMode;

    public Pages pages;
    public Rigidbody rb;

    [SerializeField] int collisions;
    [SerializeField] public bool isColliding;

    public AbilitiesManager abManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //flashlight = GetComponent<Light>();
        anim = GetComponent<Animator>();
        pages = FindObjectOfType<Pages>();
}

    // Update is called once per frame
    void Update()
    {
        moveForward = Input.GetAxis("Vertical");
        moveAside = Input.GetAxis("Horizontal");

        Vector3 direction = (transform.forward * moveForward + transform.right* moveAside).normalized;
        rb.velocity = direction * speed; //+ (transform.right * moveAside);// + (transform.up * rb.velocity.y);
     //   rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y, rb.velocity.z).normalized * speed * Time.deltaTime;
       // Debug.Log(rb.velocity);        
        if (rb.velocity.x > 30 || rb.velocity.z > 30)
        {
            rb.velocity = rb.velocity / 1.5f;
        }
        if (rb.velocity.x > 0f|| rb.velocity.x < 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.E) && abManager.Telekenesis)
        {
            Magnet = true;
        }
        if(Input.GetKeyDown(KeyCode.R) && abManager.SpeedBoost)
        {
            speedBoost = true;
        }
        if (Input.GetKeyDown(KeyCode.F) /*&& abManager.SpeedBoost*/)
        {
            Teleporting = true;
        }
        //setup for teleport here

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flashlight.enabled = !flashlight.enabled;
        }
        if (Magnet)
        {
            TeleTime += Time.deltaTime;
            if(TeleTime > 1) { Magnet = false; TeleTime = 0; }
        }
        if (speedBoost)
        {
            speed = 35;
            speedTimer += Time.deltaTime;
            if(speedTimer >= 8) { speedBoost = false; speedTimer = 0; speed = 25; }
        }
        if (Teleporting)
        {
            Teleport();
            tpObject.transform.parent = cam.transform;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
        collisions++;
    }
    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
    void Teleport()
    {
      if(Input.GetButtonDown("Fire1"))
      {
            transform.Translate(Vector3.forward * Time.deltaTime * 1000);
            Teleporting = false;
      }
    }

}
