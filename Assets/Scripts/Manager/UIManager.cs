using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIEvent
{
    None,
    //Btn
    //Status
    Final,
}

public class UIManager : Singleton<UIManager>
{
    private Dictionary<UIEvent, Action<EventParam>> _uiEventActionDictionary = new Dictionary<UIEvent, Action<EventParam>>();

    public void ListenEvent(UIEvent eventName, Action<EventParam> action)
    {
        if (_uiEventActionDictionary.ContainsKey(eventName))
        {
            _uiEventActionDictionary[eventName] += action;
        }
        else
        {
            _uiEventActionDictionary.Add(eventName, action);
        }
    }

    public void RemoveEvent(UIEvent eventName, Action<EventParam> action)
    {
        if (_uiEventActionDictionary.ContainsKey(eventName))
        {
            _uiEventActionDictionary[eventName] -= action;
        }
        else
        {
            Debug.LogError($"There is no event function to delete. | EventName : {eventName}");
        }
    }

    public void TriggerEvent(UIEvent eventName,EventParam param)
    {
        if (_uiEventActionDictionary.ContainsKey(eventName))
        {
            _uiEventActionDictionary[eventName]?.Invoke(param);
        }
        else
        {
            Debug.LogError($"There is no event function to trigger. | EventName : {eventName}");
        }
    }
}
