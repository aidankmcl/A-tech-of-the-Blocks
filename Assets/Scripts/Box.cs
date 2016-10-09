using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public Vector3 vectorIndex;
	public bool available;
	public GameObject parentPlatform;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay () {
		GetComponent<MeshRenderer>().enabled = true;
		if (!parentPlatform.GetComponent<Platform>().activeSnapBoxes.Contains(transform.gameObject)) {
			parentPlatform.GetComponent<Platform>().activeSnapBoxes.Add(transform.gameObject);
		}
	}

	void OnTriggerExit () {
		parentPlatform.GetComponent<Platform>().activeSnapBoxes.Remove(transform.gameObject);
		transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
	}

	void Snapped () {
		available = false;
	}
}
