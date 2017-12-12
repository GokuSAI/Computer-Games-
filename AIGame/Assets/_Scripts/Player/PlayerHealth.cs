using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour {

    public int health;
    public Text healthText;
    public GameManager gameManager;
    public FirstPersonController playerControl;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        playerControl = GetComponent<FirstPersonController>();
        SetHealthText();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SetHealthText();
    }

    public void GotShot(int damage)
    {
        health -= damage;

        // Display the gameover display
        if (health <= 0)
        {
            // player death sound here?
            gameManager.GameOver();
            playerControl.m_MouseLook.lockCursor = false;
        }
    }
   
    // Update health UI text
    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }
}
