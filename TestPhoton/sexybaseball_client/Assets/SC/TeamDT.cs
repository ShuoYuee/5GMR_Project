
//============================================
//
//    Team来自Team.xlsx文件自动生成脚本
//    2021/6/21 16:46:44
//    
//
//============================================
using System;
using System.Collections.Generic;



public class TeamDT : BaseItemDT
{

    /// <summary>
    /// 团名
    /// </summary>
    public string szName;
    /// <summary>
    /// Logo
    /// </summary>
    public string szLogo;
    /// <summary>
    /// 团里包含的女生清单分号隔开1;21;8;9
    /// </summary>
    public string szGirl;



    public override string f_GetLogo()
    {
        return szLogo;
    }

    public override string f_GetName()
    {
        return szName;
    }

    private int[] _aGirlData = null;
    private void CreateGirlData()
    {
        if (_aGirlData == null)
        {
            _aGirlData = ccU3DEngine.ccMath.f_String2ArrayInt(szGirl, ";");
        }
    }
    public override string f_GetNum()
    {
        CreateGirlData();
        return "" + _aGirlData.Length;


    }

    public int[] f_GetGirlData()
    {
        CreateGirlData();
        return _aGirlData;
    }

}
