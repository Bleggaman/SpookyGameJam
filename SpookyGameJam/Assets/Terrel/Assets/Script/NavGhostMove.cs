using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// note: consider some kind of smoothing operation for movement

[RequireComponent(typeof(Rigidbody))]
public class NavGhostMove : MonoBehaviour {

    //Set in Editor
    public Transform graphics;
    public float moveSpeed = 9;
    public movementNode targetNode;
    public movementNode prevNode;
    public GameObject candyDrop;
    public NavMeshAgent agent;
    public float foVision = 120;
    public float visionRange = 5;

    //Properties
    public bool scared = false;
    public bool fleeing = false;
    public Transform scareLocation;
    public Vector3 movementVector;

    public bool alert = false;
    public Transform focusLocation;
    public bool hiding = false;
    public bool playerSeen = false;

    //Initialized in Start
    public Rigidbody rb;
    public float transparency;
    public Renderer rend;

    // Periods define the amount of time states persist
    public float fleePeriod = 5;
    public float animPeriod = 3;
    public float reactionPeriod = 0.5f;
    public float alertPeriod = 10f;
    
    // Timers track time internally
    private float idleTimer = 0;
    private float fleeTimer = 0;
    private float animTimer = 0;
    private float reactionTime = 0;

    //hidetimer


    void Start () {
        if (prevNode == null)
            prevNode = targetNode;
        rb = GetComponent<Rigidbody>();
        rend = graphics.GetComponent<Renderer>();
        transparency = rend.material.color.a;
    }
	
	void Update () {
        
    }

    // UNTESTED METHOD
    private bool canSeePlayer () {
        Vector3 dir = GameManager.gameManager.player.transform.position - this.transform.position;
        float angle = Mathf.Acos(Vector3.Dot(transform.forward, dir.normalized));
        //Checks if the player is in front of the ghost
        if (angle < foVision) {
            RaycastHit losPlayer;
            Physics.Raycast(transform.up, dir, out losPlayer);
            // Checks if line of sight is blocked
            if (losPlayer.collider.transform == GameManager.gameManager.player.transform) {
                // Checks if the player is in range
                if (losPlayer.distance < visionRange) {
                    return playerSeen = true;
                }
            }
        }
        return playerSeen = false;
    }

    // Resets variables that help contol AI state
    private void resetStatus() {
        fleeing = false;
        scared = false;
        animTimer = 0;
        fleeTimer = 0;
        idleTimer = 0;
        transparency = 1;
    }

    private void Wander() {
        Vector3 dirVector = Vector3.zero;
        dirVector = targetNode.transform.position - this.transform.position;
        if (dirVector.magnitude < targetNode.range) {
            // Reached destination
            changeTargetNode();
        } else if (idleTimer > prevNode.waitPeriod) {
            // Done waiting
            agent.destination = targetNode.transform.position;
        } else {
            // Currently waiting
            idleTimer += Time.deltaTime;
        }
    }

    private void flee(Transform source) {
        // What does it mean to flee
        Instantiate(candyDrop, transform.position, transform.rotation);
        fleeing = true;
        fleeTimer = 0;
        // fade to become invisible
        transparency = 0.2f;
        // run away from scare source
        Vector3 dirVector = transform.position - source.position;
        agent.destination = transform.position + dirVector.normalized * 10;
    }

    // UNTESTED METHOD
    private void hide() {
        Transform bestSpot = chooseHidingSpot();
        if (bestSpot == null) {
            return;
        }
        hiding = true;
        agent.destination = bestSpot.transform.position;
    }

    private bool increaseTimer(float timer, float period) {
        period += Time.deltaTime;
        return (period < timer);
    }

    // UNTESTED METHOD
    private Transform chooseHidingSpot() {
        Transform bestSpot = null;
        foreach (Transform location in targetNode.hidingSpots) {
            Vector3 dir = location.transform.position - this.transform.position;
            RaycastHit losLocation;
            Physics.Raycast(transform.up, dir, out losLocation);
            // has line of sight with hiding spot
            if (losLocation.collider.transform == location.transform) {
                // WHAT DO I DO?
            }
            // is it closer the the best spot?
            if (dir.magnitude < Vector3.Distance(bestSpot.transform.position, this.transform.position)) {
                bestSpot = location;
            }
        }
        foreach (Transform location in prevNode.hidingSpots) {

        }
        return bestSpot;
    }

    // Chooses a new node to move to
    private void changeTargetNode() {
        prevNode = targetNode;
        idleTimer = 0;
        movementNode[] newNodes = targetNode.nodes;
        int index = chooseNode(newNodes);
        targetNode = newNodes[index];
        while (targetNode == null) {
            newNodes = targetNode.nodes;
            index = chooseNode(newNodes);
            targetNode = newNodes[index];
        }
    }

    // returns the index of a random node
    private int chooseNode(movementNode[] nodes) {
        return Random.Range(0, nodes.Length);
    }
}
