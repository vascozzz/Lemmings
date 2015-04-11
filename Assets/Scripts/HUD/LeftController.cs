using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeftController : MonoBehaviour {

	private Text text;

	void Start () {
		text = GetComponent<Text> ();
	}

	void Update () {
		text.text = "Left: " + GameController.lemmingsLeft;
	}
}
