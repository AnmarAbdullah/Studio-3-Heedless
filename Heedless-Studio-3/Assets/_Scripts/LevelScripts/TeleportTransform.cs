using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTransform : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.transform.position = new Vector3(-862f, 3.48f, -40);
        }
    }
}