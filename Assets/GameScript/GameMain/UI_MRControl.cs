using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;

namespace GameLogic
{
    public class UI_MRControl : ccUILogicBase
    {
        private XRCubeUDPSender _XRCubeUDPSender;

        protected override void On_Init()
        {
            MessageBox.DEBUG("啟用遊戲包中的UI_MRControl腳本");
            _XRCubeUDPSender = f_GetObject("Panel").GetComponent<XRCubeUDPSender>();

            f_RegClickEvent(f_GetObject("BtnPos"), OnClick_MainPanel, 0);
            f_RegClickEvent(f_GetObject("BtnController"), OnClick_MainPanel, 1);
            f_RegClickEvent(f_GetObject("BtnEdit"), OnClick_MainPanel, 2);
            f_RegClickEvent(f_GetObject("BtnSet"), OnClick_MainPanel, 3);

            Button[] buttons = f_GetObject("Main_Edit").GetComponentsInChildren<Button>();
            for(int i = 0; i < buttons.Length; i++)
            {
                f_RegClickEvent(buttons[i].gameObject, f_Send, 1);
            }
        }

        protected override void On_Open(object e)
        {
            
        }

        protected override void On_Close()
        {
            
        }

        protected override void On_Destory()
        {
            
        }

        protected override void On_UpdateGUI()
        {
            
        }

        private void OnClick_MainPanel(GameObject go, object obj1, object obj2)
        {
            int iSet = (int)obj1;
            if (iSet == 3)
            {
                _XRCubeUDPSender.Setip();
            }
            else
            {
                _XRCubeUDPSender.ChangeMain(iSet);
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
                        _XRCubeUDPSender.f_SendCus(1, 3);
                        break;

                    case "Main_Pos2":
                        _XRCubeUDPSender.f_SendCus(1, 4);
                        break;

                    case "Main_Pos3":
                        _XRCubeUDPSender.f_SendCus(1, 2);
                        break;

                    case "Main_Pos4":
                        _XRCubeUDPSender.f_SendCus(1, 1);
                        break;

                    case "Main_Pos5":
                        _XRCubeUDPSender.f_SendCus(1, 6);
                        break;

                    case "Main_Pos6":
                        _XRCubeUDPSender.f_SendCus(1, 5);
                        break;

                    case "BtnEditState":
                        _XRCubeUDPSender.f_SendCus(1, 7);
                        break;

                    case "BtnEditPoint":
                        _XRCubeUDPSender.f_SendCus(1, 8);
                        break;
                }
            }
        }
    }
}
