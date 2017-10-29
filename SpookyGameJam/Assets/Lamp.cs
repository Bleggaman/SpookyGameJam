using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : ScareMapObject, iActivate  {

	Collider collider;
	GameObject sender;
	bool lampOn = true; 
	new int activateRange = 10;

	public Lamp (){
		scareValue = 3;

	}

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider> ();

	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (lampOn);
	}

	public override void activate(GameObject characterActivated){
		sender = characterActivated;
		lampOn = !lampOn;

	}

	void OnTriggerEnter(Collider other){
		if (lampOn) {
			scare (sender, other.gameObject);
		}
	}
}
