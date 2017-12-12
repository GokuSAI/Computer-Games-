using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisionTracking : MonoBehaviour
{
	private GameObject player;
	private Vector3 target;
    [Range(0f, 180f)] public float viewAngle = 110f;
    [Range(0f, 100f)] public float viewRadius;
    public Color colorPick;
    public LayerMask obstacleLayer;
    private SphereCollider col;
    private NavMeshAgent agent;
    public GameManager gameManager;
    public EnemyController control;


    // Use this for initialization
    void Start ()
    {
        control = GetComponent<EnemyController>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		player = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        col.radius = viewRadius / 2;
    }
	

    // Check to see if player is in vision
    void OnTriggerStay (Collider col)
    {
        if (col.CompareTag("Player"))
        {
            target = (col.transform.position - transform.position);

            if (Vector3.Angle(transform.forward, target) < viewAngle / 2)
            {
                // Check to see if player is behind an object like a wall
                if (!Physics.Raycast(transform.position + transform.up, target.normalized, viewRadius, obstacleLayer))
                {
                    Debug.Log("Spotted Player");
                    Debug.DrawRay(transform.position + transform.up, target.normalized * viewRadius, Color.blue);
                    control.playerSpotted = true;
                }
            }
        }
    }

}
