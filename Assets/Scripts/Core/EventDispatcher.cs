using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public delegate void EventDelegate(Dictionary<string, object> eventParams);

public class EventDispatcher : Singleton<EventDispatcher> 
{
	private Dictionary<string, System.Delegate> events = new Dictionary<string, System.Delegate>();

	protected override void OnSingletonAwake()
	{
		
	}

	public void RegisterEvent(string eventName, EventDelegate listener)
	{
		System.Delegate result;
		if (this.events.TryGetValue(eventName, out result))
		{
			this.events[eventName] = System.Delegate.Combine(result, listener);
		}
		else
		{
			this.events.Add(eventName, new EventDelegate(listener));
		}

		Debug.Log("[EventDispatcher] RegisterEvent() - EventName: " + eventName);
	}

	public void UnregisterEvent(string eventName, EventDelegate listener)
	{
		System.Delegate result;
		if (this.events.TryGetValue(eventName, out result))
		{
			// result -= listener;
			System.Delegate.Remove(result, listener);
		}
	}

	public void DispatchEvent(string eventName, Dictionary<string, object> eventParams)
	{
		System.Delegate result;
		if (this.events.TryGetValue(eventName, out result))
		{
			if (result != null)
			{
				Debug.Log("[EventDispatcher] DispatchEvent() - EventName: " + eventName);
				result.DynamicInvoke(eventParams);
			}
		}
	}
}
