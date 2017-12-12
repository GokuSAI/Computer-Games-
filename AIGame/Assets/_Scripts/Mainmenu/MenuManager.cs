using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Canvas quitMenu;
	public Button startGame;
//	public Button help;
	public Button exit;

	public string loadLevel;

	void Start() {
		quitMenu = quitMenu.GetComponent<Canvas> (); //get the canvas component
		startGame = startGame.GetComponent<Button> ();
//		help = help.GetComponent<Button> ();
		exit = exit.GetComponent<Button> ();

		quitMenu.enabled = false;
	}

	public void onExitClicked() {
		quitMenu.enabled = true;
		startGame.enabled = false;
		exit.enabled = false;
//		help.enabled = false;
	}

	public void onNoClicked() {
		quitMenu.enabled = false;
		startGame.enabled = true;
		exit.enabled = true;
//		help.enabled = true;

	}

	public void startLevel() {
		SceneManager.LoadScene (loadLevel);
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
