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
        //5001.顯示對話文字資訊，參數1中間LOGO圖，參數2標題文字，參數3底部顯示文字Id（每個Id為頁顯示文字）

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
