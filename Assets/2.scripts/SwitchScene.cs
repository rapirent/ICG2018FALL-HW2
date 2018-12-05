using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(Input.GetKeyDown(KeyCode.M)) {
            if(SceneManager.GetActiveScene().buildIndex == 0) {
                SceneManager.LoadScene(1);
            }
            else {
                SceneManager.LoadScene(0);
            }
        }
	}
}
