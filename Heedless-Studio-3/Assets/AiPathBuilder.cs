using UnityEditor;
using UnityEngine;
using System.Linq;

public class AiPathBuilder : EditorWindow
{
    Node Waypoint;
    GameObject objectToSpawn;
    bool isBuilding;
    int index;

    [MenuItem("OurTools / AI Waypoints Builder")]
    public static void Int()
    {
        GetWindow(typeof(AiPathBuilder));
    }
    RaycastHit hit;
    private void OnGUI()
    {

        Debug.Log(isBuilding);
        objectToSpawn = EditorGUILayout.ObjectField("Node", objectToSpawn, typeof(GameObject), false) as GameObject;

        if (isBuilding)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Build();
            }
        }

        if (GUILayout.Button("Start Building"))
        {
            BuildMode();
        }

        if (Event.current.type == EventType.MouseDown)
        {
            Build();
        }
    }

    void BuildMode()
    {
        isBuilding = true;
    }

    void Build()
    {
        /*Vector3 mousePos = Input.mousePosition;

        GameObject node = Instantiate(objectToSpawn, mousePos, Quaternion.identity);
        node.name = "Node " + index;
        index++;*/     
            Ray ray = Camera.current.ScreenPointToRay(Event.current.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                Debug.Log(Event.current.mousePosition);
                Vector3 newTilePosition = hit.point;
                Instantiate(objectToSpawn, newTilePosition, Quaternion.identity);
            }
        
    }
}
