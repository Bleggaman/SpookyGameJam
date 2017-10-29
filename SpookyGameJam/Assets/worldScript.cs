using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScript : MonoBehaviour {

	public ScareMapObject[] scareMapObjects;

	// Use this for initialization
	void Start () {
		scareMapObjects = GameObject.FindObjectsOfType(typeof(ScareMapObject)) as ScareMapObject[];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


