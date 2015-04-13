using UnityEngine;
using System.Collections;

public class HellephantController : LemmingController 
{
	public int rageTime;
	public int rageSpeed;

	private Color rageColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
	private bool rage;
	private float rageStartTime;

	protected override void Awake()
	{
		base.Awake();
		rage = false;
	}

	protected void Update()
	{
		if (rage && Time.time - rageStartTime >= rageTime) {
			rage = false;
			RemoveHighLight();
		}
	}

	protected override void FixedUpdate()
	{
		if (rage) {
			Vector3 velocity = rigidBody.velocity;
			velocity.x = -rageSpeed;
			rigidBody.velocity = velocity;
		} else
			base.FixedUpdate();
	}

	protected override void Action3() {
		base.Action3();
		shieldObj.transform.localPosition = new Vector3(0f, 1.1f, 1.9f);
	}

	protected override void Action5() {
		usedUltimate = true;

		rage = true;
		rageStartTime = Time.time;
		mat.SetColor("_RimColor", rageColor);
	}

	protected override void OnCollisionEnter(Collision collision) {

		Collider other = collision.collider;

		if (rage && (other.gameObject.tag == "Vidal" || other.gameObject.tag == "Trampoline" || other.gameObject.tag == "Hole")) {
			other.gameObject.GetComponent<ObstacleController>().Dispose();
			Destroy(other.gameObject);
		}

		base.OnCollisionEnter(collision);
	}

	public override void Kill()
	{
		if (rage)
			return;

		base.Kill();
	}

	public override void Jump(float jumpForce)
	{
		if (rage)
			return;

		base.Jump(jumpForce);
	}

	public override void FallThrough() {
		if (rage)
			return;

		base.FallThrough();
	}

	protected override void OnMouseEnter() {
		if (rage)
			return;

		base.OnMouseEnter();
	}

	protected override void OnMouseExit() {
		if (rage)
			return;

		base.OnMouseExit();
	}
}
