using System;
using System.Collections;
using System.Collections.Generic;
using ccU3DEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic
{
    public class UI_ServerFailure : ccUILogicBase
    {
        private Text _textMsg;
        private Button _btnPanel;

        protected override void On_Init()
        {
            _textMsg = f_GetObject("Text").GetComponent<Text>();
            _btnPanel = f_GetObject("Panel").GetComponent<Button>();

            f_RegClickEvent("Panel", OnClick_Panel);
        }

        protected override void On_Open(object e)
        {
            MessageBox.DEBUG(LanguageManager.GetInstance().f_GetText("SvrErr_ConnectionFailed"));
            _textMsg.text = LanguageManager.GetInstance().f_GetText("SvrErr_ConnectionFailed");

            // For controllers like keyboard or game pad.
            EventSystem.current.SetSelectedGameObject(_btnPanel.gameObject);
        }

        protected override void On_Close()
        {
        }

        protected override void On_Update()
        {
            if (glo_Main.GetInstance().m_GameSocket.m_bIsConnected)
            {
                f_Close();
                ccUIManage.GetInstance().f_SendMsg(StrUI.Login, BaseUIMessageDef.UI_OPEN);
            }
        }

        protected override void On_UpdateGUI()
        {
        }

        protected override void On_Destory()
        {
        }



        private void OnClick_Panel(GameObject go, object obj1, object obj2)
        {
            MessageBox.DEBUG("Retry Connect");
            glo_Main.GetInstance().m_GameSocket.f_Connect(GloData.glo_strSvrIP, GloData.glo_iSvrPort);
            //#if UNITY_EDITOR
            //            UnityEditor.EditorApplication.isPlaying = false;
            //#else
            //            Application.Quit();
            //#endif
        }
    }
}