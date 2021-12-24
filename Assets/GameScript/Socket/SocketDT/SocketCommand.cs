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
    /// --客户端<-->游戏服务器
    /// </summary>
    MSG_CGameMsg = 6,

    //////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 创建账户 
    /// </summary>
    CS_UserCreate = 10001,
    /// <summary>
    /// 登陆申请 CMsg_CTG_AccountEnter
    /// </summary>
    CS_UserLogin = 10002,

    //////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 创建账户结果 
    /// </summary>
    SC_UserCreate = 30001,
    /// <summary>
    /// 登陆结果返回 CMsg_AccountLoginRelt
    /// </summary>
    SC_UserLogin = 30002,
    SC_Kickout = 30003,
   
    /// <summary>
    /// 操作结果回应
    /// </summary>
    CONTROL_CTG_OperateResult = 30017,

    /// <summary>
    /// 成绩更新消息
    /// </summary>
    emResultUpdate = 30030,

    /// <summary>
    /// 最后结算成绩
    /// </summary>
    emResultEnd = 30040,

    emLedMotor = 30050,

    emPlayerFishing = 30060,
    emGameControll = 30070,

    emPlayerCharge = 30080,
}

