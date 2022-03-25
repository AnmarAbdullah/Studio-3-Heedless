using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    [SerializeField] float maxVelocity = 3;
    Vector3 velocity;
    [SerializeField] float max_force = 3;
    [SerializeField] float mass = 3;
    [SerializeField] float slowingRadius = 5;
    [SerializeField]float MAX_AVOIDANCE_FORCE = 5;

    float MAX_SEE_AHEAD = 5;

    public Transform target;

    Vector3 ahead;
    Vector3 ahead2;

    RaycastHit hit;
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.yellow);
            if (hit.collider.gameObject != null)
            {
                transform.position += CollisionAvoid() * Time.deltaTime;
            }
        }
        if (Physics.Raycast(transform.position, transform.right, out hit, 10))
        {
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.yellow);
            if (hit.collider.gameObject != null)
            {
                transform.position += CollisionAvoid() * Time.deltaTime;
            }
        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, 10))
        {
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.yellow);
            if (hit.collider.gameObject != null)
            {
                transform.position += CollisionAvoid() * Time.deltaTime;
            }
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 10))
        {
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.yellow);
            if (hit.collider.gameObject != null)
            {
                transform.position += CollisionAvoid() * Time.deltaTime;
            }
        }

        //transform.LookAt(target.position);
        /*var desired_velocity = transform.position;
        desired_velocity = desired_velocity.normalized * maxVelocity;
        var steering = desired_velocity - velocity;


        steering = steering + CollisionAvoid();
        steering = Vector3.ClampMagnitude(steering, max_force);
        steering = steering / mass;

        velocity = Vector3.ClampMagnitude(velocity += steering, maxVelocity);
        transform.position = transform.position + velocity * Time.deltaTime;*/


    }
    float distance(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.z + b.z) * (a.z - b.z));
    }
    bool collision;
    GameObject gb;
    GameObject findMostThreatenting()
    {
        GameObject mostThreatening = null;
        {
            gb = hit.collider.gameObject;
            /* = transform.position + (velocity).normalized * MAX_SEE_AHEAD;
            ahead2 = transform.position + (velocity).normalized * MAX_SEE_AHEAD * 0.5f;*/
            //for (int e = 0; e < obstacles.Length; e++)
            //{
            if (hit.collider.gameObject != null)
            {
                collision = true;
            }

            //collision = hit.collider.gameObject;
            //}
            //bool collision = lineintersected(ahead, ahead2, hit);
        }
        // for (int z = 0; z < obstacles.Length; z++)
        //{
        if (collision && (mostThreatening == null || distance(transform.position, gb.transform.position) < distance(transform.position, mostThreatening.transform.position)))
        {
            mostThreatening = gb;
        }
        // }

        return mostThreatening;
    }
    Vector3 avoidanceForce;
    Vector3 CollisionAvoid()
    {
        ahead = transform.position + (velocity).normalized * MAX_SEE_AHEAD;
        ahead2 = transform.position + (velocity).normalized * MAX_SEE_AHEAD * 0.5f;
        // for (int i = 0; i < obstacles.Length; i++)
        //{
        //avoidanceForce = ahead - obstacles[0].transform.position;
        //}
        // Vector3 avoidanceForce = ahead - obstacles[i];
        //avoidanceForce = avoidanceForce.normalized * MAX_AVOIDANCE_FORCE;

        GameObject mostThreatening = findMostThreatenting();
        Vector3 avoidance = new Vector3(0, 0, 0);

        if (mostThreatening != null)
        {
            avoidance.x = ahead.x - mostThreatening.transform.position.x;
            avoidance.y = 0;
            avoidance.z = ahead.z - mostThreatening.transform.position.z;

            avoidance = avoidance.normalized;
            //avoidance.Scale(MAX_AVOIDANCE_FORCE);
            avoidance = avoidance * MAX_AVOIDANCE_FORCE;
        }
        else
        {
            avoidance = Vector3.zero;
        }
        return avoidance;
    }
}
