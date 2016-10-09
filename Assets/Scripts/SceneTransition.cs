using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour {

	void OnTriggerEnter()
    {
        SceneManager.LoadScene(1);
    }


}
