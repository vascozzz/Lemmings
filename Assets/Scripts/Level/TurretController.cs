using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

	public float cooldown;
	public int force;
	public int rangeX;
	public int rangeY;
	public GameObject[] obstacles;

	public GameObject CannonObj;
	public GameObject barrelObj;
	public GameObject terrainObj;

	private Transform barrel;
	private Transform cannon;
	private Transform terrain;

	private float nextShoot;

	private Vector3 target;

	// Use this for initialization
	void Start () {
		barrel = barrelObj.transform;
		cannon = CannonObj.transform;
		terrain = terrainObj.transform;

		nextShoot = Time.time + cooldown;

		ChoosePosition ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextShoot)
			Shoot ();
		else
			Rotate ();
	}

	private void ChoosePosition() {
		int x = Random.Range (0, rangeX);
		int y = Random.Range (0, rangeY);

		target = new Vector3 (terrain.position.x -x,
		                      	terrain.position.y,
		                      	terrain.position.z - y);
	}

	private void Shoot() {
		nextShoot = Time.time + cooldown;

		GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];

		obstacle = Instantiate (obstacle) as GameObject;
		obstacle.transform.position = barrel.transform.position;
		obstacle.GetComponent<Rigidbody> ().AddForce (cannon.transform.forward * force);

		ChoosePosition ();
	}

	private void Rotate() {
		//cannon.transform.Rotate(0, 2, 0);
		cannon.Rotate (0, 1, 0, Space.Self);

	}
}
