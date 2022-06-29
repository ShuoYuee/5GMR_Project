using System;
using System.Collections;
using System.Collections.Generic;
using ccU3DEngine;
using SexyBaseball.Server;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic
{
    public class UI_Start : ccUILogicBase
    {

        protected override void On_Init()
        {
            f_RegClickEvent("Panel", OnClick_Bg);
            //f_GetObject("EnterGameMask").SetActive(true);
        }

        protected override void On_Open(object e)
        {
            PlayerEnterGame();
        }

        void PlayerEnterGame()
        {
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_EnterGameSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_EnterGameFail;
            Debug.Log("PlayerEnterGame");
        }

        void CallBack_EnterGameSuc(object Obj)
        {
            MessageBox.DEBUG("CallBack_EnterGameSuc");
            //TODO：更新玩家資料  CMsg_SendPlayerInfor
            //f_GetObject("EnterGameMask").SetActive(false);
        }

        void CallBack_EnterGameFail(object Obj)
        {
            eMsgOperateResult teMsgOperateResult = (eMsgOperateResult)Obj;
            MessageBox.DEBUG("EnterGameFail:" + teMsgOperateResult.ToString());

        }

        protected override void On_Close()
        {
        }

        protected override void On_Update()
        {
        }

        protected override void On_UpdateGUI()
        {
        }

        protected override void On_Destory()
        {
        }

        private void OnClick_Bg(GameObject go, object obj1, object obj2)
        {
            f_Close();
            ccUIManage.GetInstance().f_SendMsg(StrUI.MainMenu, BaseUIMessageDef.UI_OPEN);
        }
    }
}