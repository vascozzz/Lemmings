using UnityEngine;
using System.Collections;

public class TerrainController : MonoBehaviour 
{
	public int numTilesX;
	public int numTilesZ;
	public GameObject hoverPointer;
	public float placementCooldown;

	private GameController gameController;
	private float nextPlacement;
	private bool canPlace;
	private float tileX;
	private float tileZ;

	void Start() {
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();

		tileX = transform.localScale.x / numTilesX;
		tileZ = transform.localScale.y / numTilesZ; // y, not z, because the terrain is a quad (operates in x,y only)

		hoverPointer = Instantiate(hoverPointer, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		hoverPointer.transform.localScale = new Vector3(tileX, 1, tileZ);
		canPlace = false;
	}

	void Update() {
		UpdateHover();
		PlaceLemming();
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

	void PlaceLemming() {
		if(Input.GetButton("Fire1") && canPlace && Time.time > nextPlacement) {
			gameController.SpawnLemming(hoverPointer.transform.position);
			nextPlacement = Time.time + placementCooldown;
		}
	}
}
