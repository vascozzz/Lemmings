using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour 
{
	private Vector3 target; // position in terrain is kept so we can tell the turret there's a new empty tile once this object is destroyed

	public void SetTarget(Vector3 target) {
		this.target = target;
	}

	public void Dispose() {
		TurretController.RemoveFilledTile(target);
	}
}
