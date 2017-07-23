﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour 
{
	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private float rateOfFire = 2f;

	private List<GameObject> targets = new List<GameObject>();

	private GameObject currentTarget;
	private float previousRateOfFireTime;


	private void FixedUpdate()
	{
		if (this.targets.Count > 0)
		{
			if (this.currentTarget == null)
			{
				// target whoever was first
				this.currentTarget = this.targets[0];
			}
		}

		if (this.currentTarget != null)
		{
			Vector3 direction = (this.currentTarget.transform.position - transform.position).normalized;
			float rotationAngle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			
			Quaternion rotation = Quaternion.AngleAxis(rotationAngle - 90f, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * 5f);

			if (Time.time - this.previousRateOfFireTime > this.rateOfFire)
			{
				FireAtTarget();
				this.previousRateOfFireTime = Time.time;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!this.targets.Contains(other.gameObject))
		{
			this.targets.Add(other.gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (this.targets.Contains(other.gameObject))
		{
			if (other.gameObject == this.currentTarget)
			{
				this.currentTarget = null;
			}

			this.targets.Remove(other.gameObject);
		}
	}

	private void FireAtTarget()
	{
		if (this.currentTarget == null)
		{
			return;
		}

		var bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
		bullet.GetComponent<Bullet>().Fire(this.currentTarget);
	}
}
