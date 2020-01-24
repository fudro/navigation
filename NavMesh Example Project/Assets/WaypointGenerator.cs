using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointGenerator : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject waypoint;
    private bool waypointSet = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.path.corners.Length > 1 && waypointSet == false)
        {
            
            foreach (Vector3 point in agent.path.corners)
            {
                Instantiate(waypoint, point, Quaternion.identity);
            }
            waypointSet = true;
            print("Waypoints set!");
        }
    }
}
