using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffectSlow : MonoBehaviour 
{
	private float slowPercentage;
	private float duration;

	private FollowPath followPath;
	private float originalSpeed;

	public void Init(float slowPercentage, float duration)
	{
		this.slowPercentage = slowPercentage;
		this.duration = duration;

		this.followPath = GetComponent<FollowPath>();
		this.originalSpeed = this.followPath.Speed;
		this.followPath.Speed *= this.slowPercentage;

		StartCoroutine(SlowCountdown());
	}

	private IEnumerator SlowCountdown()
	{
		yield return new WaitForSeconds(this.duration);
		this.followPath.Speed = originalSpeed;

		Destroy(this);
	}
}
