namespace SexyBaseball.Server
{
    using ccU3DEngine;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;


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

    /*[StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_SendPlayerInfor : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        public int iLevel;
        public int iExp;

        /// <summary>
        /// 0123每垒的情况 0为空 1为占人
        /// </summary>
        public byte iTable;

        /// <summary>
        /// 总得分
        /// </summary>
        public byte iScore;

        public long iGameStarTime;

        /// <summary>
        /// 出局数
        /// </summary>
        public int iPushNum;

        /// <summary>
        /// 全壘打能量
        /// </summary>
        public int iHomeRunEnergy;

        /// <summary>
        /// 總全壘打數
        /// </summary>
        public byte iHomeRunScore;
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        //public string rol_name;

        /// <summary>
        /// 伺服器時間
        /// </summary>
        public long lServerTime;
    }

    /// <summary>
    /// 通讯数据量小，采用固定协议方式，后期测试数据量大再修改为变长数据协议方式
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_SendGirlInfor : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
        public byte iNum;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I8)]
        public long[] id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I4)]
        public int[] iTempId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
        public byte[] iLvStar;
    }

    /// <summary>
    /// 增加女優協定(抽卡)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_SendGirlInforAdd : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        public long id;
        public int iTempId;
        public byte iLvStar;
    }

    /// <summary>
    /// 通讯数据量小，采用固定协议方式，后期测试数据量大再修改为变长数据协议方式//TODO：製作 道具資料 接收 POOLDT and POOL
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_SendItemInfor : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
        /// <summary>
        /// 這次傳輸的道具種類數量(最多為10個)
        /// </summary>
        public byte iNum;
        /// <summary>
        /// 道具ID(DB)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I8)]
        public long[] id;
        /// <summary>
        /// 道具ID(SC)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I4)]
        public int[] iTempId;
        /// <summary>
        /// 擁有數量
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
        public byte[] iHaveNum;
    }

    /// <summary>
    /// 使用道具
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_UseItem : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        /// <summary>
        /// 道具KeyID(30003 = 回體，30004 = 勝拳)
        /// </summary>
        public int m_iKeyID;
        /// <summary>
        /// 道具数量
        /// </summary>
        public int m_iValue;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_OperateResultResp : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        public int m_iCommandType;
        public int m_iReturnCode;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_IngameGameStartReq : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string m_szGirlId;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_IngameRPSReq : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        public int m_iRpsPlayer;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_IngameRPSResp : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string m_szModelName;

        public int m_iRpsPlayer;
        public int m_iRpsOpponent;
        public int m_iResult;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_IngameSlotMachineReq : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_IngameSlotMachineResp : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string m_szModelName;

        public int m_iSlotMachineItem;
        public short m_siFlagBases;
        public int m_iScore;
        public int m_iOuts;
        public int m_iEnergy;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string m_szLastConsumeTime;

        public bool IsOnBase(Flag_Bases onBase)
        {
            return ((Flag_Bases)m_siFlagBases & onBase) > 0;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_IngameGameEndReq : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_IngameGameEndEvent : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
    }

    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
    //public struct CMsg_PlayerEnergyReq : SockBaseDT
    //{
    //    public SockBaseDT Clone()
    //    {
    //        return (SockBaseDT)MemberwiseClone();
    //    }
    //}

    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
    //public struct CMsg_PlayerEnergyResp : SockBaseDT
    //{
    //    public SockBaseDT Clone()
    //    {
    //        return (SockBaseDT)MemberwiseClone();
    //    }

    //    public int m_iEnergy;

    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
    //    public string m_szLastConsumeTime;
    //}

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_PlayerItemActivationReq : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        public int m_iItem;
        public bool m_bActivate;
    }


    /// <summary>
    /// 玩家進入遊戲封包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_PlayerEnterGame : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        public long m_iUserId;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_PlayerPlayerAd : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        public int m_iAdId;
    }

    /// <summary>
    /// 玩家初始遊戲封包(選女優後的遊戲)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_PlayerPlayGame : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        public int m_iGirlId;

        public int m_iGameMod;
    }
    /// <summary>
    /// 玩家请求猜拳封包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_PlayerGuess : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }

        public int m_iGuesssType;
    }

    /// <summary>
    /// 關服通知封包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_StopServer : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
        /// <summary>
        /// 關服剩餘時間(單位：秒)
        /// </summary>
        public int m_iCloseServerTime;
    }

    /// <summary>
    /// 遊戲結束下發封包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_GameOver : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
    }
    /// <summary>
    /// 通知回體力封包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_PlayerRecover : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
    }
    /// <summary>
    /// 回體力下發封包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_PlayerRecoverResult : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
        /// <summary>
        /// 剩餘時間(單位秒)
        /// </summary>
        public int m_iRemainingTime;
    }
    /// <summary>
    /// 玩家猜拳結果下發封包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_PlayerGuessResult : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
        /// <summary>
        /// 本地猜拳结果
        /// </summary>
        public int m_iGuesssResult;
        /// <summary>
        /// 女生出拳类型
        /// </summary>
        public int m_iGirlGuessType;
        /// <summary>
        /// 本次胜获得分数（正为获胜得分，负为被扣分数）
        /// </summary>
        public int m_iScore;
        /// <summary>
        /// 胜利或失败结果数据
        /// </summary>
        public int m_iResultData;
    }

    #region 測試用
    /// <summary>
    /// 指定猜拳結果
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_SpecifyRPS : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
        /// <summary>
        /// NPC贏的機率
        /// </summary>
        public int m_iWinNPC;
        /// <summary>
        /// 玩家贏的機率
        /// </summary>
        public int m_iWinPlayer;
    }

    /// <summary>
    /// 獲得道具(買)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CMsg_ItemInforAdd : SockBaseDT
    {
        public SockBaseDT Clone()
        {
            return (SockBaseDT)MemberwiseClone();
        }
        /// <summary>
        /// SC_ID
        /// </summary>
        public int iTempId;
        /// <summary>
        /// 數量
        /// </summary>
        public int iNum;
    }
    #endregion*/
}