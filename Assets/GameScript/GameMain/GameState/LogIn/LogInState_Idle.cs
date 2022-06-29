using System;
using System.Collections.Generic;
using ccU3DEngine;
using GameLogic;
using MR_Edit;

public class LogInState_Idle : ccMachineStateBase
{
    private UI_Login UI_GameLogin;

    public LogInState_Idle() : base((int)EM_LogInState.Idle)
    {

    }

    public override void f_Enter(object Obj)
    {
        base.f_Enter(Obj);
        UI_GameLogin = (UI_Login)Obj;

        UI_GameLogin.f_UpdataText(0, "");
        UI_GameLogin.f_EnableSelectables();
    }
}