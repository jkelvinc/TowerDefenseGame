using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseConditionLives : GameCondition 
{

	private void Start()
	{
		ResourceManager.Instance.OnResourceChanged += HandleResourceChanged;
	}

	private void HandleResourceChanged(ResourceType type, int value)
	{
		this.IsFulfilled = (type == ResourceType.Live) && (value == 0);

		RaiseConditionChangedEvent();
	}
}
