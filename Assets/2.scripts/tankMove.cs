using UnityEngine;
using System.Collections;

public class tankMove : MonoBehaviour {

	public float minSpeed = 0.5f;
	public float rSpeed = 70f;
    public float maxSpeed = 7F;
    public float acceleration = 0.5F;
    public trackControl tankTrackControl;
    public Rigidbody tankBody;
    public AudioSource trackSound;
    private float nowSpeed;
    private float h;
    private float v;
    private bool playing;
	// Use this for initialization
	void Start () {
        playing = false;
        trackSound.volume = 1.5F;
	}

	// Update is called once per frame
	void Update () {
		h = Input.GetAxis ("Horizontal");//獲取水平軸向按鍵
		v = Input.GetAxis ("Vertical");//獲取垂直軸向按鍵
		// transform.Translate(0,0,-mSpeed * v);//根據垂直軸向按鍵來前進或後退
        // transform.Rotate(0,rSpeed * h,0);//根據水平軸向按鍵來旋轉
    }
    private void FixedUpdate() {
        Move();
        Turn();
    }
    private void Move(){
        if (Mathf.Abs(v) >= 0.1f && nowSpeed < maxSpeed) {
            //發動
            nowSpeed = nowSpeed + acceleration;
        }
        else if (Mathf.Abs(v) >= 0.1f) {
            nowSpeed = nowSpeed + acceleration;
            nowSpeed = Mathf.Clamp(nowSpeed, minSpeed, maxSpeed);
        }
        else {
            nowSpeed = minSpeed;
        }
        Vector3 moveVec = transform.forward * v * (-nowSpeed) * Time.deltaTime;
        if (v!=0F) {
            if (!playing) {
                playing = true;
                trackSound.Play();
            }
        }
        else {
            playing = false;
            trackSound.Stop();
        }
        tankTrackControl.Move(v * (-nowSpeed));
        tankBody.MovePosition(tankBody.position + moveVec);
    }
    private void Turn() {
        float turnAngle = h * rSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAngle, 0f);
        tankTrackControl.Turn(turnAngle);
        tankBody.MoveRotation(tankBody.rotation * turnRotation);
    }
}
