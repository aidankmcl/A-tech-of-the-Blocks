using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckSnap : MonoBehaviour {

	public GameObject platform;
	public List<GameObject> potentialSnaps;
	private GameObject heldBlock;
	private Material tempMaterial;
	private float snapDistance;

	private bool noMatch = false;


	// Use this for initialization
	void Start () {
		snapDistance = platform.GetComponent<Platform> ().snapDistance;
		Debug.Log (snapDistance);
	}

	// Update is called once per frame
	void Update () {
		if (GetComponent<NewtonVR.NVRHand> ().HoldButtonPressed && GetComponent<NewtonVR.NVRHand> ().CurrentlyInteracting != null) {
			heldBlock = GetComponent<NewtonVR.NVRHand> ().CurrentlyInteracting.gameObject;
			heldBlock.GetComponent<Rigidbody> ().mass = 0f;
			foreach (Transform child in heldBlock.transform) {

				if (platform.GetComponent<Platform> ().activeSnapBoxes.Count >= heldBlock.transform.childCount) {
					foreach (GameObject activeCollider in platform.GetComponent<Platform>().activeSnapBoxes) {
						
						if (activeCollider.GetComponent<Box> ().available && Vector3.Distance (child.position, activeCollider.transform.position) < snapDistance) {
							platform.GetComponent<Platform> ().boxesToSnap.Add (activeCollider);
						}
					}

				}
			}
		} else {
			Debug.Log ("Please please please please please");
			if (platform.GetComponent<Platform> ().boxesToSnap.Count > 0) {
				Debug.Log ("SUUUUUHUSDHUSHADUSAIDHISN");
				tempMaterial = heldBlock.GetComponentsInChildren<MeshRenderer> ()[0].material;
				foreach (GameObject snapBox in platform.GetComponent<Platform> ().boxesToSnap) {
					snapBox.GetComponent<MeshRenderer> ().material = tempMaterial;
					snapBox.GetComponent<MeshRenderer> ().enabled = true;
					snapBox.GetComponent<Box> ().available = false;
				}
				platform.GetComponent<Platform> ().boxesToSnap.Clear ();
			}
		}

		if (platform.GetComponent<Platform> ().boxesToSnap.Count == heldBlock.transform.childCount) {
			foreach (GameObject snapBox in platform.GetComponent<Platform> ().boxesToSnap)
				snapBox.GetComponent<MeshRenderer> ().enabled = true;
		} else {
			foreach (GameObject snapBox in platform.GetComponent<Platform> ().boxesToSnap)
				snapBox.GetComponent<MeshRenderer> ().enabled = false;
			platform.GetComponent<Platform> ().boxesToSnap.Clear ();
		}
	}
}
