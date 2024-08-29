using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // DNS (Domain Name System)
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddress = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddress, 7777);

        // ��ű� ����
        Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            // ��� ��û
            socket.Connect(endPoint);

            Console.WriteLine($"Connected to {socket.RemoteEndPoint.ToString()}");

            // �۽� ������ ����Ʈ�� ��ȯ
            byte[] snedBuff = Encoding.UTF8.GetBytes("Hello World!");

            // �۽� �ϱ�
            int senrByteLength = socket.Send(snedBuff);

            // ���� ������ ������ ���� ����
            byte[] recvBuff = new byte[1024];

            // ���� �ޱ�
            int recvByteLength = socket.Receive(recvBuff);

            // ���� �ؼ�
            string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvByteLength);

            // ���� ���
            Console.WriteLine($"[From Server] {recvData}");

            // ��� ���� ����( ���� �ȳ־ �� )
            socket.Shutdown(SocketShutdown.Both);

            // ��� ����
            socket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
