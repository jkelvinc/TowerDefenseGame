using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour 
{
	public event Action<Unit> OnUnitDestroyed;

	private Health health;


	private void Awake()
	{
		this.health = GetComponentInChildren<Health>();
		if (this.health != null)
		{
			this.health.OnHealthChanged += HandleHealthChanged;
		}
	}

	private void HandleHealthChanged(int currentHealth)
	{
		if (currentHealth == 0)
		{
			if (OnUnitDestroyed != null)
			{
				OnUnitDestroyed(this);
			}

			Destroy(gameObject);
		}
	}
}
