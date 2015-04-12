using UnityEngine;
using System.Collections;

public class TrampolineController : ObstacleController 
{
	public float jumpForce;
	private bool used;
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Lemming" && !used) {
			other.gameObject.GetComponent<LemmingController>().Jump(jumpForce);
			used = true;

			GetComponent<AudioSource>().Play();
			Dispose();
			Destroy(this.gameObject, 0.8f);
		}
	}
}
