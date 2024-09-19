using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClientManager : Singleton<ClientManager>
{
    [SerializeField] Client armClient;
    [SerializeField] Client uiClient;

    [SerializeField] Canvas canvas;
    [SerializeField] LoadingUI loadingUI;
    [SerializeField] TextMeshProUGUI explanationText;

    [SerializeField] Transform ipPanel;
    [SerializeField] TMP_InputField armIP;
    [SerializeField] TMP_InputField uiIP;
    [SerializeField] TMP_InputField armPort;
    [SerializeField] TMP_InputField uiPort;

    private void Start()
    {
        Connect();
        //uiClient.InitClient("192.168.0.191", 1470);
    }

    private void Update()
    {
        if(isReady_1&&isReady_2&&isNext == false)
        {
            isNext = true;
            AddScene();
        }
    }
    private void Connect()
    {
        StreamReader sr = new StreamReader($"{Application.dataPath}/Save/IP_1.txt");
        string[] armIPs = sr.ReadToEnd().Split(",");
        if(armIPs.Length <= 1 )
        {
            ConnectFailed();
            return;
        }
        armIP.text = armIPs[0];
        armPort.text = armIPs[1];
        sr.Close();
        sr = new StreamReader($"{Application.dataPath}/Save/IP_2.txt");
        string[] uiIPs = sr.ReadToEnd().Split(",");
        if (uiIPs.Length <= 1)
        {
            ConnectFailed();
            return;
        }
        uiIP.text = uiIPs[0];
        uiPort.text = uiIPs[1];
        armClient.InitClient(armIPs[0], int.Parse(armIPs[1]), () => { isReady_1 = true; }, ConnectFailed);
        //uiClient.InitClient(uiIPs[0], int.Parse(uiIPs[1]), () => { isReady_2 = true; }, ConnectFailed);
        sr.Close();
    }

    bool isReady_1 = false;
    bool isReady_2 = true;

    bool isNext = false;
    public void ReConnect()
    {
        explanationText.SetText("데이터를 불러오는 중입니다.\n 잠시만 기다려 주세요");
        StreamWriter sw = new StreamWriter($"{Application.dataPath}/Save/IP_1.txt");
        sw.Write($"{armIP.text},{armPort.text}");
        sw.Close();
        sw = new StreamWriter($"{Application.dataPath}/Save/IP_2.txt");
        sw.Write($"{uiIP.text},{uiPort.text}");
        sw.Close();
        armClient.InitClient(armIP.text, int.Parse(armPort.text), () => { isReady_1 = true; }, ConnectFailed);
        uiClient.InitClient(uiIP.text, int.Parse(uiPort.text), () => { isReady_2 = true; }, ConnectFailed);
    }

    public void ConnectPass()
    {
        armClient.m_isFail = true;
        uiClient.m_isFail = true;

        armClient.CloseClient();
        uiClient.CloseClient();

        AddScene();
    }

    private void AddScene()
    {
        StartCoroutine(ConnectAnimation());
    }

    private void ConnectFailed()
    {
        ipPanel.gameObject.SetActive(true);
        explanationText.SetText("서버 접속을 실패했습니다.\n IP와 Port를 변경하거나 종료 후 다시 접속해 주세요");
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
            return armClient.m_getData;
        else
            return uiClient.m_getData;
    }
}
