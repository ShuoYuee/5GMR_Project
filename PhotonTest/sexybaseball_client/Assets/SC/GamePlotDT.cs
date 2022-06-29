
//============================================
//
//    GamePlot来自GamePlot.xlsx文件自动生成脚本
//    2021/2/1 17:00:09
//    
//
//============================================
using System;
using System.Collections.Generic;



public class GamePlotDT : NBaseSCDT
{

    /// <summary>
    /// 说明
    /// </summary>
    public string szName;
    /// <summary>
    /// 等待多少时间后开始执行（单位秒）
    /// </summary>
    public float fStartSleepTime;
    /// <summary>
    /// 开始时执行指令动作
    /// </summary>
    public int iStartAction;
    /// <summary>
    /// 动作参数1
    /// </summary>
    public string szData1;
    /// <summary>
    /// 动作参数2
    /// </summary>
    public string szData2;
    /// <summary>
    /// 动作参数3
    /// </summary>
    public string szData3;
    /// <summary>
    /// 动作参数4
    /// </summary>
    public string szData4;
    /// <summary>
    /// 是否需要等待结束
    /// </summary>
    public int iNeedEnd;
    /// <summary>
    /// 结束事件后等待多少时间后执行（单位秒）
    /// </summary>
    public float fEndSleepTime;
    /// <summary>
    /// 结束时执行Id
    /// </summary>
    public int iEndAction;
}
