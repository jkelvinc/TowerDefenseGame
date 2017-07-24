using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeResourceAmount : MonoBehaviour 
{
	public enum ChangeResourceOperation
	{
		Increase,
		Decrease
	}


	[SerializeField]
	private ResourceType type;

	[SerializeField]
	private int amount;

	[SerializeField]
	private ChangeResourceOperation operation;


	public void Trigger()
	{
		if (this.operation == ChangeResourceOperation.Increase)
		{
			ResourceManager.Instance.IncreaseResource(this.type, this.amount);
		}
		else if (this.operation == ChangeResourceOperation.Decrease)
		{
			ResourceManager.Instance.DecreaseResource(this.type, this.amount);
		}
	}
}
