using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTransform : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.transform.position = new Vector3(-431f, 1.74f, -20);
        }
    }
}