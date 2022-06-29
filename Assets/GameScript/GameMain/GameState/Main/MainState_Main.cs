using System;
using System.Collections.Generic;
using ccU3DEngine;
using GameLogic;
using MR_Edit;

public class MainState_Main : ccMachineStateBase
{
    private UI_MRControl UI_MRControl;

    public MainState_Main() : base((int)EM_MainState.Main)
    {

    }

    public override void f_Enter(object Obj)
    {
        base.f_Enter(Obj);
        MessageBox.DEBUG("進入MainState_Main狀態");
        UI_MRControl = (UI_MRControl)Obj;

        glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.Guess_MainLogOut, f_LogOut);
        glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.Guess_JoinRoom, f_JoinRoom);
    }

    public override void f_Execute()
    {
        base.f_Execute();
    }

    public override void f_Exit()
    {
        base.f_Exit();
        glo_Main.GetInstance().m_GameMessagePool.f_RemoveListener(MessageDef.Guess_MainLogOut, f_LogOut);
        glo_Main.GetInstance().m_GameMessagePool.f_RemoveListener(MessageDef.Guess_JoinRoom, f_JoinRoom);
    }

    private void f_JoinRoom(object Obj)
    {
        UI_MRControl._machineManager.f_ChangeState((int)EM_MainState.Guess);
    }

    private void f_LogOut(object Obj)
    {
        UI_MRControl._machineManager.f_ChangeState((int)EM_MainState.Logout);
    }
}
