using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ccU3DEngine;

public enum SocketCommand
{
    /// <summary>
    /// PING
    /// </summary>
    PING = 10000,
    PING_Reps = 30000,

    /// <summary>
    /// --用戶端<-->遊戲伺服器
    /// </summary>
    MSG_CGameMsg = 6,

    //////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 創建帳戶 
    /// </summary>
    CS_UserCreate = 10001,
    /// <summary>
    /// 登入申請 CMsg_CTG_AccountEnter
    /// </summary>
    CS_UserLogin = 10002,

    //////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 創建帳戶結果 
    /// </summary>
    SC_UserCreate = 30001,
    /// <summary>
    /// 登入結果返回 CMsg_AccountLoginRelt
    /// </summary>
    SC_UserLogin = 30002,
    SC_Kickout = 30003,

    /// <summary>
    /// 操作結果回應
    /// </summary>
    CONTROL_CTG_OperateResult = 30017,

    /// <summary>
    /// 成績更新消息
    /// </summary>
    emResultUpdate = 30030,

    /// <summary>
    /// 最後結算成績
    /// </summary>
    emResultEnd = 30040,

    emLedMotor = 30050,

    emPlayerFishing = 30060,
    emGameControll = 30070,

    emPlayerCharge = 30080,
}


