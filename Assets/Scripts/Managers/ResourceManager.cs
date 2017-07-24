using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum ResourceType
{
	Live,
	Currency
}

public class ResourceManager : Singleton<ResourceManager> 
{
	public delegate void ResourceChangedDelegate(ResourceType type, int newValue);

	public event ResourceChangedDelegate OnResourceChanged;


	[System.Serializable]
	public class ResourceInfo
	{
		public ResourceType Type;
		public int Value;
	}


	[SerializeField]
	private List<ResourceInfo> resourceInfo;


	public int GetResourceValue(ResourceType type)
	{
		var resourceInfo = this.resourceInfo.Find(x => x.Type == type);
		return resourceInfo == null ? 0 : resourceInfo.Value;
	}

	public void DecreaseResource(ResourceType type, int value)
	{
		var resourceInfo = this.resourceInfo.Find(x => x.Type == type);
		if (resourceInfo != null)
		{
			resourceInfo.Value -= value;
			if (resourceInfo.Value < 0)
			{
				resourceInfo.Value = 0;
			}

			if (OnResourceChanged != null)
			{
				OnResourceChanged(type, resourceInfo.Value);
			}
		}
	}

	public void IncreaseResource(ResourceType type, int value)
	{
		var resourceInfo = this.resourceInfo.Find(x => x.Type == type);
		if (resourceInfo != null)
		{
			resourceInfo.Value += value;

			if (OnResourceChanged != null)
			{
				OnResourceChanged(type, resourceInfo.Value);
			}
		}
	}
}
