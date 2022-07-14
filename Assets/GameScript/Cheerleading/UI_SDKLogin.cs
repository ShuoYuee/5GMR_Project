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
using System;

namespace GameLogic
{
    public class UI_SDKLogin : ccUILogicBase
    {
        private ccMachineManager _machineManager;
        private Button _BtnLogIn;

        private InputField _AccountInputField, _PwdInputField;
        IPEndPoint remoteEndPoint;
        UdpClient client;
        float text;

        protected override void On_Init()
        {
            MessageBox.DEBUG("啟用SDKLogin");

            _machineManager = new ccMachineManager(new ccMachineStateBase(-1));
            _machineManager.f_RegState(new LogInState_Idle());
            _machineManager.f_RegState(new LogInState_LogIn());

            _AccountInputField = f_GetObject("Account_InputField").GetComponent<InputField>();
            _PwdInputField = f_GetObject("Pwd_InputField").GetComponent<InputField>();
            _BtnLogIn = f_GetObject("BtnLogIn").GetComponent<Button>();

            f_RegClickEvent(_BtnLogIn.gameObject, OnClickBtn_LogIn);
        }

        private void OnClickBtn_LogIn(GameObject go, object obj1, object obj2)
        {
            f_LoginSend();
        }

        protected override void On_Open(object e)
        {
            f_Init();
            //glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.UserLogin_Reps, new CMsg_GTC_LoginRelt(), OnGTC_CMsg_LogInRelt);
        }

        public void f_Init()
        {
            //print("UDPSend.init()");

            remoteEndPoint = new IPEndPoint(IPAddress.Parse(GloData.glo_strSvrIP), GloData.glo_iSvrPort);
            client = new UdpClient();
        }

        public void f_LoginSend()
        {
            MessageBox.DEBUG("按下按鈕");
            sendString("Login" + "," + _AccountInputField.text + "," + _PwdInputField.text);

            bool _bInputIsNull = (_AccountInputField.text == " ") && (_PwdInputField.text == " ");
            if (_bInputIsNull)
            {
                MessageBox.DEBUG("欄位不得為空");
            }
            else
            {
                //f_DisableSelectables();
                StaticValue.m_strAccount = _AccountInputField.text; //獲得輸入的帳號
                StaticValue.m_strPwd = _PwdInputField.text; //獲得輸入的密碼

                #region 手機登入
                ccUIManage.GetInstance().f_SendMsgV3("ui_mrcontrol.bundle", "UI_MRControl", UIMessageDef.UI_OPEN);
                ccUIManage.GetInstance().f_SendMsgV3("ui_login.bundle", "UI_LoginSDK", UIMessageDef.UI_CLOSE);              
                #endregion
                //收到手機回傳
                //StaticValue.m_strAccount = head[0]; //獲得輸入的帳號 => head[0]
                //StaticValue.m_strPwd = head[1]; //獲得輸入的密碼 => head[1]

                //CMsg_CTG_AccountEnter tCTG_AccountEnter = new CMsg_CTG_AccountEnter();
                //tCTG_AccountEnter.m_strAccount = StaticValue.m_strAccount;
                //tCTG_AccountEnter.m_strPassword = StaticValue.m_strPwd;
                //glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.CS_UserLogin, tCTG_AccountEnter);

                //_machineManager.f_ChangeState((int)EM_LogInState.LoggingIn, this);
            }
        }

        private void sendString(string message)
        {          
            try
            {
                MessageBox.DEBUG("進入sendString，message : " + message);
                byte[] data = Encoding.UTF8.GetBytes(message);

                client.Send(data, data.Length, remoteEndPoint);

            }
            catch (Exception err)
            {
                MessageBox.DEBUG(err.Message);
            }
        }

        protected override void On_UpdateGUI()
        {

        }

        protected override void On_Update()
        {
        }

        protected override void On_Close()
        {

        }


        protected override void On_Destory()
        {

        }
    }
}

