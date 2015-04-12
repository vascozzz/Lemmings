using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour 
{
	void Start() {
		Destroy(this.gameObject, 1);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Vidal" || other.tag == "Bullet" || other.tag == "Trampoline") {
			other.gameObject.GetComponent<ObstacleController>().Dispose();
			Destroy(other.gameObject);
		} 
		else if (other.tag == "Lemming") {
			other.gameObject.GetComponent<LemmingController>().Kill();
		}
	}
}
