using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour {
	public WorldInfo worldInfo;

	public Transform graphics;
	public float moveSpeed = 9;
	public iNode targetNode;
	public iNode prevNode;
	public GameObject candyDrop;
	public NavMeshAgent agent;
	public float foVision = 120;
	public float visionRange = 5;

	public float nodeSensitivity = 1;
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

	// Use this for initialization
	void Start () {
		worldInfo = GameObject.Find("Main Camera").GetComponent<WorldInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		print (prevNode);
		print (targetNode);

		rend.material.color = new Vector4 (rend.material.color.r, rend.material.color.g, 
			rend.material.color.b, Mathf.Clamp01(transparency));
		if (!scared && !fleeing){
			//Logic for wandering
			Wander();
		} else {
			// Logic for when scared, pauses, then flees from scare
			if (animTimer > animPeriod) {
				//Start fleeing
			//	scareLocation = targetNode.transform;
				//call once
				if (!fleeing)
					flee(scareLocation);
				// when to stop fleeing
				fleeTimer += Time.deltaTime;
				if (fleeTimer > fleePeriod) {
					resetStatus();
				}
			} else {
				//Stop and Animate
				agent.destination = transform.position;
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
		Vector3 dirVector = Vector3.zero;

		if (targetNode != null) {
			dirVector = targetNode.getGameObject ().transform.position - this.transform.position;
			if (dirVector.magnitude < nodeSensitivity) {
				// Reached destination
				prevNode = targetNode;
				targetNode = Utils.findiNode (gameObject, worldInfo);
			}
			if (idleTimer > 1) {
				// Done waiting
				agent.destination = targetNode.getGameObject ().transform.position;
			} else {
				idleTimer += Time.deltaTime;
			}
		} else {
			// Currently waiting
			targetNode = Utils.findiNode (gameObject, worldInfo);

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
		iNode bestSpot = Utils.findHidingSpotNode(gameObject, worldInfo);
		if (bestSpot == null) {
			return;
		}
		hiding = true;
		//agent.destination = bestSpot.transform.position;
	}

	private bool increaseTimer(float timer, float period) {
		period += Time.deltaTime;
		return (period < timer);
	}
}
