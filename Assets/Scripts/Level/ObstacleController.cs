using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public float speed;

	private Vector3 target;
	private Vector3 targetVector;
	private bool reachedTarget;

	public void Start() {
		reachedTarget = false;
	}

	public void FixedUpdate() {
		if (!reachedTarget) {
			GetComponent<Rigidbody>().velocity = targetVector * speed;
		} 
		else {
			GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
		}
	}

	public void SetTarget(Vector3 target, Vector3 targetVector) {
		this.target = target;
		this.targetVector = targetVector;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Terrain") {
			reachedTarget = true;
			transform.position = target;
		}
	}
}
