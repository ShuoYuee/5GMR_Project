public enum EM_eGameStep
{
    Loop,
    PlayBall,
    Win,
    Lost,
    Plot,

};

    
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


    OR_Error_WIFIConnectTimeOut = 993, //WIFI網路未開
    OR_Error_ConnectTimeOut = 994, //連接逾時
    OR_Error_CreateAccountTimeOut = 995, //註冊超時
    OR_Error_LoginTimeOut = 996, //登陸超時
    OR_Error_ExitGame = 997, //遊戲出錯，強制玩家離開
    OR_Error_ServerOffLine = 998, //伺服器未開啟
    OR_Error_Disconnect = 999, //遊戲斷開連接
    OR_Error_Default = 10000, //操作失敗
};

public enum EM_TeamID
{//玩家陣營
    None = 0,
    TeamA = 1,
    TeamB = 2,
}

public enum EM_RoleState
{
    /// <summary>
    /// 玩家
    /// </summary>
    Player = 0,
    /// <summary>
    /// 動畫物件
    /// </summary>
    Anim = 1,
    /// <summary>
    /// 網頁連結物件
    /// </summary>
    URL = 2,
    /// <summary>
    /// 動畫和網頁連結物件
    /// </summary>
    AnimAndURL = 3,
}

public enum EM_InterState
{
    None = 0,
    Anim,
    URL,
}

public enum EM_GameResult
{
    /// <summary>
    /// 0未影响
    /// </summary>
    Default = 0,

    /// <summary>
    /// 1死亡游戏失败
    /// </summary>
    Lost = 1,

    /// <summary>
    /// 2死亡游戏胜利
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

public enum EM_TableWay
{
    Top,
    Bottom,
    Left,
    Right,
}

/*public enum EM_Sound
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
}*/

public enum EM_GameControllAction
{

    
    /// <summary>
    /// 显示UI文字
    /// </summary>
    ShowUIText = 30,

    /// <summary>
    /// 31.控制相应的UI组件显示 （参数1有效玩家Id -99表示所有玩家同时有效，参数2组件名称，参数3显示时间）
    /// </summary>
    UIActionShow = 31,
    

    /// <summary>
    /// 一次性定时器事件(等待操作对此指令无效)
    /// 1303.定时器事件(参数1为定时时间到后执行的下一条指令,参数23无效)
    /// </summary>
    AutoClock = 1303,

    

    /// <summary>
    /// 3000.系统初始化指令（参数1无效, 参数2无效，参数3无效，参数4无效）
    /// </summary>
    V3_Init = 3000,

    /// <summary>
    /// 3001.设置变量的值（参数1为变量名, 参数2为变量值，参数3无效）
    /// </summary>
    V3_SetParament = 3001,

    /// <summary>
    /// 3002.变量值加上（参数1为变量名,参数2为变量要加上的值，参数3无效）
    /// </summary>
    V3_AddParament = 3002,

    /// <summary>
    /// 3003.变量值减去（参数1为变量名, 参数2为变量要减去的值，参数3无效）
    /// </summary>
    V3_SubParament = 3003,


    /// <summary> 4001 場景單純淡入淡出 </summary>
    V3_FadeScreen = 4001,
    

    /// <summary>
    /// 
    /// </summary>
    ShowText = 5001,

    /// <summary>
    /// 中间提示文字信息，参数1提示信息Logo，参数2标题文字，参数3显示文字Id（只能显示一个Id的文字）
    /// </summary>
    ShowCTLText = 5002,

    /// <summary>
    /// 任务进度文字信息，参数1提示信息Logo，参数2标题文字，参数3进度显示文字Id（只能显示一个Id的文字）
    /// </summary>
    ShowStepInfor = 5003,
    
    /// <summary>
    /// 8020：PromptBox角色发生对话确认事件（参数1为角色分配的指定KeyId,参数2触发时执行的指令Id，参数3无效，参数4无效）
    /// </summary>
    PromptBox = 8020,


    /// <summary>
    /// 8000.检测变量的值等于目标值（参数1为变量名,参数2为变量等于的目标值，参数3无效）
    /// </summary>
    Parament = 8000,

    /// <summary>
    /// 6000 显示剧情（参数1为剧情Id,参数2无效，参数3无效，参数4无效）
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
    /// 正常结束
    /// </summary>
    MissionRunEnd = 1,

    /// <summary>
    /// 超时结束
    /// </summary>
    MessionTimeOut = 2,

}

namespace MR_Edit
{
    #region 編輯器相關
    /// <summary>
    /// 編輯器模式
    /// </summary>
    public enum EM_EditCtrlState
    {
        None = 0,   //空
        Position = 1,   //座標
        Rotation = 2,  //旋轉
        Scale = 3,  //縮放
    }

    /// <summary>
    /// 編輯軸向
    /// </summary>
    public enum EM_EditAxis
    {
        AxisX = 1,
        AxisY = 2,
        AxisZ = 3,
        Free = 4,
    }

    /// <summary>
    /// 編輯座標模式
    /// </summary>
    public enum EM_EditPoint
    {
        WorldPoint = 1,
        LocalPoint = 2,
        UserPoint = 3,
    }
    #endregion

    #region 狀態機
    /// <summary>
    /// 狀態機LogIn
    /// </summary>
    public enum EM_LogInState
    {
        Idle = 1,
        LoggingIn,
    }
    
    /// <summary>
    /// 狀態機Main
    /// </summary>
    public enum EM_MainState
    {
        Idle = 1,
        Main,
        Edit,
        GuessWait,
        Guess,
        Logout,
    }
    #endregion

    #region 啦啦隊相關
    /// <summary>
    /// 遊戲模式
    /// </summary>
    public enum EM_GameMod
    {
        None = 0,
        Guess,
    }

    public enum EM_GuessState
    {
        /// <summary>
        /// Client確認遊戲狀態
        /// </summary>
        CheckState = 1,
        /// <summary>
        /// Server開始遊戲
        /// </summary>
        Start,
        /// <summary>
        /// Server執行猜測
        /// </summary>
        Guess,
        /// <summary>
        /// Server重啟遊戲
        /// </summary>
        Restart,
        /// <summary>
        /// Server結束遊戲
        /// </summary>
        End,

        /// <summary>
        /// Client確認遊戲狀態
        /// </summary>
        CallCheckState,
        /// <summary>
        /// Client建立房主
        /// </summary>
        CallRoomMaster,
        /// <summary>
        /// Client開始遊戲
        /// </summary>
        CallStart,
        /// <summary>
        /// Client執行猜測
        /// </summary>
        CallGuess,
        /// <summary>
        /// Client重啟遊戲
        /// </summary>
        CallRestart,
        /// <summary>
        /// Client結束遊戲
        /// </summary>
        CallEnd,

        /// <summary>
        /// 遊戲進行中
        /// </summary>
        GameIng,
        /// <summary>
        /// 遊戲並未開始
        /// </summary>
        NotGameIng,

        /// <summary>
        /// 已有房主啟動遊戲
        /// </summary>
        Error_GameIsStart,
    }

    /// <summary>
    /// 猜拳结果
    /// </summary>
    public enum EM_GuessResult
    {
        None = 0,
        Win = 1,
        Lost,
    }
    #endregion
}