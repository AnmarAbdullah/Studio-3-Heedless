using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] int speedtwo = 20;
    [SerializeField] float dist;
    [SerializeField] Transform player;
    PlayerController pplayer;
    public ParticleSystem particle;
    public AudioSource PageSFX;
    private void Start()
    {
        pplayer = FindObjectOfType<PlayerController>();
        player = GameObject.FindWithTag("Player").transform;
        particle.transform.position = transform.position;
    }
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
        dist = Vector3.Distance(transform.position, player.transform.position);

        if (pplayer.Magnet && !pplayer.TelekenesisOnCD && dist <= 40)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedtwo * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pplayer.pageCounter += 1;
            particle.transform.position = transform.position;
            Destroy(gameObject);
            particle.Play();
            PageSFX.Play();
        }
    }
}
