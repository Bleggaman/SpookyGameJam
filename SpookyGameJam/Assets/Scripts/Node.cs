using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, iNode {

	public List<Node> nodeLinks = new List<Node>(); 
	public float waitTime;



	public Node nextNode(){

		//gets a node
		return nodeLinks [0];

	}

}
