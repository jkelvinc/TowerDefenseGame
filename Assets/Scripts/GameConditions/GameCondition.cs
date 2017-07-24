using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameCondition : MonoBehaviour 
{
	public enum GameConditionType
	{
		Win,
		Lose
	}

	[SerializeField]
	private GameConditionType type;

	public GameConditionType Type
	{
		get { return this.type; }
	}

	public event Action<GameConditionType> OnConditionChanged;

	public virtual bool IsFulfilled { get; protected set; }

	protected void RaiseConditionChangedEvent()
	{
		if (this.OnConditionChanged != null)
		{
			this.OnConditionChanged(this.type);
		}
	}
}
