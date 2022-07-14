
using System;
using System.Collections.Generic;



public class GameControll_ConditionDT : NBaseSCDT
{

    /// <summary>
    /// 說明
    /// </summary>
    public string szName;
    /// <summary>
    /// 檢測參數
    /// </summary>
    public string szParament;
    /// <summary>
    /// 參數值
    /// </summary>
    public string szParamentData;
    /// <summary>
    /// 檢測條件
    /// </summary>
    public int iConditionId;
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
    /// 條件滿足時執行指令動作
    /// </summary>
    public int iRunAction;
    /// <summary>
    /// 是否迴圈執行
    /// </summary>
    public int iLoop;
}

