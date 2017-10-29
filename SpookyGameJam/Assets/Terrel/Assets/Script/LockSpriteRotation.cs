using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSpriteRotation : MonoBehaviour {

    public Quaternion rotation;

	// Use this for initialization
	void Start () {
        rotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = rotation;
	}
}
