using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PoolManager PoolManager_ = new PoolManager();
    public ResourceManager ResourceManager_ = new ResourceManager();

    private int simulation_idx = 0;
    public int SIMULATION_IDX
    {
        get { return simulation_idx; }
        set
        {
            simulation_idx = value;
        }
    }

    public float PLAYTIME;

    [Serializable]
    public enum RobotArmPlayMode
    {
        None = 0,
        Live = 1,
        Simulation = 2,
    }
    public RobotArmPlayMode PLAY_MODE = RobotArmPlayMode.None;
    public bool IsPlayGame = false;

    public int WorkQuanity { get; set; } = 0;

    public void StartBtn()
    {
        if (IsPlayGame) return;
        IsPlayGame = true;

        EventParam ep = new EventParam();
        ep.strParam = $"Start {PLAY_MODE.ToString()}";
        EventManager.TriggerEvent(EventTrigger.EventType.Alarm, ep);
    }
    public void StopBtn()
    {
        if (IsPlayGame ==false) return;
        IsPlayGame = false;

        EventParam ep = new EventParam();
        ep.strParam = $"Stop {PLAY_MODE.ToString()}";
        EventManager.TriggerEvent(EventTrigger.EventType.Alarm, ep);
    }

    public void ChangePlayMode(int idx)
    {
        if (PLAY_MODE == (RobotArmPlayMode)idx) return;
        PLAY_MODE = (RobotArmPlayMode)idx;
        simulation_idx = 0;
        PLAYTIME = 0;
        IsPlayGame = false;

        WorkQuanity = 0;

        EventParam ep = new EventParam();
        ep.strParam = $"Change Play Mode : {PLAY_MODE.ToString()}";
        EventManager.TriggerEvent(EventTrigger.EventType.Alarm, ep);
    }

    public string LIVE_UI_DATA = "";
    public string LIVE_ARM_DATA = "";

    private void Update()
    {
        if(IsPlayGame)
        {
            PLAYTIME += Time.deltaTime;
            if (PLAY_MODE == RobotArmPlayMode.Live)
            {
                LIVE_ARM_DATA = ClientManager.Instance.GetClientData(0);
                LIVE_UI_DATA = ClientManager.Instance.GetClientData(1);
                Debug.Log($"LIVE_ARM_DATA : {LIVE_ARM_DATA}");
                Debug.Log($"LIVE_UI_DATA : {LIVE_UI_DATA}");
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}