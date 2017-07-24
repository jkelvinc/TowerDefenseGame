using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePanel : MonoBehaviour 
{
	[SerializeField]
	private Text livesValueText;

	[SerializeField]
	private Text currencyValueText;


	private void Start()
	{
		UpdateValues();

		ResourceManager.Instance.OnResourceChanged += HandleResourceChanged;
	}

	private void UpdateValues()
	{
		HandleResourceChanged(ResourceType.Live, ResourceManager.Instance.GetResourceValue(ResourceType.Live));
		HandleResourceChanged(ResourceType.Currency, ResourceManager.Instance.GetResourceValue(ResourceType.Currency));
	}

	private void HandleResourceChanged(ResourceType type, int newValue)
	{
		switch (type)
		{
			case ResourceType.Live:
			if (livesValueText != null)
			{
				livesValueText.text = newValue.ToString();
			}
			break;

			case ResourceType.Currency:
			if (currencyValueText != null)
			{
				currencyValueText.text = newValue.ToString();
			}
			break;
		}
	}
}
