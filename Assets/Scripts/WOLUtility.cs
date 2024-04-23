using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.Sockets;
using System.Net;
using System.Globalization;
using UnityEngine.UI;
using System.Text;
using static UnityEngine.TouchScreenKeyboard;
using rv;

public class WOLClass : UdpClient {
    public WOLClass() : base() { }
    //this is needed to send broadcast packet
    public void SetClientToBrodcastMode() {
        if (this.Active)
            this.Client.SetSocketOption(SocketOptionLevel.Socket,
                                      SocketOptionName.Broadcast, 0);
    }
}

public class WOLUtility : UDPSend {
    private readonly string KEY_ROUTERIP = "RouterIP";
    private readonly string KEY_PORT = "Port";
    private readonly string KEY_MACADDRESS = "MAC";

    [SerializeField] InputField input_IP;
    [SerializeField] InputField input_Port;
    [SerializeField] InputField input_Mac;

    protected override void Start() {
        if (PlayerPrefs.HasKey(KEY_ROUTERIP)) {
            input_IP.text = PlayerPrefs.GetString(KEY_ROUTERIP);
        }

        if (PlayerPrefs.HasKey(KEY_PORT)) {
            input_Port.text = PlayerPrefs.GetString(KEY_PORT);
        }

        if (PlayerPrefs.HasKey(KEY_MACADDRESS)) {
            input_Mac.text = PlayerPrefs.GetString(KEY_MACADDRESS);
        }
    }

    public override void Init() {
        if (string.IsNullOrEmpty(input_IP.text) || string.IsNullOrEmpty(input_Port.text)) {
            return;
        }

        //print("UDPSend.init()");

        IP = input_IP.text;
        port = Convert.ToInt32(input_Port.text);

        PlayerPrefs.SetString(KEY_PORT, input_Port.text);
        PlayerPrefs.SetString(KEY_ROUTERIP, input_IP.text);

        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
        print("Init");
    }

    public void SendAwake() {
        if (string.IsNullOrEmpty(input_Mac.text)) {
            return;
        }

        string mac = input_Mac.text.Split(':').Join("");

        PlayerPrefs.SetString(KEY_MACADDRESS, input_Mac.text);

        wakeOnLan(mac);
    }

    public void wakeOnLanWithConnectedClient() {
        if (string.IsNullOrEmpty(input_Mac.text)) {
            return;
        }

        if (client == null) {
            return;
        }

        string msg = "FF FF FF FF FF FF" + "\n";
        string mac = " " + input_Mac.text.Replace(":", " ");
        for (int i = 0; i < 16; i++) {
            msg += mac;
        }

        sendString(msg);
    }

    //MAC_ADDRESS should  look like '013FA049'
    private void wakeOnLan(string MAC_ADDRESS) {
        WOLClass client = new WOLClass();
        client.Connect(new
           IPAddress(0xffffffff),  //255.255.255.255  i.e broadcast
           0x0009); // port=12287(0x2fff) let's use this one 
        client.SetClientToBrodcastMode();
        //set sending bites
        int counter = 0;
        //buffer to be send
        List<byte> bytes = new List<byte>();   // more than enough :-)
                                               //first 6 bytes should be 0xFF
        for (int y = 0; y < 6; y++)
            bytes.Add(0xFF);
        //now repeate MAC 16 times
        for (int y = 0; y < 16; y++) {
            int i = 0;
            for (int z = 0; z < 6; z++) {
                bytes.Add(
                    byte.Parse(MAC_ADDRESS.Substring(i, 2),
                    NumberStyles.HexNumber));
                i += 2;
            }
        }

        //now send wake up packet
        int reterned_value = client.Send(bytes.ToArray(), bytes.Count);
        print(bytes.Join(" "));
    }


}

