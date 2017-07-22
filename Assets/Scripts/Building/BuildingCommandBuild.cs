using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingCommandBuild : MonoBehaviour, ICommand, IPointerClickHandler
{
	[SerializeField]
	private GameObject buildingPrefab;

	[SerializeField]
	private GameObject buildingHolder;


	public event Action<string> OnCommandExecuted;

	private void CreateBuilding()
	{
		GameObject buildingGO = Instantiate(buildingPrefab, buildingHolder.transform);
		buildingGO.transform.localPosition = Vector3.zero;
	}
	
	#region IPointerClickHandler Implementation
	public void OnPointerClick(PointerEventData eventData)
	{
		CreateBuilding();

		if (this.OnCommandExecuted != null)
		{
			this.OnCommandExecuted("Build");
		}
	}
	#endregion
}
