﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : Singleton<EventManager>
{

    private Dictionary <string, UnityEvent> eventDictionary;

    // private static EventManager eventManager;

    // public static EventManager instance
    // {
    //     get
    //     {
    //         if (!eventManager)
    //         {
    //             eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

    //             if (!eventManager)
    //             {
    //                 Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
    //             }
    //             else
    //             {
    //                 eventManager.Init (); 
    //             }
    //         }

    //         return eventManager;
    //     }
    // }

    // void Init ()
    // {
    //     if (eventDictionary == null)
    //     {
    //         eventDictionary = new Dictionary<string, UnityEvent>();
    //     }
    // }

	protected override void OnSingletonAwake()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, UnityEvent>();
		}
	}

    public void StartListening (string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.AddListener (listener);
        } 
        else
        {
            thisEvent = new UnityEvent ();
            thisEvent.AddListener (listener);
            eventDictionary.Add (eventName, thisEvent);
        }
    }

    public void StopListening (string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.RemoveListener (listener);
        }
    }

    public void TriggerEvent (string eventName)
    {
        UnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.Invoke ();
        }
    }
}