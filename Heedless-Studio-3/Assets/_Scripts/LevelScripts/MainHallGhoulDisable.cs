using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHallGhoulDisable : MonoBehaviour
{
    public GameObject GhoulDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(GhoulDisable.gameObject);
        }
    }

}
