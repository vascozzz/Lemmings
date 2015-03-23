using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	void OnTriggerEnter(Collider other) {
		// should look for 'lemming' tag instead, need to update prefabs
		if (other.tag != "Terrain" && other.tag != "Hover") {
			Destroy(other.gameObject);
		}
	}
}
