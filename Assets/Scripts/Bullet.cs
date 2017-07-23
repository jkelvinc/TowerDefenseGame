using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	[SerializeField]
	private float speed = 2f;

	private GameObject target;
	private Vector3 startPosition;
	private float startTime;
	private float totalDistance;

	private void FixedUpdate()
	{
		if (this.target != null)
		{
			float distanceTravelled = (Time.time - this.startTime) * this.speed;
			transform.position = Vector3.Lerp(this.startPosition, this.target.transform.position, distanceTravelled / this.totalDistance);
			
			if (HasReachedTarget())
			{
				Destroy(gameObject);
			}
		}
	}

	public void Fire(GameObject target)
	{
		this.target = target;
		this.startPosition = transform.position;
		this.startTime = Time.time;
		this.totalDistance = Vector3.Distance(transform.position, this.target.transform.position);
	}

	private bool HasReachedTarget()
	{
		return transform.position.Equals(this.target.transform.position);
	}
}
