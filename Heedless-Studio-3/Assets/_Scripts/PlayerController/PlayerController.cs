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
    AudioSource music;


    //abilities manager....
    //------
    [SerializeField] float TeleTime;
    [SerializeField] public bool Magnet;
    float TelekenesisCD;
    public bool TelekenesisOnCD;
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

    public GameObject JumpscareObject;
    [SerializeField]float deathTimer;
    public CameraController camm;
    float scareDist;
    [SerializeField]bool Caught;
    public float dist;
    public float dist2;
    [SerializeField]bool jumpscare;

    //public AbilitiesManager abManager;
    public GameObject BlueVig;
    public GameObject RedVig;
    public GameObject Map;
    public TextMeshProUGUI AbilitiesInfo;
    public TextMeshProUGUI AbilitiesInfo2;
    TextMeshProUGUI nulll;
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
        if (Input.GetKeyDown(KeyCode.E) /*&& abManager.Telekenesis*/)
        {
            Magnet = true;
           // AbilitiesInfo[0].SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.R) /*&& abManager.SpeedBoost*/)
        {
            speedBoost = true;
           // AbilitiesInfo[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F) && !TPOnCD /*&& abManager.Teleport*/)
        {
            Teleporting = true;
            //AbilitiesInfo[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!Map.activeInHierarchy) { Map.SetActive(true); } 
            else { Map.SetActive(false); }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) { Map.SetActive(false); }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flashlight.enabled = !flashlight.enabled;
        }
        if (Magnet  &&  !TelekenesisOnCD)
        {
            TeleTime += Time.deltaTime;
            AbilityCoolDownOrDuration(ref TeleTime, 1.5f, ref Magnet);
            AbilitiesInfos(ref BlueVig, ref nulll, 1.5f);
            if (TeleTime > 1.5f) { TelekenesisOnCD = true; } 
        }
        if (TelekenesisOnCD)
        {
            AbilityCoolDownOrDuration(ref TelekenesisCD, 30, ref TelekenesisOnCD);
        }
        if (speedBoost && !SpeedOnCD)
        {
            speed = 40;
            AbilityCoolDownOrDuration(ref speedTimer, 8, ref speedBoost);
            if (speedTimer >= 10) { SpeedOnCD = true; speed = 20; }
            AbilitiesInfos(ref BlueVig, ref nulll, 8);
        }
        if (SpeedOnCD)
        {
            speedBoost = false;
            AbilityCoolDownOrDuration(ref SpeedCD, 20, ref SpeedOnCD);
        }
        if (Teleporting)
        {
            Teleport();
            AbilitiesInfos(ref BlueVig, ref nulll, 10000);
        }
        if (Input.GetButtonDown("Fire1") && Teleporting)
        {
            transform.position = tpObject.transform.position;
            TPOnCD = true;
            BlueVig.SetActive(false);
        }
        if (TPOnCD)
        {
            Teleporting = false;
            tpObject.SetActive(false);
            AbilityCoolDownOrDuration(ref TpCD, 15, ref TPOnCD);
        }

        if (isStunned)
        {
            FindObjectOfType<Astar>().speed = 0;
            AbilityCoolDownOrDuration(ref stunTimer, 15, ref isStunned);
        }
        else
        {
            FindObjectOfType<Astar>().speed = 20;
        }
        if (isVanished)
        {
            AbilityCoolDownOrDuration(ref vanishTimer, 15, ref isVanished);
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

    void Teleport()
    {
        RaycastHit hit;

        tpObject.SetActive(true);
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100) && !TPOnCD)
        {
            if (hit.collider.gameObject.tag == "Floor")
            {
                tpObject.transform.position = hit.point;
            }
        }
        //tpObject.transform.LookAt(cam.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stun"))
        {
            isStunned = true;
            stunTimer = 0;
            /*AbilitiesInfo[1].SetActive(true);
            AbilitiesInfo[2].SetActive(true);*/
            AbilitiesInfos(ref RedVig, ref AbilitiesInfo2, 15);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Vanish"))
        {
            isVanished = true;
            vanishTimer = 0;
            /* AbilitiesInfo[1].SetActive(true);
             AbilitiesInfo[3].SetActive(true);*/
            AbilitiesInfos(ref RedVig, ref AbilitiesInfo, 15);
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
    void AbilitiesInfos(ref GameObject obj, ref TextMeshProUGUI obj2, float timer)
    {
        StartCoroutine(Notfication(obj,obj2, timer));
    }

    IEnumerator Notfication(GameObject obj, TextMeshProUGUI obj2, float time)
    {
        obj.SetActive(true);
        //AbilitiesInfo.gameObject.SetActive(true);
        // AbilitiesInfo.text = text;
        obj2.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
       // AbilitiesInfo.gameObject.SetActive(false);
        obj.SetActive(false);
        obj2.gameObject.SetActive(false);
    }

    void AbilityCoolDownOrDuration(ref float abilityTimer, float time, ref bool ability)
    {
        abilityTimer += Time.deltaTime;
        if(abilityTimer >= time)
        {
            ability = false;
            abilityTimer = 0;
        }
    }

}
