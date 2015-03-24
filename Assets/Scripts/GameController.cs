using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] lemmings;

	void Start () {
		GameObject lemming = GetRandomLemming();
		Instantiate(lemming, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity);
	}

	public GameObject GetRandomLemming() {
		return lemmings[Random.Range(0, lemmings.Length)];
	}

	public void SpawnLemming(Vector3 position) {
		GameObject lemming = GetRandomLemming();
		Instantiate(lemming, position, Quaternion.identity);
	}
}
