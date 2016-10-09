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
							noMatch = false;
							break;
						} else {
							noMatch = true;
						}
					}

					if (noMatch) {
						break;
					}

				}
			}
		} else {
			if (platform.GetComponent<Platform> ().boxesToSnap.Count > 0) {
				Debug.Log ("SUUUUUH DOOOOOO");
				tempMaterial = heldBlock.GetComponentsInChildren<MeshRenderer> ()[0].material;
				foreach (GameObject snapBox in platform.GetComponent<Platform> ().boxesToSnap) {
					snapBox.GetComponent<MeshRenderer> ().material = tempMaterial;
					snapBox.GetComponent<MeshRenderer> ().enabled = true;
					snapBox.GetComponent<Box> ().available = false;

					if (!platform.GetComponent<Platform>().checkTheseYValues.Contains ((int)snapBox.GetComponent<Box> ().vectorIndex.y))
						platform.GetComponent<Platform>().checkTheseYValues.Add ((int)snapBox.GetComponent<Box> ().vectorIndex.y);
				}
				platform.GetComponent<Platform> ().boxesToSnap.Clear ();

				foreach (int y in platform.GetComponent<Platform>().checkTheseYValues) {
					bool full = true;
					for (int x = 0; x < platform.GetComponent<Platform>().rows; x++) {
						for (int z = 0; z < platform.GetComponent<Platform>().columns; z++) {
							Debug.Log (platform.GetComponent<Platform>().GetBox (x, z, y).GetComponent<Box> ().available);
							if (platform.GetComponent<Platform>().GetBox (x, z, y).GetComponent<Box> ().available) {
								full = false;
							}
						}
					}
					if (full) {
						Debug.Log ("One floor is good!!");
						platform.GetComponent<Platform> ().FloorCheck (y);
					}
				}

				Destroy (heldBlock);
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
