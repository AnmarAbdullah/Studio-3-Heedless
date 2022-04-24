using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float moveForward;
    float moveAside;
    bool FlashLight;
    bool mapp;
    public Light flashlight;
    public Animator anim;
    public Camera cam;
    public Transform Enemy;
    public Transform EnemyRespawn;
    public TextMesh PageCounterText;
    public TextMesh PlayerLifeText;

    public int PlayerLifes = 5;
    
    public int pageCounter;


    public Pages pages;
    public Rigidbody rb;

    public GameObject JumpscareObject;
    [SerializeField]float deathTimer;
    public CameraController camm;
    float scareDist;
    [SerializeField]bool Caught;
    public float dist;
    public float dist2;
    [SerializeField]bool jumpscare;
    public GameObject Map;

    [SerializeField] int ThisScenePages;
    Abilities ability;
    IllusioOfChoice dialogue;

    Astar astar;

    void Start()
    {
        ability = GetComponent<Abilities>();
        rb = GetComponent<Rigidbody>();
        //flashlight = GetComponent<Light>();
        anim = GetComponent<Animator>();
        pages = FindObjectOfType<Pages>();
        camm = GetComponentInChildren<CameraController>();
       // music = this.GetComponent<AudioSource>();
        astar = FindObjectOfType<Astar>();
        dialogue = FindObjectOfType<IllusioOfChoice>();
    }

    // Update is called once per frame
    void Update()
    {

        moveForward = Input.GetAxis("Vertical");
        moveAside = Input.GetAxis("Horizontal");

        Vector3 direction = (transform.forward * moveForward + transform.right* moveAside).normalized;
        rb.velocity = direction * speed;
        rb.velocity = new Vector3(rb.velocity.x, -9.81f, rb.velocity.z);
;        
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
        if (Input.GetMouseButtonDown(1))
        {
            if (!Map.activeInHierarchy) { Map.SetActive(true); }
            else { Map.SetActive(false); }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) { Map.SetActive(false); }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flashlight.enabled = !flashlight.enabled;
        }

        dist = Vector3.Distance(transform.position, Enemy.transform.position);
       // dist = Vector3.Distance(transform.position, Enemy2.transform.position);
        if (dist <= 5 && !ability.isStunned)
        {
            Caught = true;
            JumpscareObject.gameObject.SetActive(true);
            RaycastHit Die;
            deathTimer += Time.deltaTime;
            scareDist = Vector3.Distance(cam.transform.position, JumpscareObject.transform.position);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out Die, 100) && !jumpscare)
            {
                camm.enabled = false;
                JumpscareObject.transform.position = Die.point;
                jumpscare = true;
                speed = 0;
            }
            if(scareDist <=  0.3f)
            {
                transform.position = new Vector3(213.039993f, 4.38000011f, -36.9799995f);
                PlayerLifes -= 1;
                camm.enabled = true;
                speed = 0;
                Enemy.transform.position = EnemyRespawn.transform.position;
            }
        }
        else
        {
            JumpscareObject.gameObject.SetActive(false);
            Caught = false;
            jumpscare = false;
            camm.enabled = true;
            if (ability.speedBoost) speed = 40;
            else { speed = 25; };
        }
        
        if (Caught && jumpscare)
        {
            JumpscareObject.gameObject.SetActive(true);
            JumpscareObject.transform.LookAt(transform.position);
            JumpscareObject.transform.position = Vector3.MoveTowards(JumpscareObject.transform.position, cam.transform.position, 50 * Time.deltaTime);
        }
        if (pageCounter == ThisScenePages)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (PlayerLifes == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        PageCounterText.text = pageCounter.ToString();
        PlayerLifeText.text = PlayerLifes.ToString();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interaction"))
        {
            speed = 0;
            anim.SetBool("isRunning", false);
            rb.isKinematic = true;
            this.enabled = false;
            FindObjectOfType<IllusioOfChoice>().InterAction();
            FindObjectOfType<IllusioOfChoice>().inDialogue = true;
            Destroy(other.gameObject);
        }
    }
}
