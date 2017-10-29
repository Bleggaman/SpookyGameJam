using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour {

    public Rigidbody rb;
    public float moveSpeed = 10;
    public float sneakSpeed = 5;
    public GameObject equipment;
    private Vector3 moveVector = Vector3.zero;

    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        moveVector = Vector3.zero;
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");
        moveVector = new Vector3(-movH, 0, -movV).normalized;

        // Determines how fast the character should move
        float moveModifier;
        if (Input.GetKey(KeyCode.RightShift)){
            moveModifier = sneakSpeed;
        } else {
            moveModifier = moveSpeed;
        }
        // Applies movement
        rb.velocity = moveVector * moveModifier;


        if (Input.GetKeyDown(KeyCode.Return)) {
            // USE EQUIPMENT TO SCARE
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            // PICKUP / DROP EQUIPMENT
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            // PAUSE GAME / VIEW SHOP
        }
	}
}
