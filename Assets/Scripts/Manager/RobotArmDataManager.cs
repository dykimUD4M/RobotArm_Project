using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmData
{
    public float temperature;
    public float humidity;
    public float powerConsumption;

    public string robotAxisData;
}
public class RobotArmDataManager : MonoBehaviour
{
    RobotArmData robotArmData;

    public void LoadData()
    {

    }
}
