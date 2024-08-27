using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotArmDataPanelUI : MonoBehaviour
{

    #region LeftPanel
    [Header("LeftPanel")]
    [SerializeField]
    private RectTransform _leftRect;
    #endregion


    #region RightPanel
    [Header("RightPanel")]
    [SerializeField]
    private RectTransform _rightRect;

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
    #endregion


    #region BottomPanel
    [Header("BottomPanel")]
    [SerializeField]
    private RectTransform _bottomRect;
    #endregion

    public void MovePanelAnime(RectTransform rect, Vector2 pos,float duration)
    {
        rect.DOAnchorPos(pos, duration);
    }

    private void Update()
    {
        TodayUI();
        EnvironmentDataUI(21, 36);

        if(Input.GetMouseButtonDown(0))
        {
            MovePanelAnime(_rightRect, new Vector2(0, 0), 0.7f);
            MovePanelAnime(_leftRect, new Vector2(0, 0), 0.7f);
            MovePanelAnime(_bottomRect, new Vector2(0, 0), 0.7f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            MovePanelAnime(_rightRect, new Vector2(_rightRect.sizeDelta.x, 0), 0.7f);
            MovePanelAnime(_leftRect, new Vector2(-_rightRect.sizeDelta.x, 0), 0.7f);
            MovePanelAnime(_bottomRect, new Vector2(0, -_bottomRect.sizeDelta.y), 0.7f);
        }
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
