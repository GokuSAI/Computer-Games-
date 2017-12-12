using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour {
    public GameObject player;
    public GameObject gunBarrel;
    public Rigidbody bullet;
    public float speed;
	public AudioSource gunshot;
	public AudioSource emptygun;

	// Reference the player object in scene
	void Start ()
    {
        player = GameObject.FindWithTag("Player");
	}
	
    void Shoot ()
    {
        Rigidbody clone;
        clone = Instantiate(bullet, gunBarrel.transform.position, gunBarrel.transform.rotation);
        clone.AddForce(-gunBarrel.transform.up * speed); // My model's locals are not "normal" turn tool handels from global to local in editor to see
    }

	// If player clicks LMB shoot pistol, decrease pistol ammo
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            if(player.GetComponent<Inventory>().pistolAmmo >= 1)
            {
				gunshot.Play ();
                Shoot();
                player.GetComponent<Inventory>().DecreasePistolAmmo();
            }
			emptygun.Play ();
            
        }
	}
}
