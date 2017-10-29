using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : ScareMapObject, iActivate, iHidingSpot, iScare  {

	Collider collider;
	GameObject sender;

	bool wellRising;
	public float wellUp;
	private float wellRiseSpeed = .5f;
	bool wellRisen = false;
	public ScareEquipItem bucketPrefab;
	bool bucketTaken = false;

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider> ();


	}

	// Update is called once per frame
	void Update () {
		//		Debug.Log (lampOn);
		if (wellRising){
			wellUp += wellRiseSpeed;
		}
		if (wellUp > 50) {
			wellRisen = true;
		}
	}

	public override void activate(GameObject characterActivated){
		sender = characterActivated;
		wellRising = true;

		if (wellRisen && !bucketTaken) {
			CharacterScript charScript = sender.GetComponent<CharacterScript> ();
			for (int i = 0; i < charScript.equipItems.Length; i++) {
				if (charScript.equipItems [i] == null && !bucketTaken) {
					ScareEquipItem bucket = Instantiate (bucketPrefab, transform.position, transform.rotation);
					bucket.GetComponent<ScareEquipItem> ()._unit = sender;
					charScript.equipItems [i] = bucket;
					bucketTaken = true;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other){

		//does nothing
	}

	#region iHidingSpot implementation

	public void iHide ()
	{
		Debug.Log ("Hiding in well");
	//	throw new System.NotImplementedException ();
	}

	#endregion
}
