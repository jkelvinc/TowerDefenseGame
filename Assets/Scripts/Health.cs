using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour 
{
	public delegate void HealthChangeDelegate(int currentHealth);

	public event HealthChangeDelegate OnHealthChanged;

	[SerializeField]
	private int maxHealth;

	[SerializeField]
	private Transform visual;


	private int currentHealth;
	private float originalVisualScaleX;


	private void Awake()
	{
		this.currentHealth = this.maxHealth;

		if (this.visual != null)
		{
			this.originalVisualScaleX = this.visual.localScale.x;
		}
	}

	public void ReduceHealth(int value)
	{
		this.currentHealth -= value;
		if (this.currentHealth < 0)
		{
			this.currentHealth = 0;
		}

		UpdateVisual();

		if (OnHealthChanged != null)
		{
			OnHealthChanged(this.currentHealth);
		}
	}

	private void UpdateVisual()
	{
		if (this.visual != null)
		{
			Vector3 currentScale = this.visual.localScale;
			currentScale.x = ((float)this.currentHealth / (float)this.maxHealth) * this.originalVisualScaleX;
			this.visual.localScale = currentScale;	
		}
	}
}
