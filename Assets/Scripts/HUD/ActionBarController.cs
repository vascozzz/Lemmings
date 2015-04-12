using UnityEngine;
using UnityEngine.UI;

public class ActionBarController : MonoBehaviour {

	public static int action;

	void Awake() {
		action = 1;
	}

	void Update() {
		if (Input.anyKeyDown) {
			if (Input.GetKeyDown ("1")) {
				action = 1;
			} else if (Input.GetKeyDown ("2")) {
				action = 2;
			} else if (Input.GetKeyDown ("3")) {
				action = 3;
			} else if (Input.GetKeyDown ("4")) {
				action = 4;
			} else if (Input.GetKeyDown ("5")) {
				action = 5;
			}

			this.gameObject.transform.GetChild(action - 1).gameObject.GetComponent<Toggle>().isOn = true;
		}
	}

	public void ChangeAction(int a) {
		action = a;
	}
}
