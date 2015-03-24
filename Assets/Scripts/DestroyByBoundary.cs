using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Lemming") {
			Destroy(other.gameObject);
		}
	}
}
