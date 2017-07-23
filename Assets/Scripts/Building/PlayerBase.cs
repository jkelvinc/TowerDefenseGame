using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour 
{
	private ChangeResourceAmount changeResourceAmount;


	private void Start()
	{
		changeResourceAmount = GetComponentInChildren<ChangeResourceAmount>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			changeResourceAmount.Trigger();
		}
	}
}
