using System;
using System.Collections;
using System.Collections.Generic;
using ccPhotonSocket;
using ccU3DEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic
{
    public class UI_Login : ccUILogicBase
    {
        public bool m_bIsRemembered => _toggleRememberMe.isOn;

        private Button _buttonLogin;
        private Button _buttonRegister;
        private Toggle _toggleRememberMe;
        private InputField _inputFieldPassword;
        private InputField _inputFieldUsername;
        private Text _textError;
        private Image _imageLoop;

        /// <summary>
        /// 登陆UI工作控制状态机
        /// </summary>
        private ccMachineManager _machineMgr = null;

        private int errorTimeEvent = -99;

        protected override void On_Init()
        {
            _machineMgr = new ccMachineManager(new ccMachineStateBase(-1));
            _machineMgr.f_RegState(new LoginState_LoggingIn());
            _machineMgr.f_RegState(new LoginState_Idle());
            _machineMgr.f_RegState(new LoginState_LoggingInDummy());
            _machineMgr.f_RegState(new LoginState_Register());

            _buttonLogin = f_GetObject("Login").GetComponent<Button>();
            _buttonRegister = f_GetObject("Register").GetComponent<Button>();
            _toggleRememberMe = f_GetObject("RememberMe").GetComponent<Toggle>();
            _inputFieldPassword = f_GetObject("Password").GetComponent<InputField>();
            _inputFieldUsername = f_GetObject("UserName").GetComponent<InputField>();
            _textError = f_GetObject("ErrorText").GetComponent<Text>();
            _imageLoop = f_GetObject("Loop").GetComponent<Image>();

            f_RegClickEvent("Login", OnClick_Login);
            f_RegClickEvent("Register", OnClick_Register);
        }


        protected override void On_Open(object e)
        {
            _machineMgr.f_ChangeState((int)EM_LoginState.Idle);

            _textError.enabled = false;
            _toggleRememberMe.isOn = false;

            if (string.IsNullOrWhiteSpace(StaticValue.m_strUsername))
            {
                _imageLoop.enabled = false;
                _inputFieldUsername.text = "";
                _inputFieldPassword.text = "";
                f_EnableSelectables();
            }
            else
            {
                // Automatic login.
                _inputFieldUsername.text = StaticValue.m_strUsername;
                _inputFieldPassword.text = "thisisdummyforfakeplainpassword";
                f_StartLogin();
            }
        }

        protected override void On_Close()
        {
            _machineMgr.f_ChangeState((int)EM_LoginState.Idle);
        }

        protected override void On_Update()
        {
            _machineMgr.f_Update();
        }

        protected override void On_UpdateGUI()
        {
        }

        protected override void On_Destory()
        {
        }

        private void f_EnableSelectables()
        {
            Selectable[] selectables = new Selectable[]
            {
                _inputFieldUsername, _inputFieldPassword, _buttonLogin, _buttonRegister, _toggleRememberMe
            };

            for (int i = 0; i < selectables.Length; i++)
            {
                selectables[i].interactable = true;
            }

            // For controllers like keyboard or game pad.
            EventSystem.current.SetSelectedGameObject(_inputFieldUsername.gameObject);
        }

        private void f_DisableSelectables()
        {
            Selectable[] selectables = new Selectable[]
            {
                _inputFieldUsername, _inputFieldPassword, _buttonLogin, _buttonRegister, _toggleRememberMe
            };

            for (int i = 0; i < selectables.Length; i++)
            {
                selectables[i].interactable = false;
            }
        }

        private void f_StartLogin()
        {
            StaticValue.m_strUsername = f_GetUserName();
            StaticValue.m_strUserPwd = f_GetUserPwd();

            _imageLoop.enabled = true;
            f_DisableSelectables();

#if OFFLINE_TESTING
            //切入测试用状态机
            _machineMgr.f_ChangeState((int)EM_LoginState.LoggingInDummy, this);
#else
            if (!glo_Main.GetInstance().m_GameSocket.m_bIsConnected)
            {
                f_Close();
                ccUIManage.GetInstance().f_SendMsg(StrUI.ServerFailure, BaseUIMessageDef.UI_OPEN);
            }
            //正式登陆状态机
            _machineMgr.f_ChangeState((int)EM_LoginState.LoggingIn, this);
#endif
        }

        private void OnClick_Login(GameObject go, object obj1, object obj2)
        {
            f_StartLogin();
        }

        private void OnClick_Register(GameObject go, object obj1, object obj2)
        {
            _machineMgr.f_ChangeState((int)EM_LoginState.Register, this);
        }

        public string f_GetUserName()
        {
            return _inputFieldUsername.text;
        }

        public string f_GetUserPwd()
        {
            return _inputFieldPassword.text;
        }

        public void f_ShowError(string strError)
        {
            if (errorTimeEvent != -99)
            {
                ccTimeEvent.GetInstance().f_UnRegEvent(errorTimeEvent);
            }

            _textError.enabled = true;
            _textError.text = strError;
            _imageLoop.enabled = false;
            f_EnableSelectables();
            errorTimeEvent = ccTimeEvent.GetInstance().f_RegEvent(2.0f, false, null, Callback_HideError);
        }

        private void Callback_HideError(object _)
        {
            ccTimeEvent.GetInstance().f_UnRegEvent(errorTimeEvent);
            errorTimeEvent = -99;

            _textError.enabled = false;
            _textError.text = "";
        }
    }
}