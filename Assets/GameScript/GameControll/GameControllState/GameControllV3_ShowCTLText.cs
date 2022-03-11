using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class GameControllV3_ShowCTLText : GameControllBaseState
{
    Transform _oGameObj = null;

    public GameControllV3_ShowCTLText()
        : base((int)EM_GameControllAction.ShowCTLText)
    {
        
    }


    public override void f_Enter(object Obj)
    {
        _CurGameControllDT = (GameControllDT) Obj;
        //5001.显示对话文字信息，参数1中间LOGO图，参数2标题文字，参数3底部显示文字Id（每个Id为页显示文字）
        
        ccUIManage.GetInstance().f_SendMsg("UIP_ShowCTLText", BaseUIMessageDef.UI_OPEN, Obj);
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
