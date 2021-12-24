using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class GameControllV3_ShowStepInfor : GameControllBaseState
{
    Transform _oGameObj = null;

    public GameControllV3_ShowStepInfor()
        : base((int)EM_GameControllAction.ShowStepInfor)
    {
        
    }


    public override void f_Enter(object Obj)
    {
        _CurGameControllDT = (GameControllDT) Obj;
        //5003.任务进度文字信息，参数1提示信息Logo，参数2标题文字，参数3进度显示文字Id（只能显示一个Id的文字）
        
        ccUIManage.GetInstance().f_SendMsg("UIP_ShowStepInfor", BaseUIMessageDef.UI_OPEN, Obj);
        ccTimeEvent.GetInstance().f_RegEvent(1, false, null, On_UI_Close);

        StartRun();
    }

    private void On_UI_Close(object Obj)
    {
        EndRun();
    }


    protected override void Run(object Obj)
    {
       
    }

}
