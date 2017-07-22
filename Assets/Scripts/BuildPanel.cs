using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPanel : MonoBehaviour 
{
	void Start()
	{
		EventDispatcher.Instance.RegisterEvent(GameEvent.BuildingCommandExecuted, HandleBuildingCommandExecuted);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	private void HandleBuildingCommandExecuted(Dictionary<string, object> eventParams)
	{
		Hide();
	}
}
