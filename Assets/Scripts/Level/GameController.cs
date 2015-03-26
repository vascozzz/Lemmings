using UnityEngine;
using System.Collections;

public class GameController: MonoBehaviour {

	// static
	public static int lemmingsLeft; // lemmings left to spawn
	public static int lemmingsSaved; // lemmings that reached the goal
	public static int lemmingsGoal; // lemmings goal

	// public
	public int maxLemmings; // lemmings that will be spawned in the level
	public int maxOnSpawn; // max lemmings in spawn zone
	public int goal; // lemmings needed to save

	public float initialDelay; // initial delay for the first lemming to spawn
	public float timeBetweenSpawn; // time between each lemming spawn
	public float spawnStartPos; // initial z position of the first lemming
	public float spaceBetween; // space between lemmings on spawn

	public GameObject[] lemmingsPrefabs; // lemmings gameobjects
	public GameObject spawn; // spawn gameobject

	// private
	private Transform spawnTransform; // transform of spawn
	private int onSpawn; // lemmings that are on spawn zone
	private GameObject[] lemmingsOnSpawn;

	void Awake() {
		lemmingsLeft = maxLemmings;
		spawnTransform = spawn.transform;
		onSpawn = 0;
		lemmingsOnSpawn = new GameObject[maxOnSpawn];
		lemmingsGoal = goal;
	}

	void Start () {
		InvokeRepeating("SpawnLemming", initialDelay, timeBetweenSpawn);
	}

	public void SpawnLemming() {
		if(onSpawn < maxOnSpawn && lemmingsLeft > 0) {
			int index = GetFreePosition();

			GameObject prefab = lemmingsPrefabs[Random.Range(0, lemmingsPrefabs.Length)];
			GameObject lemming = Instantiate(prefab) as GameObject;

			lemming.transform.parent = spawnTransform;
			float lemmingZPos = spawnTransform.position.z + spawnStartPos - (spaceBetween * index);
			lemming.transform.position = new Vector3(spawnTransform.position.x,
			                                         spawnTransform.position.y,
			                                         lemmingZPos);

			lemmingsOnSpawn[index] = lemming;
			lemming.GetComponent<LemmingController> ().SetSpawnIndex(index);

			onSpawn++;
			lemmingsLeft--;
		}
	}

	private int GetFreePosition() {
		for(int i = 0; i < lemmingsOnSpawn.Length; i++) {
			if(lemmingsOnSpawn[i] == null)
				return i;
		}
		return -1;
	}
	
	public void RemoveFromSpawn(int index) {
		lemmingsOnSpawn [index] = null;
		onSpawn--;
	}
}
