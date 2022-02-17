using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遊戲任務主控制器
/// </summary>
public class GameControll
{
    private int _iConditionId;
    private ccMachineManager<ccMachineParamentBase> _GameControllMachineManager = null;
    private GameControllMunClock _GameControllMunClock = null;

    private bool _bIsRuning = false;

    public GameControll(int iConditionId)
    {
        _iConditionId = iConditionId;
        _GameControllMunClock = new GameControllMunClock(); //Loop
        _GameControllMachineManager = new ccMachineManager<ccMachineParamentBase>(new GameControllLoop()); //Loop


        // ********* ( 除了這邊之外，要到「GameControllTools.cs」填寫對應的類型！！ ) ************

        //_GameControllMachineManager.f_RegState(new GameControllRead(_iConditionId)); //讀取任務

        RegState(new GameControllEnd());          //結束任務
                                                  //RegState(new GameControllServerAction()); //
                                                  //--------------------------------------------------------------------------------------

        RegState(new GameControllV3_SetParament());          //3001.設置變數的值（參數1為變數名, 參數2為變數值，參數3無效）
        RegState(new GameControllV3_AddParament());
        RegState(new GameControllV3_SubParament());

        RegState(new GameControllV3_FadeScreen());           //4001. 畫面淡入淡出


        //--------------------------------------------------------------------------------------
        RegState(new GameControllEnd());
        RegState(new GameControllV3_ShowText());                 //5001.顯示對話文字資訊
        RegState(new GameControllV3_ShowCTLText());                 //5002.中間提示文字資訊，參數1提示資訊Logo，參數2標題文字，參數3顯示文字Id（只能顯示一個Id的文字）       
        RegState(new GameControllV3_ShowStepInfor());                 //5003.任務進度文字資訊，參數1提示資訊Logo，參數2標題文字，參數3進度顯示文字Id（只能顯示一個Id的文字）
                                                                      //--------------------------------------------------------------------------------------

        RegState(new GameControllV3_ShowGamePlot());

        //特殊處理計時器動作
        _GameControllMachineManager.f_RegState(new GameControllClock(_GameControllMunClock));
        _GameControllMachineManager.f_ChangeState((int)EM_GameControllAction.Loop);
    }

    void RegState(GameControllBaseState tccMachineStateBase)
    {
        _GameControllMachineManager.f_RegState(tccMachineStateBase);
        //ccMachineStateBase tNewState = tccMachineStateBase.f_Clone();
    }



    public void f_Start(int iActionId = -99)
    {
        _bIsRuning = true;
        GameControllDT tGameControllDT = GameControllTools.f_LoadGameControllDT(iActionId);
        if (tGameControllDT == null)
        {
            MessageBox.ASSERT("GameControllRead 讀取的任務Id非法 ");
        }
        //_GameControllMachineManager.f_ChangeState((int)EM_GameControllAction.Read, iActionId);
        int iId = tGameControllDT.iId;
        if (iId > 100000)
        {
            iId = iId / 100000;
            MessageBox.DEBUG("Clock執行Action " + iId + " " + tGameControllDT.szName);
            tGameControllDT.iNeedEnd = 0;
            tGameControllDT.fEndSleepTime = 0;
            tGameControllDT.iEndAction = 0;
            DoClockState(tGameControllDT);
        }
        else
        {
            //MessageBox.DEBUG("主線任務執行Action " + iId);
            _GameControllMachineManager.f_ChangeState(tGameControllDT.iStartAction, tGameControllDT);
        }
    }

    public void f_Stop()
    {
        _bIsRuning = false;
        _GameControllMachineManager.f_ChangeState((int)EM_GameControllAction.Loop);
    }

    public void f_Update()
    {

        _GameControllMachineManager.f_Update();
        _GameControllMunClock.f_Update();
    }


    void DoClockState(object Obj)
    {
        GameControllDT tGameControllDT = (GameControllDT)Obj;
        _GameControllMunClock.f_Execute(tGameControllDT);
    }

    public bool f_IsEnd()
    {
        if (_GameControllMachineManager.f_GetCurMachineState().iId == (int)EM_GameControllAction.End)
        {
            return true;
        }
        return false;
    }

}
