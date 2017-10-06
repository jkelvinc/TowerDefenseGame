using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSplash : MonoBehaviour 
{
	[SerializeField]
	private Collider rangeCollider;

	private List<GameObject> affectedGameObjects = new List<GameObject>();



	private void Start()
	{
		StartCoroutine(Activate());
	}

	private IEnumerator Activate()
	{
		EnableCollider(true);

		yield return new WaitForFixedUpdate();
		yield return new WaitForSeconds(1.0f);

		EnableCollider(false);
		Debug.Log("affected gameobjects: " + this.affectedGameObjects.Count);
	}

	private void EnableCollider(bool enabled)
	{
		if (this.rangeCollider != null)
		{
			this.rangeCollider.enabled = enabled;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			if (!affectedGameObjects.Contains(other.gameObject))
			{
				affectedGameObjects.Add(other.gameObject);
			}
		}
	}
}
