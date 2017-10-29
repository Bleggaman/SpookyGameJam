using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScareProp : MonoBehaviour, iScare, iActivate {
	
	public int scareValue = 1;

	public virtual void scare(GameObject sender, GameObject receiver){
		iScarable unitScript = receiver.GetComponent<iScarable> ();

		unitScript.scare (scareValue);
		//Debug.Log ("scared by " + sender);
	}
		
	public virtual void activate (GameObject playerActived)
	{
		Debug.Log ("activated " + gameObject);
	}
}


