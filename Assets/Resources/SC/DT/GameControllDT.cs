using System;
using System.Collections.Generic;

public class GameControllDT : NBaseSCDT
{
    public GameControllDT()
    {
        m_iRunTimes = 0;
        m_emMissionEndType = EM_MissionEndType.None;
    }

    /// <summary>
    /// 說明
    /// </summary>
    public string szName;
    /// <summary>
    /// 段落
    /// </summary>
    public int iSection;
    /// <summary>
    /// 等待多少時間後開始執行（單位秒）
    /// </summary>
    public float fStartSleepTime;
    /// <summary>
    /// 開始時執行指令動作
    /// </summary>
    public int iStartAction;
    /// <summary>
    /// 所屬陣營
    /// </summary>
    public int iTeam;
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
    /// 動作參數3
    /// </summary>
    public string szData4;
    /// <summary>
    /// 有效攻擊部位
    /// </summary>
    public string szBeAttackPos;
    /// <summary>
    /// 有效攻擊陣營
    /// </summary>
    public int iBeAttackTeam;
    /// <summary>
    /// 是否需要等待結束
    /// </summary>
    public int iNeedEnd;
    /// <summary>
    /// 結束事件後等待多少時間後執行（單位秒）
    /// </summary>
    public float fEndSleepTime;
    /// <summary>
    /// 結束時執行指令動作
    /// </summary>
    public int iEndAction;
    /// <summary>
    /// 角色出生時對遊戲結果影響0未影響1死亡遊戲失敗2死亡遊戲勝利
    /// </summary>
    public int iGameResult;
    /// <summary>
    /// 運行超時時間，指定時間腳本沒有結束就被自動結束不填寫默認10秒超時
    /// </summary>
    public float fRunTimeOut;


    #region  附加緩存資訊

    /// <summary>
    /// 執行次數
    /// </summary>
    public int m_iRunTimes;

    /// <summary>
    /// 結束類型
    /// </summary>
    public EM_MissionEndType m_emMissionEndType;


    #endregion
}

