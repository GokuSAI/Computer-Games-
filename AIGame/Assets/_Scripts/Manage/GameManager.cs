using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public bool haveKey = false;
    public bool alarm = false;
    public bool hitUnit = false;
    public GameObject gameOverDisplay;
    public GameObject keyDisplay;
    public int guardCount;
    private EnemyController[] guardHold;
    private Scene scene;
    //public UnityEvent onGameOver;

    // Use this for initialization
    void Start ()
    {
        guardHold = FindObjectsOfType<EnemyController>();
        guardCount = guardHold.Length;
        gameOverDisplay.SetActive(false);
        keyDisplay.SetActive(false);
        scene = SceneManager.GetActiveScene();

    }

    void Update()
    {
        //Update guard count
        guardHold = FindObjectsOfType<EnemyController>();
        guardCount = guardHold.Length;

        //For level one completion
        if (scene.name == "TestLevel" && guardCount <= 0)
        {
            SceneManager.LoadScene("Level 2");
        }

        //level two completion player must hit e on KeyUnit
        if (scene.name == "Level 2" && hitUnit)
        {
            SceneManager.LoadScene("Level 3");
        }

        //For level three completion and end of game
        if (scene.name == "Level 3" && (guardCount <= 0 || gameOverDisplay.activeInHierarchy))
        {   
            if (Input.anyKey)
             Application.Quit();
            //Ending();
        }
    }
	
    //public IEnumerator Ending()
    //{
    //    // Set wait timer
    //    float waitTime = 3f;
    //    float timePassed = 0f;
    //    Debug.Log("Made it");
    //    if (timePassed >= waitTime)
    //    {
    //        Application.Quit();
    //        yield break;
    //    }
    //}

    public void AddKey()
    {
        haveKey = true;
        keyDisplay.SetActive(true);
    }

	public void GameOver()
    {
        gameOverDisplay.SetActive(true);
        // Unlock mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //onGameOver.Invoke();
    }

    public void SetUnit()
    {
        hitUnit = true;
    }

    public void Restart()
    {
        Debug.Log("Button Clicked");
        int s = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(s);
    }

    public void Alarm()
    {
        // Placeholder for alarm
        alarm = true;
    }
}
