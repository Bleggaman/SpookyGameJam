using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public Transform mainCam;
    public Transform player;
    public Transform nodeParent;
    public List<movementNode> nodeList;

	// Use this for initialization
	void Start () {
        // Singleton pattern
        if (gameManager == null) {
            gameManager = this;
        } else {
            Debug.LogError("There is already a Game Manager in the scene");
        }
        // Populate Node list
        if (nodeParent!= null) {
            foreach (Transform child in nodeParent) {
                nodeList.Add(child.GetComponent<movementNode>());
            }
        }
    }
}
