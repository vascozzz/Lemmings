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

	public void NextLevel() {
		if(Application.loadedLevelName != "Level3") // if not level 3 load next
			Application.LoadLevel (Application.loadedLevel + 1);
		else // load main menu
			Application.LoadLevel ("MainMenu");
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

	public void GameOver() {
		animtr = GetComponent<Animator>();

		// set information
		bool win = GameController.lemmingsSaved >= GameController.lemmingsGoal;

		if (win) {
			resultText.text = "Great! You won!";
			scoreText.text = "Score: " + ((double) GameController.lemmingsSaved / (double) GameController.lemmingsTotal) * 100 + "%";
			if(Application.loadedLevelName == "Level3")
				next.gameObject.SetActive(false);
		} else {
			resultText.text = "You have failed!";
			scoreText.gameObject.SetActive(false);
			next.gameObject.SetActive(false);
		}

		// set trigger
		animtr.SetTrigger ("GameEnd");
	}
}
