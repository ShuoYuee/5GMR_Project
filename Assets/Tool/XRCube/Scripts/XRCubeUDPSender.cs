using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;

public class XRCubeUDPSender : MonoBehaviour
{
    private static int localPort=8001;
    private string serverIP = "192.168.0.101";
    public int port;  
    bool showInput=false;
    public GameObject InputF;

    IPEndPoint remoteEndPoint;
    UdpClient client;

    public GameObject Canvas;
    public GameObject MainPos;
    public GameObject MainCtrl;
    public GameObject MainEdit;

    private static void Main()
    {
        XRCubeUDPSender sendObj = new XRCubeUDPSender();
        sendObj.init();

        sendObj.sendEndless(" endless infos \n");

    }

    public void Start()
    {
        float per = 1f;
        if (1080f / 1920f > (float)Screen.width / (float)Screen.height)
        {
            per = Screen.width / 1080f;
        }
        else if (1080f / 1920f < (float)Screen.width / (float)Screen.height)
        {
            per = Screen.height / 1920f;
        }
        else
        {
            per = Screen.height / 1920f;
        }
        Canvas.GetComponent<RectTransform>().localScale = new Vector3(per, per, per);
        init();
        showInput = false;
        InputF.SetActive(false);
        StartCoroutine(InitializeGyro());
        ChangeMain(0);
    }

    IEnumerator InitializeGyro()
    {
        Input.gyro.enabled = true;
        yield return null;
        Debug.Log(Input.gyro.attitude); // attitude has data now
    }
    float time3 = 0;
    public void Update()
    {
        GyroModifyCamera();
    }
    void GyroModifyCamera()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
        sendString("Rot," + Input.gyro.attitude.w + "," + Input.gyro.attitude.x + "," + Input.gyro.attitude.y + "," + Input.gyro.attitude.z);

    }
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
   

    // init
    public void init()
    {

        print("UDPSend.init()");

        port = localPort;

        remoteEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);
        client = new UdpClient();
        print("Sending to " + serverIP + " : " + port);
        print("Testing: nc -lu " + serverIP + " : " + port);

    }

    private void inputFromConsole()
    {
        try
        {
            string text;
            do
            {
                text = Console.ReadLine();

                if (text != "")
                {
                    byte[] data = Encoding.UTF8.GetBytes(text);
                    client.Send(data, data.Length, remoteEndPoint);
                }
            } while (text != "");
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    public void Send(int x)
    {
        sendString("Ctr," + x);
     //   print("Send" + x);
    }
    public void ChangeMain(int x) //切換頁面
    {
        if (x == 0)
        {
            //MainPos.SetActive(true);
            //MainCtrl.SetActive(false);
            //MainEdit.SetActive(true);

            MainPos.SetActive(false);
            MainCtrl.SetActive(true);
            MainEdit.SetActive(true);
        }
        else if (x == 1)
        {
            //MainPos.SetActive(true);
            //MainCtrl.SetActive(true);
            //MainEdit.SetActive(false);

            MainPos.SetActive(true);
            MainCtrl.SetActive(false);
            MainEdit.SetActive(true);
        }
        else if (x == 2)
        {
            //MainPos.SetActive(true);
            //MainCtrl.SetActive(false);
            //MainEdit.SetActive(true);

            MainPos.SetActive(true);
            MainCtrl.SetActive(true);
            MainEdit.SetActive(false);
        }
    }
    public void Setip()
    {
       if (showInput)
        {
            InputF.SetActive(false);
            showInput = false;
            serverIP = InputF.GetComponentInChildren<InputField>().text;
            init();
        }
       else
        {
            InputF.SetActive(true);
            showInput = true;
        }
        print("Setip");
    }
    
    public void SendPos(int x)
    {
        sendString("Pos," + x);
        print("Send" + x);
    }

    public void f_SendCus1(int iSet)
    {
        sendString("Cus1," + iSet);
        print("Send" + iSet);
    }

    public void f_SendCus2(int iSet)
    {
        sendString("Cus2," + iSet);
        print("Send" + iSet);
    }

    private void sendString(string message)
    {
        try
        {
          
            byte[] data = Encoding.UTF8.GetBytes(message);

            client.Send(data, data.Length, remoteEndPoint);
            
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }


    private void sendEndless(string testStr)
    {
        do
        {
            sendString(testStr);


        }
        while (true);

    }

}