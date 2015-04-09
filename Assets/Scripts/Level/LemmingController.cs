using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class LemmingController : MonoBehaviour
{
	public static GameObject selectedLemming; // select lemming
	
	public float speed; // speed of the lemming

	public Color selectedColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	public Color highlightedColor = new Color (0.117f, 1.0f, 0.0f, 1.0f);

	private int spawnIndex; // index in spawn

	private ToggleGroup actionBar;
	private Rigidbody rigidBody; // rigid body component
	
	private Material mat; // material
	private Color rimColor; // default rim color

	private bool running; // if it is running, it is on terrain
	private bool selected; // if it is selected

	private Animator animtr;

	void Start () {
		//actionBar = GameObject.Find("ActionBar").GetComponent<ToggleGroup>();
		rigidBody = GetComponent<Rigidbody>();
		GameObject child = (GameObject)transform.GetChild (1).gameObject;
		mat = child.GetComponent<Renderer> ().materials [0];
		rimColor = mat.GetColor ("_RimColor");
		running = false;
		animtr = GetComponent<Animator> () as Animator;
	}

	void Update()
	{

	}

	void FixedUpdate()
	{
		if (running) {
			Vector3 velocity = rigidBody.velocity;
			velocity.x = -speed;
			rigidBody.velocity = velocity;
		}
	}

	void OnMouseDown ()
	{
		if (!running && !selected)
			Select ();
		else if (!selected) {
			Invoke ("Action" + ActionBarController.action, 0.0f);
		}
	}

	void OnMouseEnter() {
		HighLight ();
	}

	void OnMouseExit() {
		if (!selected)
			RemoveHighLight ();
	}
	
	public void StartRunning() {
		running = true;
		animtr.SetTrigger ("Run");
	}

	public void Select() {
		selected = true;
		mat.SetColor ("_RimColor", selectedColor);
		if(selectedLemming != null)
			selectedLemming.GetComponent<LemmingController> ().Deselect();
		selectedLemming = gameObject;
	}
	
	public void Deselect() {
		selected = false;
		selectedLemming = null;
		mat.SetColor ("_RimColor", rimColor);
	}

	public void HighLight() {
		if(!selected)
			mat.SetColor ("_RimColor", highlightedColor);
	}

	public void RemoveHighLight() {
		if(selected)
			mat.SetColor ("_RimColor", selectedColor);
		else
			mat.SetColor ("_RimColor", rimColor);
	}

	private void Action1()
	{
		GetComponent<Rigidbody>().AddForce(new Vector3(-1.0f, 1.0f, 0.0f) * 500);
	}

	private void Action2()
	{
		transform.localScale = new Vector3(2, 2, 2);
	}

	private void Action3()
	{
		// transform.localScale = new Vector3(5, 5, 5);
	}

	public int GetSpawnIndex() {
		return spawnIndex;
	}

	public void SetSpawnIndex(int i) {
		spawnIndex = i;
	}
}