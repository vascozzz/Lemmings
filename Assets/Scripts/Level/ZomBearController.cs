using UnityEngine;
using System.Collections;

public class ZomBearController : LemmingController 
{
	public GameObject cloneType;

	protected override void Action5() {
		// really shouldn't be using magic numbers, but lacking time right now
		// min z: -8, max z: 8, diff between lanes: 2

		if (transform.position.z < 8) {
			GameObject newBear1 = Instantiate(cloneType) as GameObject;
			newBear1.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
			newBear1.GetComponent<LemmingController>().StartRunning();
		}

		if (transform.position.z > -8) {
			GameObject newBear2 = Instantiate(cloneType) as GameObject;
			newBear2.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
			newBear2.GetComponent<LemmingController>().StartRunning();
		}
	}
}
