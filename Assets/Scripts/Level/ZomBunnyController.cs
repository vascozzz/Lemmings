using UnityEngine;
using System.Collections;

public class ZomBunnyController : LemmingController 
{
	private float timeUnderGround;
	private bool underGround;

	protected override void Start() {
		underGround = false;
	}

	protected override void FixedUpdate() {
		if (running) {
			Vector3 velocity = rigidBody.velocity;
			velocity.x = -speed;
			rigidBody.velocity = velocity;
		}

		if (underGround && timeUnderGround > 0f) {
			timeUnderGround -= Time.deltaTime;

			Vector3 pos = transform.position;
			if(timeUnderGround > 3f/2f)
				pos.y -= 0.1f;
			else
				pos.y += 0.1f;
			transform.position = pos;
		} else if(underGround) {
			underGround = false;
			GetComponent<Collider> ().enabled = true;
			Vector3 pos = transform.position;
			pos.y = -0.2f;
			transform.position = pos;
			rigidBody.useGravity = true;

			Vector3 v = rigidBody.velocity;
			v.y = 0f;
			rigidBody.velocity = v;
		}		

		// should reach at least this velocity to be considered falling (excludes small bumps, placing of lemmings on terrain)
		if (rigidBody.velocity.y < -5) { 
			falling = true;
		}
	}

	protected override void Action5() {
		usedUltimate = true;
		
		GetComponent<Collider> ().enabled = false;
		rigidBody.useGravity = false;
		underGround = true;
		timeUnderGround = 3f;
	}
}