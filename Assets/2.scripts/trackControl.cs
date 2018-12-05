using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class trackControl : MonoBehaviour {
    private List<Transform> Lgear = new List<Transform>();
    private List<Transform> Rgear = new List<Transform>();
    private Transform Ltrack, Rtrack;
    private float shiftLSum = 0f;
    private float shiftRSum = 0f;
    // Use this for initialization
    void Start () {
        Transform[] gChildren = GetComponentsInChildren<Transform>();
        Regex LgearExp1 = new Regex(@"gear\w?-L-\d+");
        Regex RgearExp1 = new Regex(@"gear\w?-R-\d+");
        Regex LgearExp2 = new Regex(@"gearL-\w?");
        Regex RgearExp2 = new Regex(@"gearR-\w?");
        foreach (Transform gear in gChildren) {
            // self
            if (gear.name == gameObject.name) {
                continue;
            }
            if (LgearExp1.IsMatch(gear.name) || LgearExp2.IsMatch(gear.name) )
                Lgear.Add(gear);
            else if (RgearExp1.IsMatch(gear.name) || RgearExp2.IsMatch(gear.name))
                Rgear.Add(gear);
            else if (gear.name == "track-L")
                Ltrack = gear;
            else if (gear.name == "track-R")
                Rtrack = gear;
        }
	}

	// Update is called once per frame
	void Update () {

	}

    public void Move(float shift) {
        shiftLSum += shift;
        shiftRSum += shift;
        Renderer lrender = Ltrack.GetComponent<Renderer>();
        //平移紋理
        lrender.material.SetTextureOffset("_MainTex", new Vector2(-(shiftLSum / 100) % 1, 0));
        Renderer rrender = Rtrack.GetComponent<Renderer>();
        //平移紋理
        rrender.material.SetTextureOffset("_MainTex", new Vector2(-(shiftRSum / 100) % 1, 0));
        Vector3 rotation = new Vector3(shift, 0f, 0f);
        foreach (Transform gear in Lgear) {
            gear.Rotate(rotation);
        }
        foreach (Transform gear in Rgear) {
            gear.Rotate(rotation);
        }
    }
    public void Turn(float shift) {
        float lshift, rshift;
        float gap = 0.3f, moveSpeed = 2;
        if (shift > 0)
        {
            shiftLSum += shift;
            shiftRSum += -shift * gap;
            lshift = shift;
            rshift = -shift * gap;
        } else
        {
            shiftLSum += -shift * gap;
            shiftRSum += shift;
            lshift = -shift * gap;
            rshift = shift;
        }
        lshift *= moveSpeed;
        rshift *= moveSpeed;
        Renderer lrender = Ltrack.GetComponent<Renderer>();
        lrender.material.SetTextureOffset("_MainTex", new Vector2(-shiftLSum / 100 % 1, 0));
        Renderer rrender = Rtrack.GetComponent<Renderer>();
        rrender.material.SetTextureOffset("_MainTex", new Vector2(-shiftRSum / 100 % 1, 0));
        Vector3 rotationL = new Vector3(lshift, 0f, 0f);
        Vector3 rotationR = new Vector3(rshift, 0f, 0f);
        foreach (Transform gear in Lgear)
        {
            gear.Rotate(rotationL);
        }
        foreach (Transform gear in Rgear)
        {
            gear.Rotate(rotationR);
        }
    }
}
