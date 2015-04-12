using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Lemming") {
			Destroy(other.gameObject);
			GameController.lemmingsSaved++;
		}
		else if(other.tag == "Bullet") {
			Destroy(other.gameObject);
		}
	}
}