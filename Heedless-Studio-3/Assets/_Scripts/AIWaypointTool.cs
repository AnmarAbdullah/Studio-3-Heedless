using UnityEditor;
using UnityEngine;

public class AIWaypointTool : EditorWindow
{
    Node Waypoint;
    Node[] neighbors;

    string name;
    int ObjectId;
    float SpawnRadius;
    int one = 1;
    bool addedFront;
    bool addedBack;
    bool addedRight;
    bool addedLeft;

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
            Debug.DrawRay(Waypoint.transform.position, hit.point - Waypoint.transform.position, Color.red);
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
            Debug.DrawRay(Waypoint.transform.position, hit.point - Waypoint.transform.position, Color.yellow);
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
            Debug.DrawRay(Waypoint.transform.position, hit.point + Waypoint.transform.position, Color.yellow);
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
            Debug.DrawRay(Waypoint.transform.position, hit.point - Waypoint.transform.position, Color.yellow);
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
        if (neighbors == null)
        {
            neighbors = new Node[0];
            neighbors[0] = Waypoint;
            for (int i = 0; i < neighbors.Length; i++)
            {
                neighbors[i] = EditorGUILayout.ObjectField("Neighbors", neighbors[i], typeof(Node), false) as Node;
            }
        }
  
        ObjectId = EditorGUILayout.IntField("WayPointId", ObjectId);
        name = EditorGUILayout.TextField("ObjectName", name);
        SpawnRadius = EditorGUILayout.FloatField("Spawn Location", SpawnRadius);
        if (GUILayout.Button("Connect"))
        {
            SpawnWaypoint();
        }
    }



    void SpawnWaypoint()
    {
        addedRight = false;
        addedLeft = false;
        addedFront = false;
        addedBack = false;
        if (Waypoint == null)
        {
            Debug.LogError("Add Waypoints");
        }
        if (neighbors == null)
        {
            Debug.LogError("Add Neighbors");
            return;
        }

        /*Vector2 spawnCircle = Random.i * SpawnRadius;
        Vector3 spawn = Selection.activeTransform.position;
        Vector3 spawnPos = new Vector3(spawnCircle.x, 0f, spawnCircle.y);
        GameObject newObject = Instantiate(Waypoint, spawn, Quaternion.identity);*/

        //newObject.name = "Waypoint" + ObjectId;

        //ObjectId++;
        //newObject.AddComponent<Waypoints>();
    }
}
