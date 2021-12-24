using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class ConditionState_PromptBoxCheck : ConditionState_Base
{
    int _iRoleId;
    string _strLogo;

    public ConditionState_PromptBoxCheck(int iId, GameControllPara tGameControllPara) :base(iId, tGameControllPara)
    {

    }

    //8020：PromptBox角色发生对话确认事件（参数1为角色分配的指定KeyId,参数2无效，参数3无效，参数4无效）
    public override void f_Init(string szParament, string szParamentData, string szData1, string szData2, string szData3, string szData4)
    {
        base.f_Init(szParament, szParamentData, szData1, szData2, szData3, szData4);
        _strLogo = szData2;
        _iRoleId = ccMath.atoi(szData1);
    }

    public override bool f_Check()
    {
        if (!base.f_Check())
        {
            return false;
        }
        //_BaseRoleControl = (BaseRoleControllV2)GameMain.GetInstance().f_GetRoleControl2(_iRoleId);
        //if (_BaseRoleControl != null)
        //{
        //    return _BaseRoleControl.f_CheckPromptBoxIsOpen(_strLogo);
        //}
        return false;
    }




}