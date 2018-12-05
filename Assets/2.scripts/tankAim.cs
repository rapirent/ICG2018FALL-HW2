using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankAim : MonoBehaviour {
    public GameObject barrelObject;
    public AudioSource loopSound;
    private float elevationInputValue;
    private float turnInputValue = 0F;
    private float barrelAngle = 0F;
    public float towerSpeed = 0.3F;
    public float barrelSpeed = 30F;
    public float barrelMaxAngle = 80f;
    public float barrelMinAngle = -5F;
    private bool is_looping = false;
    private float lastTurnInput = 0F;
    public float LimitAngle = 90f;
    private float currentAngle=359f;

    public int RotateSpeed = 50;
    // private float Limit = 180f;
    // private float InitLocalRotY = 0f;
    // Use this for initialization
    void Start() {
        //InitLocalRotY = transform.localRotation.eulerAngles.y % 360f;
        is_looping = false;
        lastTurnInput = 0F;
    }

    // Update is called once per frame
    void Update() {
        // 用滑鼠移動控制砲台方向
        turnInputValue = Input.GetAxis("Mouse X");
        // 得到砲管角度
        elevationInputValue = Input.GetAxis("Mouse Y");
        if(is_looping) {
            // loopSound.time = 0.5f;
            if (!loopSound.isPlaying){
                loopSound.Play();
            }
        }
        else {
            loopSound.Stop();
        }
    }
    private void FixedUpdate()
    {
        RotateUpdate();
        Elevation();
    }

    private void Turn()
    {
        // this.transform.Rotate(new Vector3(0, 0, turnInputValue*Time.deltaTime));
        // object_rotX = Input.GetAxis("Mouse X") * object_rotSens * Time.deltaTime;
        // object_rotY = Input.GetAxis("Mouse Y") * object_rotSens * Time.deltaTime;
        this.transform.RotateAround(this.transform.position, new Vector3(0, turnInputValue*Time.deltaTime, 0), 100 * Time.deltaTime);
        // float step = towerSpeed * Time.deltaTime;
        // Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        if (Mathf.Abs(lastTurnInput - turnInputValue) >= 0.01f) {
            if (!is_looping) {
                is_looping = true;
                loopSound.Play();
            }
            lastTurnInput = turnInputValue;
        }
        // this.transform.rotation = Quaternion.AngleAxis(turnInputValue * 10f * Time.deltaTime, Vector3.right);
        // this.transform.position = Quaternion.Slerp(this.transform.position, new Vector3(0, 0, turnInputValue), step);
        if(loopSound.time > 0.3f){
            is_looping = false;
            loopSound.Stop();
        }
        // Quaternion targetRotation = Quaternion.LookRotation(Input.mousePosition - this.transform.position);

        	// Smoothly rotate towards the target point.
        // this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, towerSpeed * Time.deltaTime);
        // this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.AngleAxis(turnInputValue*180, Vector3.up), towerSpeed * Time.deltaTime);
    }

    private void Elevation()
    {
        barrelAngle += elevationInputValue;
        barrelAngle = Mathf.Clamp(barrelAngle, barrelMinAngle, barrelMaxAngle);
        Vector3 temp = barrelObject.transform.localEulerAngles;
        temp.x = barrelAngle;
        barrelObject.transform.localEulerAngles = temp;
    }

    void RotateUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward*10f,Color.blue);
        Debug.DrawRay(transform.position, transform.up*10f,Color.green);
        Debug.DrawRay(transform.position, transform.right * 10f, Color.red);
        Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 dir;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitpos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            dir = (hitpos - transform.position).normalized;
            // transform.Rotate(transform.up, Mathf.Min(RotateSpeed * Time.deltaTime, angle));
        }
        else {
            dir = transform.forward;
        }
        Debug.DrawRay(transform.position, dir*10f, Color.yellow);

        float angle = Vector3.Angle(transform.forward, dir);
        Quaternion rotate = Quaternion.RotateTowards(transform.rotation,
                                                    Quaternion.LookRotation(dir,transform.up),
                                                    RotateSpeed * Time.deltaTime);
        this.transform.rotation = rotate;
        //效果不好
        if(Vector3.Angle( transform.forward, dir) < 0.2) {
            is_looping = false;
        }
        else {
            is_looping = true;
        }
    }
}
