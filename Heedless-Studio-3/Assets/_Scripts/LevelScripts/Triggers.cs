using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    [SerializeField]bool isUsed;
    public GameObject JumpScare;
    [SerializeField] float jumpscareTimer;

    private void Update()
    {
        if (isUsed)
        {
            jumpscareTimer += Time.deltaTime;
            if (jumpscareTimer <= 0.15f)
            {
                JumpScare.SetActive(true);
            }
            else
            {
                JumpScare.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isUsed)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isUsed = true;
            }
        }
    }
}
