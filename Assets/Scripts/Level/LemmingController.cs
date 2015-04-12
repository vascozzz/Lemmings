using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class LemmingController : MonoBehaviour
{
	// static
	public static GameObject selectedLemming; // select lemming

	// public
	public float speed; // speed of the lemming
	public Color selectedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	public Color highlightedColor = new Color(0.117f, 1.0f, 0.0f, 1.0f);

	// private
	protected int spawnIndex; // index in spawn

	protected ToggleGroup actionBar;
	protected Rigidbody rigidBody; // rigid body component
	
	protected Material mat; // material
	protected Color rimColor; // default rim color

	protected bool running; // if it is running, it is on terrain
	protected bool selected; // if it is selected
	protected bool falling; // if it is falling (-y velocity)

	protected Animator animtr;

	// abilities
	public GameObject umbrella; // umbrella object spawned on ability use
	protected GameObject umbrellaObj; // instance of spawned umbrella so it can be destroyed at any time
	protected bool usedUmbrella; // if the umbrella ability has been used

	public GameObject shield;
	protected GameObject shieldObj;
	protected bool usedShield;

	public GameObject explosion;
	protected bool usedExplosion;

	protected virtual void Awake() {
		rigidBody = GetComponent<Rigidbody>();
		GameObject child = (GameObject) transform.GetChild(1).gameObject;
		mat = child.GetComponent<Renderer>().materials[0];
		rimColor = mat.GetColor("_RimColor");
		running = false;
		animtr = GetComponent<Animator>() as Animator;
	}

	protected virtual void Start() {

	}

	protected virtual void FixedUpdate() {
		if (running) {
			Vector3 velocity = rigidBody.velocity;
			velocity.x = -speed;
			rigidBody.velocity = velocity;
		}

		// should reach at least this velocity to be considered falling (excludes small bumps, placing of lemmings on terrain)
		if (rigidBody.velocity.y < -5) { 
			falling = true;
		}
	}

	void OnMouseDown() {
		if (!running && !selected)
			Select();
		else if (!selected) {
			Invoke("Action" + ActionBarController.action, 0.0f);
		}
	}

	protected virtual void OnMouseEnter() {
		HighLight();
	}

	protected virtual void OnMouseExit() {
		if (!selected)
			RemoveHighLight();
	}

	protected virtual void OnCollisionEnter(Collision collision) {
		if (falling && !usedUmbrella) {
			Kill();
		}
		
		usedUmbrella = false;

		if (umbrellaObj != null) {
			Destroy(umbrellaObj);
		}
	}
	
	protected virtual void Action1() {
		// really shouldn't be using magic numbers, but lacking time right now
		// min z: -8, max z: 8, diff between lanes: 2

		if (rigidBody.velocity.y > 1 || rigidBody.velocity.y < -1) {
			return;
		}

		else if (transform.position.z > 8) {
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
		} 
		else if (transform.position.z < -8) {
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
		} 
		else {
			float change = Random.Range(0, 2) == 0 ? 2f : -2f;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + change);
		}
	}
	
	protected virtual void Action2() {
		if (!usedUmbrella) {
			usedUmbrella = true;
			umbrellaObj = Instantiate(umbrella) as GameObject;
			umbrellaObj.transform.parent = transform;
			umbrellaObj.transform.localPosition = new Vector3(0f, 0f, 0f);
		}
	}
	
	protected virtual void Action3() {
		if (!usedShield) {
			usedShield = true;
			shieldObj = Instantiate(shield) as GameObject;
			shieldObj.transform.parent = transform;
			shieldObj.transform.localPosition = new Vector3(0f, 0.7f, 0.51f);
		}
	}

	protected virtual void Action4() {
		if (!usedExplosion) {
			usedExplosion = true;
			GameObject explosionObj = Instantiate(explosion) as GameObject;
			explosionObj.transform.position = transform.position;
			Destroy(this.gameObject);
		}
	}

	protected virtual void Action5() {
		
	}
	
	public void StartRunning() {
		running = true;
		animtr.SetTrigger("Run");
	}

	public void Select() {
		selected = true;
		mat.SetColor("_RimColor", selectedColor);

		if(selectedLemming != null)
			selectedLemming.GetComponent<LemmingController>().Deselect();

		selectedLemming = gameObject;
	}
	
	public void Deselect() {
		selected = false;
		selectedLemming = null;
		mat.SetColor("_RimColor", rimColor);
	}

	public void HighLight() {
		if(!selected)
			mat.SetColor("_RimColor", highlightedColor);
	}

	public void RemoveHighLight() {
		if(selected)
			mat.SetColor("_RimColor", selectedColor);
		else
			mat.SetColor("_RimColor", rimColor);
	}

	public int GetSpawnIndex() {
		return spawnIndex;
	}

	public void SetSpawnIndex(int i) {
		spawnIndex = i;
	}

	public virtual void Kill() {
		running = false;
		animtr.SetTrigger("Death");
		GetComponent<AudioSource>().Play();

		if (umbrellaObj != null) {
			Destroy(umbrellaObj);
		}

		if (shieldObj != null) {
			Destroy(shieldObj);
		}

		Destroy(this.gameObject, 2);
	}

	public virtual void Jump(float jumpForce) {
		gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1.0f, 1.0f, 0.0f) * jumpForce);
	}

	public virtual void FallThrough() {
		GetComponent<Collider>().isTrigger = true;
		Kill();
	}
}