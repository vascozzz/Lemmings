using UnityEngine;
using System.Collections;

public class LemmingController : MonoBehaviour 
{
	public float speed;

	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
}
