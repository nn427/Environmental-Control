
/*
 
    -----------------------
    UDP-Send
    -----------------------
    // [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]
   
    // > gesendetes unter
    // 127.0.0.1 : 8050 empfangen
   
    // nc -lu 127.0.0.1 8050
 
        // todo: shutdown thread at the end
*/
using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

public class UDPSend : MonoBehaviour {
    private static int localPort;

    [SerializeField] protected string IP = "192.168.0.50";  
    [SerializeField] protected int port = 9;  

    protected IPEndPoint remoteEndPoint;
    protected UdpClient client;

    string strMessage = "";

    [SerializeField] protected bool InitOnStart = false;

    protected virtual void Start() {
        if (InitOnStart) {
            Init();
        }
    }

    public virtual void Init() {
        if (client != null) {
            Close();
        }

        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
    }

    public virtual void Close() {
        if (client != null) {
            client.Close();
            client = null;
        }
    }

    protected void sendBytes(byte[] data) {
        try {
            print($"{data.Join(" ")}");
            int a = client.Send(data, data.Length, remoteEndPoint);
        } catch (Exception err) {
            Debug.LogError(err.ToString());
        }
    }

    protected void sendString(string message) {
        try {
            if (!string.IsNullOrEmpty(message)) {
                //byte[] data = Encoding.UTF8.GetBytes(message);
                byte[] data = message.Split(' ').Select(b => byte.Parse(b, NumberStyles.HexNumber)).ToArray();

                print($"{data.Join(" ")}");
                int a = client.Send(data, data.Length, remoteEndPoint);
            }
        } catch (Exception err) {
            Debug.LogError(err.ToString());
        }
    }

    

}