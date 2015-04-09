using UnityEngine;
using System.Collections;

public class ActionBarController : MonoBehaviour {

	public static int action;

	void Awake() {
		action = 1;
	}

	public void ChangeAction(int a) {
		action = a;
	}
}
