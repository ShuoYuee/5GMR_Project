
using ccU3DEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDT : BaseItemDT
{

    /// <summary>
    /// 角色名
    /// </summary>
    public string szName;
    /// <summary>
    /// 角色類型
    /// </summary>
    public int iType;
    /// <summary>
    /// 資源名
    /// </summary>
    public string szResName;
    /// <summary>
    /// 人物說明(中文)
    /// </summary>
    public string strReadme;
    /// <summary>
    /// 列表Logo
    /// </summary>
    public string szLogo;
    /// <summary>
    /// 攻擊力
    /// </summary>
    public int iAttackPower;
    /// <summary>
    /// 攻擊距離
    /// </summary>
    public float fAttackSize;
    /// <summary>
    /// 移動速度
    /// </summary>
    public float fMoveSpeed;
    /// <summary>
    /// 視野
    /// </summary>
    public float fViewSize;
    /// <summary>
    /// 攻擊目標類型
    /// </summary>
    public string szAttackType;
    /// <summary>
    /// 身體高
    /// </summary>
    public float fHeight;
    /// <summary>
    /// 身體大小
    /// </summary>
    public float fBodySize;
    /// <summary>
    /// 是否可以被偵測0可以被偵測1隱身不能被偵測
    /// </summary>
    public int iNoFind;
    /// <summary>
    /// 是否無敵0正常 1無敵
    /// </summary>
    public int iInvincible;
    /// <summary>
    /// 重生次數
    /// </summary>
    public int iReBirth;

    /// <summary>
    /// AI列表
    /// </summary>
    public string szAI;


    /// <summary>
    /// 預覽物來源(0或不填寫：共用模式    1：AB資源模式)
    /// </summary>
    public int iDisplayResource;
    /// <summary>
    /// 預覽物AB資源名稱
    /// </summary>
    public string szDisplayAB;
    /// <summary>
    /// 預覽物大小
    /// </summary>
    public float fDisplayScale;
    /// <summary>
    /// 物件預覽動畫
    /// </summary>
    public string szAnimGroup;
    /// <summary>
    /// 點擊物件打開的外部連接地址
    /// </summary>
    public string szURL;

    public override string f_GetLogo()
    {
        return szLogo;
    }

    public override string f_GetName()
    {
        return szName;
    }
}

