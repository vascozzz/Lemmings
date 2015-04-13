using UnityEngine;
using UnityEngine.UI;

public class ActionBarController : MonoBehaviour {

	public static int action;

	void Awake() {
		action = 1;
	}

	void Update() {
		for (int i = 0; i < 5; i++) {
			if(LemmingController.abilitiesCooldown [i] < 1.0f)
				LemmingController.abilitiesCooldown [i] += Time.deltaTime;
			else
				LemmingController.abilitiesCooldown [i] = 1.0f;

			gameObject.transform.GetChild(5 + i).gameObject.GetComponent<Slider>().value = LemmingController.abilitiesCooldown [i];
		}

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
