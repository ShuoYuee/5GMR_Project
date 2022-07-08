
//============================================
//
//    GameTennis来自GameTennis.xlsx文件自动生成脚本
//    2021/1/22 11:11:51
//    
//
//============================================
using System;
using System.Collections.Generic;



public class GameTennisDT : NBaseSCDT
{

    /// <summary>
    /// 比赛名称
    /// </summary>
    public string szName;
    /// <summary>
    /// 文字播放速度
    /// </summary>
    public int iSpeed;
    /// <summary>
    /// 获胜比分
    /// </summary>
    public int iWinBall;
    /// <summary>
    /// 对手模型
    /// </summary>
    public string szOpponentModel;
    /// <summary>
    /// 比赛类型0普通比赛1练习赛
    /// </summary>
    public int iGameType;
    /// <summary>
    /// 随机道具概率
    /// </summary>
    public int iRandObj;
}
