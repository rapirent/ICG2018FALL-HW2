using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerhealthBar : MonoBehaviour {
    public GameObject targetObject;
    public string objectName;
    private tankHealth targetHealthScript;
    private GameObject gameoverObject = GameObject.Find("Gameover");
	// Use this for initialization
	void Start () {
        targetObject = GameObject.Find(objectName);
        targetHealthScript = targetObject.GetComponent<tankHealth>();
        gameoverObject = GameObject.Find("Gameover");
        gameoverObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(-98 + 96 * targetHealthScript.GetHealthPercent(), 2f, 0f);
        if (targetHealthScript.GetHealthPercent() <= 0) {
            Debug.Log("Dead");
            gameoverObject.SetActive(true);
        }
	}
}
