using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretController : MonoBehaviour 
{
	// static
	static List<Vector3> filledTiles;

	// public
	public GameObject CannonObj; // turret cannon gameobject
	public GameObject barrelObj; // turret barrel gameobject
	public GameObject terrainObj; // terrain gameobject

	public float cooldown; // time before calculating a new target
	public int force; // force at which to shoot
	public float rotationSpeed; // speed at which to rotate turret

	public GameObject box; // turret "bullets"

	// private
	private Transform barrel; // turret barrel transform
	private Transform cannon; // turret cannon transform (base of the turret)
	private TerrainController terrainComp; // terrain script

	private Vector3 target; // turret aim
	private float nextShoot; // time of the next shot

	void Start() {
		barrel = barrelObj.transform;
		cannon = CannonObj.transform;
		terrainComp = terrainObj.GetComponent<TerrainController>();
		filledTiles = new List<Vector3>();

		StartCoroutine("Play");
	}

	IEnumerator Play()
	{
		target = terrainComp.GetRandomPosition();
		filledTiles.Add(target);
		nextShoot = Time.time + cooldown;

		while (true) {
			Quaternion targetRotation = Quaternion.LookRotation(target - cannon.position);
			Quaternion currentRotation = cannon.rotation;
			
			if (targetRotation == currentRotation && Time.time > nextShoot) {
				Shoot();

				//Check if already filled
				while(filledTiles.Contains(target = terrainComp.GetRandomPosition()))
					yield return null;

				filledTiles.Add(target);

			}
			else {
				cannon.rotation = Quaternion.RotateTowards(cannon.rotation, targetRotation, rotationSpeed);		
			} 

			yield return null;
		}
	}

	void Shoot() {
		nextShoot = Time.time + cooldown;
		
		GameObject newBox = Instantiate (box) as GameObject;
		newBox.transform.position = barrel.transform.position;

		Vector3 obstacleTarget = (target - newBox.transform.position).normalized;
		newBox.GetComponent<BoxController>().SetTarget(target, obstacleTarget);
	}

	public static void RemoveFilledTile(Vector3 tile) {
		filledTiles.Remove(tile);
	}
}
