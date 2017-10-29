﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : ScareEquipItem, iActivate {

	public bool lightOn = false;
	public Collider collider;
	public MeshRenderer mesh;
	public bool scaredCoolDown;

	public FlashLight(GameObject unit){
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
	}

	public override void activate (GameObject playerActived)
	{
		lightOn = !lightOn;

		mesh.enabled = !mesh.enabled;

	}

	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Ghost")) {  //one of the things to be changed if make for two player
			//have to make it so it compares tag to be either player or ghost but not the player that is holding the objects
			if (lightOn && !scaredCoolDown) {
				Debug.Log ("adf'_;");
				scare (_unit, other.gameObject); 
				StartCoroutine (scareCool ());
			}
		}
	}

	IEnumerator scareCool()
	{
		scaredCoolDown = true;
		yield return new WaitForSeconds(3);
		scaredCoolDown = false;
	}
}
