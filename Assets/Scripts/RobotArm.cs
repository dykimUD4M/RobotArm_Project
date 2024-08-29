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



    private void RobotArmAxisInit()
    {
        bool isFind = true;
        Transform trm = transform;
        while (isFind)
        {
            //model 
            for (int i = 0; i < trm.childCount; i++)
            {
                string[] name = trm.GetChild(i).name.Split("_");
                if (name.Length == 3)
                {
                    trm = trm.GetChild(i);
                    RobotArmInfo robotArmInfo = new RobotArmInfo();
                    robotArmInfo.axisNum = int.Parse(name[1]);
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

    private void Start()
    {
        RobotArmAxisInit();
        LoadMoveData();
        //RotateRobotArm(_idx);


        _textMeshProUGUI.SetText(_smoothSpeed.ToString());
        //StartBtn();
    }

    Coroutine coroutine;
    bool isStart = false;
    public void StartBtn()
    {
        if (isStart) return;
        isStart = true;
        switch(GameManager.Instance.PLAY_MODE)
        {
            case GameManager.RobotArmPlayMode.Live:
                break;
            case GameManager.RobotArmPlayMode.Simulation:
                coroutine = StartCoroutine(RobotCorotine(_smoothSpeed));
                break;
        }
        
    }

    public void StopBtn()
    {
        if(isStart == false) return;
        isStart = false;

        if(coroutine != null)
            StopCoroutine(coroutine);
    }
    private void Update()
    {
        if (GameManager.Instance.IsPlayGame == false)
            StopBtn();
    }

    #region Simulation
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

    private void RotateRobotArm(int idx,float moveTime)
    {

        if (idx >= _robotMoveData.Length - 1)
        {
            GameManager.Instance.SIMULATION_IDX = 0;
            return;
        }

        _chatPanelUI.AddText(_robotMoveData[idx]);

        if (_robotMoveData[idx].Contains("S")) return;

        string[] datas = _robotMoveData[idx].Replace("(","").Replace(")","").Split(",");

        foreach(RobotArmInfo info in m_robotAxisDictionary.Values)
        {
            int num = info.axisNum - 1;
            if (info.axis == "x")
            {
     
                if (num == 5)
                {
                    info.trm.DOLocalRotate(new Vector3(float.Parse(datas[num]) * -1 + axisOffsetValueList[num], 0, 90), moveTime);
                }
                else
                    info.trm.DOLocalRotate(new Vector3(float.Parse(datas[num]) * -1 + axisOffsetValueList[num], 0, 0), moveTime);
            }
                

            if (info.axis == "y")
                info.trm.DOLocalRotate(new Vector3(0, float.Parse(datas[num]) * -1 + axisOffsetValueList[num], 0), moveTime);
            if (info.axis == "z")
                if(num == 4)
                    info.trm.DOLocalRotate(new Vector3(0, 0, float.Parse(datas[num]) * 1 + axisOffsetValueList[num]), moveTime);
                else
                    info.trm.DOLocalRotate(new Vector3(0, 0, float.Parse(datas[num]) * -1 + axisOffsetValueList[num]), moveTime);
        }
    }

    IEnumerator RobotCorotine(float waitTime)
    {
        while (true)
        {

            yield return new WaitForSeconds(_smoothSpeed);
            RotateRobotArm(GameManager.Instance.SIMULATION_IDX, _smoothSpeed);
            GameManager.Instance.SIMULATION_IDX++;

            //if(_idx >= 10)
            //    _idx = 0;
        }
    }

    #endregion 



}
