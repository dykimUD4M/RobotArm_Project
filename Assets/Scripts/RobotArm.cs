using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public int axisNum;
    public Transform trm;
    public string axis;
}

public class RobotArm : MonoBehaviour
{
    private Dictionary<string, RobotArmInfo> m_robotAxisDictionary = new Dictionary<string, RobotArmInfo>();

    [SerializeField,]
    private bool _smoothMoving = false;
    [SerializeField]
    private float _smoothSpeed = 0.2f;
    [SerializeField]
    private TextMeshProUGUI _textMeshProUGUI;

    [SerializeField]
    private ChatPanelUI _chatPanelUI;

    [SerializeField]
    private List<float> axisOffsetValueList = new List<float>();

    private string[] _robotMoveData;

    [SerializeField, Header("AirCompressor_Module")]
    private AirCompressor _airCompressor;

    private void Start()
    {
        RobotArmAxisInit();
        LoadMoveData();
        //RotateRobotArm(_idx);
        

        _textMeshProUGUI.SetText(_smoothSpeed.ToString());
    }

    public void StartBtn()
    {
        StartCoroutine(RobotCorotine(_smoothSpeed));
    }

    public void AddSmoothSpeed(float value)
    {
        _smoothSpeed += value;
        if (_smoothSpeed <= 0.1f)
            _smoothSpeed = 0.1f;
        _textMeshProUGUI.SetText(_smoothSpeed.ToString());
    }

    private void LoadMoveData()
    {
        StreamReader reader = new StreamReader($"{Application.dataPath}/Save/data.txt");
        _robotMoveData = reader.ReadToEnd().Split("\n");
        reader.Close();
    }

    private void RotateRobotArm(int idx)
    {
        Debug.Log(_robotMoveData[idx]);

        if (idx >= _robotMoveData.Length - 1)
        {
            _idx = 0;
            return;
        }

        _chatPanelUI.AddText(_robotMoveData[idx]);

        if (_robotMoveData[idx].Contains("S")) return;

        string[] datas = _robotMoveData[idx].Replace("(","").Replace(")","").Split(",");

        foreach(RobotArmInfo info in m_robotAxisDictionary.Values)
        {
            int num = info.axisNum - 1;
            if (info.axis == "x")
                if(num == 5)
                {
                    info.trm.DOLocalRotate(new Vector3(float.Parse(datas[num])* -1 + axisOffsetValueList[num], 0, 90), 0.01f);
                }
                else
                    info.trm.DOLocalRotate(new Vector3(float.Parse(datas[num]) * -1 + axisOffsetValueList[num], 0, 0), 0.01f);

            if (info.axis == "y")
                info.trm.DOLocalRotate(new Vector3(0, float.Parse(datas[num]) * -1 + axisOffsetValueList[num], 0), 0.01f);
            if (info.axis == "z")
                info.trm.DOLocalRotate(new Vector3(0, 0, float.Parse(datas[num]) * -1 + axisOffsetValueList[num]), 0.01f);
        }
    }

    IEnumerator RobotCorotine(float waitTime)
    {
        while (true)
        {

            yield return new WaitForSeconds(_smoothSpeed);
            _idx++;
            RotateRobotArm(_idx);

            //if(_idx >= 10)
            //    _idx = 0;
        }
    }

    private void RobotArmAxisInit()
    {
        bool isFind = true;
        Transform trm = transform;
        while(isFind)
        {
            //model 
            for(int i = 0;i<trm.childCount;i++)
            {
                string[] name = trm.GetChild(i).name.Split("_");
                if (name.Length == 3)
                {
                    trm = trm.GetChild(i);
                    RobotArmInfo robotArmInfo = new RobotArmInfo();
                    robotArmInfo.axisNum = int.Parse( name[1]);
                    robotArmInfo.trm = trm;
                    robotArmInfo.axis = name[2];
                    m_robotAxisDictionary.Add(name[1], robotArmInfo);

                    if (name[1] == "6")
                        isFind = false;

                    break;
                }
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
            //if (_idx >= 14)
            //    _idx = 14;
            //TestGetInfo(_idx);
            RotateRobotArm(_idx);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _idx--;
            if (_idx <= 0)
                _idx = 0;
            //TestGetInfo(_idx);
            RotateRobotArm(_idx);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //TestSave(_idx);
        }
    }

}
