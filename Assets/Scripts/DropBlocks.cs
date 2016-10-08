using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropBlocks : MonoBehaviour {

	public List<GameObject> Children;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			Children.Add(child.gameObject);
		}
		InvokeRepeating("DropBlock", 0, 2);
	}

	// Update is called once per frame
	void Update () {

	}

	private int randomChildIndex;
	private GameObject newChild;

	void DropBlock () {
		randomChildIndex = Random.Range(0, Children.Count);
		newChild = Instantiate(Children[randomChildIndex], Children[randomChildIndex].transform.position, Quaternion.identity) as GameObject;
		newChild.SetActive(true);
	}
}
