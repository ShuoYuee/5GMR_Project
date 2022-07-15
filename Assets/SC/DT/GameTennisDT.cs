using System;
using System.Collections.Generic;



public class GameTennisDT : NBaseSCDT
{

    /// <summary>
    /// 比賽名稱
    /// </summary>
    public string szName;
    /// <summary>
    /// 文字播放速度
    /// </summary>
    public int iSpeed;
    /// <summary>
    /// 獲勝比分
    /// </summary>
    public int iWinBall;
    /// <summary>
    /// 對手模型
    /// </summary>
    public string szOpponentModel;
    /// <summary>
    /// 比賽類型0普通比賽1練習賽
    /// </summary>
    public int iGameType;
    /// <summary>
    /// 隨機道具概率
    /// </summary>
    public int iRandObj;
}

