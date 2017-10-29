using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement2 : MonoBehaviour {

	public float moveSpeed;
	public float crouchSpeed;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		Vector3 velocityVector = Vector3.zero;
		if (Input.GetKey(KeyCode.UpArrow)){
			velocityVector.z += moveSpeed;
		}
		if (Input.GetKey(KeyCode.LeftArrow)){
			velocityVector.x -= moveSpeed;
		}
		if (Input.GetKey(KeyCode.DownArrow)){
			velocityVector.z -= moveSpeed;
		}
		if (Input.GetKey(KeyCode.RightArrow)){
			velocityVector.x += moveSpeed;
		}
		rb.velocity = velocityVector;
	}
}
