using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiAim : MonoBehaviour {

    public GameObject targetObject;
    public GameObject barrelObject;
    public GameObject firePoint;
	public Rigidbody projcetile1;
	public Rigidbody projcetile2;
    // public float mBarrelSpeed = 30f;
    // public float mBarrelMaxAngle = 80f;
    // public float mBarrelMinAngle = -5f;

    public float baseAngle = 0F;

    public float towerSpeed = 6F;
    public float contactRange = 50f;
    public float fireRange = 40f;
    public float fireTimeRange = 3f;

    private float mBarrelAngle = 0f;

    private float elevationInputValue;
    private float turnInputValue;
    private int mAmmoSelectIndex;

    private float mCurrentLaunchForce = 30f;
    private bool is_fired;
    private float lastFireTime;
    private bool mBaseFirstFlag = true;
    // Use this for initialization
    void Start() {
        mAmmoSelectIndex = 0;
        lastFireTime = Time.time;
        targetObject = GameObject.Find("tank");
        Vector3 dV = this.transform.position - this.transform.root.position;
        Quaternion rotation = Quaternion.LookRotation(dV);
        baseAngle = rotation.y;
        //baseAngle = Quaternion.LookRotation(this.transform.position - this.transform.root.position).y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastFireTime > fireTimeRange && is_fired)
        {
            is_fired = false;
        }
        if (!is_fired && Vector3.Distance(gameObject.transform.position, targetObject.transform.position) < fireRange)
        {
            Fired();
        }
        Turn();
    }

    private void Turn()
    {
        Vector3 dV;
        Quaternion rotation;
        if (Vector3.Distance(gameObject.transform.position, targetObject.transform.position) > contactRange) {
            dV = this.transform.position - this.transform.root.position;
            rotation = Quaternion.LookRotation(dV);
        }
        else {
            dV = targetObject.transform.position - gameObject.transform.position;
            rotation = Quaternion.LookRotation(dV);
        }


        // Calcuate turn angle by relative position
        rotation.x = 0f;
        rotation.z = 0f;

        float turnAngle = rotation.y;
        if (Mathf.Abs(turnAngle) < 0.01f || 1f - Mathf.Abs(turnAngle) < 0.01f) {
            return;
        }
        rotation.y = baseAngle;
        baseAngle += Mathf.Clamp((turnAngle - baseAngle) / 10f, -0.1f, 0.1f);
        this.transform.rotation = rotation;
        this.transform.Rotate(-90f, 180f, 0f);

        Vector3 dP = (targetObject.transform.position - this.transform.root.position);
        float theta = Mathf.Asin(-dP.y / Mathf.Sqrt(dP.x * dP.x + dP.z * dP.z));
        theta = Mathf.Rad2Deg * theta;
        barrelObject.transform.rotation = rotation * Quaternion.Euler(90f+theta, 0, 0);
    }

    void Fired()
    {
        is_fired = true;
        lastFireTime = Time.time;
        if (Random.Range(0.0f, 10.0f) > 9.0f) {
            Rigidbody shoot =
				(Rigidbody)Instantiate(projcetile2, firePoint.transform.position, firePoint.transform.rotation);
			//給砲彈方向力，將他從y軸推出去
			shoot.velocity = firePoint.transform.TransformDirection(new Vector3( 0, 20, 0));
			//讓坦克的碰撞框忽略砲彈的碰撞框
			Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), shoot.GetComponent<Collider>());
        }
        else {
            Rigidbody shoot =
				(Rigidbody)Instantiate(projcetile1, firePoint.transform.position, firePoint.transform.rotation);
			//給砲彈方向力，將他從y軸推出去
			shoot.velocity = firePoint.transform.TransformDirection(new Vector3( 0, 30, 0));
			//讓坦克的碰撞框忽略砲彈的碰撞框
			Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), shoot.GetComponent<Collider>());
        }
    }
}
