using UnityEngine;
using System;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;

[Serializable]
public class SimToolsData
{
    public int Roll;
    public int Pitch;
    public int Yaw; //Yaw <-> Heave
    public int Heave; //Yaw <-> Heave
    public int Sway;
    public int Surge;
    public int Extra1;
    public int Extra2;
    public int Extra3;

    public SimToolsData() { }
    public SimToolsData(int Roll, int Pitch, int Yaw, int Heave, int Sway, int Surge, int Extra1, int Extra2, int Extra3) //Yaw <-> Heave
    {
        this.Roll = Roll;
        this.Pitch = Pitch;
        this.Yaw = Yaw; //Yaw <-> Heave
        this.Heave = Heave; //Yaw <-> Heave
        this.Sway = Sway;
        this.Surge = Surge;
        this.Extra1 = Extra1;
        this.Extra2 = Extra2;
        this.Extra3 = Extra3;
    }
}

public class ControlSimTools : MonoBehaviour
{
    public static ControlSimTools instance;

    public string ip = "127.0.0.1";
    public int port = 4123;

    public SimToolsData simData;

    // Network
    private UdpClient _client;
    private IPEndPoint _remoteEndPoint;

    //__________________________________________________ Awake
    private void Awake()
    {
        instance = this;

        _client = new UdpClient();
        simData = new SimToolsData();

        getNetworkInfo();
    }
    private void getNetworkInfo()
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();

            string rootPath = Application.dataPath;
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf('/') + 1);

            xmlDoc.Load(rootPath + "network.xml"); //DataPath에 있는 xml 로드
            XmlNode root = xmlDoc.SelectSingleNode("network"); //network 노드 검색
            XmlAttributeCollection atts = root.Attributes;

            ip = atts["ip"].Value; //IP 지정
            port = int.Parse(atts["port"].Value); //Port 지정
        }
        catch(Exception e)
        {
            Debug.Log($"ControlSimTools | {e.Message} | Xml을 불러오지 못했습니다");
        }
    }

    //__________________________________________________ Start
    private void Start()
    {
        _remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        StartCoroutine(sendAsync());
    }
    private IEnumerator sendAsync()
    {
        while (true)
        {
            string info = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", simData.Roll, simData.Pitch, simData.Yaw, simData.Heave, simData.Sway, simData.Surge, simData.Extra1, simData.Extra2, simData.Extra3); //Yaw <-> Heave
            byte[] data = Encoding.Default.GetBytes(info);
            _client.Send(data, data.Length, _remoteEndPoint);
            yield return null;
        }
    }
}