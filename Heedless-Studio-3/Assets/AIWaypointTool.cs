//using unity.editor
using UnityEngine;
using System.Linq;

public class AIWaypointTool //: EditorWindow
{
   /* Node Waypoint;
   // Node[] neighbors;

    string name;
    int ObjectId;
    float SpawnRadius;
    int one = 1;
    bool addedFront;
    bool addedBack;
    bool addedRight;
    bool addedLeft;
    bool added;

    [MenuItem("OurTools / AI Waypoints")]
    public static void Int()
    {
        GetWindow(typeof(AIWaypointTool));
    }
    RaycastHit hit;
    private void OnGUI()
    {
        Waypoint = EditorGUILayout.ObjectField("WayPoint", Waypoint, typeof(Node), true) as Node;
        //RaycastHit hit;
        if (Physics.Raycast(Waypoint.transform.position, Waypoint.transform.forward, out hit, 1000))
        {           
            //Debug.DrawRay(Waypoint.transform.position, hit.point - Waypoint.transform.position, Color.yellow);
            if (!addedFront)
            {
                //Debug.DrawRay(Waypoint.transform.position, hit.point - Waypoint.transform.position, Color.red);
                if (hit.collider.gameObject.tag == "Node")
                {
                    Waypoint.neighbors.Add(hit.collider.gameObject.GetComponent<Node>());
                    for (int i = 0; i < Waypoint.neighbors.Count; i++)
                    {
                        Waypoint.neighbors[i].neighbors.Add(Waypoint);
                        addedFront = true;
                    }
                }
            }
        }
        if (Physics.Raycast(Waypoint.transform.position, -Waypoint.transform.forward, out hit, 1000))
        {
            //Debug.DrawRay(Waypoint.transform.position, hit.point - Waypoint.transform.position, Color.yellow);
            if (!addedBack)
            {
                if (hit.collider.gameObject.tag == "Node")
                {
                    Waypoint.neighbors.Add(hit.collider.gameObject.GetComponent<Node>());
                    for (int i = 0; i < Waypoint.neighbors.Count; i++)
                    {
                        Waypoint.neighbors[i].neighbors.Add(Waypoint);
                        addedBack = true;
                    }
                }
            }
        }
        if (Physics.Raycast(Waypoint.transform.position, -Waypoint.transform.right, out hit, 1000))
        {
            //Debug.DrawRay(Waypoint.transform.position, hit.point + -Waypoint.transform.position, Color.yellow);
            if (!addedLeft)
            {
                if (hit.collider.gameObject.tag == "Node")
                {
                    Waypoint.neighbors.Add(hit.collider.gameObject.GetComponent<Node>());
                    for (int i = 0; i < Waypoint.neighbors.Count; i++)
                    {
                        Waypoint.neighbors[i].neighbors.Add(Waypoint);
                        addedLeft = true;
                    }
                }
            }
        }
        if (Physics.Raycast(Waypoint.transform.position, Waypoint.transform.right, out hit, 1000))
        {
            //Debug.DrawRay(Waypoint.transform.position, hit.point - Waypoint.transform.position, Color.yellow);
            if (!addedRight)
            {
                if (hit.collider.gameObject.tag == "Node")
                {
                    Waypoint.neighbors.Add(hit.collider.gameObject.GetComponent<Node>());
                    for (int i = 0; i < Waypoint.neighbors.Count; i++)
                    {
                        Waypoint.neighbors[i].neighbors.Add(Waypoint);
                        addedRight = true;
                    }
                }
            }
        }
        for (int i = 0; i < Waypoint.neighbors.Count; i++)
        {
            if(Waypoint.neighbors[i].neighbors == null)
            {
                Waypoint.neighbors[i].neighbors.Add(Waypoint);
            }
            Waypoint.neighbors[i].neighbors = Waypoint.neighbors[i].neighbors.Distinct().ToList();
            Waypoint.neighbors = Waypoint.neighbors.Distinct().ToList();
        }
        if (GUILayout.Button("Connect"))
        {
            SpawnWaypoint();
        }
    }*/



    /*void SpawnWaypoint()
    {
        addedRight = false;
        addedLeft = false;
        addedFront = false;
        addedBack = false;
        added = false;
        if (Waypoint == null)
        {
            Debug.LogError("Add Waypoints");
        }
    }*/
}
