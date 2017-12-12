using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardUnit : MonoBehaviour {

    public float distance;
    private GameObject player;
    public GameManager gameManager;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 2f && Input.GetKeyDown(KeyCode.E) && gameManager.haveKey)
        {
            gameManager.SetUnit();
        }
    }
}
