using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScareEquipItem : ScareProp, iScare, iActivate {
	
	public GameObject _unit;

	public virtual void scare (GameObject sender, GameObject receiver)
	{
		//throw new System.NotImplementedException ();
		Debug.Log("scared by flashlight");
	}


	public abstract void activate (GameObject playerActived);
}


