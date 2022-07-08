using System;
using System.Collections.Generic;
using ccU3DEngine;
using GameLogic;
using MR_Edit;

public class MainState_Edit : ccMachineStateBase
{
    private UI_MRControl UI_MRControl;

    public MainState_Edit() : base((int)EM_MainState.Edit)
    {

    }

    public override void f_Enter(object Obj)
    {
        base.f_Enter(Obj);
        UI_MRControl = (UI_MRControl)Obj;
        glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.MainLogOut, f_LogOut);
    }

    public override void f_Exit()
    {
        base.f_Exit();
        glo_Main.GetInstance().m_GameMessagePool.f_RemoveListener(MessageDef.MainLogOut, f_LogOut);
    }

    private void f_LogOut(object Obj)
    {
        UI_MRControl._machineManager.f_ChangeState((int)EM_MainState.Logout);
    }
}
