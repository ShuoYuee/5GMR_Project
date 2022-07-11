using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;
using MR_Edit;

namespace GameLogic
{
    public class UI_MRControl : ccUILogicBase
    {
        private XRCubeUDPSender _XRCubeUDPSender;

        public ccMachineManager _machineManager;

        protected override void On_Init()
        {
            MessageBox.DEBUG("啟用遊戲包中的UI_MRControl腳本");
            _XRCubeUDPSender = f_GetObject("Panel").GetComponent<XRCubeUDPSender>();
            _machineManager = new ccMachineManager(new ccMachineStateBase(-1)); //狀態機
            _machineManager.f_RegState(new MainState_Main());
            _machineManager.f_RegState(new MainState_Edit());
            _machineManager.f_RegState(new MainState_GuessGame());
            _machineManager.f_RegState(new MainState_Logout());

            f_RegClickEvent(f_GetObject("BtnPos"), OnClick_MainPanel, 0);
            f_RegClickEvent(f_GetObject("BtnController"), OnClick_MainPanel, 1);
            f_RegClickEvent(f_GetObject("BtnEdit"), OnClick_MainPanel, 2);
            f_RegClickEvent(f_GetObject("BtnSet"), OnClick_MainPanel, -1);

            Button[] buttons = f_GetObject("Main_Edit").GetComponentsInChildren<Button>();
            for(int i = 0; i < buttons.Length; i++)
            {
                f_RegClickEvent(buttons[i].gameObject, f_Send, 1);
            }

            InputField[] inputFields = f_GetObject("Main_Edit").GetComponentsInChildren<InputField>();
            for(int i = 0; i < inputFields.Length; i++)
            {
                f_RegClickEvent(inputFields[i].gameObject, f_Send, 2);
            }
        }

        protected override void On_Open(object e)
        {
            _machineManager.f_ChangeState((int)EM_MainState.Main);
        }

        protected override void On_Close()
        {
            _machineManager.f_ChangeState((int)EM_MainState.Main);
        }

        protected override void On_Destory()
        {
            
        }

        protected override void On_UpdateGUI()
        {
            
        }

        private void OnClickBtn_Logout(GameObject go, object obj1, object obj2)
        {//登出
            glo_Main.GetInstance().m_GameMessagePool.f_Broadcast(MessageDef.MainLogOut);
        }

        #region XR_UDP_Send
        private void OnClick_MainPanel(GameObject go, object obj1, object obj2)
        {
            MessageBox.DEBUG("OnClick_MainPanel 觸發 <Obj1> : " + (int)obj1);
            int iSet = (int)obj1;
            if (iSet == -1)
            {
                _XRCubeUDPSender.Setip();
            }
            else
            {
                _XRCubeUDPSender.ChangeMain(iSet); //切換至iSet頁面
            }
        }

        private void f_Send(GameObject go, object obj1, object obj2)
        {
            int iState = (int)obj1;
            if (iState == 1)
            {
                switch (go.name)
                {
                    case "Main_Pos1":
                        _XRCubeUDPSender.f_SendCus1(5);
                        break;

                    case "Main_Pos2":
                        _XRCubeUDPSender.f_SendCus1(6);
                        break;

                    case "Main_Pos3":
                        _XRCubeUDPSender.f_SendCus1(2);
                        break;

                    case "Main_Pos4":
                        _XRCubeUDPSender.f_SendCus1(1);
                        break;

                    case "Main_Pos5":
                        _XRCubeUDPSender.f_SendCus1(3);
                        break;

                    case "Main_Pos6":
                        _XRCubeUDPSender.f_SendCus1(4);
                        break;

                    case "BtnEditState":
                        _XRCubeUDPSender.f_SendCus1(7);
                        break;

                    case "BtnEditPoint":
                        _XRCubeUDPSender.f_SendCus1(8);
                        break;
                }
            }
            else if (iState == 2)
            {
                go.GetComponentInParent<EditDisplayText>().f_InputValue();
            }
        }
        #endregion
    }
}
