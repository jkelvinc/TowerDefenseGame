using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour 
{
	public event Action<Unit> OnUnitDestroyed;

	private Health health;

	private UnityAction gameLostAction;


	private void Awake()
	{
		this.health = GetComponentInChildren<Health>();
		if (this.health != null)
		{
			this.health.OnHealthChanged += HandleHealthChanged;
		}
	}

	private void Start()
	{
		this.gameLostAction = new UnityAction(HandleGameLost);
		EventManager.Instance.StartListening(GameEvents.GameLost, this.gameLostAction);
	}

	private void OnDestroy()
	{
		if (EventManager.Exists)
		{
			EventManager.Instance.StopListening(GameEvents.GameLost, this.gameLostAction);
		}
	}


	public void DestroyUnit()
	{
		if (OnUnitDestroyed != null)
		{
			OnUnitDestroyed(this);
		}

		Destroy(gameObject);
	}

	private void HandleGameLost()
	{
		Destroy(gameObject);
	}

	private void HandleHealthChanged(int currentHealth)
	{
		if (currentHealth == 0)
		{
			var changeResourceAmount = GetComponent<ChangeResourceAmount>();
			if (changeResourceAmount != null)
			{
				changeResourceAmount.Trigger();
			}

			DestroyUnit();
		}
	}
}
