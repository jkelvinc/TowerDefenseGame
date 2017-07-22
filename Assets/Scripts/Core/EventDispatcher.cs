using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public delegate void EventDelegate(Dictionary<string, object> eventParams);

public class EventDispatcher : Singleton<EventDispatcher> 
{
	private Dictionary<GameEvent, EventDelegate> events = new Dictionary<GameEvent, EventDelegate>();

	protected override void OnSingletonAwake()
	{
		
	}

	public void RegisterEvent(GameEvent eventName, EventDelegate listener)
	{
		EventDelegate result;
		if (this.events.TryGetValue(eventName, out result))
		{
			result += listener;
		}
		else
		{
			this.events.Add(eventName, new EventDelegate(listener));
		}

		Debug.Log("[EventDispatcher] RegisterEvent() - EventName: " + eventName);
	}

	public void UnregisterEvent(GameEvent eventName, EventDelegate listener)
	{
		EventDelegate result;
		if (this.events.TryGetValue(eventName, out result))
		{
			result -= listener;
		}
	}

	public void DispatchEvent(GameEvent eventName, Dictionary<string, object> eventParams)
	{
		EventDelegate result;
		if (this.events.TryGetValue(eventName, out result))
		{
			if (result != null)
			{
				Debug.Log("[EventDispatcher] DispatchEvent() - EventName: " + eventName);
				result(eventParams);
			}
		}
	}
}
