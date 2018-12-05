using UnityEngine;
using System.Collections;

public class playerhealthBar : MonoBehaviour {
    public GameObject targetObject;
    public string objectName;
    private tankHealth targetHealthScript;
	// Use this for initialization
	void Start () {
        targetObject = GameObject.Find(objectName);
        targetHealthScript = targetObject.GetComponent<tankHealth>();
    }

	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(-98 + 96 * targetHealthScript.GetHealthPercent(), 2f, 0f);
	}
}
