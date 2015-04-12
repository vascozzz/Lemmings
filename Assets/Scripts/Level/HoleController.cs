using UnityEngine;
using System.Collections;

public class HoleController : ObstacleController 
{
	void Start() {
		// since it is a plane, it needs a small offset in y
		transform.position = new Vector3(transform.position.x, 0.01f, transform.position.z);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Lemming") {
			other.gameObject.GetComponent<LemmingController>().FallThrough();
			
			Dispose();
			Destroy(this.gameObject);
		}
	}
}