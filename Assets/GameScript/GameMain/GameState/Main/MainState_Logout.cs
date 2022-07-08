using System;
using System.Collections.Generic;
using GameLogic;
using ccU3DEngine;
using MR_Edit;

public class MainState_Logout : ccMachineStateBase
{
    //private UI_MRControl UI_MRControl;

    public MainState_Logout() : base((int)EM_MainState.Logout)
    {

    }

    public override void f_Enter(object Obj)
    {
        base.f_Enter(Obj);
        MessageBox.DEBUG("進入MainState_Logout狀態");
        MessageBox.DEBUG("玩家：" + StaticValue.m_strUserName + "   登出遊戲");

        CMsg_CTG_AccountExit tCMsg_CTG_AccountExit = new CMsg_CTG_AccountExit();
        tCMsg_CTG_AccountExit.m_strAccount = StaticValue.m_strAccount;
        tCMsg_CTG_AccountExit.m_iPlayerID = StaticValue.m_lPlayerID;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.CS_UserLogout, tCMsg_CTG_AccountExit);
        ccTimeEvent.GetInstance().f_RegEvent(1f, false, null, CallBack_Logout);
    }

    private void CallBack_Logout(object Obj)
    {
        GameDataLoad.f_SaveGameSystemMemory();
        GameDataLoad.f_LogoutGame();
        glo_Main.GetInstance().f_Destroy();
    }
}
