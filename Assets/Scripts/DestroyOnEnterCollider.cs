using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnterCollider : MonoBehaviour 
{
	[SerializeField]
	private string colliderTag;

	[SerializeField]
	private float destroyDelayInSeconds;


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == this.colliderTag)
		{
			if (this.destroyDelayInSeconds > 0f)
			{
				StartCoroutine(DelayedDestroy());
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

	private IEnumerator DelayedDestroy()
	{
		yield return new WaitForSeconds(destroyDelayInSeconds);
		Destroy(gameObject);
	}
}
