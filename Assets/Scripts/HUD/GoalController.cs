using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalController : MonoBehaviour {

	private Text text;
	
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		text.text = "Goal: " + GameController.lemmingsGoal;
	}
}
