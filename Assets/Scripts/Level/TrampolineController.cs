using UnityEngine;
using System.Collections;

public class TrampolineController : MonoBehaviour 
{
	public float jumpForce;

	private Vector3 target;

	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Lemming") {
			other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1.0f, 1.0f, 0.0f) * jumpForce);

			//TurretController.filledTiles.Remove(target);
			//Destroy(this.gameObject);
		}
	}

	public void SetTarget(Vector3 target) {
		this.target = target;
	}
}
