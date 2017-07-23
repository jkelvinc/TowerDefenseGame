using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LoseCondition : MonoBehaviour 
{
	public event Action OnConditionChanged;

	public virtual bool FulfilledCondition { get; protected set; }

	protected void RaiseConditionChangedEvent()
	{
		if (this.OnConditionChanged != null)
		{
			this.OnConditionChanged();
		}
	}
}
