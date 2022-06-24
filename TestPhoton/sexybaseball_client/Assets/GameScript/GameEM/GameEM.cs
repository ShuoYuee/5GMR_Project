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

/// <summary>
/// 狀態機Login
/// </summary>
public enum EM_LoginState
{
    Idle = 1,
    Logout,
    LoggingIn,
    Register,
    AccountCreate,

    // States for test.
    LoggingInDummy,
}

public enum EM_MainState
{
    Idle = 1,
    MenuMain,
    Logout,
}

public enum EM_IngameState
{
    /// <summary>
    /// 等待玩家进行猜拳操作
    /// </summary>
    Idle = 50,
    Video,
    UseItem,
    RPS,

    /// <summary>
    /// 播放结束剧情
    /// </summary>
    Slot,

    /// <summary>
    /// 执行猜拳
    /// </summary>
    Guess,
    /// <summary>
    /// 显示猜拳结果
    /// </summary>
    ShowOpponent,   
    /// <summary>
    /// 提示体力不足
    /// </summary>
    Popup_OutofEnergy,
    /// <summary>
    /// 游戏失败
    /// </summary>
    GameOver
}

public enum EM_GuessState
{
    /// <summary>
    /// 確認遊戲狀態
    /// </summary>
    CheckState = 1,
    /// <summary>
    /// 開始遊戲
    /// </summary>
    Start,
    /// <summary>
    /// 執行猜測
    /// </summary>
    Guess,
    /// <summary>
    /// 重啟遊戲
    /// </summary>
    Restart,
    /// <summary>
    /// 結束遊戲
    /// </summary>
    End,

    /// <summary>
    /// 遊戲進行中
    /// </summary>
    GameIng,
    /// <summary>
    /// 遊戲並未開始
    /// </summary>
    NotGameIng,
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

/// <summary>
/// 遊戲模式
/// </summary>
public enum EM_GameMod
{
    None = 0,
    Story,
    Challenge
}

public enum EM_VideoType
{
    None = -1,
    Picking,
    TakeOff
}

public enum EM_TeamID
{
    None = 0,
    TeamA = 1,
    TeamB = 2,
}