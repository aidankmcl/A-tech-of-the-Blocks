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
	public int snapDistance;

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
		foreach (GameObject activeSnapBox in activeSnapBoxes) {
			activeSnapBox.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	private GameObject GetBox(int x, int y, int z) {
		// How to index into our 3D Matrix:
		//  a = completed rows x columns x levels
		//  b = completed columns x levels
		//  c = completed levels in current run
		// index = a + b + c
		return snapBoxes[(x * (columns * levels)) + (y*levels) + z];
	}
}
