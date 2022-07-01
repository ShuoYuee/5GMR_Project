using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;
using MR_Edit;

/// <summary>
/// 啦啦隊遊戲
/// </summary>
public class GameCheLead : MonoBehaviour
{
    public Animator _ModelAnimA, _ModelAnimB;
    public GameObject _MainPanel, _BtnJoin;
    public Text _ScoreText, _InforText, _TimeText;
    public Button _BtnTeamA, _BtnTeamB, _BtnStart;

    private static GameCheLead _Instance = null;
    public static GameCheLead GetInstance()
    {
        return _Instance;
    }

    private void Awake()
    {
        f_Init();
    }

    public void f_Init()
    {
        _Instance = this;
        f_UpdateScore("");
        f_UpdateInfor("");
        _BtnStart.onClick.AddListener(f_Start);

        glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.Guess_ExitRoom, f_ExitRoom);
    }

    #region 遊戲UI流程
    private void f_ExitRoom(object obj)
    {
        _BtnJoin.SetActive(true);
        _MainPanel.SetActive(false);
    }

    public void f_Start()
    {
        _MainPanel.SetActive(true);
        _BtnJoin.gameObject.SetActive(false);
        _BtnStart.gameObject.SetActive(false);
        _BtnTeamA.gameObject.SetActive(true);
        _BtnTeamB.gameObject.SetActive(true);

        MessageBox.DEBUG("開啟遊戲，成為房主");
        CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
        tCMsg_CTG_ServerCommand.m_szAccount = StaticValue.m_strAccount;
        tCMsg_CTG_ServerCommand.m_lPlayerID = StaticValue.m_lPlayerID;
        tCMsg_CTG_ServerCommand.m_iCommand = (int)EM_GameMod.Guess;
        tCMsg_CTG_ServerCommand.m_iCallState = (int)EM_GuessState.Start;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.ServerCommand, tCMsg_CTG_ServerCommand);
    }

    public void f_WaitRoom()
    {//等待遊戲開始
        _MainPanel.SetActive(true);
        _BtnJoin.gameObject.SetActive(false);
        _BtnStart.gameObject.SetActive(true);
        _BtnTeamA.gameObject.SetActive(false);
        _BtnTeamB.gameObject.SetActive(false);
        f_UpdateInfor("遊戲還未開始");
        f_UpdateScore("");
        f_UpdateTime("");
    }

    public void f_WaitGameEnter()
    {//等待該回合結束
        _MainPanel.SetActive(true);
        _BtnJoin.gameObject.SetActive(false);

        string strInfor = "等待該回合結束......";
        f_UpdateInfor(strInfor);
        f_DisEnableBtn();
    }

    public void f_ReGame()
    {//重啟遊戲
        f_EnableBtn();
        f_UpdateInfor("猜測\\n哪位隊員會獲勝？");
    }

    public void f_GameOver()
    {//遊戲結束
        f_UpdateInfor("遊戲已結束");
    }
    #endregion

    public void f_EnableBtn()
    {
        _BtnTeamA.interactable = true;
        _BtnTeamB.interactable = true;
    }

    public void f_DisEnableBtn()
    {
        _BtnTeamA.interactable = false;
        _BtnTeamB.interactable = false;
    }

    public void f_UpdateScore(string strInfor)
    {
        GameTools.f_SetText(_ScoreText, "分數：" + strInfor);
    }

    public void f_UpdateInfor(string strInfor)
    {
        GameTools.f_SetText(_InforText, strInfor);
    }
    
    public void f_UpdateTime(string strInfor)
    {
        GameTools.f_SetText(_TimeText, strInfor);
    }
}
