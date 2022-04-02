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
    public Light flashlight;
    public Animator anim;
    public Camera cam;
    public Transform Enemy;
    public Transform Enemy2;
    public Transform EnemyRespawn;
    public Transform EnemyRespawn2;
    public TextMesh PageCounterText;
    public TextMesh PlayerLifeText;

    public int PlayerLifes = 5;
    
    public int pageCounter;
    AudioSource music;


    //abilities manager....
    //------
    [SerializeField] float TeleTime;
    [SerializeField] public bool Magnet;
    float TelekenesisCD;
    bool TelekenesisOnCD;
    //-----
    public float speedTimer;
    public bool speedBoost;
    float SpeedCD;
    bool SpeedOnCD;
    //-----
    [SerializeField]bool Teleporting = true;
    public GameObject tpObject;
    bool TPOnCD;
    [SerializeField]float TpCD;
    //------
    float stunTimer;
    public bool isStunned;
    [SerializeField]float vanishTimer;
    public bool isVanished;
    //........

    public Pages pages;
    public Rigidbody rb;

    [SerializeField] int collisions;
    [SerializeField] public bool isColliding;

    public GameObject JumpscareObject;
    [SerializeField]float deathTimer;
    public CameraController camm;
    float scareDist;
    [SerializeField]bool Caught;
    public float dist;
    public float dist2;
    [SerializeField]bool jumpscare;

    public AbilitiesManager abManager;
    public GameObject BlueVig;
    public GameObject RedVig;
    public TextMeshProUGUI AbilitiesInfo;
    [SerializeField] int ThisScenePages;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //flashlight = GetComponent<Light>();
        anim = GetComponent<Animator>();
        pages = FindObjectOfType<Pages>();
        camm = GetComponentInChildren<CameraController>();
        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        moveForward = Input.GetAxis("Vertical");
        moveAside = Input.GetAxis("Horizontal");

        Vector3 direction = (transform.forward * moveForward + transform.right* moveAside).normalized;
        rb.velocity = direction * speed;
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
        if (Input.GetKeyDown(KeyCode.E) && abManager.Telekenesis)
        {
            Magnet = true;
           // AbilitiesInfo[0].SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.R) && abManager.SpeedBoost)
        {
            speedBoost = true;
           // AbilitiesInfo[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F) && abManager.Teleport)
        {
            Teleporting = true;
            //AbilitiesInfo[0].SetActive(true);
        }
        //setup for teleport here

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flashlight.enabled = !flashlight.enabled;
        }
        if (Magnet  &&  !TelekenesisOnCD)
        {
            TeleTime += Time.deltaTime;
            AbilityCoolDownOrDuration(TeleTime, 1.5f, Magnet);
            if (TeleTime > 1.5f) { TelekenesisOnCD = true; } 
        }
        if (TelekenesisOnCD)
        {
            AbilityCoolDownOrDuration(TelekenesisCD, 30, TelekenesisOnCD);
        }
        if (speedBoost)
        {
            speed = 35;
            AbilityCoolDownOrDuration(speedTimer, 8, speedBoost);
            if (speedTimer >= 8) { SpeedOnCD = true; } 
        }
        if (SpeedOnCD)
        {
            speedBoost = false;
            AbilityCoolDownOrDuration(SpeedCD, 15, SpeedOnCD);
        }
        if (Teleporting)
        {
            Teleport();
        }
        if (Input.GetButtonDown("Fire1") && Teleporting)
        {
            transform.position = tpObject.transform.position;
            TPOnCD = true;
        }
        if (TPOnCD)
        {
            Teleporting = false;
            AbilityCoolDownOrDuration(TpCD, 15, TPOnCD);
        }

        if (isStunned)
        {
            FindObjectOfType<Astar>().speed = 0;
            AbilityCoolDownOrDuration(stunTimer, 15, isStunned);
        }
        else
        {
            FindObjectOfType<Astar>().speed = 20;
        }
        if (isVanished)
        {
            AbilityCoolDownOrDuration(vanishTimer, 15, isVanished);
        }
        dist = Vector3.Distance(transform.position, Enemy.transform.position);
       // dist = Vector3.Distance(transform.position, Enemy2.transform.position);
        if (dist <= 5 /*|| dist2 <= 5*/)
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
                Enemy2.transform.position = EnemyRespawn2.transform.position;
            }
        }
        else
        {
            JumpscareObject.gameObject.SetActive(false);
            Caught = false;
            jumpscare = false;
            camm.enabled = true;
            if (speedBoost) speed = 35;
            else { speed = 20; };
        }
        
        if (Caught && jumpscare)
        {
            JumpscareObject.gameObject.SetActive(true);
            JumpscareObject.transform.LookAt(transform.position);
            JumpscareObject.transform.position = Vector3.MoveTowards(JumpscareObject.transform.position, cam.transform.position, 50 * Time.deltaTime);
        }

        if(pageCounter == ThisScenePages)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(pageCounter >= ThisScenePages - 15)
        {
            music.Play();
        }
        if (PlayerLifes == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        PageCounterText.text = pageCounter.ToString();
        PlayerLifeText.text = PlayerLifes.ToString();

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
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100) && !TPOnCD)
        {
            if (hit.collider.gameObject.tag == "Floor")
            {
                tpObject.transform.position = hit.point;
            }
        }
        tpObject.transform.LookAt(cam.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stun"))
        {
            isStunned = true;
            stunTimer = 0;
            /*AbilitiesInfo[1].SetActive(true);
            AbilitiesInfo[2].SetActive(true);*/
            AbilitiesInfos(RedVig, 3);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Vanish"))
        {
            isVanished = true;
            vanishTimer = 0;
            /* AbilitiesInfo[1].SetActive(true);
             AbilitiesInfo[3].SetActive(true);*/
            AbilitiesInfos(RedVig, 3);
            Destroy(other.gameObject);
        }
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
    void AbilitiesInfos(GameObject obj, /*string text,*/ float timer)
    {
        StartCoroutine(Notfication(obj, timer));
    }

    IEnumerator Notfication(GameObject obj, /*string text,*/ float time)
    {
        obj.SetActive(true);
        AbilitiesInfo.gameObject.SetActive(true);
       // AbilitiesInfo.text = text;
        yield return new WaitForSeconds(time);
        AbilitiesInfo.gameObject.SetActive(false);
        obj.SetActive(false);
    }

    void AbilityCoolDownOrDuration(float abilityTimer, float time, bool ability)
    {
        abilityTimer += Time.deltaTime;
        /*if(abilityTimer >= time)
        {
            abilityTimer = 0;
            ability = false;
        }*/
    }

}
