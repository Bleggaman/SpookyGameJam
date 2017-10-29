using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utils {


	public static iNode getFirstNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[0];
	}

	public static iNode getLastNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[nodes.Count -1];
	}

	public static iNode getRandomNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[(int) Random.Range (0, nodes.Count)];

	}
}
