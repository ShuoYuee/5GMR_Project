using System;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;
using ccU3DEngine;
using MR_Edit;

public class LogInState_LogIn : ccMachineStateBase
{
    private UI_Login UI_GameLogin;
    private float _fLogInTime = 0f, _fNotTime = -99;
    private int _iWait = 0;
    private string _strWaitInfor = "請稍後", _strWait = "";
    private float text;

    public LogInState_LogIn() : base((int)EM_LogInState.LoggingIn)
    {
        
    }

    public override void f_Enter(object Obj)
    {
        base.f_Enter(Obj);
        UI_GameLogin = (UI_Login)Obj;
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.UserLogin_Reps, new CMsg_GTC_LoginRelt(), OnGTC_CMsg_LogInRelt);

        UI_GameLogin.f_UpdataText(0, "請稍後......");
        UI_GameLogin.f_UpdataText(1, "");

        _strWait = _strWaitInfor;
        _iWait = 0;
        _fLogInTime = 6f;
    }

    public override void f_Execute()
    {
        base.f_Execute();
        if (_fLogInTime != _fNotTime)
        {
            _fLogInTime += Time.deltaTime;
            if (_fLogInTime >= 1)
            {
                _iWait += 1;
                _fLogInTime = 0;
                _strWait = _strWait + ".";
                UI_GameLogin.f_UpdataText(0, _strWait);

                if (_iWait >= 6)
                {
                    CMsg_GTC_LoginRelt cMsg_GTC_LoginRelt = new CMsg_GTC_LoginRelt();
                    cMsg_GTC_LoginRelt.m_result = (int)eMsgOperateResult.OR_Error_LoginTimeOut;
                    OnGTC_CMsg_LogInRelt(cMsg_GTC_LoginRelt);
                }
            }
        }
    }

    public override void f_Exit()
    {
        base.f_Exit();
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.UserLogin_Reps);
    }

    private void OnGTC_CMsg_LogInRelt(object Obj)
    {
        _fLogInTime = _fNotTime;
        _iWait = 0;
        UI_GameLogin.f_UpdataText(0, "");

        CMsg_GTC_LoginRelt tCMsg_GTC_LoginRelt = (CMsg_GTC_LoginRelt)Obj;
        if (tCMsg_GTC_LoginRelt.m_result == (int)eMsgOperateResult.OR_Succeed)
        {
            MessageBox.DEBUG("UserId:" + tCMsg_GTC_LoginRelt.m_PlayerId);
            GameDataLoad.f_SaveGameSystemMemory();
            GameDataLoad.f_LoginGame(tCMsg_GTC_LoginRelt.m_userName, tCMsg_GTC_LoginRelt.m_PlayerId, tCMsg_GTC_LoginRelt.m_iTeam);

            MessageBox.DEBUG("Logged in successfully.");
            ccUIManage.GetInstance().f_SendMsgV3("ui_login.bundle", "UI_Login", UIMessageDef.UI_CLOSE);
            //ccSceneMgr.GetInstance().f_ChangeScene("GameMain");
            ccSceneMgr.GetInstance().f_ChangeScene("Cheerleading");
            return;
        }
        else if (tCMsg_GTC_LoginRelt.m_result == (int)eMsgOperateResult.OR_Error_LoginTimeOut)
        {
            MessageBox.DEBUG("登入超時");
            UI_GameLogin.f_UpdataText(1, "登入超時");
            f_SetComplete((int)EM_LogInState.Idle, UI_GameLogin);
        }
        else if (tCMsg_GTC_LoginRelt.m_result == (int)eMsgOperateResult.OR_Error_Password)
        {
            MessageBox.DEBUG("登入失敗，密碼錯誤");
            UI_GameLogin.f_UpdataText(1, "登入失敗，密碼錯誤");
            f_SetComplete((int)EM_LogInState.Idle, UI_GameLogin);
        }
        else if (tCMsg_GTC_LoginRelt.m_result == (int)eMsgOperateResult.OR_Error_NoAccount)
        {
            MessageBox.DEBUG("帳戶未註冊");
            UI_GameLogin.f_UpdataText(1, "帳戶未註冊");
            f_SetComplete((int)EM_LogInState.Idle, UI_GameLogin);
        }
        else
        {
            MessageBox.DEBUG("出現未知錯誤");
            UI_GameLogin.f_UpdataText(1, "出現未知錯誤");
            f_SetComplete((int)EM_LogInState.Idle, UI_GameLogin);
        }
    }
}
