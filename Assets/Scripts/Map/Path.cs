using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class Path : MonoBehaviour 
{
	private List<PathNode> pathNodes;

	
	private void Awake()
	{
		// find all the nodes on the path
		var pathNodes = GetComponentsInChildren<PathNode>();
		this.pathNodes = new List<PathNode>(pathNodes);

		Assert.IsTrue(this.pathNodes.Count > 0, "[Path] Path has no nodes!");
	}

	public PathNode GetNextPathNode(PathNode current)
	{
		if (current == null)
		{
			return this.pathNodes[0];
		}

		int nodeIndex = current.transform.GetSiblingIndex();

		if (nodeIndex < transform.childCount - 1)
		{
			++nodeIndex;
			return this.pathNodes[nodeIndex];
		}
		
		// we're at the end of the path
		return null;
	}


}
