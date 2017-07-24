using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSlow : MonoBehaviour 
{
	[SerializeField]
	[Range(0, 1)]
	private float slowPercentage;

	[SerializeField]
	private float duration;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			var effect = other.GetComponent<BulletEffectSlow>();
			if (effect == null)
			{
				effect = other.gameObject.AddComponent<BulletEffectSlow>();
				effect.Init(this.slowPercentage, this.duration);
			}
		}
	}
}
