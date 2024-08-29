using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotArmDataPanelUI : MonoBehaviour
{

    #region LeftPanel
    [Header("LeftPanel")]
    [SerializeField]
    private RectTransform _leftRect;

    [SerializeField] private TextMeshProUGUI _secText;
    [SerializeField] private TextMeshProUGUI _minText;
    [SerializeField] private TextMeshProUGUI _hourText;

    [SerializeField]

    #endregion


    #region RightPanel
    [Header("RightPanel")]
    private RectTransform _rightRect;

    private string[] _uiDataStr;

    [SerializeField] private TextMeshProUGUI _todayText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _temperatureText;
    [SerializeField] private Slider _temperatureSlider;
    [SerializeField] private TextMeshProUGUI _humidityText;
    [SerializeField] private Slider _humiditySlider;
    [SerializeField] private TextMeshProUGUI _powerConsumptionText;
    [SerializeField] private ChatPanelUI _robotArmDataUI;
    [SerializeField] private TextMeshProUGUI _machineInfoText;
    #endregion


    #region BottomPanel
    [Header("BottomPanel")]
    [SerializeField]
    private RectTransform _bottomRect;
    #endregion

    private void Start()
    {
        StreamReader sr = new StreamReader($"{Application.dataPath}/Save/UIData.txt");
        _uiDataStr = sr.ReadToEnd().Split("\n");
    }

    public void MovePanelAnime(RectTransform rect, Vector2 pos,float duration)
    {
        rect.DOAnchorPos(pos, duration);
    }

    private void Update()
    {
        TodayUI();

        PlayTimeUI();
        if (GameManager.Instance.IsPlayGame)
        {
            
            switch (GameManager.Instance.PLAY_MODE)
            {
                case GameManager.RobotArmPlayMode.None:
                    break;
                case GameManager.RobotArmPlayMode.Live:
                    break;
                case GameManager.RobotArmPlayMode.Simulation:
                    SimulationUIValue();
                    break;
            }
        }



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

    private void SimulationUIValue()
    {
        if (GameManager.Instance.SIMULATION_IDX >= _uiDataStr.Length) return;

        string[] datas = _uiDataStr[GameManager.Instance.SIMULATION_IDX].Split(',');
        EnvironmentDataUI(float.Parse(datas[0]), float.Parse(datas[1]));
        PowerDataUI(float.Parse(datas[2]));
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

    public void PlayTimeUI()
    {
        float time = GameManager.Instance.PLAYTIME;

        int second = (int)time%60;
        int min = (int)time /60;
        int hour = (int)min /60;

        _secText.SetText(second.ToString());
        _minText.SetText(min.ToString());
        _hourText.SetText(hour.ToString());
    }
}
