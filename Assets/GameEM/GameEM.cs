
public enum EM_eLocalData
{
    iCurGameControllIndex = 1,

};

public enum EM_eGameStep
{
    Loop,
    PlayBall,
    Win,
    Lost,
    Plot,

};


//協議操作結果
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

    OR_Error_VersionNotMatch = 71, //版本不匹配 2016-7-8 
    OR_Error_ElseWhereLogin = 72, //異地登錄 2016-7-8 
    OR_Error_SeverMaintain = 73, //伺服器維護 2016-7-8 

    OR_Error_PosIsHavePlayer = 74, //位置上已經有玩家，操作失敗


    OR_Error_WIFIConnectTimeOut = 993, //WIFI網路未開
    OR_Error_ConnectTimeOut = 994, //連接逾時
    OR_Error_CreateAccountTimeOut = 995, //註冊超時
    OR_Error_LoginTimeOut = 996, //登陸超時
    OR_Error_ExitGame = 997, //遊戲出錯，強制玩家離開
    OR_Error_ServerOffLine = 998, //伺服器未開啟
    OR_Error_Disconnect = 999, //遊戲斷開連接
    OR_Error_Default = 10000, //操作失敗
};


public enum EM_GameResult
{
    /// <summary>
    /// 0未影響
    /// </summary>
    Default = 0,

    /// <summary>
    /// 1死亡遊戲失敗
    /// </summary>
    Lost = 1,

    /// <summary>
    /// 2死亡遊戲勝利
    /// </summary>
    Win = 2,

}

public enum EM_GameStatic
{
    Waiting = 100,
    Gaming,
    Win,
    Lost,
}

public enum EM_PlayerControllStep
{
    Wait,
    Throw,
    Recevie,
    InFishNet,
    FishNetReceive,
    /// <summary>
    /// 魚線斷開
    /// </summary>
    FishingRodDis,
    Complete,
    //FishForce,

}

public enum EM_FishStep
{
    /// <summary>
    /// 魚遊動
    /// </summary>
    Swim,
    CatahToShock,
    /// <summary>
    /// 電擊
    /// </summary>
    Shock,
    /// <summary>
    /// 游向食物準備吃釣
    /// </summary>
    Move2Food,
    /// <summary>
    /// 吃釣，播放跳起來轉身動畫
    /// </summary>
    Catch,
    /// <summary>
    /// 魚已經吃釣，開始掙紮
    /// </summary>
    EatFooding,
    /// <summary>
    /// 收竿中
    /// </summary>
    BeReciver,
    /// <summary>
    /// 收完竿後進行一次快速逃跑掙紮
    /// </summary>
    FastRunAway,
    /// <summary>
    /// 魚死亡
    /// </summary>
    Die,
    /// <summary>
    /// 魚進入網中
    /// </summary>
    FishInNet,
    Invincible,
    SucessRunAway,
    FishFindPlayer,

}


public enum EM_GameScene
{
    GameMain,

}

public enum EM_PlayerIndex
{
    Player1 = 1,
    Player2 = 2,
    Player3,
    Player4,
    Player5,
    Player6,
    Player7,
    Player8,
    Player9,
    Player10,
    Player11,
    Player12,
    Player13,
    Player14,
    Player15,
    Player16,
    Player17,
    Player18,
    Player19,
    Player20,
    Player21,
    Player22,
    Player23,
    Player24,
    Player25,
    Player26,
    Player27,
    Player28,
    Player29,
    Player30,
}

public enum EM_MoveWay
{
    Left,
    Right,


}

public enum EM_TableWay
{
    Top,
    Bottom,
    Left,
    Right,


}

public enum EM_Sound
{
    /// <summary>
    /// 聲道1-遊戲操作
    /// </summary>
    Effect,
    /// <summary>
    /// 聲道2-遊戲背景
    /// </summary>
    Music,
    /// <summary>
    /// 聲道3-BOSS出場專用聲道
    /// </summary>
    Boss,
    /// <summary>
    /// 語音
    /// </summary>
    Voice,
}

/// <summary>
/// 編輯器模式
/// </summary>
public enum EM_EditState
{
    None = 0,   //空
    Position = 1,   //座標
    Rotation = 2,  //旋轉
    Scale = 3,  //縮放
}

public enum EM_EditAxis
{
    AxisX = 1,
    AxisY = 2,
    AxisZ = 3,
    Free = 4,
}

public enum EM_EditPoint
{
    WorldPoint = 1,
    LocalPoint = 2,
    UserPoint = 3,
}

public enum EM_GameControllAction
{


    /// <summary>
    /// 顯示UI文字
    /// </summary>
    ShowUIText = 30,

    /// <summary>
    /// 31.控制相應的UI元件顯示 （參數1有效玩家Id -99表示所有玩家同時有效，參數2元件名稱，參數3顯示時間）
    /// </summary>
    UIActionShow = 31,


    /// <summary>
    /// 一次性計時器事件(等待操作對此指令無效)
    /// 1303.計時器事件(參數1為定時時間到後執行的下一條指令,參數23無效)
    /// </summary>
    AutoClock = 1303,



    /// <summary>
    /// 3000.系統初始化指令（參數1無效, 參數2無效，參數3無效，參數4無效）
    /// </summary>
    V3_Init = 3000,

    /// <summary>
    /// 3001.設置變數的值（參數1為變數名, 參數2為變數值，參數3無效）
    /// </summary>
    V3_SetParament = 3001,

    /// <summary>
    /// 3002.變數值加上（參數1為變數名,參數2為變數要加上的值，參數3無效）
    /// </summary>
    V3_AddParament = 3002,

    /// <summary>
    /// 3003.變數值減去（參數1為變數名, 參數2為變數要減去的值，參數3無效）
    /// </summary>
    V3_SubParament = 3003,


    /// <summary> 4001 場景單純淡入淡出 </summary>
    V3_FadeScreen = 4001,


    /// <summary>
    /// 
    /// </summary>
    ShowText = 5001,

    /// <summary>
    /// 中間提示文字資訊，參數1提示資訊Logo，參數2標題文字，參數3顯示文字Id（只能顯示一個Id的文字）
    /// </summary>
    ShowCTLText = 5002,

    /// <summary>
    /// 任務進度文字資訊，參數1提示資訊Logo，參數2標題文字，參數3進度顯示文字Id（只能顯示一個Id的文字）
    /// </summary>
    ShowStepInfor = 5003,

    /// <summary>
    /// 8020：PromptBox角色發生對話確認事件（參數1為角色分配的指定KeyId,參數2觸發時執行的指令Id，參數3無效，參數4無效）
    /// </summary>
    PromptBox = 8020,


    /// <summary>
    /// 8000.檢測變數的值等於目標值（參數1為變數名,參數2為變數等於的目標值，參數3無效）
    /// </summary>
    Parament = 8000,

    /// <summary>
    /// 6000 顯示劇情（參數1為劇情Id,參數2無效，參數3無效，參數4無效）
    /// </summary>
    ShowGamePlot = 6000,

    //--------------------------------------------------------------------------
    Loop = 200,
    Read,
    ServerAction,


    End = 50000,
}



public enum EM_MissionEndType
{
    None = 0,

    /// <summary>
    /// 正常結束
    /// </summary>
    MissionRunEnd = 1,

    /// <summary>
    /// 超時結束
    /// </summary>
    MessionTimeOut = 2,

}

