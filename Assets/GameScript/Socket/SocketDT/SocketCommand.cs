using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SocketCommand
{
    PING = 100,
    PING_Reps = 201,

    //////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 创建账户 
    /// </summary>
    CS_UserCreate = 101,
    UserCreate_Reps = 201,

    /// <summary>
    /// 登陆申请 CMsg_CTG_AccountEnter
    /// </summary>
    CS_UserLogin = 102,
    UserLogin_Reps = 202,

    /// <summary>
    /// 登出
    /// </summary>
    CS_UserLogout = 103,
    UserLogout_Reps = 203,

    /// <summary>
    /// 傳遞給Server通訊
    /// </summary>
    ServerCommand = 150,
    /// <summary>
    /// 傳遞給Client通訊
    /// </summary>
    ClientCommand = 151,

    #region 啦啦隊遊戲
    PlayerPlayGame = 180,
    /// <summary>
    /// 玩家猜拳请求
    /// </summary>
    PlayerGuess = 182,
    /// <summary>
    /// 猜拳结果数据下发
    /// </summary>
    PlayerGuessResult = 183,
    /// <summary>
    /// 取得當前遊戲分數
    /// </summary>
    GameScore = 184,
    /// <summary>
    /// 玩家勝利下發
    /// </summary>
    GameOver = 185,
    /// <summary>
    /// 確認遊戲狀態結果
    /// </summary>
    GamePlayCheckRelt = 187,
    #endregion
}

