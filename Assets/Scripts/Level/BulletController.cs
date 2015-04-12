using UnityEngine;
using System.Collections;

public class BulletController : ObstacleController 
{
	public float speed;
	private Rigidbody rigidBody;

	void Start() {
		rigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		Vector3 velocity = rigidBody.velocity;
		velocity.x = speed;
		rigidBody.velocity = velocity;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Shield") {
			Destroy(other.gameObject);

			Dispose();
			Destroy(this.gameObject);
		}
		else if (other.tag == "Lemming") {
			other.gameObject.GetComponent<LemmingController>().Kill();
			
			Dispose();
			Destroy(this.gameObject);
		}
	}
}