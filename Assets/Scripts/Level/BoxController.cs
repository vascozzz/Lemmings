using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour 
{
	// public
	public float speed;
	public GameObject[] obstacleTypes;

	// private
	private Vector3 target;
	private Vector3 targetVector;
	private bool reachedTarget;

	void Start() {
		reachedTarget = false;
	}

	void FixedUpdate() {
		if (!reachedTarget) {
			GetComponent<Rigidbody>().velocity = targetVector * speed;
		} 
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Terrain") {
			GameObject newType = obstacleTypes[Random.Range(0, obstacleTypes.Length)];
			GameObject newObstacle = Instantiate(newType) as GameObject;

			newObstacle.transform.position = new Vector3(target.x, 0.0f, target.z); // once we're at the correct position, we should get rid of the y offset
			newObstacle.GetComponent<ObstacleController>().SetTarget(target);

			Destroy(this.gameObject);
		}
	}

	public void SetTarget(Vector3 target, Vector3 targetVector) {
		this.target = target;
		this.targetVector = targetVector;
	}
}
