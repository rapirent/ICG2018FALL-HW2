using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiTrace : MonoBehaviour {
    private UnityEngine.AI.NavMeshAgent agent;
    public float stopRadius = 30F;
    public GameObject target;

	// Use this for initialization
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.autoBraking = false;
        agent.stoppingDistance = stopRadius;
	}

	// Update is called once per frame
	void Update () {
        // Vector3 direction = (agent.transform.position - transform.position).normalized;
        // Quaternion rotation = Quaternion.LookRotation(direction);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        // transform.Translate(0f, 0f, -0.01f);
        agent.SetDestination(target.transform.position);
        this.transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }
}
