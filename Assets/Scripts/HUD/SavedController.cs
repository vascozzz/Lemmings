using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SavedController : MonoBehaviour {

	private Text text;

	void Start () {
		text = GetComponent<Text> ();
	}

	void Update () {
		text.text = "Saved: " + GameController.lemmingsSaved;

		// change color
		if (GameController.lemmingsSaved >= GameController.lemmingsGoal)
			text.color = Color.green;
		else
			text.color = Color.red;
	}
}
