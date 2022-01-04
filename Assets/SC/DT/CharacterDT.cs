
//============================================
//
//    Character来自Character.xlsx文件自动生成脚本
//    2018/5/23 20:30:20
//    
//
//============================================
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
    /// 角色类型
    /// </summary>
    public int iType;
    /// <summary>
    /// 资源名
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
    /// 攻击力
    /// </summary>
    public int iAttackPower;
    /// <summary>
    /// 攻击距离
    /// </summary>
    public float fAttackSize;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float fMoveSpeed;
    /// <summary>
    /// 视野
    /// </summary>
    public float fViewSize;
    /// <summary>
    /// 攻击目标类型
    /// </summary>
    public string szAttackType;
    /// <summary>
    /// 身体高
    /// </summary>
    public float fHeight;
    /// <summary>
    /// 身体大小
    /// </summary>
    public float fBodySize;
    /// <summary>
    /// 是否可以被侦测0可以被侦测1隐身不能被侦测
    /// </summary>
    public int iNoFind;
    /// <summary>
    /// 是否无敌0正常 1无敌
    /// </summary>
    public int iInvincible;
    /// <summary>
    /// 重生次数
    /// </summary>
    public int iReBirth;

    /// <summary>
    /// AI列表
    /// </summary>
    public string szAI;

    /// <summary>
    /// 預覽物AB資源
    /// </summary>
    public string szDisplayAB;
    /// <summary>
    /// 預覽物資源
    /// </summary>
    public string szDisplayName;

    public override string f_GetLogo()
    {
        return szLogo;
    }

    public override string f_GetName()
    {
        return szName;
    }

   


}
