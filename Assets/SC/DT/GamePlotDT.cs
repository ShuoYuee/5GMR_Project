
using System;
using System.Collections.Generic;



public class GamePlotDT : NBaseSCDT
{

    /// <summary>
    /// 說明
    /// </summary>
    public string szName;
    /// <summary>
    /// 等待多少時間後開始執行（單位秒）
    /// </summary>
    public float fStartSleepTime;
    /// <summary>
    /// 開始時執行指令動作
    /// </summary>
    public int iStartAction;
    /// <summary>
    /// 動作參數1
    /// </summary>
    public string szData1;
    /// <summary>
    /// 動作參數2
    /// </summary>
    public string szData2;
    /// <summary>
    /// 動作參數3
    /// </summary>
    public string szData3;
    /// <summary>
    /// 動作參數4
    /// </summary>
    public string szData4;
    /// <summary>
    /// 是否需要等待結束
    /// </summary>
    public int iNeedEnd;
    /// <summary>
    /// 結束事件後等待多少時間後執行（單位秒）
    /// </summary>
    public float fEndSleepTime;
    /// <summary>
    /// 結束時執行Id
    /// </summary>
    public int iEndAction;
}

