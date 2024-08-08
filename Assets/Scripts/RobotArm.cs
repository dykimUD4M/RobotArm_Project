using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class RobotAxisInfo
{
    public string axisName;
    public Quaternion quaternion = Quaternion.identity;
}

[Serializable]
public class RobotArmInfo
{
    public bool isAir = false;
    public bool isRedBall = false;
    public List<RobotAxisInfo> axisInfoList;
}

public class RobotArm : MonoBehaviour
{
    private Dictionary<string, Transform> m_robotAxisDictionary = new Dictionary<string, Transform>();

    [SerializeField,]
    private bool _smoothMoving = false;
    [SerializeField]
    private float _smoothSpeed = 0.2f;
    [SerializeField]
    private TextMeshProUGUI _textMeshProUGUI;

    [SerializeField, Header("AirCompressor_Module")]
    private AirCompressor _airCompressor;
    [SerializeField, Header("AirCompressor_Module")]
    private ArmSlide _armSlide;

    private GameObject _ball;
    private bool _isAir = false;

    private void Start()
    {
        RobotArmAxisInit();
        _textMeshProUGUI.SetText(_smoothSpeed.ToString());
    }

    public void AddSmoothSpeed(float value)
    {
        _smoothSpeed += value;
        if (_smoothSpeed <= 0.1f)
            _smoothSpeed = 0.1f;
        _textMeshProUGUI.SetText(_smoothSpeed.ToString());
    }

    private void RobotArmAxisInit()
    {
        bool isFind = true;
        Transform trm = transform;
        while(isFind)
        {
            //model 
            for(int i = 0;i<trm.childCount;i++)
                if (trm.GetChild(i).name.Contains("axis"))
                {
                    string name = trm.GetChild(i).name.Replace("axis_", "");
                    m_robotAxisDictionary.Add(name, trm.GetChild(i));
                    trm = trm.GetChild(i);
                    break;
                }

            if (trm.childCount <= 0)
                isFind = false;
        }
    }

    int _idx = 0;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            _idx++;
            if (_idx >= 14)
                _idx = 14;
            TestGetInfo(_idx);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _idx--;
            if (_idx <= 0)
                _idx = 0;
            TestGetInfo(_idx);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            TestSave(_idx);
        }
    }

    public void PlayRobotArm(int idx)
    {
        _idx += idx;
        if (_idx >= 14)
            _idx = 14;
        if (_idx <= 0)
            _idx = 0;

        TestGetInfo(_idx);
    }

    public void TestGetInfo(int idx)
    {
        Debug.Log(idx);
        RobotArmInfo list = JsonLoader.LoadJsonFile<RobotArmInfo>($"{Application.dataPath}/Save/Json", idx.ToString());

        foreach (RobotAxisInfo info in list.axisInfoList)
        {
            if (m_robotAxisDictionary.ContainsKey(info.axisName))
            {
                if(_smoothMoving)
                    m_robotAxisDictionary[info.axisName].DORotate(info.quaternion.eulerAngles, _smoothSpeed);
                else
                    m_robotAxisDictionary[info.axisName].rotation = info.quaternion;
            }
            else
            {
                Debug.LogError($"There is no axis whose name matches the received data | Number : {idx}");
            }
        }

        _airCompressor.IsAir = list.isAir;
    }

    public void TestGetInfo(string json)
    {
        RobotArmInfo list = JsonLoader.JsonToObject<RobotArmInfo>(json);

        foreach (RobotAxisInfo info in list.axisInfoList)
        {
            if(m_robotAxisDictionary.ContainsKey(info.axisName))
            {
                if (_smoothMoving)
                    m_robotAxisDictionary[info.axisName].DORotate(info.quaternion.eulerAngles, _smoothSpeed);
                else
                    m_robotAxisDictionary[info.axisName].rotation = info.quaternion;
            }
            else
            {
                Debug.LogError($"There is no axis whose name matches the received data | Name : {info.axisName}");
            }
        }
    }

    public void TestSave(int idx)
    {
        RobotArmInfo list = new RobotArmInfo();
        list.axisInfoList = new List<RobotAxisInfo>();

        foreach(var a in m_robotAxisDictionary.Keys)
        {
            RobotAxisInfo info = new RobotAxisInfo();
            info.axisName = a;
            info.quaternion = m_robotAxisDictionary[a].rotation;
            list.axisInfoList.Add(info);
        }

        string json = JsonLoader.ObjectToJson(list);
        JsonLoader.SaveJsonFile($"{Application.dataPath}/Save/Json", idx.ToString(), json);

        Debug.Log($"Save Json Idx : {idx}");
    }
}
