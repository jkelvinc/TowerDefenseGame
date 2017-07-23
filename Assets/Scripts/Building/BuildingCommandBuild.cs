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
		int cost = GetBuildingCost();

		if (CanBuild(cost))
		{
			ResourceManager.Instance.SpendResource(ResourceType.Currency, cost);

			GameObject buildingGO = Instantiate(buildingPrefab, buildingHolder.transform);
			buildingGO.transform.localPosition = Vector3.zero;
		}
	}

	private bool CanBuild(int cost)
	{
		return (ResourceManager.Instance.GetResourceValue(ResourceType.Currency) - cost > 0);
	}

	private int GetBuildingCost()
	{
		var cost = buildingPrefab.GetComponent<Cost>();
		return (cost == null) ? 0 : cost.Value;
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
