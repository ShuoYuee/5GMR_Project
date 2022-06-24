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

    public MainState_MenuMain() : base((int)EM_MainState.MenuMain)
    {

    }

    public override void f_Enter(object Obj)
    {
        base.f_Enter(Obj);
        UI_MainMenu = (UI_MainMenu)Obj;
        _fWaitTime = 0;
        _fWaitTime2 = _fGameNotTime;
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.CallPlayerGuess, new CMsg_CTG_GuessCallPlay(), On_CMsg_GTC_CallGuess);
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.PlayerGuessResult, new CMsg_CTG_GuessResult(), On_CMsg_GTC_GuessRelt);
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.GameRestart, new CMsg_CTG_GuessReGame(), On_CMsg_GTC_GameRestart);
    }

    public override void f_Execute()
    {
        base.f_Execute();
        if (_fWaitTime != _fGameNotTime)
        {//猜測倒數
            _fWaitTime += Time.deltaTime;
            if (_fWaitTime >= 20f)
            {
                _fWaitTime = _fGameNotTime;
                _fWaitTime2 = 0;
                CMsg_CTG_GuessCommand tCMsg_CTG_GuessCommand = new CMsg_CTG_GuessCommand();
                tCMsg_CTG_GuessCommand.m_iCheckState = (int)EM_GuessState.Guess;
                glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.GamePlayCheck, tCMsg_CTG_GuessCommand);
            }
        }
        if (_fWaitTime2 != _fGameNotTime)
        {//休息倒數
            _fWaitTime2 += Time.deltaTime;
            if (_fWaitTime2 >= 10f)
            {
                _fWaitTime2 = _fGameNotTime;
                _fWaitTime = 0;
                CMsg_CTG_GuessCommand tCMsg_CTG_GuessCommand = new CMsg_CTG_GuessCommand();
                tCMsg_CTG_GuessCommand.m_iCheckState = (int)EM_GuessState.Restart;
                glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.GamePlayCheck, tCMsg_CTG_GuessCommand);
            }
        }
    }

    public override void f_Exit()
    {
        base.f_Exit();
        _fWaitTime = _fGameNotTime;
        _fWaitTime2 = _fGameNotTime;
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.CallPlayerGuess);
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.PlayerGuessResult);
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.GameRestart);
    }

    private void On_CMsg_GTC_CallGuess(object obj)
    {
        int iCurSelTeam = UI_MainMenu._iCurSelTeam;
        CMsg_CTG_Guess tCMsg_CTG_Guess = new CMsg_CTG_Guess();
        tCMsg_CTG_Guess.m_iGuess = UI_MainMenu._iCurSelTeam;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.PlayerGuess, tCMsg_CTG_Guess);
    }

    private void On_CMsg_GTC_GuessRelt(object obj)
    {
        CMsg_CTG_GuessResult tCMsg_CTG_GuessResult = (CMsg_CTG_GuessResult)obj;
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
        UI_MainMenu.f_UpdateText(1, "分數：" + tCMsg_CTG_GuessResult.m_Score);
    }

    private void On_CMsg_GTC_GameRestart(object obj)
    {//遊戲重啟
        UI_MainMenu.f_ReGame();
        UI_MainMenu.f_UpdateText(0, "等待猜測中......");
    }
}
