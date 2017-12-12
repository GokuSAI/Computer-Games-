using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watercooler : MonoBehaviour {

    public float distance;
    private GameObject player;
    private PlayerHealth pHealth;
    private int count;

    // Use this for initialization
    void Start ()
    {
        count = 3;
        player = GameObject.FindGameObjectWithTag("Player");
        pHealth = player.GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance <= 2f && Input.GetKeyDown(KeyCode.E))
        {
           if(pHealth.health < 100 && count > 0)
            {
                //drink sound
                int mHealth = 100 - pHealth.health;
                pHealth.health += mHealth;
                count--;
            }
        }
    }
}
