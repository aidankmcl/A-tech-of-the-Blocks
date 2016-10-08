using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public class NewBlock : MonoBehaviour {

	public List<Material> materials;
	public List<GameObject> Children;
	public Renderer rend;

	// Use this for initialization
	void Start () {
		DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Materials/");
		FileInfo[] info = dir.GetFiles("*.*");
		foreach (FileInfo f in info) {
			string fname = f.Name.Split('.')[0];
			materials.Add(Resources.Load("Materials/"+fname, typeof(Material)) as Material);
		}

		int randomNum = Random.Range(0, materials.Count);

		foreach (Transform child in transform) {
			child.gameObject.GetComponent<Renderer>().material = materials[randomNum];
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
