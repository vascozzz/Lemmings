using UnityEngine;
using System.Collections;

public class DestroyByBackBoundary : MonoBehaviour 
{
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Lemming") {
			Destroy(other.gameObject);
		}
		else if(other.tag == "Bullet") {
			Destroy(other.gameObject);
		}
	}
}