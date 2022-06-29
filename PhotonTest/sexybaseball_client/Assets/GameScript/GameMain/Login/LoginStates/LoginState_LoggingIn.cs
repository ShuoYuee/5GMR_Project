using System.Collections;
using ccPhotonSocket;
using ccU3DEngine;
using GameLogic;
using SexyBaseball.Server;
using UnityEngine;

public class LoginState_LoggingIn : ccMachineStateBase
{
    private const float NoTimeout = -99;

    private float _fLoginTimeOut = 6;
    private UI_Login _UI_Login;

    private bool isAutomatic = false;

    public LoginState_LoggingIn() : base((int)EM_LoginState.LoggingIn) { }

    public override void f_Enter(object input)
    {
        _UI_Login = (UI_Login)input;

        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.UserLogin_Reps, new CMsg_GTC_LoginRelt(), On_CMsg_GTC_LoginRelt);

        if (string.IsNullOrWhiteSpace(StaticValue.m_strAccount) || string.IsNullOrWhiteSpace(StaticValue.m_strUserPwd))
        {
            isAutomatic = false;
            f_ManuelLogin();
        }
        else
        {
            isAutomatic = true;
            f_AutomaticLogin();
        }
        _UI_Login.f_ShowErrorV2("");
    }

    public override void f_Execute()
    {
        if (_fLoginTimeOut == NoTimeout)
        {
            return;
        }

        _fLoginTimeOut -= Time.deltaTime;
        if (_fLoginTimeOut < 0)
        {
            f_LoginTimeOut();
        }
    }

    public override void f_Exit()
    {
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.UserLogin_Reps);
    }

    private void f_ManuelLogin()
    {
        string username = _UI_Login.f_GetUserName();
        string password = _UI_Login.f_GetUserPwd();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            _UI_Login.f_ShowError(LanguageManager.GetInstance().f_GetText("ErrorStr_Login_UserPwdEmpty"));
            f_SetComplete((int)EM_LoginState.Idle);
            return;
        }

        f_LoginUser();
    }

    private void f_AutomaticLogin()
    {
        f_LoginUser();
    }

    private void f_LoginUser()
    {
        /*var request = new CMsg_LoginUserReq()
        {
            m_szUserName = StaticValue.m_strUsername,
            m_szUserPwd = StaticValue.m_strUserPwd
        };
        MessageBox.DEBUG($"Request username '{request.m_szUserName}'");
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.CS_UserLogin, request);*/

        CMsg_CTG_AccountEnter tCTG_AccountEnter = new CMsg_CTG_AccountEnter();
        tCTG_AccountEnter.m_strAccount = StaticValue.m_strAccount;
        tCTG_AccountEnter.m_strPassword = StaticValue.m_strUserPwd;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.CS_UserLogin, tCTG_AccountEnter);

        _fLoginTimeOut = 6;
    }

    private void f_LoginTimeOut()
    {
        //_UI_Login.f_ShowError(LanguageManager.GetInstance().f_GetText("ErrorStr_Login_Timeout"));
        /*MessageBox.DEBUG("登入超時");

        // Send a fake server broadcast.
        CMsg_LoginUserResp input = new CMsg_LoginUserResp()
        {
            m_iReturnCode = (int)eMsgOperateResult.OR_Error_LoginTimeOut,
            m_iUserId = -1
        };

        On_CMsg_LoginUserResp(input);*/
        CMsg_GTC_LoginRelt tCMsg_GTC_LoginRelt = new CMsg_GTC_LoginRelt();
        tCMsg_GTC_LoginRelt.m_result = (int)eMsgOperateResult.OR_Error_LoginTimeOut;
        tCMsg_GTC_LoginRelt.m_PlayerId = -1;
        On_CMsg_GTC_LoginRelt(tCMsg_GTC_LoginRelt);
    }

    /*private void On_CMsg_LoginUserResp(object input)
    {
        var response = (CMsg_LoginUserResp)input;

        if ((eMsgOperateResult)response.m_iReturnCode == eMsgOperateResult.OR_Succeed)
        {
            MessageBox.DEBUG("��½�ɹ�.UserId:" + response.m_iUserId);



            GameDataLoad.f_SaveGameSystemMemory();

            ccUIManage.GetInstance().f_SendMsg(StrUI.Start, BaseUIMessageDef.UI_OPEN);
            MessageBox.DEBUG("Logged in successfully.");

            f_SetComplete((int)EM_LoginState.Idle);
            ccUIManage.GetInstance().f_SendMsg(StrUI.Login, BaseUIMessageDef.UI_CLOSE);

            return;
        }
        else if (response.m_iReturnCode == ReturnCode.NotExist)
        {
            _UI_Login.f_ShowError(LanguageManager.GetInstance().f_GetText("ErrorStr_Login_NotExist"));
        }
        else if ((eMsgOperateResult)response.m_iReturnCode == eMsgOperateResult.OR_Error_ServerOffLine)
        { 
            _UI_Login.f_ShowError("ErrorStr_ServerOffLine");//TODO：要處理文本資料_多語系
        }
        else
        {
            _UI_Login.f_ShowError(LanguageManager.GetInstance().f_GetText("ErrorStr_Login_Failure"));
        }

        f_SetComplete((int)EM_LoginState.Idle);
    }*/

    void On_CMsg_GTC_LoginRelt(object Obj)
    {
        _fLoginTimeOut = NoTimeout;
        CMsg_GTC_LoginRelt tCMsg_GTC_LoginRelt = (CMsg_GTC_LoginRelt)Obj;
        if (tCMsg_GTC_LoginRelt.m_result == (int)eMsgOperateResult.OR_Succeed)
        {
            MessageBox.DEBUG("UserId:" + tCMsg_GTC_LoginRelt.m_PlayerId);
            GameDataLoad.f_SaveGameSystemMemory();
            GameDataLoad.f_LoginGame(tCMsg_GTC_LoginRelt.m_userName, tCMsg_GTC_LoginRelt.m_PlayerId, tCMsg_GTC_LoginRelt.m_iTeam);

            ccUIManage.GetInstance().f_SendMsg(StrUI.Start, BaseUIMessageDef.UI_OPEN);
            MessageBox.DEBUG("Logged in successfully.");

            ccUIManage.GetInstance().f_SendMsg(StrUI.Login, BaseUIMessageDef.UI_CLOSE);
            return;
        }
        else if (tCMsg_GTC_LoginRelt.m_result == (int)eMsgOperateResult.OR_Error_LoginTimeOut)
        {
            MessageBox.DEBUG("登入超時");
            _UI_Login.f_ShowErrorV2("登入超時");
        }
        else if (tCMsg_GTC_LoginRelt.m_result == (int)eMsgOperateResult.OR_Error_Password)
        {
            MessageBox.DEBUG("登入失敗，密碼錯誤");
            _UI_Login.f_ShowErrorV2("登入失敗，密碼錯誤");
        }
        else if (tCMsg_GTC_LoginRelt.m_result == (int)eMsgOperateResult.OR_Error_NoAccount)
        {
            MessageBox.DEBUG("帳戶未註冊");
            _UI_Login.f_ShowErrorV2("帳戶未註冊");
        }
        else
        {
            MessageBox.DEBUG("出現未知錯誤");
            _UI_Login.f_ShowErrorV2("出現未知錯誤");
        }
    }

    void On_Cmsg_GTC_CreateRelt(object Obj)
    {
        CMsg_CTG_AccountCreateRelt tCMsg_CTG_AccountCreateRelt = (CMsg_CTG_AccountCreateRelt)Obj;
        if (tCMsg_CTG_AccountCreateRelt.m_iResult == (int)eMsgOperateResult.OR_Succeed)
        {
            MessageBox.DEBUG("註冊成功：" + tCMsg_CTG_AccountCreateRelt.m_strAccount);
        }
        else if (tCMsg_CTG_AccountCreateRelt.m_iResult == (int)eMsgOperateResult.OR_Error_AccountRepetition)
        {
            MessageBox.DEBUG("註冊失敗；" + tCMsg_CTG_AccountCreateRelt.m_strAccount + "已被註冊");
        }
        else
        {
            MessageBox.DEBUG("出現未知錯誤，請重新操作");
        }
    }
}
