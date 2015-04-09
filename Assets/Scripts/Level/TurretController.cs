using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {
	
	public GameObject CannonObj; // turret cannon gameobject
	public GameObject barrelObj; // turret barrel gameobject
	public GameObject terrainObj; // terrain gameobject

	public float cooldown; // time before calculating a new target
	public int force; // force at which to shoot
	public float rotationSpeed; // speed at which to rotate turret

	private Transform barrel; // turret barrel transform
	private Transform cannon; // turret cannon transform (base of the turret)
	private TerrainController terrainComp; // terrain script

	private Vector3 target; // turret aim
	private float nextShoot; // time of the next shot (probably not necessary now?)

	public GameObject[] obstacles; // turret "bullets"

	void Start () {
		barrel = barrelObj.transform;
		cannon = CannonObj.transform;
		terrainComp = terrainObj.GetComponent<TerrainController>();
		
		GenerateTargetPosition();
		nextShoot = Time.time + cooldown;
	}

	void Update () {
		Quaternion targetRotation = Quaternion.LookRotation(target - cannon.position);
		Quaternion currentRotation = cannon.rotation;

		if (targetRotation == currentRotation && Time.time > nextShoot) {
			Shoot ();
			GenerateTargetPosition();
		}
		else {
			cannon.rotation = Quaternion.RotateTowards(cannon.rotation, targetRotation, rotationSpeed);		
		} 
	}
	
	private void GenerateTargetPosition() {
		target = terrainComp.GetRandomPosition();
	}
	
	private void Shoot() {
		nextShoot = Time.time + cooldown;
		
		GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];
		
		obstacle = Instantiate (obstacle) as GameObject;
		obstacle.transform.position = barrel.transform.position;

		Vector3 obstacleTarget = (target - obstacle.transform.position).normalized;
		obstacle.GetComponent<ObstacleController>().SetTarget(target, obstacleTarget);
	}
}
