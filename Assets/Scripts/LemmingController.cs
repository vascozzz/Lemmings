using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class LemmingController : MonoBehaviour
{
	public float speed;

	private ToggleGroup actionBar;
	private Rigidbody rigidBody;

	void Start () {
		actionBar = GameObject.Find("ActionBar").GetComponent<ToggleGroup>();
		rigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		Vector3 velocity = rigidBody.velocity;
		velocity.z = speed;
		rigidBody.velocity = velocity;
	}

	void OnMouseDown() {
		Toggle active = actionBar.ActiveToggles().FirstOrDefault();

		switch (active.name) {
			case "Action1":
				action1();
				break;

			case "Action2":
				action2();
				break;
		}
	}

	private void action1() {
		GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
	}

	private void action2() {
		transform.localScale = new Vector3(2, 2, 2);
	}
}