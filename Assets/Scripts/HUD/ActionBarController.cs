using UnityEngine;
using System.Collections;

public class ActionBarController : MonoBehaviour {

	public static int action;

	void Awake() {
		action = 1;
	}

	// Use this for initialization
	/*void Start () {
		
	}*/
	
	// Update is called once per frame
	/*void Update () {
	
	}*/

	public void ChangeAction(int a) {
		action = a;
	}
}
