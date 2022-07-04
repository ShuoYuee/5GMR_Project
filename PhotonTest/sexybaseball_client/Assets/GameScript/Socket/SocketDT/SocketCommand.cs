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
    OR_Error_GameIsStart,      //已有房主啟動遊戲

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
}