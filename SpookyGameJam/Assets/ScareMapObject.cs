using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScareMapObject : ScareProp , iNode {

	public List<Node> nodeList = new List<Node>();
	public float activateRange = 5;


	public Node nextNode ()
	{
		return nodeList[0];
	}

}


