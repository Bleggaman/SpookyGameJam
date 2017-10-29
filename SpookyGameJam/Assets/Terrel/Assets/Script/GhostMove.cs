using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// note: consider some kind of smoothing operation for movement

[RequireComponent(typeof(Rigidbody))]
public class GhostMove : MonoBehaviour {

    //Set in Editor
    public Transform graphics;
    public float moveSpeed = 9;
    public movementNode targetNode;
    public movementNode prevNode;
    public GameObject candyDrop;

    //Properties
    public bool scared = false;
    public bool fleeing = false;
    public Transform scareLocation;
    public Vector3 movementVector;


    //Initialized in Start
    public Rigidbody rb;
    public float transparency;
    public Renderer rend;

    //Timers
    private float idleTimer = 0;
    private float fleeTimer = 0;
    public float fleePeriod = 5;
    private float animTimer = 0;
    public float animPeriod = 3;


    void Start () {
        if (prevNode == null)
            prevNode = targetNode;
        rb = GetComponent<Rigidbody>();
        rend = graphics.GetComponent<Renderer>();
        transparency = rend.material.color.a;
    }
	
	void Update () {
        rend.material.color = new Vector4 (rend.material.color.r, rend.material.color.g, rend.material.color.b, Mathf.Clamp01(transparency));
        //Logic for wandering
        if (!scared && !fleeing){
            rb.velocity = Vector3.zero;
            Wander();
        } else {
            // Logic for when scared, pauses, then flees from scare
            if (animTimer > animPeriod) {
                //Start fleeing
                scareLocation = targetNode.transform;
                //call once
                if (!fleeing)
                    flee(scareLocation);
                // when to stop fleeing
                fleeTimer += Time.deltaTime;
                if (fleeTimer > fleePeriod) {
                    resetStatus();
                }
            } else {
                rb.velocity = Vector3.zero;
                animTimer += Time.deltaTime;
            }
        }
    }

    private void resetStatus() {
        fleeing = false;
        scared = false;
        animTimer = 0;
        fleeTimer = 0;
        idleTimer = 0;
        transparency = 1;
    }

    private void Wander() {
        // Do i 
        if (idleTimer > prevNode.waitPeriod) {
            // move to next position
            move();
        } else {
            // wait if at next position
            idleTimer += Time.deltaTime;
        }
    }

    private void flee(Transform source) {
        Instantiate(candyDrop, transform.position, transform.rotation);
        fleeing = true;
        fleeTimer = 0;
        // fade to become invisible
        transparency = 0.2f;
        // run away from scare source
        Vector3 dirVector = transform.position - source.position;
        rb.velocity += dirVector.normalized * moveSpeed;
    }


    private void move() {
        // Move towards the target node
        Vector3 dirVector = Vector3.zero;
        dirVector = targetNode.transform.position - transform.position;
        if (dirVector.magnitude < targetNode.range) {
            changeTargetNode();
            dirVector = targetNode.transform.position - transform.position;
        }
        rb.velocity += dirVector.normalized * moveSpeed;
    }

    private void changeTargetNode() {
        prevNode = targetNode;
        idleTimer = 0;
        movementNode[] newNodes = targetNode.nodes;
        int index = chooseNode(newNodes);
        targetNode = newNodes[index];
    }

    // returns the index of the node selected
    private int chooseNode(movementNode[] nodes) {
        //selects random
        return Random.Range(0, nodes.Length);
    }
}
