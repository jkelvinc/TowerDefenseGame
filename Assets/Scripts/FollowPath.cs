using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour 
{
	[SerializeField]
	private float speed = 1f;

	private Path path;
	private PathNode targetNode;


	public void FixedUpdate()
	{
		if (this.targetNode != null)
		{
			if (HasReachedTarget())
			{
				GetNextTargetNode(this.targetNode);
			}
			else
			{
				transform.position = Vector2.MoveTowards(transform.position, this.targetNode.transform.position, speed * Time.fixedDeltaTime);
			}
		}
	}

	public void Execute(Path path)
	{
		if (path == null)
		{
			return;
		}

		this.path = path;
		GetNextTargetNode(null);
	}

	private void GetNextTargetNode(PathNode currentNode)
	{
		this.targetNode = this.path.GetNextPathNode(currentNode);
	}

	private bool HasReachedTarget()
	{
		return (Mathf.Approximately(this.targetNode.transform.position.x, transform.position.x) &&
				Mathf.Approximately(this.targetNode.transform.position.y, transform.position.y));
	}
}
