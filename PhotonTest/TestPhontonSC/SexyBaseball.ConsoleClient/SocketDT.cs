using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


//協議結果(先寫這裡)
public enum eMsgOperateResult
{
    OR_Succeed = 0, // 成功
    OR_Fail = 1, //未知原因失敗
    OR_SocketConnectFail = 2, //網格無法連接     
    OR_VerFail = 3, //獲取版本失敗 
    OR_ScFail = 4, //獲取腳本失敗 
    OR_ResourceFail = 5, //載入資源失敗

    OR_Error_AccountRepetition = 20, // 註冊：帳號重複
    OR_Error_NoAccount = 21, // 登陸：帳號不存在
    OR_Error_Password = 22, // 登陸：密碼錯誤
    OR_Error_AccountOnline = 24, // 登陸：帳號線上
    OR_Error_NameRepetition = 23, // 改名：名稱重複

    OR_Error_VersionNotMatch = 71, //版本不匹配
    OR_Error_ElseWhereLogin = 72, //異地登錄
    OR_Error_SeverMaintain = 73, //伺服器維護

    OR_Error_PosIsHavePlayer = 74, //位置上已經有玩家，操作失敗


    OR_Error_WIFIConnectTimeOut = 993, //WIFI網路未開
    OR_Error_ConnectTimeOut = 994, //連接逾時
    OR_Error_CreateAccountTimeOut = 995, //註冊超時
    OR_Error_LoginTimeOut = 996, //登陸超時
    OR_Error_ExitGame = 997, //遊戲出錯，強制玩家離開
    OR_Error_ServerOffLine = 998, //伺服器未開啟
    OR_Error_Disconnect = 999, //遊戲斷開連接
    OR_Error_Default = 10000, //操作失敗
}

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

}



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
    public int m_state;									// 服务器UID

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
    public int m_dwServerID;									// 服务器UID
    //GamePlayer标示，服务器用
    public long m_GamePlayerPoint;
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

/// <summary>
/// 玩家資訊
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_UserAttr : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }

    public char attrEnum;
    public int iValue;
}