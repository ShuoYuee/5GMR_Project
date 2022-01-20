
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

    
//协议操作结果
public enum eMsgOperateResult
{
    OR_Succeed = 0, // 成功
    OR_Fail = 1, //未知原因失败
    OR_SocketConnectFail = 2, //网格无法连接     
    OR_VerFail = 3, //获取版本失败 
    OR_ScFail = 4, //获取脚本失败 
    OR_ResourceFail = 5, //加载资源失败

    OR_Error_AccountRepetition = 20, // 注册：账号重复
    OR_Error_NoAccount = 21, // 登陆：账号不存在
    OR_Error_Password = 22, // 登陆：密码错误
    OR_Error_AccountOnline = 24, // 登陆：账号在线
    OR_Error_NameRepetition = 23, // 改名：名称重复

    OR_Error_VersionNotMatch = 71, //版本不匹配 2016-7-8 
    OR_Error_ElseWhereLogin = 72, //异地登录 2016-7-8 
    OR_Error_SeverMaintain = 73, //服务器维护 2016-7-8 

    OR_Error_PosIsHavePlayer = 74, //位置上已经有玩家，操作失败


    OR_Error_WIFIConnectTimeOut = 993, //WIFI网络未开
    OR_Error_ConnectTimeOut = 994, //连接超时
    OR_Error_CreateAccountTimeOut = 995, //注册超时
    OR_Error_LoginTimeOut = 996, //登陆超时
    OR_Error_ExitGame = 997, //游戏出错，强制玩家离开
    OR_Error_ServerOffLine = 998, //服务器未开启
    OR_Error_Disconnect = 999, //游戏断开连接
    OR_Error_Default = 10000, //操作失败
};


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

public enum EM_PlayerControllStep
{
    Wait,
    Throw,
    Recevie,
    InFishNet,
    FishNetReceive,
    /// <summary>
    /// 鱼线断开
    /// </summary>
    FishingRodDis,
    Complete,
    //FishForce,

}

public enum EM_FishStep
{
    /// <summary>
    /// 鱼游动
    /// </summary>
    Swim,    
    CatahToShock,
    /// <summary>
    /// 电击
    /// </summary>
    Shock,
    /// <summary>
    /// 游向食物准备吃钓
    /// </summary>
    Move2Food,
    /// <summary>
    /// 吃钓，播放跳起来转身动画
    /// </summary>
    Catch,
    /// <summary>
    /// 鱼已经吃钓，开始挣扎
    /// </summary>
    EatFooding,    
    /// <summary>
    /// 收竿中
    /// </summary>
    BeReciver,
    /// <summary>
    /// 收完竿后进行一次快速逃跑挣扎
    /// </summary>
    FastRunAway,
    /// <summary>
    /// 鱼死亡
    /// </summary>
    Die,
    /// <summary>
    /// 鱼进入网中
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
    RotationH = 2,  //水平旋轉
    RotationV = 3,  //垂直旋轉
    Scale = 4,  //縮放
}

public enum EM_EditAxis
{
    AxisX,
    AxisY,
    AxisZ,
}

public enum EM_EditPoint
{
    WorldPoint,
    LocalPoint,
    UserPoint,
}

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
