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
        //6000 顯示劇情（參數1為劇情Id,參數2無效，參數3無效，參數4無效）
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
        MessageBox.DEBUG("劇情結束繼續後面的任務:" + _CurGameControllDT.iEndAction);
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
