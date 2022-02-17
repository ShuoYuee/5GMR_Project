using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class ConditionState_Parament : ConditionState_Base
{
    string _szData1;
    string _szData2;

    public ConditionState_Parament(int iId, GameControllPara tGameControllPara) :base(iId, tGameControllPara)
    {

    }

    //8000.檢測變數的值等於目標值（參數1為變數名,參數2為變數等於的目標值，參數3無效）
    public override void f_Init(string szParament, string szParamentData, string szData1, string szData2, string szData3, string szData4)
    {
        base.f_Init(szParament, szParamentData, szData1, szData2, szData3, szData4);

        _szData1 = szData1;
        _szData2 = szData2;
        //if (!GameMain.GetInstance().f_CheckHaveParament(szData1))
        //{
        //    MessageBox.ASSERT("獲取變數失敗，未找到變數，請查對變數 " + szData1 + " 是否存在");
        //    return;
        //}

    }

    public override bool f_Check()
    {
        if (!base.f_Check())
        {
            return false;
        }
        //string strData = GameMain.GetInstance().f_GetParamentData(_szData1);
        //if (strData.Equals(_szData2))
        //{
        //    return true;
        //}
        return false;
    }




}