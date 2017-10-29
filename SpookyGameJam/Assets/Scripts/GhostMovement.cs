using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour, iScarable {
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

	private float moveAgainTime = 1;

	public GameObject player;
	public CharacterScript playerScript;

	bool seePlayer;
	bool playerSees;
	bool playerAlert;
	bool isAlert;
	float distLong = 10;
	float distShort = 2;
	float distMed = 5;
	public float fastReact = .1f;
	public float slowReact = .3f;
	public float playerAlertTime = .4f;

	public enum GhostState {hiding, wandering, seeing, dead};
	GhostState state = new GhostState();

	public bool isHiding;

	// Use this for initialization
	void Start () {
		worldInfo = GameObject.Find("Main Camera").GetComponent<WorldInfo>();
		player = GameObject.Find ("Player");
		playerScript = player.GetComponent<CharacterScript> ();
		state = GhostState.wandering;
	}
	
	// Update is called once per frame
	void Update () {
		print (prevNode);
		print (targetNode);

		interactionUpdate();

		if (seePlayer) {
			Debug.Log ("Sees player");
			ghostSeesPlayer (); 
		}
			moveTo ();
	
		rend.material.color = new Vector4 (rend.material.color.r, rend.material.color.g, 
			rend.material.color.b, Mathf.Clamp01(transparency));


	}
	private void resetStatus() {
		fleeing = false;
		scared = false;
		animTimer = 0;
		fleeTimer = 0;
		idleTimer = 0;
		transparency = 1;
	}

	private void moveTo() {
		Vector3 dirVector = Vector3.zero;
		if (state == GhostState.wandering) {
			if (targetNode != null) {
				dirVector = targetNode.getGameObject ().transform.position - this.transform.position;
				if (dirVector.magnitude < nodeSensitivity) {
					// Reached destination
					prevNode = targetNode;
					targetNode = Utils.findiNode (gameObject, worldInfo);
				}

			} else {
				targetNode = Utils.findiNode (gameObject, worldInfo);
			} 
			agent.destination = targetNode.getGameObject ().transform.position;
		}
		if (state == GhostState.hiding) {
			if (targetNode != null) {
				dirVector = targetNode.getGameObject ().transform.position - this.transform.position;
				if (dirVector.magnitude < nodeSensitivity) {
					// Reached destination
					prevNode = targetNode;
					idleTimer += Time.deltaTime;
					if (idleTimer > moveAgainTime) {
						state = GhostState.wandering;
						idleTimer = 0;
					}
				}
			}
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

	private bool increaseTimer(float timer, float period) {
		period += Time.deltaTime;
		return (period < timer);
	}

	IEnumerator ghostReact(float reactTime){
		yield return new WaitForSeconds (reactTime);
		isAlert = true;

	}

	IEnumerator deAlert(){
		yield return new WaitForSeconds (3);
		isAlert = false;

	}

	IEnumerator playerReact(){
		yield return new WaitForSeconds (playerAlertTime);
		playerAlert = true;
	}

	IEnumerator playerDeAlert(){
		yield return new WaitForSeconds (3);
		playerAlert = false;
	}

	#region iScarable implementation

	public void scare (int scarePower)
	{
		Debug.Log ("eek + " + scarePower);
	}

	#endregion

	private bool canSeePlayer () {
		Vector3 dir = GameManager.gameManager.player.transform.position - this.transform.position;
		float angle = Mathf.Acos(Vector3.Dot(transform.forward, dir.normalized));
		//Checks if the player is in front of the ghost
		if (angle < foVision) {
			RaycastHit losPlayer;
			Physics.Raycast(transform.up, dir, out losPlayer);
			// Checks if line of sight is blocked
			if (losPlayer.collider != null){
				if (losPlayer.collider.transform == GameManager.gameManager.player.transform) {
					// Checks if the player is in range
					if (losPlayer.distance < visionRange) {
						return playerSeen = true;
					}
				}
			}
		}
		return playerSeen = false;
	}

	public void ghostSeesPlayer(){
		
		if (playerSees) { //PLAYER SEES
			if (!playerAlert) { //PLAYER NOT ALERT
				if (isAlert) { //GHOST IS ALERT
					if (Vector3.Distance (transform.position, player.transform.position) < distShort) {
						//if within range of activatable  use it 
						playerScript.scare (5);
					} else {
						targetNode = Utils.findCloseHiding (gameObject, worldInfo);
						state = GhostState.hiding;
						moveAgainTime = 10;
					}
				}
			} else { //PLAYER IS ALERT

				if (Vector3.Distance (transform.position, player.transform.position) < distShort) {
					Utils.findiNode (gameObject, worldInfo);
					//continues to face player
				}
			}
		} else { //PLAYER DOES NOT SEE
			if (isAlert) {
				if (Vector3.Distance (transform.position, player.transform.position) < distShort) {
					playerScript.scare (5);

				} else if (Vector3.Distance (transform.position, player.transform.position) < distMed) {
					targetNode = Utils.findCloseHiding (gameObject, worldInfo);
					state = GhostState.hiding;
					moveAgainTime = 10;
				} else {
					targetNode = Utils.findCloseHiding (gameObject, worldInfo);
					state = GhostState.hiding;
					moveAgainTime = 10;
				}
			}
		}
	}

	public void interactionUpdate(){
		seePlayer = canSeePlayer ();

		if (!seePlayer && isAlert) {
			StartCoroutine (deAlert ());
		}

		if (seePlayer) {
			if (isHiding) {
				StartCoroutine (ghostReact (fastReact));
			} else {
				StartCoroutine (ghostReact (slowReact));
			}
		} 

		if (playerSees) {
			StartCoroutine (playerReact ());
		} else {
			if (playerAlert) {
				StartCoroutine (playerDeAlert ());
			}
		}
	}

	public void receiveHint(Vector3 hintLocation){
		targetNode = Utils.findHidingFromHint (gameObject, worldInfo, hintLocation);
		state = GhostState.wandering;
		moveAgainTime = 10;
	}
}
