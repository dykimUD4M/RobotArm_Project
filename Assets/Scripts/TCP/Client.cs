using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Socket m_Client;
    public string m_Ip = "192.168.0.191";
    public int m_Port = 1470;
    public ToServerPacket m_SendPacket = new ToServerPacket();
    public ToClientPacket m_ReceivePacket = new ToClientPacket();
    //private IPEndPoint m_ServerIpEndPoint;
    


    void Update()
    {
        Receive();
        //Send();
    }

    void OnApplicationQuit()
    {
        CloseClient();
    }

    public void InitClient(string ip,int port,Action connectCallback)
    {
        m_Ip = ip;
        m_Port = port;
        // SendPacket�� �迭�� ������ ���� �� �־�� ��.
        m_SendPacket.m_IntArray = new int[2];


        //m_ServerIpEndPoint = new IPEndPoint(IPAddress.Parse(m_Ip), m_Port);
        m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_Client.Connect(m_Ip, m_Port);

        connectCallback.Invoke();
        //m_Client.Connect(m_ServerIpEndPoint);
    }

    void SetSendPacket()
    {
        m_SendPacket.m_BoolVariable = true;
        m_SendPacket.m_IntVariable = 13;
        m_SendPacket.m_IntArray[0] = 7;
        m_SendPacket.m_IntArray[1] = 47;
        m_SendPacket.m_FloatlVariable = 2020;
        m_SendPacket.m_StringlVariable = "Coder Zero";
    }

    void Send()
    {
        try
        {
            SetSendPacket();

            byte[] sendPacket = StructToByteArray(m_SendPacket);
            m_Client.Send(sendPacket, 0, sendPacket.Length, SocketFlags.None);
        }

        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return;
        }
    }

    void Receive()
    {
        int receive = 0;
        if (m_Client.Available != 0)
        {
            byte[] packet = new byte[1024];

            try
            {
                receive = m_Client.Receive(packet);
            }

            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                return;
            }

            m_ReceivePacket = ByteArrayToStruct<ToClientPacket>(packet);

            if (receive > 0)
            {
                DoReceivePacket(); // ���� �� ó��
            }
        }
    }

    void DoReceivePacket()
    {
        //Debug.LogFormat($"m_IntArray[0] = {m_ReceivePacket.m_IntArray[0]} " +
        //   $"m_IntArray[1] = {m_ReceivePacket.m_IntArray[1]} " +
        //   $"FloatlVariable = {m_ReceivePacket.m_FloatlVariable} " +
        //   $"StringlVariable = {m_ReceivePacket.m_StringlVariable}" +
        //   $"BoolVariable = {m_ReceivePacket.m_BoolVariable} " +
        //   $"IntlVariable = {m_ReceivePacket.m_IntVariable} ");
        //���: m_IntArray[0] = 7 m_IntArray[1] = 47 FloatlVariable = 2020 StringlVariable = Coder ZeroBoolVariable = True IntlVariable = 13 
    }

    void CloseClient()
    {
        if (m_Client != null)
        {
            m_Client.Close();
            m_Client = null;
        }
    }

    byte[] StructToByteArray(object obj)
    {
        int size = Marshal.SizeOf(obj);
        byte[] arr = new byte[size];
        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.StructureToPtr(obj, ptr, true);
        Marshal.Copy(ptr, arr, 0, size);
        Marshal.FreeHGlobal(ptr);
        return arr;
    }

    T ByteArrayToStruct<T>(byte[] buffer) where T : struct
    {
        int size = Marshal.SizeOf(typeof(T));
        if (size > buffer.Length)
        {
            throw new Exception();
        }

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(buffer, 0, ptr, size);
        T obj = (T)Marshal.PtrToStructure(ptr, typeof(T));
        Marshal.FreeHGlobal(ptr);
        return obj;
    }
}
