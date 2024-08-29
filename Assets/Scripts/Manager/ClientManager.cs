using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : Singleton<ClientManager>
{
    [SerializeField] Client armClient;
    [SerializeField] Client uiClient;

    [SerializeField] Canvas canvas;
    [SerializeField] LoadingUI loadingUI;

    private void Start()
    {
        StreamReader sr = new StreamReader($"{Application.dataPath}/Save/IP_1.txt");
        string[] armIP = sr.ReadToEnd().Split(",");
        sr = new StreamReader($"{Application.dataPath}/Save/IP_2.txt");
        string[] uiIP = sr.ReadToEnd().Split(",");
        armClient.InitClient(armIP[0], int.Parse(armIP[1]), AddScene);
        //uiClient.InitClient("192.168.0.191", 1470);
    }

    private void AddScene()
    {
        StartCoroutine(ConnectAnimation());
    }

    private IEnumerator ConnectAnimation()
    {
        yield return new WaitForSeconds(1f);
        loadingUI.isComplate = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("RobotArm", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.1f);
        canvas.gameObject.SetActive(false);
    }

    public string GetClientData(int clientNum)
    {
        if (clientNum == 0)
            return armClient.m_ReceivePacket.m_StringlVariable;
        else
            return uiClient.m_ReceivePacket.m_StringlVariable;
    }
}
