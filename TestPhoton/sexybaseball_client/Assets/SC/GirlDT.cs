
//============================================
//
//    Girl来自Girl.xlsx文件自动生成脚本
//    2021/12/14 下午 01:37:57
//    
//
//============================================
using System;
using System.Collections.Generic;



public class GirlDT : BaseItemDT
{
    /// <summary>
    /// 女优名
    /// </summary>
    public string szName;
    /// <summary>
    /// 對戰前引導對話
    /// </summary>
    public int iStarPlot;
    /// <summary>
    /// 尾局剧情
    /// </summary>
    public int iEndPlot;
    /// <summary>
    /// Story Logo
    /// </summary>
    public string szLogo;
    /// <summary>
    /// Challenge Logo
    /// </summary>
    public string szLogo1;
    /// <summary>
    /// Icon
    /// </summary>
    public string szIcon;
    /// <summary>
    /// 正常角色立繪資源
    /// </summary>
    public string szTachie1;
    /// <summary>
    /// 脫上1角色立繪資源
    /// </summary>
    public string szTachie2;
    /// <summary>
    /// 脫下1角色立繪資源
    /// </summary>
    public string szTachie3;
    /// <summary>
    /// 脫上2角色立繪資源
    /// </summary>
    public string szTachie4;
    /// <summary>
    /// 脫下2角色立繪資源
    /// </summary>
    public string szTachie5;
    /// <summary>
    /// 解鎖1星数
    /// </summary>
    public int iLv1Star;
    /// <summary>
    /// 解鎖2星数
    /// </summary>
    public int iLv2Star;

    #region Story Mode
    /// <summary>
    /// Story Mode壘上沒人時NPC的勝率
    /// </summary>
    public float fWinRateNPC10;
    /// <summary>
    /// Story Mode壘上沒人時玩家的勝率
    /// </summary>
    public float fWinRatePlayer10;
    /// <summary>
    /// Story Mode一壘有人時NPC的勝率
    /// </summary>
    public float fWinRateNPC11;
    /// <summary>
    /// Story Mode一壘有人時玩家的勝率
    /// </summary>
    public float fWinRatePlayer11;
    /// <summary>
    /// Story Mode二壘有人時NPC的勝率
    /// </summary>
    public float fWinRateNPC12;
    /// <summary>
    /// Story Mode二壘有人時玩家的勝率
    /// </summary>
    public float fWinRatePlayer12;
    /// <summary>
    /// Story Mode三壘有人時NPC的勝率
    /// </summary>
    public float fWinRateNPC13;
    /// <summary>
    /// Story Mode三壘有人時玩家的勝率
    /// </summary>
    public float fWinRatePlayer13;
    #endregion

    #region Challenge Mode
    /// <summary>
    /// Challenge Mode壘上沒人時NPC的勝率
    /// </summary>
    public float fWinRateNPC20;
    /// <summary>
    /// Challenge Mode壘上沒人時玩家的勝率
    /// </summary>
    public float fWinRatePlayer20;
    /// <summary>
    /// Challenge Mode一壘有人時NPC的勝率
    /// </summary>
    public float fWinRateNPC21;
    /// <summary>
    /// Challenge Mode一壘有人時玩家的勝率
    /// </summary>
    public float fWinRatePlayer21;
    /// <summary>
    /// Challenge Mode二壘有人時NPC的勝率
    /// </summary>
    public float fWinRateNPC22;
    /// <summary>
    /// Challenge Mode二壘有人時玩家的勝率
    /// </summary>
    public float fWinRatePlayer22;
    /// <summary>
    /// Challenge Mode三壘有人時NPC的勝率
    /// </summary>
    public float fWinRateNPC23;
    /// <summary>
    /// Challenge Mode三壘有人時玩家的勝率
    /// </summary>
    public float fWinRatePlayer23;
    #endregion

    public override string f_GetLogo()
    {
        return szLogo;
    }

    public override string f_GetName()
    {
        return szName;
    }
}