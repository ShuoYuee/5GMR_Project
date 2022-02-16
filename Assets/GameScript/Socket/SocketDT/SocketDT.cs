using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


public struct stIp
{
    public string m_szIp;
    public int m_iPort;
}

#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct basicNode1 : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }
    public int value1;
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
    /// 操作結果
    /// </summary>
    public int iResult;
    //public int iData2;
    //public int iData3;
}


/// <summary>
/// 登入封包
/// </summary>
#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CMsg_CTG_AccountEnter
{
    /// <summary>
    /// 0：正常登入 1：重新連接
    /// </summary>
    public int m_state;									// 伺服器UID

    /// <summary>
    /// 帳戶名
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]//SocketDT.MAX_USER_NAME)]
    public string m_strAccount;                      //// 機器碼		

    /// <summary>
    /// 密碼
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]//SocketDT.MAX_USER_NAME)]
    public string m_strPassword;                      //// 機器碼		
    //伺服器ID： 默認1
    public int m_dwServerID;									// 伺服器UID
    //GamePlayer標示，伺服器用
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
    /// 帳戶ID
    /// </summary>
    public long m_PlayerId;                                  // 帳戶id

    /// <summary>
    /// OR_Succeed = 0, // 成功 OR_Error_NoAccount = 21, // 登入：帳號不存在 OR_Error_Password = 22, // 登入：密碼錯誤
    /// </summary>
    public int m_result;									// 帳戶id
    public long userPtr;
    public int m_iServerTime;

    public int m_iServerId;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]//SocketDT.MAX_USER_NAME)]
    public string m_strServerName;

    /// <summary>
    /// 伺服器保留
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 96)]//SocketDT.MAX_USER_NAME)]
    public string uniqueLoginId;
}


#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct stCarStatic : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }
    public int iControllID;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]//SocketDT.MAX_USER_NAME)]
    public string m_strData;                      //// 

}

#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct stLedCommand : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }
    public int iControllID;
    public int iScId;
    public int iAdd;        //0 Add 1Remove
                            /// <summary>
                            /// 魚竿上的燈泡
                            /// </summary>
    public int iLed1;
    public int iLedTime1;
    /// <summary>
    /// 桌上的燈條
    /// </summary>
    public int iLed2;
    public int iLedTime2;
    /// <summary>
    /// 馬達震動
    /// </summary>
    public int iMotor;
    public int iMotorTime;
}


#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct stPlayerFishing : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }
    public int iControllID;
    public int iFishScId;
    public int iScore;

}

#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct stGameControll : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }
    public int iGameControllId;

}

#if UNITY_IPHONE
[System.Serializable]
#endif
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct stPlayerCharge : SockBaseDT
{
    public SockBaseDT Clone()
    {
        SockBaseDT tGoodsPoolDT = (SockBaseDT)MemberwiseClone();
        return tGoodsPoolDT;
    }
    public int iControllID;
    public int iScore;

}


