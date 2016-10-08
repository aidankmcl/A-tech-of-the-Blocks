using UnityEngine;
using System.Collections;

public class SnapCollide : MonoBehaviour {



	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	// void OnTriggerEnter () {
	// 	GetComponent<MeshRenderer>().enabled = true;
	// }
	//
	void OnTriggerExit () {
		GetComponent<MeshRenderer>().enabled = false;
	}
	void OnTriggerStay() {
		GetComponent<MeshRenderer>().enabled = true;
	}
}
