using MR_Edit;
using ccU3DEngine;

public class GuessPool
{
    private int _iCurScoreA, _iCurScoreB;
    private int _iCurWin;

    private ccCallback _GuessSucCall, _GuessFailCall;
    private ccCallback _GameReltSucCall, _GameReltFailCall;
    private ccCallback _CommandReltSucCall, _CommandReltFailCall;

    public void f_InitManager()
    {//開始監聽訊息
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.GamePlayCheckRelt, new CMsg_CTG_CheckGuessRelt(), On_CMsg_GTC_CheckGameRelt);
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.PlayerGuessResult, new CMsg_CTG_GetScoreResult(), On_CMsg_GTC_GuessRelt);
        glo_Main.GetInstance().m_GameSocket.f_AddListener((int)SocketCommand.ClientCommand, new CMsg_CTG_ClientCommand(), On_CMsg_GTC_CommandCall);
    }

    public void f_DisManager()
    {//取消監聽訊息
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.GamePlayCheckRelt);
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.PlayerGuessResult);
        glo_Main.GetInstance().m_GameSocket.f_RemoveListener((int)SocketCommand.ClientCommand);
    }

    #region 呼叫伺服器
    /// <summary>
    /// 開始遊戲
    /// </summary>
    /// <param name="strAccount">帳號</param>
    /// <param name="lPlayerID">ID</param>
    public void f_CheLead_Start(string strAccount, long lPlayerID, SocketCallbackDT tSocketCallbackDT)
    {
        _GameReltSucCall = tSocketCallbackDT.m_ccCallbackSuc;
        _GameReltFailCall = tSocketCallbackDT.m_ccCallbackFail;

        MessageBox.DEBUG("開啟遊戲");
        CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
        tCMsg_CTG_ServerCommand.m_szAccount = strAccount;
        tCMsg_CTG_ServerCommand.m_lPlayerID = lPlayerID;
        tCMsg_CTG_ServerCommand.m_iCommand = (int)EM_GameMod.Guess;
        tCMsg_CTG_ServerCommand.m_iCallState = (int)EM_GuessState.Start;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.ServerCommand, tCMsg_CTG_ServerCommand);
    }

    /// <summary>
    /// 呼叫全體玩家進行猜測判定
    /// </summary>
    public void f_CheLead_CallGuess(string strAccount, long lPlayerID, SocketCallbackDT tSocketCallbackDT)
    {
        _CommandReltSucCall = tSocketCallbackDT.m_ccCallbackSuc;
        _CommandReltFailCall = tSocketCallbackDT.m_ccCallbackFail;

        CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
        tCMsg_CTG_ServerCommand.m_szAccount = strAccount;
        tCMsg_CTG_ServerCommand.m_lPlayerID = lPlayerID;
        tCMsg_CTG_ServerCommand.m_iCommand = (int)EM_GameMod.Guess;
        tCMsg_CTG_ServerCommand.m_iCallState = (int)EM_GuessState.Guess;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.ServerCommand, tCMsg_CTG_ServerCommand);
    }

    /// <summary>
    /// 呼叫全體玩家重啟遊戲
    /// </summary>
    public void f_CheLead_Restart(string strAccount, long lPlayerID, SocketCallbackDT tSocketCallbackDT)
    {
        _CommandReltSucCall = tSocketCallbackDT.m_ccCallbackSuc;
        _CommandReltFailCall = tSocketCallbackDT.m_ccCallbackFail;

        CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
        tCMsg_CTG_ServerCommand.m_szAccount = strAccount;
        tCMsg_CTG_ServerCommand.m_lPlayerID = lPlayerID;
        tCMsg_CTG_ServerCommand.m_iCommand = (int)EM_GameMod.Guess;
        tCMsg_CTG_ServerCommand.m_iCallState = (int)EM_GuessState.Restart;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.ServerCommand, tCMsg_CTG_ServerCommand);
    }

    /// <summary>
    /// 呼叫全體玩家關閉遊戲
    /// </summary>
    public void f_CheLead_End(string strAccount, long lPlayerID, SocketCallbackDT tSocketCallbackDT)
    {
        _CommandReltSucCall = tSocketCallbackDT.m_ccCallbackSuc;
        _CommandReltFailCall = tSocketCallbackDT.m_ccCallbackFail;

        CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
        tCMsg_CTG_ServerCommand.m_szAccount = strAccount;
        tCMsg_CTG_ServerCommand.m_lPlayerID = lPlayerID;
        tCMsg_CTG_ServerCommand.m_iCommand = (int)EM_GameMod.Guess;
        tCMsg_CTG_ServerCommand.m_iCallState = (int)EM_GuessState.End;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.ServerCommand, tCMsg_CTG_ServerCommand);
    }

    /// <summary>
    /// 確認遊戲狀態
    /// </summary>
    public void f_CheLead_CheckState(string strAccount, long lPlayerID, SocketCallbackDT tSocketCallbackDT)
    {
        _GameReltSucCall = tSocketCallbackDT.m_ccCallbackSuc;
        _GameReltFailCall = tSocketCallbackDT.m_ccCallbackFail;

        CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = new CMsg_CTG_ServerCommand();
        tCMsg_CTG_ServerCommand.m_szAccount = strAccount;
        tCMsg_CTG_ServerCommand.m_lPlayerID = lPlayerID;
        tCMsg_CTG_ServerCommand.m_iCommand = (int)EM_GameMod.Guess;
        tCMsg_CTG_ServerCommand.m_iCallState = (int)EM_GuessState.CheckState;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.ServerCommand, tCMsg_CTG_ServerCommand);
    }
    #endregion

    #region 玩家呼叫動作
    /// <summary>
    /// 玩家進行猜測
    /// </summary>
    public void f_CheLead_Guess(string strAccount, long lPlayerID, int iCurSelTeam)
    {
        MessageBox.DEBUG("進行猜測");
        CMsg_CTG_Guess tCMsg_CTG_Guess = new CMsg_CTG_Guess();
        tCMsg_CTG_Guess.m_szAccount = strAccount;
        tCMsg_CTG_Guess.m_lPlayerID = lPlayerID;
        tCMsg_CTG_Guess.m_iGuess = iCurSelTeam;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.PlayerGuess, tCMsg_CTG_Guess);
    }

    /// <summary>
    /// 獲得分數
    /// </summary>
    public void f_CheLead_GetScore(string strAccount, long lPlayerID, int iCurSelTeam, SocketCallbackDT tSocketCallbackDT)
    {
        _GuessSucCall = tSocketCallbackDT.m_ccCallbackSuc;
        _GuessFailCall = tSocketCallbackDT.m_ccCallbackFail;

        CMsg_CTG_GetScore tCMsg_CTG_GetScore = new CMsg_CTG_GetScore();
        tCMsg_CTG_GetScore.m_szAccount = strAccount;
        tCMsg_CTG_GetScore.m_lPlayerID = lPlayerID;
        tCMsg_CTG_GetScore.m_iGuess = iCurSelTeam;
        glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.GameScore, tCMsg_CTG_GetScore);
    }
    #endregion

    #region 伺服器回傳
    /// <summary>
    /// 確認遊戲狀態回傳
    /// </summary>
    /// <param name="Obj"></param>
    private void On_CMsg_GTC_CheckGameRelt(object Obj)
    {
        CMsg_CTG_CheckGuessRelt tCMsg_CTG_CheckGuessRelt = (CMsg_CTG_CheckGuessRelt)Obj;
        if (tCMsg_CTG_CheckGuessRelt.m_iResult == (int)eMsgOperateResult.OR_Succeed)
        {
            _GameReltSucCall(tCMsg_CTG_CheckGuessRelt.m_iBackCall);
        }
        else
        {
            _GameReltFailCall(tCMsg_CTG_CheckGuessRelt.m_iResult);
        }
    }

    /// <summary>
    /// 客戶端通訊回傳
    /// </summary>
    /// <param name="obj"></param>
    private void On_CMsg_GTC_CommandCall(object obj)
    {
        CMsg_CTG_ClientCommand tCMsg_CTG_ClientCommand = (CMsg_CTG_ClientCommand)obj;
        if(tCMsg_CTG_ClientCommand.m_iCommand == (int)EM_GameMod.Guess)
        {
            if (tCMsg_CTG_ClientCommand.m_iResult == (int)eMsgOperateResult.OR_Succeed)
            {
                _CommandReltSucCall(tCMsg_CTG_ClientCommand.m_iCallState);
            }
            else
            {
                _CommandReltFailCall(tCMsg_CTG_ClientCommand.m_iResult);
            }
        }
        else
        {
            _CommandReltFailCall((int)eMsgOperateResult.OR_Error_Default);
        }
    }

    /// <summary>
    /// 猜測結果回傳
    /// </summary>
    /// <param name="obj"></param>
    private void On_CMsg_GTC_GuessRelt(object obj)
    {
        CMsg_CTG_GetScoreResult tCTG_GetScoreResult = (CMsg_CTG_GetScoreResult)obj;
        if (tCTG_GetScoreResult.m_iResult == (int)eMsgOperateResult.OR_Succeed)
        {
            _iCurScoreA = tCTG_GetScoreResult.m_iScoreA;
            _iCurScoreB = tCTG_GetScoreResult.m_iScoreB;
            _iCurWin = tCTG_GetScoreResult.m_iWin;
            _GuessSucCall(tCTG_GetScoreResult.m_iResult);
        }
        else
        {
            _GuessFailCall(tCTG_GetScoreResult.m_iResult);
        }
    }
    #endregion

    #region 取得回傳資料
    /// <summary>
    /// 取得分數
    /// </summary>
    /// <param name="iTeam">隊伍</param>
    /// <returns></returns>
    public int f_GetScore(int iTeam)
    {
        if (iTeam == (int)EM_TeamID.TeamA)
        {
            return _iCurScoreA;
        }
        else if (iTeam == (int)EM_TeamID.TeamB)
        {
            return _iCurScoreB;
        }
        return 0;
    }

    /// <summary>
    /// 取得勝負
    /// </summary>
    /// <returns></returns>
    public int f_GetWin()
    {
        return _iCurWin;
    }
    #endregion
}
