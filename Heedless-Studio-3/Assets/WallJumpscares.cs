using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpscares : MonoBehaviour
{
    public GameObject ghoulappear;
    public Rigidbody ghoul;
    bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            activated = true;
            ghoul.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (activated)
        {
            ghoul.AddForce(ghoul.transform.forward * 5000 * Time.deltaTime);
            Destroy(ghoulappear, 3);
        }
    }
}
