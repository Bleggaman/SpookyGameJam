using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utils {


	//FIND: not already on a iNode, identifying iNode to go to
	public static iNode findiNode(GameObject ghost, WorldInfo worldInfo){

		return worldInfo.iNodes [0];
			

	}

	public static iNode findHidingSpotNode(GameObject ghost, WorldInfo worldInfo){

		return worldInfo.iNodes
			.Where (asdf => asdf is iHidingSpot)
			.FirstOrDefault ();
	}

	//GET: on an iNode, choosing next iNode
	public static iNode getFirstiNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[0];
	}

	public static iNode getLastiNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[nodes.Count -1];
	}

	public static iNode getRandomiNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[(int) Random.Range (0, nodes.Count)];

	}

	public static iNode getCloseiNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[(int) Random.Range (0, nodes.Count)];

	}

	public static iNode getCloseHidingSpot(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[(int) Random.Range (0, nodes.Count)];

	}

	public static iNode getStrategicHidingSpot(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[(int) Random.Range (0, nodes.Count)];

	}


}
