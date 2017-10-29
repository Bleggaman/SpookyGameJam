﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : ScareEquipItem, iActivate {

	public Collider collider;
	public MeshRenderer mesh;
	public bool bucketSpilling;
	public bool bucketSpilled;
	public bool scaredReceived;

	public Bucket(GameObject unit){
		_unit = unit;
		 
	}

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshRenderer> ();
		mesh.enabled = false;

	}

	// Update is called once per frame
	void Update () {
		transform.position = _unit.transform.position;
		transform.position = _unit.transform.position;
	}

	public override void activate (GameObject playerActived)
	{
		if (!bucketSpilled) {
			StartCoroutine (bucketTimeSpill ());
		
		}

	}

	void OnTriggerStay(Collider other){
		if (bucketSpilling) {
			if (other.CompareTag("Ghost")){  
			if (!scaredReceived) {
				Debug.Log ("bucket spill on " + other);
				scare (_unit, other.gameObject); 
				scaredReceived = true;
				}
			}
		}
	}

	IEnumerator bucketTimeSpill(){
		bucketSpilling = true;
		mesh.enabled = true;
		yield return new WaitForSeconds (2);
		bucketSpilling = false;
		mesh.enabled = false;
		GameObject.Destroy (this);
	}
}
