using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotArmDataPanelUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _todayText;
    [SerializeField]
    private TextMeshProUGUI _environmentText;
    [SerializeField]
    private TextMeshProUGUI _powerConsumptionText;
    [SerializeField]
    private ChatPanelUI _robotArmDataUI;
    [SerializeField]
    private TextMeshProUGUI _machineInfoText;



    private void Update()
    {
        TodayUI();
        EnvironmentDataUI(21, 36);
    }

    public void TodayUI()
    {
        string str = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
        _todayText.SetText(str);
    }

    public void EnvironmentDataUI(float temperature,float humidity)
    {
        string str = $"Temperature : {temperature}\nHumidity : {humidity}";
        _environmentText.SetText(str);
    }

    public void PowerDataUI(float value)
    {
        _powerConsumptionText.SetText(value.ToString());
    }

    public void RobotMoveDataUI(string str)
    {
        _robotArmDataUI.AddText(str);
    }

    public void MachineInfoDataUI()
    {

    }


}
