using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class RobotArmEditor : EditorWindow
{
#if UNITY_EDITOR
    private GameObject _robotArm;

    private List<Transform> _joints = new List<Transform>();


    [MenuItem("Window/RobotArmEdit")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<RobotArmEditor>("RobotArmEdit");
    }

    int page = 0;
    bool isInit = false;
    private void OnGUI()
    {
        GUILayout.Label("RobotArm Object", EditorStyles.boldLabel);

        _robotArm = (GameObject)EditorGUILayout.ObjectField("RobotArm",_robotArm, typeof(GameObject), true, GUILayout.MaxWidth(300));
        if( _robotArm != null )
        {
            if(isInit == false)
            {
                Init();
                isInit = true;
            }

            for(int i = 0; i< _joints.Count; i++)
            {
                _joints[i].localRotation = Quaternion.Euler(EditorGUILayout.Vector3Field($"Axis_{i+1}", _joints[i].localRotation.eulerAngles, GUILayout.MaxWidth(300)));

            }

            EditorGUILayout.Space(10);
            GUILayout.Label("Page", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("<", GUILayout.MaxWidth(20)))
            {
                page--;
                InfoLoad(page);
            }
            page = EditorGUILayout.IntField(page, GUILayout.MaxWidth(30));
            if (GUILayout.Button(">", GUILayout.MaxWidth(20)))
            {
                page++;
                InfoLoad(page);
            }

            if (GUILayout.Button("Save", GUILayout.MaxWidth(50)))
            {
                Save(page);
            }
            GUILayout.EndHorizontal();
        } 
    }

    void Init()
    {
        Transform transform = _robotArm.transform;

        _joints.Clear();
        int idx = 1;
        while (transform != null)
        {
            _joints.Add(transform);
            if (transform.childCount <= 0)
            {
                transform = null;
                break;
            }
            transform = transform.GetChild(0);
            idx++;
        }
    }

    void InfoLoad(int idx)
    {
        RobotArmInfo list = JsonLoader.LoadJsonFile<RobotArmInfo>($"{Application.dataPath}/Save/Json", idx.ToString());

        for(int i=0;i<list.axisInfoList.Count;i++)
        {
            _joints[i].rotation = list.axisInfoList[i].quaternion;
        }
    }

    void Save(int idx)
    {
        RobotArmInfo list = new RobotArmInfo();
        list.axisInfoList = new List<RobotAxisInfo>();

        foreach (var a in _joints)
        {
            RobotAxisInfo info = new RobotAxisInfo();
            info.axisName = "";
            info.quaternion = a.rotation;
            list.axisInfoList.Add(info);
        }

        string json = JsonLoader.ObjectToJson(list);
        JsonLoader.SaveJsonFile($"{Application.dataPath}/Save/Json", idx.ToString(), json);
    }
#endif
}
