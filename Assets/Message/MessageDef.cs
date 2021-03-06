using UnityEngine;
using System.Collections;

public class MessageDef
{
    /// <summary>
    /// 開始載入資源腳本消息
    /// </summary>
    public static string STARTLOADSC = "STARTLOADSC";
    public static string LOADSCSUC = "LOADSCSUC";

    public static string PAUSEGAME = "PAUSEGAME";
    public static string RESUMEGAME = "RESUMEGAME";

    public static string LOGINEROINFOR = "LOGINEROINFOR";

    /// <summary>
    /// 全域錯誤提示
    /// </summary>
    public static string GAMEMESSAGEBOX = "GAMEMESSAGEBOX";

    /// <summary>
    /// 遊戲結束消息
    /// </summary>
    public static string GAMEOVER = "GAMEOVER";
    public static string GameStart = "GameStart";

    //public static string PowerShow = "PowerShow";
    //public static string PowerUnShow = "PowerUnShow";


    /// <summary>
    /// 
    /// </summary>
    public static string LockPlayerControll = "LockPlayerControll";
    /// <summary>
    /// 
    /// </summary>
    public static string UI_GameTextClose = "UI_GameTextClose";

    /// <summary>
    /// 
    /// </summary>
    public static string UI_GamePlotClose = "UI_GamePlotClose";

    public static string UI_MapObjInit = "UI_MapObjInit";
    public static string UI_LoadBtn = "UI_LoadBtn";

    #region 啦啦隊遊戲
    public static string Guess_JoinRoom = "Guess_JoinRoom";
    public static string Guess_ExitRoom = "Guess_ExitRoom";
    public static string Guess_SelGuessTeam = "Guess_SelGuessTeam";
    #endregion

    public static string MainLogOut = "MainLogOut";
}

