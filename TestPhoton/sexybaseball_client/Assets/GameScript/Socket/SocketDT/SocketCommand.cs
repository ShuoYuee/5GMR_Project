//
// Do not change number if it's in production.
//

public static class ReturnCode
{
    public const int
        Success = 0,
        Failure = -1,
        NotExist = -2,
        AuthFailed = -3,

        Last = int.MinValue;
}

//协议操作结果
public enum eMsgOperateResult
{
    OR_Succeed = 0, // 成功
    #region 客户端专用
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
    #endregion

    eOR_Default = 50001,    // 操作失败
    eOR_CreateAndLogin = 50007,     // 创建账号并登陆
    eOR_IP_Forbidden = 50008,       // IP封禁
    eOR_Account_Forbidden = 50009,  // 账号封禁
    eOR_DuplicateRoleName = 50021,  // 重复角色名

    eOR_LevelLimit = 50030,         // 等级限制
    eOR_TimesLimit = 50031,  // 次数限制
    //eOR_Sycee = 50032,   // 元宝不足
    eOR_Money = 50033,                   //银币不足
    //eOR_Energy = 50034,             // 体力不足
    //eOR_VipLimit = 50035, //Vip等级不足
    //未找到女生数据结构
    eOR_GirlDTNoFind,

    eOR_ItemNoFind,
    eOR_ItemNumError,
    eOR_EnergyNotEnough,
};


public enum SocketCommand
{
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

    PlayerEnterGame = 30,

    StopServer = 31,

    //广告相关
    PlayerPlayAd = 158,

    //数据更新协议
    UpdatePlayerInfor = 160,

    UpdateGirlInfor = 170,
    UpdateGirlInforAdd = 171,
    UpdateItemInfor = 172,//TODO：道具資訊更新與下發
    UpdateItemInforAdd = 173,
    //UpdateGirlInforDel = 230,



    PlayerPlayGame = 180,
    /// <summary>
    /// 呼叫玩家進行猜拳請求
    /// </summary>
    CallPlayerGuess = 181,
    /// <summary>
    /// 玩家猜拳请求
    /// </summary>
    PlayerGuess = 182,
    /// <summary>
    /// 猜拳结果数据下发
    /// </summary>
    PlayerGuessResult = 183,
    /// <summary>
    /// 遊戲重啟
    /// </summary>
    GameRestart = 184,
    /// <summary>
    /// 玩家勝利下發
    /// </summary>
    GameOver = 185,
    /// <summary>
    /// 確認遊戲狀態
    /// </summary>
    GamePlayCheck = 186,
    /// <summary>
    /// 確認遊戲狀態結果
    /// </summary>
    GamePlayCheckRelt = 187,

    /// <summary>
    /// 操作结果回应
    /// </summary>
    CONTROL_CTG_OperateResult = 250,
}