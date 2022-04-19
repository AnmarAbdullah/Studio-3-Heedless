using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpscares : MonoBehaviour
{
    public GameObject ghoulappear;
    public Rigidbody ghoul;
    bool activated;
    PlayerController player;
    CameraController cam;
    [SerializeField]float timer;
    bool used;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cam = FindObjectOfType<CameraController>();
            player.enabled = false;
            if (!used)
            {
                activated = true;
                transform.gameObject.GetComponent<AudioSource>().Play();
            }
            if (ghoul!= null)ghoul.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (activated)
        {
            if(ghoul!=null)ghoul.AddForce(ghoul.transform.forward * 5000 * Time.deltaTime); //player.transform.LookAt(ghoulappear.transform);
            //player.speed = 0;
            player.rb.isKinematic = true;
            player.enabled = false;
            cam.enabled = false;
            player.anim.SetBool("isRunning", false);
            if (ghoul != null) player.transform.LookAt(ghoulappear.transform);
            timer += Time.deltaTime;
            if(timer >= 1.5f)
            {
                used = true;
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
                activated = false;
            }
        }
        if (used && !activated)
        {
            player.enabled = true;
            cam.enabled = true;
            player.rb.isKinematic = false;
            Destroy(ghoulappear);
        }
    }
}
