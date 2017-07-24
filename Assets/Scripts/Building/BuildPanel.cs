using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BuildPanel : MonoBehaviour 
{
	public event Action OnBuildingCreated;

	private List<ICommand> commands;

	void Start()
	{
		this.commands = new List<ICommand>(transform.GetComponentsInChildren<ICommand>());
		foreach (ICommand c in this.commands)
		{
			c.OnCommandExecuted += HandleBuildingCommandExecuted;
		}
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	private void HandleBuildingCommandExecuted(string type)
	{
		if (type == "Build")
		{
			Hide();
			if (this.OnBuildingCreated != null)
			{
				this.OnBuildingCreated();
			};
		}
	}
}
