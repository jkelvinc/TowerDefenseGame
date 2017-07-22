using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSpot : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private BuildPanel buildingCommands;


	private void Start()
	{
		// EventDispatcher.Instance.RegisterEvent(GameEvent.BuildingCommandExecuted, HandleBuildingCommandExecuted);
	}

	private void HandleBuildingCommandExecuted(Dictionary<string, object> eventParams)
	{
		gameObject.GetComponent<Renderer>().enabled = false;
	}

	#region IPointerClickHandler Implementation
	public void OnPointerClick(PointerEventData eventData)
	{
		if (buildingCommands != null)
		{
			buildingCommands.Show();
		}
	}
	#endregion
}
