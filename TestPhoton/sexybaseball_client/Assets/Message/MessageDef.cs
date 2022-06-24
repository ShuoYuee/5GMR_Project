public static class MessageDef
{
    /// <summary>
    /// 开始加载资源脚本消息
    /// </summary>
    public static string LOGINEROINFOR = "LOGINEROINFOR";

    /// <summary>
    /// 全局错误提示
    /// </summary>
    public static string GAMEMESSAGEBOX = "GAMEMESSAGEBOX";
    
    /// <summary>
    /// 剧情对事消息
    /// </summary>
    public static string UI_GamePlotClose = "UI_GamePlotClose";

    /// <summary>
    /// 資源更新進度消息
    /// </summary>
    public static string UI_UpdateInitProgress = "UI_UpdateInitProgress";
    public static string UI_UpdateInitSuccess = "UI_UpdateInitSuccess";

    #region In-Game State Keys.
    public const string
        Ingame_Idle = nameof(Ingame_Idle),
        Ingame_Video = nameof(Ingame_Video),
        Ingame_RPS = nameof(Ingame_RPS),
        Ingame_Slot = nameof(Ingame_Slot),
        Ingame_GameOver = nameof(Ingame_GameOver),
        Ingame_UseItem = nameof(Ingame_UseItem);
    #endregion

    #region UI事件KEY值
    public const string
        UpdateSlotAndRPSResult = nameof(UpdateSlotAndRPSResult),
        BaseballRoundStart = nameof(BaseballRoundStart),
        UpdateAllData = nameof(UpdateAllData),
        UpdateAllItemData = nameof(UpdateAllItemData),
        UpdateAllItemUI = nameof(UpdateAllItemUI),
        QuitGame = nameof(QuitGame),
        StopSlotMachine = nameof(StopSlotMachine),
        OpenVideo = nameof(OpenVideo);
    #endregion
}
