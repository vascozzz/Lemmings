using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeftController : MonoBehaviour {

	private Text text;
	
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Left: " + GameController.lemmingsLeft;
	}
}
