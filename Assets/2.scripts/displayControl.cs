using UnityEngine;
using System.Collections;

public class displayControl : MonoBehaviour {
    public GameObject firstPlayerCamera;
    public GameObject thirdPlayerCamera;

    private bool is_change;
    void Awake() {
        is_change = true;
        firstPlayerCamera.SetActive(!is_change);
        firstPlayerCamera.SetActive(is_change);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            is_change = !is_change;
            thirdPlayerCamera.SetActive(!is_change);
            firstPlayerCamera.SetActive(is_change);
        }
    }
}
