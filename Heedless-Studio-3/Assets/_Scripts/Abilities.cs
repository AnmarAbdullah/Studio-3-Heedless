using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Abilities : MonoBehaviour
{
    //abilities manager....
    PlayerController player;
    public Camera cam;
    public ParticleSystem abilityEarn;
    //------
    public bool TeleEarned = true;
    [SerializeField] float TeleTime;
    [SerializeField] public bool Magnet;
    float TelekenesisCD;
    public bool TelekenesisOnCD;
    //-----
    public bool speedEarned = true;
    public float speedTimer;
    public bool speedBoost;
    float SpeedCD;
    [SerializeField] bool SpeedOnCD;
    //-----
    [SerializeField] bool Teleporting = true;
    public bool TpEarned = true;
    public GameObject tpObject;
    bool TPOnCD;
    [SerializeField] float TpCD;
    //------
    [SerializeField] float stunTimer;
    public bool isStunned;
    [SerializeField] float vanishTimer;
    public bool isVanished;
    public bool isRevealed;
    [SerializeField]float telepathyTimer;

    public GameObject BlueVig;
    public GameObject RedVig;
    public TextMeshProUGUI AbilitiesInfo;
    public TextMeshProUGUI AbilitiesInfo2;
    public TextMeshProUGUI AbilitiesInfo3;
    public TextMeshProUGUI nulll;
    public ParticleSystem particle;
    public AudioSource PageSFX;
    Astar astar;
    //........

    Shader shader1;
    Shader shader2;
    Renderer rend;
    public GameObject[] ghoul;
    public Material mat1;
    public Material mat2;

    void Start()
    {
        player = GetComponent<PlayerController>();
        astar = FindObjectOfType<Astar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && TeleEarned/*&& abManager.Telekenesis*/)
        {
            Magnet = true;
            // AbilitiesInfo[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R) && speedEarned /*&& abManager.SpeedBoost*/)
        {
            speedBoost = true;
            // AbilitiesInfo[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F) && !TPOnCD && TpEarned /*&& abManager.Teleport*/)
        {
            Teleporting = true;
            //AbilitiesInfo[0].SetActive(true);
        }

        if (Magnet && !TelekenesisOnCD)
        {
            TeleTime += Time.deltaTime;
            AbilityCoolDownOrDuration(ref TeleTime, 1.5f, ref Magnet);
            AbilitiesInfos(ref BlueVig, ref nulll, 1.5f);
            if (TeleTime >= 1.4f) { TelekenesisOnCD = true; }
        }
        if (TelekenesisOnCD)
        {
            AbilityCoolDownOrDuration(ref TelekenesisCD, 15, ref TelekenesisOnCD);
        }
        if (speedBoost && !SpeedOnCD)
        {
            player.speed = 40;
            AbilityCoolDownOrDuration(ref speedTimer, 9, ref speedBoost);
             if (speedTimer >= 8) { player.speed = 20;  SpeedOnCD = true; }
            AbilitiesInfos(ref BlueVig, ref nulll, 8);
        }
        if (SpeedOnCD)
        {
            speedTimer = 0;
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
            tpObject.gameObject.SetActive(false);
            AbilityCoolDownOrDuration(ref TpCD, 15, ref TPOnCD);
        }

        if (isStunned)
        {
            if (astar != null) astar.speed = 0;
            AbilityCoolDownOrDuration(ref stunTimer, 15, ref isStunned);
        }
        else
        {
            if (astar != null) astar.speed = 20;
        }

        if (isVanished)
        {
            AbilityCoolDownOrDuration(ref vanishTimer, 15, ref isVanished);
        }

        if (isRevealed)
        {
            if (astar != null)
            {
                if (!astar.isChasing)
                {
                    for (int i = 0; i < ghoul.Length; i++)
                    {
                        ghoul[i].GetComponent<Renderer>().material = mat1;
                        ghoul[i].GetComponent<Renderer>().material.color = Color.red;
                        AbilityCoolDownOrDuration(ref telepathyTimer, 30, ref isRevealed);
                    }
                }
            }
            else
            {
                for (int i = 0; i < ghoul.Length; i++)
                {
                    ghoul[i].GetComponent<Renderer>().material = mat1;
                    ghoul[i].GetComponent<Renderer>().material.color = Color.red;
                    AbilityCoolDownOrDuration(ref telepathyTimer, 30, ref isRevealed);
                }
            }
        }
        else
        {
            for (int i = 0; i < ghoul.Length; i++)
            {
                ghoul[i].GetComponent<Renderer>().material = mat2;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stun"))
        {
            isStunned = true;
            stunTimer = 0;
            AbilitiesInfos(ref RedVig, ref AbilitiesInfo2, 15);
            particle.transform.position = other.transform.position;
            Destroy(other.gameObject);
            particle.Play();
            PageSFX.Play();
        }
        if (other.gameObject.CompareTag("Vanish"))
        {
            isVanished = true;
            vanishTimer = 0;
            particle.transform.position = other.transform.position;
            AbilitiesInfos(ref RedVig, ref AbilitiesInfo, 15);
            Destroy(other.gameObject);
            particle.Play();
            PageSFX.Play();
        }
        if (other.gameObject.CompareTag("Telepathy"))
        {
            isRevealed = true;
            telepathyTimer = 0;
            particle.transform.position = other.transform.position;
            AbilitiesInfos(ref RedVig, ref AbilitiesInfo3, 30);
            Destroy(other.gameObject);
            particle.Play();
            PageSFX.Play();
        }
        if (other.gameObject.CompareTag("RestArea"))
        {
            isVanished = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RestArea"))
        {
            isVanished = false;
        }
    }

    void Teleport()
    {
        RaycastHit hit;

        tpObject.gameObject.SetActive(true);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100) && !TPOnCD)
        {
            if (hit.collider.gameObject.tag == "Floor")
            {
                tpObject.transform.position = hit.point;
            }
        }
        //tpObject.transform.LookAt(cam.transform.position);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isVanished = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isVanished = false;
        }
    }

    void AbilitiesInfos(ref GameObject obj, ref TextMeshProUGUI obj2, float timer)
    {
        StartCoroutine(Notfication(obj, obj2, timer));
    }

    IEnumerator Notfication(GameObject obj, TextMeshProUGUI obj2, float time)
    {
        obj.SetActive(true);

        if (obj2 != null) obj2.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
        if(obj2 != null)obj2.gameObject.SetActive(false);
    }

    void AbilityCoolDownOrDuration(ref float abilityTimer, float time, ref bool ability)
    {
        abilityTimer += Time.deltaTime;
        if (abilityTimer >= time)
        {
            ability = false;
            abilityTimer = 0;
        }
    }
}
