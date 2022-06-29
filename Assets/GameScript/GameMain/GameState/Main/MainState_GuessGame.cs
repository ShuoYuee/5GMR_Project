using System;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;
using GameLogic;
using MR_Edit;

public class MainState_GuessGame : ccMachineStateBase
{
    private UI_MRControl UI_MRControl;
    private float _fWaitTime = 0, _fWaitTime2 = 0, _fGameNotTime = -99;

    public bool _bMainGameCtrl = false;
    public int _iCurSelTeam = 0;

    public MainState_GuessGame() : base((int)EM_MainState.Guess)
    {
        
    }

    public override void f_Enter(object Obj)
    {
        base.f_Enter(Obj);
        MessageBox.DEBUG("進入MainState_GuessGame狀態");
        UI_MRControl = (UI_MRControl)Obj;

        _fWaitTime = 10;
        _fWaitTime2 = _fGameNotTime;
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.PlayerGuessResult, new CMsg_CTG_GetScoreResult(), On_CMsg_GTC_GuessRelt);
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.ClientCommand, new CMsg_CTG_ClientCommand(), On_CMsg_GTC_CommandCall);
        glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.Guess_SelGuessTeam, f_SelGuessTeam);
        glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.Guess_ExitRoom, f_ExitRoom);
        glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.Guess_MainLogOut, f_LogOut);
    }

    public override void f_Execute()
    {
        if (_fWaitTime != _fGameNotTime)
        {//猜測倒數
            _fWaitTime -= Time.deltaTime;
            UI_MRControl.f_UpdateText(2, ((int)_fWaitTime).ToString());
            if (_fWaitTime <= 0f)
            {
                MessageBox.DEBUG("選擇時間結束");
                _fWaitTime = _fGameNotTime;

                if (_bMainGameCtrl)
                {
                    CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
                    tCMsg_CTG_ServerCommand.m_szAccount = StaticValue.m_strAccount;
                    tCMsg_CTG_ServerCommand.m_lPlayerID = StaticValue.m_lPlayerID;
                    tCMsg_CTG_ServerCommand.m_iCommand = (int)EM_GameMod.Guess;
                    tCMsg_CTG_ServerCommand.m_iCallState = (int)EM_GuessState.Guess;
                    glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.ServerCommand, tCMsg_CTG_ServerCommand);
                }
                else
                {
                    MessageBox.DEBUG("等待房主");
                }
            }
        }
        if (_fWaitTime2 != _fGameNotTime)
        {//休息倒數
            _fWaitTime2 -= Time.deltaTime;
            UI_MRControl.f_UpdateText(2, ((int)_fWaitTime2).ToString());
            if (_fWaitTime2 <= 0f)
            {
                MessageBox.DEBUG("休息時間結束");
                _fWaitTime2 = _fGameNotTime;

                if (_bMainGameCtrl)
                {
                    CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
                    tCMsg_CTG_ServerCommand.m_szAccount = StaticValue.m_strAccount;
                    tCMsg_CTG_ServerCommand.m_lPlayerID = StaticValue.m_lPlayerID;
                    tCMsg_CTG_ServerCommand.m_iCommand = (int)EM_GameMod.Guess;
                    tCMsg_CTG_ServerCommand.m_iCallState = (int)EM_GuessState.Restart;
                    glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.ServerCommand, tCMsg_CTG_ServerCommand);
                }
                else
                {
                    MessageBox.DEBUG("等待房主");
                }
            }
        }
        base.f_Execute();
    }

    public override void f_Exit()
    {
        base.f_Exit();
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.PlayerGuessResult);
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.ClientCommand);
        glo_Main.GetInstance().m_GameMessagePool.f_RemoveListener(MessageDef.Guess_SelGuessTeam, f_SelGuessTeam);
         glo_Main.GetInstance().m_GameMessagePool.f_RemoveListener(MessageDef.Guess_MainLogOut, f_LogOut);
    }

    private void f_SelGuessTeam(object Obj)
    {
        _iCurSelTeam = (int)Obj;
    }

    private void f_ExitRoom(object Obj)
    {
        UI_MRControl._machineManager.f_ChangeState((int)EM_MainState.Main);
    }

    #region GTC
    private void On_CMsg_GTC_CommandCall(object obj)
    {
        CMsg_CTG_ClientCommand tCMsg_CTG_ClientCommand = (CMsg_CTG_ClientCommand)obj;
        if (tCMsg_CTG_ClientCommand.m_iCallState == (int)EM_GuessState.CallGuess)
        {//執行猜測
            MessageBox.DEBUG("進行猜測");
            CMsg_CTG_Guess tCMsg_CTG_Guess = new CMsg_CTG_Guess();
            tCMsg_CTG_Guess.m_szAccount = StaticValue.m_strAccount;
            tCMsg_CTG_Guess.m_lPlayerID = StaticValue.m_lPlayerID;
            tCMsg_CTG_Guess.m_iGuess = _iCurSelTeam;
            glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.PlayerGuess, tCMsg_CTG_Guess);

            ccTimeEvent.GetInstance().f_RegEvent(0.5f, false, null, CallBack_GetScore);
        }
        else if (tCMsg_CTG_ClientCommand.m_iCallState == (int)EM_GuessState.CallRestart)
        {//遊戲重啟
            MessageBox.DEBUG("新一輪開始");
            UI_MRControl.f_ReGame();
            _fWaitTime = 11;
        }
        else if (tCMsg_CTG_ClientCommand.m_iCallState == (int)EM_GuessState.CallEnd)
        {//遊戲結束
            MessageBox.DEBUG("遊戲結束");
            UI_MRControl.f_GameOver();
            UI_MRControl.f_UpdateText(0, "遊戲已結束");
        }
    }

    private void CallBack_GetScore(object Obj)
    {
        CMsg_CTG_GetScore tCMsg_CTG_GetScore = new CMsg_CTG_GetScore();
        tCMsg_CTG_GetScore.m_szAccount = StaticValue.m_strAccount;
        tCMsg_CTG_GetScore.m_lPlayerID = StaticValue.m_lPlayerID;
        tCMsg_CTG_GetScore.m_iGuess = _iCurSelTeam;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.GameScore, tCMsg_CTG_GetScore);
    }

    private void On_CMsg_GTC_GuessRelt(object obj)
    {//猜測結果
        MessageBox.DEBUG("本回合結束");
        CMsg_CTG_GetScoreResult tCMsg_CTG_GuessResult = (CMsg_CTG_GetScoreResult)obj;
        if (tCMsg_CTG_GuessResult.m_iResult == (int)EM_GuessResult.Win)
        {
            UI_MRControl.f_UpdateText(0, "你贏了！");
        }
        else if (tCMsg_CTG_GuessResult.m_iResult == (int)EM_GuessResult.Lost)
        {
            UI_MRControl.f_UpdateText(0, "你輸了");
        }
        else
        {
            UI_MRControl.f_UpdateText(0, "未參與本輪遊戲");
        }
        //更新自己所在组的分数
        if (StaticValue.m_iTeam == (int)EM_TeamID.TeamA)
        {
            UI_MRControl.f_UpdateText(1, "分數：" + tCMsg_CTG_GuessResult.m_iScoreA);
        }
        else if (StaticValue.m_iTeam == (int)EM_TeamID.TeamB)
        {
            UI_MRControl.f_UpdateText(1, "分數：" + tCMsg_CTG_GuessResult.m_iScoreB);
        }
        _fWaitTime2 = 5;
    }
    #endregion

    private void f_LogOut(object Obj)
    {//登出
        if (_bMainGameCtrl)
        {//若為房主則關閉遊戲
            MessageBox.DEBUG("房主關閉遊戲");
            _bMainGameCtrl = false;

            CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
            tCMsg_CTG_ServerCommand.m_szAccount = StaticValue.m_strAccount;
            tCMsg_CTG_ServerCommand.m_lPlayerID = StaticValue.m_lPlayerID;
            tCMsg_CTG_ServerCommand.m_iCommand = (int)EM_GameMod.Guess;
            tCMsg_CTG_ServerCommand.m_iCallState = (int)EM_GuessState.End;
            glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.ServerCommand, tCMsg_CTG_ServerCommand);
        }
        ccTimeEvent.GetInstance().f_RegEvent(1f, false, null, CallBack_Logout);
    }

    private void CallBack_Logout(object Obj)
    {
        UI_MRControl._machineManager.f_ChangeState((int)EM_MainState.Logout);
    }
}
