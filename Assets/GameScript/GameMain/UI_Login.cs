using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;
using MR_Edit;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GameLogic
{
    public class UI_Login : ccUILogicBase
    {
        private ccMachineManager _machineManager;

        private Text _ShowText, _ErrorText;
        private Button _BtnLogIn;
        private InputField _AccountInputField, _PwdInputField;

        #region 接收SDK
        Socket socket;
        EndPoint clientEnd;
        IPEndPoint ipEnd;
        public Thread connectThread;

        string recvStr;
        string sendStr;
        byte[] recvData;
        byte[] sendData;
        int recvLen;
        //string[] head;
        #endregion

        protected override void On_Init()
        {
            _machineManager = new ccMachineManager(new ccMachineStateBase(-1));
            _machineManager.f_RegState(new LogInState_Idle());
            _machineManager.f_RegState(new LogInState_LogIn());

            _ShowText = f_GetObject("ShowText").GetComponent<Text>();
            _ErrorText = f_GetObject("ErrorText").GetComponent<Text>();
            _AccountInputField = f_GetObject("Account_InputField").GetComponent<InputField>();
            _PwdInputField = f_GetObject("Pwd_InputField").GetComponent<InputField>();
            _BtnLogIn = f_GetObject("BtnLogIn").GetComponent<Button>();

            f_RegClickEvent(_BtnLogIn.gameObject, OnClickBtn_LogIn);
        }

        void InitSocket()
        {
            ipEnd = new IPEndPoint(IPAddress.Any, GloData.glo_iSvrPort + 1);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(ipEnd);
            socket.ReceiveBufferSize = 2000000;
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, GloData.glo_iSvrPort + 1);
            MessageBox.DEBUG("Adress : " + sender.Address.ToString() + " Port " + sender.Port);
            //IPEndPoint sender = new IPEndPoint(IPAddress.Parse(XRCubeUDPSender.serverIP), XRCubeUDPSender.localPort);
            clientEnd = (EndPoint)sender;
            //print("waiting for UDP dgram");
            connectThread = new Thread(new ThreadStart(SocketReceive));
            connectThread.Start();
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
                MessageBox.DEBUG("Login " + "Head[0] : " + head[0] + " " + " Head[1] : " + head[1]);
                //if (head[0] == "Ctr")
                //{
                //    MessageBox.DEBUG("收到手機傳遞Ctr : " + int.Parse(head[1]));
                //    KeyDownCtr[int.Parse(head[1])] = true;
                //}             
                //if (head[0] == "Rot")
                //{

                //    Qua.w = float.Parse(head[1]);
                //    Qua.x = float.Parse(head[2]);
                //    Qua.y = float.Parse(head[4]);
                //    Qua.z = float.Parse(head[3]);

                //}
                //if (head[0] == "Login") 
                //{
                //    f_LogInGetSDK(head[1], head[2]);
                //}
            }

        }

        protected override void On_Open(object e)
        {
            _machineManager.f_ChangeState((int)EM_LogInState.Idle, this);

            glo_Main.GetInstance().m_UIMessagePool.f_AddListener(UIMessageDef.PlayerLogin, f_LogInGetSDK);
            //InitSocket();
        }

        protected override void On_Close()
        {
            _machineManager.f_ChangeState((int)EM_LogInState.Idle, this);
        }

        protected override void On_Destory()
        {
           
        }

        protected override void On_UpdateGUI()
        {

        }

        protected override void On_Update()
        {
            base.On_Update();
            _machineManager.f_Update();
        }

        private void f_LogIn()
        {
            //這邊接收手機傳遞的訊號
            bool _bInputIsNull = (_AccountInputField.text == "") && (_PwdInputField.text == "");
            //bool _bInputIsNull = (head[0] == "") && (head[1] == "");
            if (_bInputIsNull)
            {
                f_EnableSelectables();
                f_UpdataText(0, "欄位不得為空");
            }
            else
            {
                f_DisableSelectables();
                StaticValue.m_strAccount = _AccountInputField.text; //獲得輸入的帳號
                StaticValue.m_strPwd = _PwdInputField.text; //獲得輸入的密碼

                //收到手機回傳
                //StaticValue.m_strAccount = head[0]; //獲得輸入的帳號 => head[0]
                //StaticValue.m_strPwd = head[1]; //獲得輸入的密碼 => head[1]

                CMsg_CTG_AccountEnter tCTG_AccountEnter = new CMsg_CTG_AccountEnter();
                tCTG_AccountEnter.m_strAccount = StaticValue.m_strAccount;
                tCTG_AccountEnter.m_strPassword = StaticValue.m_strPwd;
                glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.CS_UserLogin, tCTG_AccountEnter);

                _machineManager.f_ChangeState((int)EM_LogInState.LoggingIn, this);
            }
        }

        private void f_LogInGetSDK(object obj)
        {
            string[] head = (string[])obj;
            //這邊接收手機傳遞的訊號
            bool _bInputIsNull = (head[1] == "") && (head[2] == "");
            //bool _bInputIsNull = (head[0] == "") && (head[1] == "");
            if (_bInputIsNull)
            {
                f_EnableSelectables();
                f_UpdataText(0, "欄位不得為空");
            }
            else
            {
                MessageBox.DEBUG(head[1] + " " + head[2]);
                //f_DisableSelectables();
                StaticValue.m_strAccount = head[1]; //獲得輸入的帳號
                StaticValue.m_strPwd = head[2]; //獲得輸入的密碼

                //收到手機回傳
                //StaticValue.m_strAccount = head[0]; //獲得輸入的帳號 => head[0]
                //StaticValue.m_strPwd = head[1]; //獲得輸入的密碼 => head[1]

                CMsg_CTG_AccountEnter tCTG_AccountEnter = new CMsg_CTG_AccountEnter();
                tCTG_AccountEnter.m_strAccount = StaticValue.m_strAccount;
                tCTG_AccountEnter.m_strPassword = StaticValue.m_strPwd;
                glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.CS_UserLogin, tCTG_AccountEnter);

                _machineManager.f_ChangeState((int)EM_LogInState.LoggingIn, this);

                //connectThread.Abort();
            }
        }

        public void OnClickBtn_LogIn(GameObject go, object obj1, object obj2)
        {
            f_LogIn();
        }

        public void f_EnableSelectables()
        {
            Selectable[] selectables = new Selectable[]
            {
                _AccountInputField, _PwdInputField, _BtnLogIn
            };

            for (int i = 0; i < selectables.Length; i++)
            {
                selectables[i].interactable = true;
            }
        }

        public void f_DisableSelectables()
        {
            Selectable[] selectables = new Selectable[]
            {
                _AccountInputField, _PwdInputField, _BtnLogIn
            };

            for (int i = 0; i < selectables.Length; i++)
            {
                selectables[i].interactable = false;
            }
        }

        public void f_UpdataText(int iSet, string strInfor)
        {
            if (iSet == 0)
            {
                _ShowText.text = strInfor;
            }
            else if (iSet == 1)
            {
                _ErrorText.text = strInfor;
            }
        }
    }

}