using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiPatrol : MonoBehaviour {
    // void Start () {
    //     agent = GetComponent<NavMeshAgent>();
    // }
    // void Update () {
    //     agent.SetDestination(target.position);
    // }
    public Transform[] waypoints;
    private int destPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        //agent.autoBraking = false;
        agent.updateRotation = false;
        GotoNextPoint();
    }


    void GotoNextPoint() {
        // Returns if no points have been set up
        if (waypoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = waypoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % waypoints.Length;
    }

    void Update () {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.6f)
            GotoNextPoint();
        this.transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }
}


