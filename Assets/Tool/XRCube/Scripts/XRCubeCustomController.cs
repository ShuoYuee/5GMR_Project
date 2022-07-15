using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class XRCubeCustomController : MonoBehaviour //負責SDK觸發事件定義
{
    private int port = 8001;
    Socket socket;
    EndPoint clientEnd; 
    IPEndPoint ipEnd; 
    string recvStr; 
    string sendStr;
    byte[] recvData; 
    byte[] sendData; 
    int recvLen; 
    Thread connectThread;
    Quaternion Qua;
    Quaternion QuaInverse;
    public GameObject CtrlLaser;
    public bool autohide = false;
    public float autohide_time=12;
    bool showCtrl = false;
    float showCtrlTime = 0;
    public bool showLaser = false;
    float showLaserTime = 0;
    public GameObject XRCubeControllerGO;
    public GameObject MRSpace;
    public bool[] KeyDownPos = new bool[] { false, false, false, false, false, false, false, false, false, false };
    public bool[] KeyDownCtr = new bool[] { false, false, false, false, false, false, false, false, false, false };
    public bool[] KeyDownCustom_1 = new bool[] { false, false, false, false, false, false, false, false, false, false };
    public bool[] KeyDownCustom_2 = new bool[] { false, false, false, false, false, false, false, false, false, false, false };

    private XRCubeCtrl _XRCubeCtrl;
    float PosSpeed = 1;
    float time = 0;
    float test1;

    void InitSocket()
    {
        ipEnd = new IPEndPoint(IPAddress.Any, GloData.glo_iSvrPort);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.Bind(ipEnd);
        socket.ReceiveBufferSize = 2000000;
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, GloData.glo_iSvrPort);
        MessageBox.DEBUG("Adress : " + sender.Address.ToString() + " Port " + sender.Port);
        //IPEndPoint sender = new IPEndPoint(IPAddress.Parse(XRCubeUDPSender.serverIP), XRCubeUDPSender.localPort);
        clientEnd = (EndPoint)sender;
        print("waiting for UDP dgram");
        MessageBox.DEBUG("aiting for UDP dgram");
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }

    void Start()
    {
        
        InitSocket(); 
        QuaInverse = Quaternion.identity;
        //if(autohide)
        //{
        //    XRCubeControllerGO.SetActive(false);
        //}
        //else
        //{
        //    XRCubeControllerGO.SetActive(true);
        //}

        //CtrlLaser.SetActive(false);
        //_XRCubeCtrl = XRCubeControllerGO.GetComponent<XRCubeCtrl>();
    }

    // Update is called once per frame

    void XRCubeKeyEvent()
    {

    }

    void Update()
    {
        time += Time.deltaTime;
        Quaternion _Qua = Qua * QuaInverse;
        //XRCubeControllerGO.transform.localRotation = Quaternion.Euler(_Qua.eulerAngles.x, -_Qua.eulerAngles.y + 180f, _Qua.eulerAngles.z);
        
        if(_XRCubeCtrl == null)
        {
            XRCubeControllerGO = GameObject.FindGameObjectWithTag("XRCubeController");
            if(XRCubeControllerGO != null)
                _XRCubeCtrl = XRCubeControllerGO.GetComponent<XRCubeCtrl>();
        }

        if (showLaser)
        {
            showLaserTime += Time.deltaTime;
        }
        if (showCtrl)
        {
            showCtrlTime += Time.deltaTime;
        }
        if (autohide & showCtrlTime > autohide_time)
        {
            showCtrl = false;
            XRCubeControllerGO.SetActive(false);
            showLaser = false;
            //XRCubeControllerGO.GetComponent<XRCubeCtrl>().showLaser = false;
            _XRCubeCtrl.showLaser = false;
           // CtrlLaser.SetActive(false);
        }
        if (showLaserTime > 8)
        {
            showLaser = false;
            //XRCubeControllerGO.GetComponent<XRCubeCtrl>().showLaser = false;
            _XRCubeCtrl.showLaser = false;
            //CtrlLaser.SetActive(false);
        }
        if (KeyDownCtr[0] && time > 0.1f)
        {
            KeyDownCtr[0] = false;
            PosSpeed = 10;
            time = 0;
        }
        if (KeyDownCtr[1] && time > 0.1f)
        {
            KeyDownCtr[1] = false;
            PosSpeed = 1;
            time = 0;
        }
        if (KeyDownCtr[2] && time > 0.1f)
        {
            KeyDownCtr[2] = false;
            PosSpeed = 0.2f;
            time = 0;
        }
        if (KeyDownCtr[3] && time > 0.1f)
        {
            MessageBox.DEBUG("KeyDownCtr[3]按下");

            KeyDownCtr[3] = false;
            if (showCtrl)
            {
                showCtrlTime = 0;
            }
            else
            {
                //XRCubeControllerGO.SetActive(true);
                showCtrl = true;
                showCtrlTime = 0;
                QuaInverse = Quaternion.Inverse(Qua);
            }

            if (showLaser)
            {
                //XRCubeControllerGO.GetComponent<XRCubeCtrl>().click();
                _XRCubeCtrl.click();
            }
            else
            {
                showLaser = true;
                //XRCubeControllerGO.GetComponent<XRCubeCtrl>().showLaser = true;
                _XRCubeCtrl.showLaser = true;
                showLaserTime = 0;
                //CtrlLaser.SetActive(true);
            }

            _XRCubeCtrl.click(0, 0);
            time = 0;
        }
        if (KeyDownPos[0] && time > 0.1f)
        {
            KeyDownPos[0] = false;
            MRSpace.transform.position = new Vector3(MRSpace.transform.position.x - 0.05f * PosSpeed, MRSpace.transform.position.y, MRSpace.transform.position.z);
            
            time = 0;
        }
        if (KeyDownPos[1] && time > 0.1f)
        {
            KeyDownPos[1] = false;
            MRSpace.transform.position = new Vector3(MRSpace.transform.position.x + 0.05f * PosSpeed, MRSpace.transform.position.y, MRSpace.transform.position.z);
           
            time = 0;
        }
        if (KeyDownPos[2] && time > 0.1f)
        {
            KeyDownPos[2] = false;
            MRSpace.transform.position = new Vector3(MRSpace.transform.position.x, MRSpace.transform.position.y, MRSpace.transform.position.z - 0.05f * PosSpeed);
          
            time = 0;
        }
        if (KeyDownPos[3] && time > 0.1f)
        {
            KeyDownPos[3] = false;
            MRSpace.transform.position = new Vector3(MRSpace.transform.position.x, MRSpace.transform.position.y, MRSpace.transform.position.z + 0.05f * PosSpeed);
          
            time = 0;
        }
        if (KeyDownPos[4] && time > 0.1f)
        {
            KeyDownPos[4] = false;
            MRSpace.transform.position = new Vector3(MRSpace.transform.position.x, MRSpace.transform.position.y - 0.05f * PosSpeed, MRSpace.transform.position.z);
           
            time = 0;
        }
        if (KeyDownPos[5] && time > 0.1f)
        {
            KeyDownPos[5] = false;
            MRSpace.transform.position = new Vector3(MRSpace.transform.position.x, MRSpace.transform.position.y + 0.05f * PosSpeed, MRSpace.transform.position.z);
                     time = 0;
        }
        if (KeyDownPos[6] && time > 0.1f)
        {
            KeyDownPos[6] = false;

            MRSpace.transform.rotation = Quaternion.Euler(0, MRSpace.transform.rotation.eulerAngles.y - 3f * PosSpeed, 0);
            time = 0;
        }
        if (KeyDownPos[7] && time > 0.1f)
        {
            KeyDownPos[7] = false;
            // MRSpace.transform.position = new Vector3(MRSpace.transform.position.x, MRSpace.transform.position.y - 0.1f, MRSpace.transform.position.z);
            MRSpace.transform.rotation = Quaternion.Euler(0, MRSpace.transform.rotation.eulerAngles.y + 3f * PosSpeed, 0);
            time = 0;
        }
        if (KeyDownPos[8] && time > 0.1f)
        {
            KeyDownPos[8] = false;
            XRCubeControllerGO.SetActive(true);
            showCtrl = true;
            showCtrlTime = 0;
            QuaInverse = Quaternion.Inverse(Qua);
            time = 0;
        }
        if (KeyDownCustom_2[0] && time > 0.1f)
        {//互動觸發
            KeyDownCustom_2[0] = false;
            f_ShowController();
            if (showLaser)
            {
                _XRCubeCtrl.click(0, 0);
            }
            time = 0;
        }
        if (KeyDownCustom_1[1] && time > 0.1f)
        {//X軸右移
            KeyDownCustom_1[1] = false;
            f_ShowController();
            if (showLaser)
            {
                _XRCubeCtrl.click(1, 1);
            }
            time = 0;
        }
        if (KeyDownCustom_1[2] && time > 0.1f)
        {//X軸左移
            KeyDownCustom_1[2] = false;
            f_ShowController();
            if (showLaser)
            {
                _XRCubeCtrl.click(1, -1);
            }
            time = 0;
        }
        if (KeyDownCustom_1[3] && time > 0.1f)
        {//Y軸上移
            KeyDownCustom_1[3] = false;
            f_ShowController();
            if (showLaser)
            {
                _XRCubeCtrl.click(2, 1);
            }
            time = 0;
        }
        if (KeyDownCustom_1[4] && time > 0.1f)
        {//Y軸下移
            KeyDownCustom_1[4] = false;
            f_ShowController();
            if (showLaser)
            {
                _XRCubeCtrl.click(2, -1);
            }
            time = 0;
        }
        if (KeyDownCustom_1[5] && time > 0.1f)
        {//Z軸前移
            KeyDownCustom_1[5] = false;
            f_ShowController();
            if (showLaser)
            {
                _XRCubeCtrl.click(3, 1);
            }
            time = 0;
        }
        if (KeyDownCustom_1[6] && time > 0.1f)
        {//Z軸後移
            KeyDownCustom_1[6] = false;
            f_ShowController();
            if (showLaser)
            {
                _XRCubeCtrl.click(3, -1);
            }
            time = 0;
        }
        if (KeyDownCustom_1[7] && time > 0.1f)
        {//更改編輯模式
            KeyDownCustom_1[7] = false;
            f_ShowController();
            if (showLaser)
            {
                _XRCubeCtrl.click(4, 3);
            }
            time = 0;
        }
        if (KeyDownCustom_1[8] && time > 0.1f)
        {//更改座標模式
            KeyDownCustom_1[8] = false;
            f_ShowController();
            if (showLaser)
            {
                _XRCubeCtrl.click(5, 2);
            }
            time = 0;
        }
    }

    private void f_ShowController()
    {
        if (showCtrl)
        {
            showCtrlTime = 0;
        }
        else
        {
            //XRCubeControllerGO.SetActive(true);
            showCtrl = true;
            showCtrlTime = 0;
            QuaInverse = Quaternion.Inverse(Qua);
        }

        if (!showLaser)
        {
            showLaser = true;
            _XRCubeCtrl.showLaser = true;
            showLaserTime = 0;
           // CtrlLaser.SetActive(true);
        }
    }

    void SocketReceive()
    {
        while (true)
        {
            recvData = new byte[2000000];         
            recvLen = socket.ReceiveFrom(recvData, ref clientEnd);
            recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
            //MessageBox.DEBUG(recvStr);
            string[] head = recvStr.Split(',');
            MessageBox.DEBUG("XR " + "Head[0] : " + head[0] + " " + " Head[1] : " + head[1]);
            if (head[0] == "Ctr")
            {
                print("收到手機傳遞Ctr : " + int.Parse(head[1]));
                KeyDownCtr[int.Parse(head[1])] = true;
            }
            else if (head[0] == "Cus1") //收到手機Sender 
            {
                MessageBox.DEBUG("收到手機傳遞 : " + int.Parse(head[1]));
                KeyDownCustom_1[int.Parse(head[1])] = true;
            }
            else if (head[0] == "Cus2")
            {
                KeyDownCustom_2[int.Parse(head[1])] = true;
            }
            else if (head[0] == "Pos")
            {
                KeyDownPos[int.Parse(head[1])] = true;
            }
            if (head[0] == "Rot")
            {

                Qua.w = float.Parse(head[1]);
                Qua.x = float.Parse(head[2]);
                Qua.y = float.Parse(head[4]);
                Qua.z = float.Parse(head[3]);

            }
            if (head[0] == "Login")
            {
                glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(UIMessageDef.PlayerLogin, head);
            }
        }

    }

    void SocketQuit()
    {
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        if (socket != null)
            socket.Close();
        print("disconnect");
    }
    void OnApplicationQuit()
    {
        SocketQuit();
    }
}
