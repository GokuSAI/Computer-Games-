using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public int health;
    private NavMeshAgent agent = null;
    [SerializeField] private float distance; // From player
    //[SerializeField] private float speed;
    private GameObject player;
    //private Rigidbody rb;
    private Animator anim;
    //private CharacterController control;
    public GameObject gunBarrel;
    public Rigidbody bullet;
    public Transform mag;
    public float bulletSpeed;
    private Vector3 target;
    public Transform[] points;
    private int destPoint = 0;
    public bool isMoving;
    public bool playerSpotted;
    public bool alarmOn;
    public enum AISTATE {Idle = 0, Patrol = 1, Chase = 2, Attack = 3}; // States guards can be in
    public AISTATE currentState = AISTATE.Idle; // Current active state

	// Use this for initialization
	void Start ()
    {
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //control = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        destPoint = Random.Range(0, points.Length); // Pick random waypoint in points array
        //NextPoint();
        ChangeState(currentState);
    }

    //State when guard reaches a waypoint
    public IEnumerator Idle()
    {
        // Set wait timer
        float waitTime = 3f;
        float timePassed = 0f;
        // Set anim condition trigger
        anim.SetBool("moving", false);

        // While in idle wait for 2 secs before moving again
        while(currentState == AISTATE.Idle)
        {
            timePassed += Time.deltaTime;

            if(timePassed >= waitTime)
            {
                ChangeState(AISTATE.Patrol);
                yield break;
            }

            //Check to see if player was seen or alarm on
            if (playerSpotted || alarmOn)
            {
                ChangeState(AISTATE.Chase);
                yield break;
            }

            yield return null;
        }
    }

    //State when guard is going to a waypoint
    public IEnumerator Patrol()
    {
        NextPoint();
        anim.SetBool("moving", true);
        while (currentState == AISTATE.Patrol)
        {
            //Check to see if made it to waypoint
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                ChangeState(AISTATE.Idle);
                yield break;
            }

            //Check to see if player was seen or alarm on
            if (playerSpotted || alarmOn)
            {
                ChangeState(AISTATE.Chase);
                yield break;
            }

            yield return null;
        }
    }

    //State when guard is going after player
    public IEnumerator Chase()
    {
        while(currentState == AISTATE.Chase)
        {
            //Goto player
            agent.SetDestination(player.transform.position);
            //Set stopping distance
            agent.stoppingDistance = 10f;
            // Look at player
            target = player.transform.position;
            target.y = 2; // Height of the guard, keeps him from looking at the floor or sky
            transform.LookAt(target); 

            // Set player spotted to false if distance > 12f
            if (distance > 12f)
            {
                playerSpotted = false;
                agent.stoppingDistance = 0f;
            }

            //If in range switch to attack
            if (distance <= 10f)
            {
                ChangeState(AISTATE.Attack);
                yield break;
            }

            //If lost sight of player and alarm is not on
            if(!playerSpotted && !alarmOn)
            {
                yield return new WaitForSeconds(3f);

                if(!playerSpotted && !alarmOn)
                {
                    ChangeState(AISTATE.Patrol);
                    yield break;
                }
            }

            yield return null;
        }
    }

    public IEnumerator Attack()
    {
        while(currentState == AISTATE.Attack)
        {
            // Look at player
            target = player.transform.position;
            target.y = 2; // Height of the guard, keeps him from looking at the floor or sky
            transform.LookAt(target);

            // Shoot player
            anim.SetBool("inRange", true);
            //Shoot();

            // Check distance again, if not in range chase
            if (!(distance < 10f))
            {
                ChangeState(AISTATE.Chase);
                anim.SetBool("inRange", false);
                yield break;
            }

            yield return null;
        }
    }

    public void ChangeState(AISTATE newState)
    {
        StopAllCoroutines();
        currentState = newState;

        switch(newState)
        {
            case AISTATE.Idle:
                StartCoroutine(Idle());
            break;

            case AISTATE.Patrol:
                StartCoroutine(Patrol());
            break;

            case AISTATE.Chase:
                StartCoroutine(Chase());
            break;

            case AISTATE.Attack:
                StartCoroutine(Attack());
            break;
        }
    }

    // Go to next waypoint in points array
    void NextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }
	
    // Called from other scripts, like BulletDamageController
    public void GotShot (int damage)
    {
        health -= damage;
        ChangeState(AISTATE.Chase);
        anim.SetBool("moving", true);
        if (health <= 0)
        {
            // Death animation and sound here?
            //Spawn an ammo pickup
            Instantiate(mag, gunBarrel.transform.position, gunBarrel.transform.rotation);
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        Rigidbody clone;
        clone = Instantiate(bullet, gunBarrel.transform.position, gunBarrel.transform.rotation);
        clone.AddForce(gunBarrel.transform.forward * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //Calc distance to player
        distance = Vector3.Distance(transform.position, player.transform.position);
        //// Travel to next waypoint
        //if (!agent.pathPending && agent.remainingDistance < 0.5f)
        //{
        //    NextPoint();
        //}
    }

    // For moving object with rigidbody
    //void FixedUpdate ()
    //{
    //    // Use this for when player gets seen
    //    if (distance <= reactionRange)
    //    {
    //        target = player.transform.position;
    //        target.y = 2; // Height of the guard, keeps him from looking at the floor or sky
    //        transform.LookAt(target); // Look at player
    //        //rb.velocity = (transform.forward * speed); // Moves the enemy
    //        agent.isStopped = false;
    //        if (distance > 5)
    //        {
    //            agent.SetDestination(player.transform.position);
    //            if (agent.hasPath)
    //            {
    //                //Set moving bool for animator to true
    //            }
    //        }
    //        else
    //        {
    //            agent.isStopped = true;
    //        }
    //    }

    //}
}
