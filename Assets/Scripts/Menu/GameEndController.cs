using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameEndController : MonoBehaviour {

	public Text resultText;
	public Text scoreText;

	public Button next;
	public Button retry;

	public Button menu;
	public Button exit;

	private Animator animtr;

	void Awake() {
		animtr.GetComponent<Animator> ();

		gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if(GameController.lemmingsLeft <= 0 && GameObject.FindWithTag("Lemming") == null) {

		//}
	}

	public void NextLevel() {
		// Application.LoadLevel (Application.loadedLevelName);
	}

	public void Retry() {
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void MainMenu() {
		Application.LoadLevel("MainMenu");
	}

	public void Exit() {
		Application.Quit ();
	}
}
