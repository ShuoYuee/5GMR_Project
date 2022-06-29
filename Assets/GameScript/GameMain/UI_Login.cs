using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;
using MR_Edit;

namespace GameLogic
{
    public class UI_Login : ccUILogicBase
    {
        private ccMachineManager _machineManager;

        private Text _ShowText, _ErrorText;
        private Button _BtnLogIn;
        private InputField _AccountInputField, _PwdInputField;

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

        protected override void On_Open(object e)
        {
            _machineManager.f_ChangeState((int)EM_LogInState.Idle, this);
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
            bool _bInputIsNull = (_AccountInputField.text == "") && (_PwdInputField.text == "");
            if (_bInputIsNull)
            {
                f_EnableSelectables();
                f_UpdataText(0, "欄位不得為空");
            }
            else
            {
                f_DisableSelectables();
                StaticValue.m_strAccount = _AccountInputField.text;
                StaticValue.m_strPwd = _PwdInputField.text;

                CMsg_CTG_AccountEnter tCTG_AccountEnter = new CMsg_CTG_AccountEnter();
                tCTG_AccountEnter.m_strAccount = StaticValue.m_strAccount;
                tCTG_AccountEnter.m_strPassword = StaticValue.m_strPwd;
                glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.CS_UserLogin, tCTG_AccountEnter);

                _machineManager.f_ChangeState((int)EM_LogInState.LoggingIn, this);
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