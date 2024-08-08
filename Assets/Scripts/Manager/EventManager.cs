using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    None,
    Final
}
public class EventManager : Singleton<EventManager>
{
    private Dictionary<EventType,Action<EventParam>> eventDictionary = new Dictionary<EventType,Action<EventParam>>();

    public void ListenEvent(EventType eventName, Action<EventParam> action)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] += action;
        }
        else
        {
            eventDictionary.Add(eventName, action);
        }
    }

    public void RemoveEvent(EventType eventName, Action<EventParam> action)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= action;
        }
        else
        {
            Debug.LogError($"There is no event function to delete. | EventName : {eventName}");
        }
    }

    public void TriggerEvent(EventType eventName, EventParam param)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName]?.Invoke(param);
        }
        else
        {
            Debug.LogError($"There is no event function to trigger. | EventName : {eventName}");
        }
    }
}
