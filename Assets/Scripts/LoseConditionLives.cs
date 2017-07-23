using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseConditionLives : LoseCondition 
{

	private void Start()
	{
		ResourceManager.Instance.OnResourceChanged += HandleResourceChanged;
	}

	private void HandleResourceChanged(ResourceType type, int value)
	{
		this.FulfilledCondition = (type == ResourceType.Live) && (value == 0);

		RaiseConditionChangedEvent();
	}
}
