using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour 
{
	// public
	public float speed;
	public GameObject[] obstacleTypes;

	public int trampolineChance;
	public int vidalChance;
	public int bulletChance;
	public int holeChance;

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
			int newType = 0;
			int chance = Random.Range(0, 100);

			if(chance < trampolineChance)
				newType = 0;
			else if(chance < vidalChance)
				newType = 1;
			else if(chance < bulletChance)
				newType = 2;
			else if(chance < holeChance)
				newType = 3;

			GameObject newObstacle = Instantiate(obstacleTypes[newType]) as GameObject;

			newObstacle.transform.position = new Vector3(target.x, 0.0f, target.z); // once we're at the correct position, we should get rid of the y offset
			newObstacle.GetComponent<ObstacleController>().SetTarget(target);
			if(newObstacle.name == "Bullet(Clone)")
				newObstacle.transform.position = new Vector3(newObstacle.transform.position.x,
				                                             newObstacle.transform.position.y + 0.5f,
				                                             newObstacle.transform.position.z);

			Destroy(this.gameObject);
		}
	}

	public void SetTarget(Vector3 target, Vector3 targetVector) {
		this.target = target;
		this.targetVector = targetVector;
	}
}
