using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour       //Control a player object with point and click. Click to set the player destination.
{
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject waypointGenerator;
    public GameObject waypoint;
    private int waypointCount;
    private bool cornersListed = false;
    private bool waypointSet = false;

    void Start()
    {
        cam = Camera.main;      //assign camera used for point and click

        //Crate waypoint parent object
        waypointGenerator = GameObject.Find("WaypointGenerator");
        if (!waypointGenerator)
        {
            waypointGenerator = new GameObject("WaypointGenerator");
        }
        waypointCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //reset flags
                cornersListed = false;
                waypointSet = false;

                //clear waypoint visuals
                /*
                print("cornerCount: " + agent.path.corners.Length);
                for (int i = 0; i < waypointCount; i++)
                {
                    if (GameObject.Find("Waypoint(Clone)") != null)
                    {
                        print("waypoint destroyed " + i);
                        Destroy(GameObject.Find("Waypoint(Clone)"));
                    }
                }
                */
                GameObject[] killEmAll;
                killEmAll = GameObject.FindGameObjectsWithTag("Waypoint");
                for (int i = 0; i < killEmAll.Length; i++)
                {
                    Destroy(killEmAll[i].gameObject);
                }

                //set new destination
                print("Going to point: " + hit.point);
                agent.SetDestination(hit.point);
            }
        }
        /*
         * //List the point coordinates
        if (agent.path.corners.Length > 1 && cornersListed == false)
        {
            print("Path set!");
            print("Corners: " + agent.path.corners.Length);
            foreach (Vector3 point in agent.path.corners)
            {
                print(point);
            }
            cornersListed = true;
        }
        */
        //create waypoint objects
        if (agent.path.corners.Length > 1 && waypointSet == false)
        {
            waypointCount = agent.path.corners.Length;
            foreach (Vector3 point in agent.path.corners)
            {
                Instantiate(waypoint, point, Quaternion.identity);
                waypoint.gameObject.tag = "Waypoint";
            }
            waypointSet = true;
            print("Waypoints set!");
            print("Waypoint Count: " + waypointCount);
        }
    }
}
