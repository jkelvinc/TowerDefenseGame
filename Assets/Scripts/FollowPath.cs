using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour 
{
	[SerializeField]
	private float speed = 1f;

	private Path path;
	private PathNode targetNode;
	private PathNode previousNode;


	public void FixedUpdate()
	{
		if (this.targetNode != null)
		{
			if (HasReachedTarget())
			{
				this.previousNode = this.targetNode;
				GetNextTargetNode(this.targetNode);
				UpdateRotation();
			}
			else
			{
				UpdatePosition();
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
		UpdateRotation();
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

	private void UpdateRotation() 
	{
		Vector3 previousPos = (this.previousNode == null) ? transform.position : this.previousNode.transform.position;
		Vector3 direction = (this.targetNode.transform.position - previousPos).normalized;
		
		float rotationAngle = Mathf.Atan2(direction.y, direction.x) * 180f / Mathf.PI;
		transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
	}

	private void UpdatePosition()
	{
		transform.position = Vector2.MoveTowards(transform.position, this.targetNode.transform.position, speed * Time.fixedDeltaTime);
	}
}
