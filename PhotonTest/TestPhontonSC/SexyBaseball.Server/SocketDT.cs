using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ccU3DEngine;

public class SocketCallbackDT
{
    public ccCallback m_ccCallbackSuc;
    public ccCallback m_ccCallbackFail;
}

#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct stGameCommandReturn : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }
    public int iCommand;
    /// <summary>
    /// 操作结果
    /// </summary>
    public int iResult;
    //public int iData2;
    //public int iData3;
}

#region 登入
/// <summary>
/// 登陆封包
/// </summary>
#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_AccountEnter : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    /// <summary>
    /// 0：正常登陆 1：重新连接
    /// </summary>
    //public int m_state;                                 // 服务器UID

    /// <summary>
    /// 账户名
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]//SocketDT.MAX_USER_NAME)]
    public string m_strAccount;                      //// 机器码		

    /// <summary>
    /// 密码
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]//SocketDT.MAX_USER_NAME)]
    public string m_strPassword;                      //// 机器码		
                                                      //服务器ID： 默认1
    public int m_dwServerID;                                    // 服务器UID
                                                                //GamePlayer标示，服务器用
                                                                //public long m_GamePlayerPoint;
}

#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_GTC_LoginRelt : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }
    /// <summary>
    /// 账户ID
    /// </summary>
    public long m_PlayerId;                                  // 账户id

    /// <summary>
    /// OR_Succeed = 0, // 成功 OR_Error_NoAccount = 21, // 登陆：账号不存在 OR_Error_Password = 22, // 登陆：密码错误
    /// </summary>
    public int m_result;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]
    public string m_userName;

    public int m_iTeam;

    //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]//SocketDT.MAX_USER_NAME)]
    //public string m_strServerName;

    ///// <summary>
    ///// 服务器保留
    ///// </summary>
    //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 96)]//SocketDT.MAX_USER_NAME)]
    //public string uniqueLoginId;
}
#endregion

#region 登出
/// <summary>
/// 登出封包
/// </summary>
#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_AccountExit : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    /// <summary>
    /// 账户名
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]//SocketDT.MAX_USER_NAME)]
    public string m_strAccount;                      //// 机器码

    /// <summary>
    /// 用戶ID
    /// </summary>
    public long m_iPlayerID;
}

/// <summary>
/// 登出結果封包
/// </summary>
#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_AccountExitRelt : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    public int m_iRelt;
}
#endregion

#region 註冊
/// <summary>
/// 創建帳戶封包
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_AccountCreate : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    /// <summary>
    /// 帳戶名
    /// </summary>
    public string m_strAccount;

    /// <summary>
    /// 密碼
    /// </summary>
    public string m_strPassword;

    /// <summary>
    /// 陣營(隊伍)
    /// </summary>
    public int m_iTeam;
}

/// <summary>
/// 創建帳戶封包_結果
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_AccountCreateRelt : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    /// <summary>
    /// 註冊結果
    /// </summary>
    public int m_iResult;

    /// <summary>
    /// 帳戶名
    /// </summary>
    public string m_strAccount;
}
#endregion

#region 猜測(抽獎)
/// <summary>
/// 猜測請求
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_Guess : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]
    public string m_szAccount;
    public long m_lPlayerID;
    public int m_iGuess;
}

/// <summary>
/// 取得分數
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_GetScore : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]
    public string m_szAccount;
    public long m_lPlayerID;
    public int m_iGuess;
}

/// <summary>
/// 取得分數結果
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_GetScoreResult : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    public int m_iScoreA;
    public int m_iScoreB;
    public int m_iResult;
}

/// <summary>
/// 確認遊戲狀態結果
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_CheckGuessRelt : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    public int m_iResult;
}

/// <summary>
/// 遊戲結束結果
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_GuessOver : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    public int m_iResult;
    public int m_iScore;
    public int m_iWin;
}
#endregion

#region 呼叫通訊
/// <summary>
/// 遊戲通訊(呼叫Server)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_ServerCommand : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]
    public string m_szAccount;
    public long m_lPlayerID;

    public int m_iCommand;
    public int m_iCallState;
}

/// <summary>
/// 遊戲通訊(呼叫Client)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_ClientCommand : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    public int m_iResult;
    public int m_iCommand;
    public int m_iCallState;
}
#endregion

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_LoginUserReq : SockBaseDT
{
    public SockBaseDT Clone()
    {
        return (SockBaseDT)MemberwiseClone();
    }

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string m_szUserName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string m_szUserPwd;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_LoginUserResp : SockBaseDT
{
    public SockBaseDT Clone()
    {
        return (SockBaseDT)MemberwiseClone();
    }

    public int m_iReturnCode;
    public long m_iUserId;
}