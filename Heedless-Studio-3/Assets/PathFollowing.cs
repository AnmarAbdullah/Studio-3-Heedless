using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    public GameObject[] path;
    float dist;
    int pathindex;

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 500);
        dist = Vector3.Distance(transform.position, path[pathindex].transform.position);
        transform.position = Vector3.MoveTowards(transform.position, path[pathindex].transform.position, 2 * Time.deltaTime);
        if(dist <= 0.1f)
        {
            pathindex++;
        }
        if (pathindex >= path.Length)
        {
            pathindex = 0;
        }
    }
}
