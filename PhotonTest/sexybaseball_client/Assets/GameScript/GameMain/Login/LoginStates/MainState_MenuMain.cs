using System;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;
using GameLogic;
using SexyBaseball.Server;

public class MainState_MenuMain : ccMachineStateBase
{
    private UI_MainMenu UI_MainMenu;
    private float _fWaitTime = 0, _fWaitTime2 = 0, _fGameNotTime = -99;
    private float _TestTime = 1;

    public MainState_MenuMain() : base((int)EM_MainState.MenuMain)
    {

    }

    public override void f_Enter(object Obj)
    {
        base.f_Enter(Obj);
        MessageBox.DEBUG("進入遊玩狀態");
        UI_MainMenu = (UI_MainMenu)Obj;
        _fWaitTime = 10;
        _fWaitTime2 = _fGameNotTime;
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.PlayerGuessResult, new CMsg_CTG_GetScoreResult(), On_CMsg_GTC_GuessRelt);
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.ClientCommand, new CMsg_CTG_ClientCommand(), On_CMsg_GTC_CommandCall);
        MessageBox.DEBUG("遊玩狀態準備完成");
    }

    public override void f_Execute()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            _TestTime = 0;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            _TestTime = 1;
        }

        if (_fWaitTime != _fGameNotTime)
        {//猜測倒數
            _fWaitTime -= Time.deltaTime * _TestTime;
            UI_MainMenu.f_UpdateText(2, ((int)_fWaitTime).ToString());
            if (_fWaitTime <= 0f)
            {
                MessageBox.DEBUG("選擇時間結束");
                _fWaitTime = _fGameNotTime;

                if (UI_MainMenu._bMainGameCtrl)
                {
                    CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
                    tCMsg_CTG_ServerCommand.m_szAccount = StaticValue.m_strAccount;
                    tCMsg_CTG_ServerCommand.m_lPlayerID = StaticValue.m_iUserID;
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
            _fWaitTime2 -= Time.deltaTime * _TestTime;
            UI_MainMenu.f_UpdateText(2, ((int)_fWaitTime2).ToString());
            if (_fWaitTime2 <= 0f)
            {
                MessageBox.DEBUG("休息時間結束");
                _fWaitTime2 = _fGameNotTime;
                
                if (UI_MainMenu._bMainGameCtrl)
                {
                    CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
                    tCMsg_CTG_ServerCommand.m_szAccount = StaticValue.m_strAccount;
                    tCMsg_CTG_ServerCommand.m_lPlayerID = StaticValue.m_iUserID;
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
        MessageBox.DEBUG("離開遊玩狀態");
        _fWaitTime = _fGameNotTime;
        _fWaitTime2 = _fGameNotTime;
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.ClientCommand);
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.PlayerGuessResult);
    }

    private void On_CMsg_GTC_CommandCall(object obj)
    {
        CMsg_CTG_ClientCommand tCMsg_CTG_ClientCommand = (CMsg_CTG_ClientCommand)obj;
        if (tCMsg_CTG_ClientCommand.m_iCallState == (int)EM_GuessState.CallGuess)
        {//執行猜測
            MessageBox.DEBUG("進行猜測");
            CMsg_CTG_Guess tCMsg_CTG_Guess = new CMsg_CTG_Guess();
            tCMsg_CTG_Guess.m_szAccount = StaticValue.m_strAccount;
            tCMsg_CTG_Guess.m_lPlayerID = StaticValue.m_iUserID;
            tCMsg_CTG_Guess.m_iGuess = UI_MainMenu._iCurSelTeam;
            glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.PlayerGuess, tCMsg_CTG_Guess);

            ccTimeEvent.GetInstance().f_RegEvent(1f, false, null, CallBack_GetScore);
        }
        else if(tCMsg_CTG_ClientCommand.m_iCallState == (int)EM_GuessState.CallRestart)
        {//遊戲重啟
            MessageBox.DEBUG("新一輪開始");
            UI_MainMenu.f_ReGame();
            _fWaitTime = 10;
        }
        else if (tCMsg_CTG_ClientCommand.m_iCallState == (int)EM_GuessState.CallEnd)
        {//遊戲結束
            MessageBox.DEBUG("遊戲結束");
            UI_MainMenu.f_GameOver();
            UI_MainMenu.f_UpdateText(0, "遊戲已結束");
        }
    }

    private void CallBack_GetScore(object Obj)
    {
        CMsg_CTG_GetScore tCMsg_CTG_GetScore = new CMsg_CTG_GetScore();
        tCMsg_CTG_GetScore.m_szAccount = StaticValue.m_strAccount;
        tCMsg_CTG_GetScore.m_lPlayerID = StaticValue.m_iUserID;
        tCMsg_CTG_GetScore.m_iGuess = UI_MainMenu._iCurSelTeam;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.GameScore, tCMsg_CTG_GetScore);
    }

    private void On_CMsg_GTC_GuessRelt(object obj)
    {//猜測結果
        MessageBox.DEBUG("本回合結束");
        CMsg_CTG_GetScoreResult tCMsg_CTG_GuessResult = (CMsg_CTG_GetScoreResult)obj;
        if (tCMsg_CTG_GuessResult.m_iResult == (int)EM_GuessResult.Win)
        {
            UI_MainMenu.f_UpdateText(0, "你贏了！");
        }
        else if (tCMsg_CTG_GuessResult.m_iResult == (int)EM_GuessResult.Lost)
        {
            UI_MainMenu.f_UpdateText(0, "你輸了");
        }
        else
        {
            UI_MainMenu.f_UpdateText(0, "未參與本輪遊戲");
        }
        //更新自己所在组的分数
        if (StaticValue.m_iTeam == (int)EM_TeamID.TeamA)
        {
            UI_MainMenu.f_UpdateText(1, "分數：" + tCMsg_CTG_GuessResult.m_iScoreA);
        }
        else if (StaticValue.m_iTeam == (int)EM_TeamID.TeamB)
        {
            UI_MainMenu.f_UpdateText(1, "分數：" + tCMsg_CTG_GuessResult.m_iScoreB);
        }
        _fWaitTime2 = 5;
    }
}
