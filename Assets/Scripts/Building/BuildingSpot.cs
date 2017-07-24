using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSpot : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private BuildPanel buildPanel;


	private void Start()
	{
		this.buildPanel.OnBuildingCreated += HandleBuildingCreated;
	}

	private void OnDestroy()
	{
		this.buildPanel.OnBuildingCreated -= HandleBuildingCreated;
	}

	private void HandleBuildingCreated()
	{
		gameObject.GetComponent<Renderer>().enabled = false;
	}

	#region IPointerClickHandler Implementation
	public void OnPointerClick(PointerEventData eventData)
	{
		if (this.buildPanel != null)
		{
			this.buildPanel.Show();
		}
	}
	#endregion
}
