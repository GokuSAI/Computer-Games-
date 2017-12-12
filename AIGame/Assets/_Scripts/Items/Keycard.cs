using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour {

    public GameManager gameManager;
	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	
	void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            // play some pickup sound here
            gameManager.AddKey();
            Destroy(gameObject);
        }
    }
}
