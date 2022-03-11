using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class GameControllV3_ShowText : GameControllBaseState
{
    Transform _oGameObj = null;

    public GameControllV3_ShowText()
        : base((int)EM_GameControllAction.ShowText)
    {
        
    }


    public override void f_Enter(object Obj)
    {
        _CurGameControllDT = (GameControllDT) Obj;
        //5001.显示对话文字信息，参数1中间LOGO图，参数2标题文字，参数3底部显示文字Id（每个Id为页显示文字）

        glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.UI_GameTextClose, On_UI_GameTextClose);
        ccUIManage.GetInstance().f_SendMsg("UIP_GameText", BaseUIMessageDef.UI_OPEN, Obj);

        StartRun();
    }

    private void On_UI_GameTextClose(object Obj)
    {
        glo_Main.GetInstance().m_GameMessagePool.f_RemoveListener(MessageDef.UI_GameTextClose, On_UI_GameTextClose);
        EndRun();
    }


    protected override void Run(object Obj)
    {
       
    }

}
