using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Transform player;
    public Vector3 cameraTarget;
    public Vector3 CameraOffset;

	void Start () {
        player = GameManager.gameManager.player;
	}
	
	void Update () {
        // moves the camera to an offset of the player character
        cameraTarget = player.transform.position;
        transform.position = cameraTarget + CameraOffset;
    }
}
