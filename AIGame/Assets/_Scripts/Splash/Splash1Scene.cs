using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash1Scene : MonoBehaviour {

	public Image splash;
	public string loadLevel;

	//reduce color alpha of image continuously
	IEnumerator Start() {
		splash.canvasRenderer.SetAlpha (0.0f); //completely invisible

		FadeIn ();
		yield return new WaitForSeconds (2.5f);
		FadeOut ();
		yield return new WaitForSeconds (2.5f);

		SceneManager.LoadScene (loadLevel);
	}

	void FadeIn(){
		splash.CrossFadeAlpha (1.0f, 1.5f, false); //changes the alpha from nothing to 100% in 1.5 seconds.
	}

	void FadeOut(){
		splash.CrossFadeAlpha (0.0f, 2.5f, false); //changes the alpha from nothing to 0% in 1.5 seconds.
	}

}
