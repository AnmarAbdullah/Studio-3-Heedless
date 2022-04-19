using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTransform : MonoBehaviour
{
    PlayerController player;
    [SerializeField]GameObject sub;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.transform.position = new Vector3(-862f, 3.48f, -40);
            player.GetComponent<AudioSource>().Stop();
            sub.gameObject.SetActive(false);
        }
    }
}