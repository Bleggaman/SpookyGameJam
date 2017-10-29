using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : UnitScript {

	public bool qPressed = false;
	public ScareEquipItem[] equipItems = new ScareEquipItem[3];
	public ScareEquipItem flashLightPrefab;

	// Use this for initialization
	void Start () {
		ScareEquipItem flash = Instantiate (flashLightPrefab, transform.position, transform.rotation);
		flashLightPrefab.GetComponent<ScareEquipItem> ()._unit = gameObject;
		equipItems [0] = flash;
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.W)) {
			if (equipItems[0] != null) {
				equipItems [0].activate (gameObject);
			}
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			if (equipItems[1] != null) {
				equipItems [1].activate (gameObject);
		
			}
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			if (equipItems[2] != null) {
				equipItems [2].activate (gameObject);
			}
		}
	}



	void OnTriggerStay(Collider other){
	
		if (Input.GetKeyDown(KeyCode.Q)){
			if (other.CompareTag("MapScareObject")){
				ScareMapObject scareObject = other.GetComponent<ScareMapObject> ();
				scareObject.activate (gameObject);
			}
		}
	}


}
