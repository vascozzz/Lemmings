using UnityEngine;
using System.Collections;

public class TerrainController : MonoBehaviour 
{
	public GameObject gameControllerObj; // game controller gameObject
	public GameObject lane; // lane highlight gameObject

	public int numTilesX; // length
	public int numTilesY; // number of lanes

	private GameController gameController; // game controller gameObject
	private float tileX;
	private float tileY;
	
	void Awake() {
		gameController = gameControllerObj.GetComponent<GameController>();
		
		tileX = transform.localScale.x / numTilesX;
		tileY = transform.localScale.y / numTilesY;
		
		lane = Instantiate(lane, new Vector3(0.0f, 0.1f, 0.0f), Quaternion.identity) as GameObject;
		lane.transform.parent = transform;
	}
	
	void Update() {
		PerformRayCast();
	}
	
	void PerformRayCast() {
		GameObject lemming = LemmingController.selectedLemming;
		if (lemming != null) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			// launch raycast, check if terrain is hit
			if(Physics.Raycast (ray, out hit, Mathf.Infinity)) {

				if (hit.collider.tag == "Terrain") {
					float posY = hit.point.z / tileY;
					posY = Mathf.Floor (posY);
					float worldY = posY * tileY + (tileY / 2) - 0.1f; // 0.1 to pixel perfect

					Vector3 pos = new Vector3 (0.0f, 0.1f, worldY);

					lane.transform.position = pos;
					lane.SetActive (true);

					PlaceLemming(lemming);
				} else if(hit.collider.tag == "Lemming"){
					lane.SetActive (false);
				} else {
					lane.SetActive (false);
					if(Input.GetButtonDown("Fire1"))
						lemming.GetComponent<LemmingController> ().Deselect();
				}
			} else {
				lane.SetActive (false);
			}
		}
	}
	
	void PlaceLemming(GameObject lemming) {
		if (Input.GetButtonDown("Fire1")) {
			LemmingController lController = lemming.GetComponent<LemmingController> ();

			gameController.RemoveFromSpawn (lController.GetSpawnIndex());

			lemming.transform.parent = transform;
			lemming.transform.position = new Vector3 (7.0f, 0.1f, lane.transform.position.z);

			lController.StartRunning ();
			lController.Deselect ();

			lane.SetActive (false);
		}
	}

	public Vector3 GetRandomPosition()
	{
		float rndTileX = Random.Range (0, numTilesX) - numTilesX/2f;
		float rndTileY = Random.Range (0, numTilesY) - numTilesY/2f;
		
		float rndX = (rndTileX * tileX) + (tileX/2f);
		float rndY = (rndTileY * tileY) + (tileY/2f);
		
		return new Vector3(rndX, 0.2f, rndY);
	}
}
