using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmPanelUI : MonoBehaviour
{
    [SerializeField] private ChatPanelUI chatPanelUI;

    private void Awake()
    {
        EventManager.ListenEvent(EventTrigger.EventType.Alarm, Alarm);
    }
    public void Alarm(EventParam eventParam)
    {
        chatPanelUI.AddText(eventParam.strParam);
    }
}
