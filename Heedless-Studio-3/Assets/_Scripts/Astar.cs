using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
  // public List<Node> neighborlist;
    public List<Node> openlist;
    public List<Node> pathlist;

    public Node startingNode;
    public Node targetNode;
    public Node current;

    public GameObject gb;
    [SerializeField]int pathIndex;
    public float speed;
    [SerializeField]float AstarTimer;
    [SerializeField]Node[] allNodes;
    GameObject Player;
    PlayerController pplayer;
    [SerializeField]bool Towardstart;
     
    [SerializeField] float dist;
    [SerializeField] float distStart;
    [SerializeField]bool check = true;

    [SerializeField]bool other;
    public bool isChasing;
    public GameObject cam;
    //[SerializeField]float[] dist;


    void Start()
    {
        openlist = new List<Node>();
        pathlist = new List<Node>();
        pplayer = FindObjectOfType<PlayerController>();
        //startingNode.Position = waypointNeighbor.nodes[10].Position;
        //targetNode.Position = waypointNeighbor.nodes[15].Position;

        //openlist.Add(startingNode);
        allNodes = FindObjectsOfType<Node>();
        Player = GameObject.FindGameObjectWithTag("Player");
        FindStartingNode(allNodes);
        FindTargetNode(allNodes);
        Findpath();
        //gb.transform.position = pathlist[0].transform.position;
      //  allNodes = FindObjectsOfType<Node>();


        //Debug.Log(neighborlist);
    }
    void FindStartingNode(Node[] nodes)
    {
        Node closestNode =  null;
        float minNode = Mathf.Infinity;
        foreach (Node item in nodes)
        {
            float distance = Vector3.Distance(item.transform.position, gb.transform.position);
            if (distance < minNode)
            {
                RaycastHit ray;
                if (Physics.Raycast(gb.transform.position, item.transform.position - gb.transform.position, out ray, distance))
                {
                    if (ray.collider.gameObject.tag == "Node")
                    {
                        Debug.DrawRay(gb.transform.position, ray.point - gb.transform.position, Color.green);
                        closestNode = item;
                        minNode = distance;
                        startingNode = closestNode;
                    }
                }
            }
        }
    }
    void FindTargetNode(Node[] nodes)
    {
        //distStart = Vector3.Distance(gb.transform.position, startingNode.transform.position);
        Node closestNode = null;
        float minNode = Mathf.Infinity;
        foreach (Node item in nodes)
        {
            float distance = Vector3.Distance(item.transform.position, Player.transform.position);
            distStart = Vector3.Distance(gb.transform.position, startingNode.transform.position);
            if (distance < minNode)
            {
                 //Debug.DrawRay(gb.transform.position, ray.point - gb.transform.position, Color.green);
                 closestNode = item;
                 minNode = distance;
                 targetNode = closestNode;                
               /* closestNode = item;
                minNode = distance;
                targetNode = closestNode;*/
            }
        }
    }

    void Update()
    {
        //Debug.DrawRay(gb.transform.position, ray.point - gb.transform.position, Color.green);
        Debug.Log(pathIndex);
        gb.transform.LookAt(Player.transform);
        //FindStartingNode(allNodes);
        dist = Vector3.Distance(gb.transform.position, pathlist[pathIndex].transform.position);
        distStart = Vector3.Distance(gb.transform.position, startingNode.transform.position);
       // while (pathIndex < pathlist.Count)
        //{
            RaycastHit hit;
        //Physics.Raycast(gb.transform.position, startingNode.transform.position - gb.transform.position, out hit, 100);
        
         if (Physics.Raycast(gb.transform.position, pathlist[0].transform.position - gb.transform.position, out hit, 20000))
         {
             Debug.DrawRay(gb.transform.position, hit.point - gb.transform.position, Color.cyan);
             if (hit.collider.gameObject.tag == "Node" && !isChasing)
             {
                 other = true;
                 Towardstart = false;
                 while (pathIndex < pathlist.Count && other)
                 {
                     ContinueWalking(0);
                     break;
                       // other = false;
                 }
                   //Towardstart = false;
             }
            if (hit.collider.gameObject.tag != "Node" && !isChasing)
            {
                check = false;
                other = false;
                Towardstart = true;
            }
         }

         bool nall = (hit.collider.gameObject.tag != "Node" && !isChasing);
         if(nall == null)
         {

         }
         if (!check)
         {
             if (Towardstart)
             {
                    gb.transform.position = Vector3.MoveTowards(gb.transform.position, startingNode.transform.position, speed * Time.deltaTime);       
             }
                    //Towardstart = check;
             if (distStart < 0.5f)
             {
                    Towardstart = false;
                    //ContinueWalking(0);
             }
         }
         if (!Towardstart && !check && !other)
         {
             while (pathIndex < pathlist.Count)
             {
                    ContinueWalking(0);
                    break;
             }
         }
         /*if(pathIndex < pathlist.Count)
         {
            gb.transform.position = Vector3.MoveTowards(gb.transform.position, targetNode.transform.position, speed * Time.deltaTime);
        }*/
        gb.transform.LookAt(Player.transform.position);
        RaycastHit PlayerRay;
        if (Physics.Raycast(gb.transform.position, Player.transform.position - gb.transform.position, out PlayerRay, 100))
        {
            Debug.DrawRay(gb.transform.position, PlayerRay.point - gb.transform.position, Color.white);
            if (PlayerRay.collider.gameObject.tag == "Player" && !pplayer.isVanished && !pplayer.isStunned)
            { 
                isChasing = true;
                Towardstart = false;
                gb.transform.position = Vector3.MoveTowards(gb.transform.position, cam.transform.position, speed * Time.deltaTime);
                // this  speed is when the player is seen
                speed = 18;
            }
            else  
            {
                isChasing = false;
                Towardstart = true;
                // this  speed is when the ghost is back to pathfinding
                speed = 20;
            }
        }
        RaycastHit startRay;
        if (Physics.Raycast(gb.transform.position, startingNode.transform.position - gb.transform.position, out startRay, 100))
        {
            Debug.DrawRay(gb.transform.position, startRay.point - gb.transform.position, Color.magenta);
        }

        Debug.Log(pathIndex);
        AstarTimer += Time.deltaTime;
        if (AstarTimer >= 1)
        {
            //pathIndex = 0;
            openlist.Clear();
            /*for (int i = 0; i < pathlist.Count; i++)
            {
             
            }*/
            pathlist.Clear();
            for (int i = 0; i < allNodes.Length; i++)
            {
                allNodes[i].parent = null;
                allNodes[i].isVisited = false;
            }
            FindStartingNode(allNodes);
            if (pplayer.isVanished)
            {
                targetNode = allNodes[UnityEngine.Random.Range(0, allNodes.Length)];
            }
            else
            {
                FindTargetNode(allNodes);
            }
            Findpath();
            pathlist.Add(targetNode);
            pathIndex = 0;
            AstarTimer = 0;
        }
        // this is fixing if the pathindex is greater than the pathlist, which is giving ''index out of range'' error
        if(pathIndex == 1 && pathlist.Count == 1)
        {
            pathIndex = 0;
        }
    }
    void ContinueWalking(int index)
    {
        gb.transform.position = Vector3.MoveTowards(gb.transform.position, pathlist[pathIndex + index].transform.position, speed * Time.deltaTime);
        float dist2 = Vector3.Distance(gb.transform.position, pathlist[pathIndex + index].transform.position);
        if (dist <= 1f) // ohhhhhh pathindex is becoming more than the  path list
        {
            //path += 1;
        }
        if (dist2 <= 1f)
        {
            pathIndex += 1;
        }
    }
    void Findpath()
    {     
        openlist.Add(startingNode);
        startingNode.gCost = 0;
        while (openlist.Count > 0)
        {
            //current = startingNode;
            openlist.Sort();
            current = openlist[0];
            openlist.Remove(current);
            current.isVisited = true;

            if (current.transform.position == targetNode.transform.position) 
                break;

            for (int i = 0; i < current.neighbors.Count; i++)
            {
                if (current.neighbors[i].isVisited)
                {
                    continue;
                }
                float movecost = current.gCost + CalculateDistance(current.transform.position, current.neighbors[i].transform.position);
                if (movecost < current.neighbors[i].gCost || !openlist.Contains(current.neighbors[i]))
                {
                    current.neighbors[i].gCost = movecost;
                    current.neighbors[i].hCost = CalculateDistance(targetNode.transform.position, current.transform.position);
                    current.neighbors[i].parent = current;
                    if (!openlist.Contains(current.neighbors[i]))
                    {
                        openlist.Add(current.neighbors[i]);
                        
                    }
                }
            }
        }

        Node tracer = targetNode.parent;     
        if (tracer != null)
        {
            while (tracer != startingNode)
            {
                Node parents = tracer.parent;
                pathlist.Add(tracer);
                tracer = parents;
            }
        }
        pathlist.Reverse();
        //bool check = true;
        /*for (int i = 0; i < pathlist.Count; i++)
        {
            //Debug.Log(pathlist[i]);
        }*/
    }

    float CalculateDistance(Vector3 pointA, Vector3 pointB)
    {
        return Vector3.Distance(pointA, pointB);
    }

    private void OnDrawGizmos()
    {
        if (startingNode != null)
        {
            Gizmos.color = new Color(0, 1, 0, 1);
            Gizmos.DrawSphere(startingNode.transform.position, 5f);
        }

        if (targetNode != null)
        {
            Gizmos.color = new Color(1, 1, 0, 1);
           Gizmos.DrawSphere(targetNode.transform.position, 5f);
        }

        for (int i = 0; i < pathlist.Count; i++)
        {
            Gizmos.color = new Color(1, 1, 1, 4);
            Gizmos.DrawSphere(pathlist[i].transform.position, 5f);
        }
    }
}