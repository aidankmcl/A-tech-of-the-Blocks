using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform : MonoBehaviour {

	public int levels;
	public int rows;
	public int columns;

	public GameObject ogSlat;
	public GameObject ogBox;

	public List<GameObject> slats;
	public List<GameObject> snapBoxes;
	public List<GameObject> activeSnapBoxes;
	public List<GameObject> boxesToSnap;
	public float snapDistance;

	public List<int> checkTheseYValues;

	private Vector3 slatVector = new Vector3(0f,0.5f,0f);
	private Vector3 boxVector = new Vector3(0f,0.66f,0f);

	// Use this for initialization
	void Start () {
		for (int i=0; i<rows; i++) {
			for (int j=0; j<columns; j++) {
				slats.Add(Instantiate(ogSlat));
				slats[i*columns + j].transform.position = slatVector;

				// Add boxes vertically
				for (int k=0; k<levels; k++) {
					snapBoxes.Add(Instantiate(ogBox));

					GetBox(i, j, k).transform.position = boxVector;
					// It's i, k, j because levels is actually in the y and j in the z
					GetBox(i, j, k).GetComponent<Box>().vectorIndex = new Vector3(i,j,k);
					GetBox(i, j, k).GetComponent<Box>().parentPlatform = transform.gameObject;
					boxVector += new Vector3(0f, 0.26f, 0f);
				}
				// Both go up a step in Z direction
				slatVector += new Vector3(0f, 0f, 0.26f);
				// After each round we need to undo y because of level increases;
				boxVector += new Vector3(0f, -levels * 0.26f, 0.26f);
			}
			slatVector += new Vector3(0.26f, 0f, -columns * 0.26f);
			boxVector += new Vector3(0.26f, 0f, -columns * 0.26f);
		}

		ogSlat.SetActive(false);
		ogBox.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetBox(int x, int y, int z) {
		// How to index into our 3D Matrix:
		//  a = completed rows x columns x levels
		//  b = completed columns x levels
		//  c = completed levels in current run
		// index = a + b + c
		return snapBoxes[(x * (columns * levels)) + (y*levels) + z];
	}

	public void PlacePiece (GameObject piece) {

		Material tempMaterial = piece.GetComponentsInChildren<MeshRenderer> ()[0].material;
		foreach (GameObject snapBox in GetComponent<Platform> ().boxesToSnap) {
			snapBox.GetComponent<MeshRenderer> ().material = tempMaterial;
			snapBox.GetComponent<MeshRenderer> ().enabled = true;
			snapBox.GetComponent<Box> ().available = false;

		}
		GetComponent<Platform> ().boxesToSnap.Clear ();
		Destroy (piece);




	}

	public void FloorCheck (int yBottom) {
		for (int z = yBottom + 1; z < levels; z++) {
			for (int x = 0; x < rows; x++) {
				for (int y = 0; y < columns; y++) {

					if (z == yBottom + 1) {
						Destroy(GetBox(x,y,yBottom));
					}

					GetBox (x, y, z).transform.position -= new Vector3 (0f, 0.26f, 0f);
					GetBox (x, y, z).GetComponent<Box>().vectorIndex -= new Vector3 (0f, 1f, 0f);

					if (z == levels - 1) {
						GameObject newTopBox = Instantiate (ogBox);
						newTopBox.transform.position = new Vector3 (x * 0.26f, z, y * 0.26f);
					}
				}
			}
		}

		
	}
}
