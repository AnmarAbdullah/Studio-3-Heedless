using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random : MonoBehaviour
{
    List<int> nList;
    void Start()
    {
        nList = new List<int>();
        nList.Add(5);
        for (int i = 0; i < nList.Count; i++)
        {
            Debug.Log(nList[i]);
        }
        
    }
    int Addtwonumber(int a, int b)
    {
        return a + b;
    }

    public Random()
    {

    }

    void Update()
    {
        Destroy(gameObject);
    }
}
