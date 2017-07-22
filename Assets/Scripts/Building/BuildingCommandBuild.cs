using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingCommandBuild : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private GameObject buildingPrefab;

	[SerializeField]
	private GameObject buildingHolder;


	
	#region IPointerClickHandler Implementation
	public void OnPointerClick(PointerEventData eventData)
	{
		GameObject buildingGO = Instantiate(buildingPrefab, buildingHolder.transform);
		buildingGO.transform.localPosition = Vector3.zero;

		EventDispatcher.Instance.DispatchEvent(GameEvent.BuildingCommandExecuted, null);
	}
	#endregion
}
