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

        // 통신기 생성
        Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            // 통신 요청
            socket.Connect(endPoint);

            Console.WriteLine($"Connected to {socket.RemoteEndPoint.ToString()}");

            // 송신 내용을 바이트로 전환
            byte[] snedBuff = Encoding.UTF8.GetBytes("Hello World!");

            // 송신 하기
            int senrByteLength = socket.Send(snedBuff);

            // 수신 내용을 저장할 상자 선언
            byte[] recvBuff = new byte[1024];

            // 수신 받기
            int recvByteLength = socket.Receive(recvBuff);

            // 수신 해석
            string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvByteLength);

            // 수신 출력
            Console.WriteLine($"[From Server] {recvData}");

            // 통신 종료 예고( 굳이 안넣어도 됨 )
            socket.Shutdown(SocketShutdown.Both);

            // 통신 끊기
            socket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
