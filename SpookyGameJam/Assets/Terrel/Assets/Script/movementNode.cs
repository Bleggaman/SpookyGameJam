using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementNode : MonoBehaviour {

    // 2 - 5 nodes
    public movementNode[] nodes;
    // average wait time once reaching the node
    public float waitPeriod = 3f;
    // how close to the node should you get 
    public float range = 0.5f;
    // hiding spots nearby
    public Transform[] hidingSpots;

    // Draws lines between nodes for debuging
    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        for (int i = 0; i < nodes.Length; i++)
            if (nodes[i] != null)
                Gizmos.DrawLine(transform.position, transform.position +(nodes[i].transform.position- transform.position)/3);
    }
}
