using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private TextMeshProUGUI _timeText;
    [SerializeField]
    private TextMeshProUGUI _temperatureText;
    [SerializeField]
    private Slider _temperatureSlider;
    [SerializeField]
    private TextMeshProUGUI _humidityText;
    [SerializeField]
    private Slider _humiditySlider;
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
            MovePanelAnime(_bottomRect, new Vector2(_bottomRect.anchoredPosition.x, 0), 0.7f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            MovePanelAnime(_rightRect, new Vector2(_rightRect.sizeDelta.x, 0), 0.7f);
            MovePanelAnime(_leftRect, new Vector2(-_rightRect.sizeDelta.x, 0), 0.7f);
            MovePanelAnime(_bottomRect, new Vector2(_bottomRect.anchoredPosition.x, -_bottomRect.sizeDelta.y), 0.7f);
        }
    }

    public void TodayUI()
    {
        _todayText.SetText(DateTime.Now.ToString("yyyy. MM. dd"));
        _timeText.SetText(DateTime.Now.ToString("HH : mm : ss"));
    }

    public void EnvironmentDataUI(float temperature,float humidity)
    {
        _temperatureText.SetText($"{temperature.ToString()}¡ÆC");
        _temperatureSlider.value = temperature / 100;
        _humidityText.SetText($"{humidity.ToString()}%");
        _humiditySlider.value = humidity / 100;
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
