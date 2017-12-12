using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AudioTracking : MonoBehaviour {
    private GameObject player;
    private Vector3 target;
    private NavMeshAgent agent = null;
    private SphereCollider col;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        col = GetComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<AudioSource>().isPlaying)
        {
            if (CalculatePathLength(player.transform.position) < col.radius)
            {
                agent.isStopped = false;
                target = player.transform.position;
                target.y = 2; // Height of the guard, keeps him from looking at the floor or sky
                transform.LookAt(target); // Look at player
                agent.SetDestination(player.transform.position);
                if (agent.hasPath)
                {
                    //Set moving bool for animator to true
                }
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }

    float CalculatePathLength(Vector3 targetPosition)
    {
        float pathLength = 0;
        NavMeshPath path = new NavMeshPath();
        if (agent.enabled)
            agent.CalculatePath(targetPosition, path);
        Vector3[] wayPoints = new Vector3[path.corners.Length + 2];

        wayPoints[0] = transform.position;
        wayPoints[wayPoints.Length - 1] = targetPosition;

        for(int i = 0; i < path.corners.Length; i++)
        {
            wayPoints[i + 1] = path.corners[i];
        }

        for(int i = 0; i < wayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(wayPoints[i], wayPoints[i + 1]);
        }
        return pathLength;
    }
}
