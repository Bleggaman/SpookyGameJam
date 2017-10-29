using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : ScareEquipItem, iActivate {

	public bool lightOn = false;
	public Collider collider;
	public MeshRenderer mesh;

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
		Debug.Log ("adf");
		mesh.enabled = !mesh.enabled;

	}

	void OnTriggerEnter(Collider other){
		if (lightOn) {
			Debug.Log ("adf'_;");
			scare (_unit, other.gameObject); 
		}
	}
}
