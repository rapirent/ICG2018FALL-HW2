using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class healthBar : MonoBehaviour {
    public GameObject targetObject;
    public string objectName;
    public Slider slider;
    private tankHealth targetHealthScript;
    private Vector3 SliderPosition;
    private Camera camera;
    private bool is_Dead;
    // The image component of the slider.
    // Use this for initialization
	void Start () {
        targetObject = GameObject.Find(objectName);
        targetHealthScript = targetObject.GetComponent<tankHealth>();
        slider.value = 100;
        is_Dead = false;
    }

	// Update is called once per frame
	void Update () {
        if (!is_Dead) {
            Debug.Log(Camera.current);
            Debug.Log(targetObject);
            SliderPosition = Camera.current.WorldToScreenPoint(targetObject.transform.position) + Vector3.up*100;
            if (SliderPosition.x > Screen.width || SliderPosition.x <0 || SliderPosition.y > Screen.height || SliderPosition.y < 0 || SliderPosition.z <0) {
                //藏起來
                slider.transform.position = Vector3.up * 5000;
            }
            else {
                slider.transform.position = SliderPosition;
            }
            slider.value = targetHealthScript.GetHealthPercent()*100;
            if (slider.value<=0) {
                is_Dead = true;
                slider.transform.position = Vector3.up * 5000;
            }
        }
        // transform.localPosition = new Vector3(-98 + 96 * targetHealthScript.GetHealthPercent(), 2f, 0f);
	}
}
