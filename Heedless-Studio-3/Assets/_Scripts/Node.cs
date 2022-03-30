using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour , IComparable
{
    public List<Node> neighbors;

    public bool isVisited;

    public float gCost;
    public float hCost;

    GameObject Player;
    Node[] allNodes;
    public float fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Node parent;

    private void Update()
    {
        //float dist = Vector3.Distance(transform.position, Player.transform.position);
    }
    void FindStartingNode()
    {

    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        gCost = float.MaxValue;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 1f);
        for (int i = 0; i < neighbors.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, neighbors[i].transform.position);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 1f);

        Gizmos.color = Color.red;
    }

    public int CompareTo(object obj)
    {
        Node node = obj as Node;
        if (node.fCost > fCost)
            return -1;
        else if (node.fCost < fCost)
            return 1;


        return 0;
    }
}