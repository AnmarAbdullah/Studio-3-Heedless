using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] int speedtwo;
    [SerializeField] float dist;
    [SerializeField] Transform player;
    PlayerController pplayer;
    private void Start()
    {
        pplayer = FindObjectOfType<PlayerController>();
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        transform.Rotate(Vector3.left * Time.deltaTime * speed);
        dist = Vector3.Distance(transform.position, player.transform.position);

        if (pplayer.Magnet && dist <= 15)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedtwo * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
