using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class GameControllV3_ShowGamePlot : GameControllBaseState
{
    Transform _oGameObj = null;
    private int _iTimeid = -99;

    public GameControllV3_ShowGamePlot()
        : base((int)EM_GameControllAction.ShowGamePlot)
    {
        
    }


    public override void f_Enter(object Obj)
    {
        _CurGameControllDT = (GameControllDT) Obj;
        //6000 显示剧情（参数1为剧情Id,参数2无效，参数3无效，参数4无效）
        MessageBox.DEBUG("6000");
        glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.UI_GamePlotClose, On_UI_GameTextClose);
        ccUIManage.GetInstance().f_SendMsg("UIP_GamePlot", BaseUIMessageDef.UI_OPEN, ccMath.atoi(_CurGameControllDT.szData1));

        StartRun();

        if (_iTimeid != -99)
        {
            ccTimeEvent.GetInstance().f_UnRegEvent(_iTimeid);
        }
        _iTimeid = ccTimeEvent.GetInstance().f_RegEvent(1, 0.5f, true, null, CallBack_CheckUIClose);
    }

    private void On_UI_GameTextClose(object Obj)
    {
        ccTimeEvent.GetInstance().f_UnRegEvent(_iTimeid);
        MessageBox.DEBUG("剧情结束继续后面的任务:" + _CurGameControllDT.iEndAction);
        glo_Main.GetInstance().m_GameMessagePool.f_RemoveListener(MessageDef.UI_GamePlotClose, On_UI_GameTextClose);
        EndRun();
    }


    void CallBack_CheckUIClose(object Obj)
    {       
        if (!ccUIManage.GetInstance().f_CheckUIIsOpen("UIP_GamePlot"))
        {
            On_UI_GameTextClose(null);
        }
    }




}
