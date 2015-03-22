using UnityEngine;
using System.Collections;

public class TerrainController : MonoBehaviour 
{
	private GameController gameController;

	public int numTilesX;
	public int numTilesZ;
	private float tileX;
	private float tileZ;

	public float placementCooldown;
	private float nextPlacement;

	public GameObject hoverPointer;
	private bool canPlace;

	void Start() {
		GameObject gameControllerObject = GameObject.Find("Game Controller");
		
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		
		if (gameController == null) {
			Debug.Log("Cannot find 'Game Controller'.");
		}

		tileX = transform.localScale.x / numTilesX;
		tileZ = transform.localScale.y / numTilesZ; // y, not z, because the terrain is a quad (operates in x,y only)

		hoverPointer = Instantiate(hoverPointer, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		hoverPointer.transform.localScale = new Vector3(tileX, 1, tileZ);
		canPlace = false;
	}

	void Update() {
		UpdateHover();
		CreateLemming();
	}

	void UpdateHover() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		// launch raycast, check if terrain is hit
		if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "Terrain") {
			float posX = hit.point.x / tileX;
			posX = Mathf.Round(posX);
			float worldX = posX * tileX;
			
			float posZ = hit.point.z / tileZ;
			posZ = Mathf.Round(posZ);
			float worldZ = posZ * tileZ;
			
			Vector3 pos = new Vector3(worldX, 0.0f, worldZ);

			hoverPointer.transform.position = pos;
			hoverPointer.GetComponent<MeshRenderer>().enabled = true;
			canPlace = true;
		}
		else {
			hoverPointer.GetComponent<MeshRenderer>().enabled = false;
			canPlace = false;
		}
	}

	void CreateLemming() {
		if(Input.GetButton("Fire1") && canPlace && Time.time > nextPlacement) {
			GameObject lemming = gameController.GetRandomLemming();
			Instantiate(lemming, hoverPointer.transform.position, Quaternion.identity);
			nextPlacement = Time.time + placementCooldown;
		}
	}
}
