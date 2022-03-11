using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏任务主控制器
/// </summary>
public class GameControll
{
    private int _iConditionId;
    private ccMachineManager _GameControllMachineManager = null;
    private GameControllMunClock _GameControllMunClock = null;

    private bool _bIsRuning = false;

    public GameControll(int iConditionId)
    {
        _iConditionId = iConditionId;
        _GameControllMunClock = new GameControllMunClock(); //Loop
        _GameControllMachineManager = new ccMachineManager(new GameControllLoop()); //Loop


        // ********* ( 除了這邊之外，要到「GameControllTools.cs」填寫對應的類型！！ ) ************

        //_GameControllMachineManager.f_RegState(new GameControllRead(_iConditionId)); //讀取任務

        RegState(new GameControllEnd());          //結束任務
        //RegState(new GameControllServerAction()); //
        //--------------------------------------------------------------------------------------
        
        RegState(new GameControllV3_SetParament());          //3001.设置变量的值（参数1为变量名, 参数2为变量值，参数3无效）
        RegState(new GameControllV3_AddParament());
        RegState(new GameControllV3_SubParament());

        RegState(new GameControllV3_FadeScreen());           //4001. 畫面淡入淡出


        //--------------------------------------------------------------------------------------
        RegState(new GameControllEnd());
        RegState(new GameControllV3_ShowText());                 //5001.显示对话文字信息
        RegState(new GameControllV3_ShowCTLText());                 //5002.中间提示文字信息，参数1提示信息Logo，参数2标题文字，参数3显示文字Id（只能显示一个Id的文字）       
        RegState(new GameControllV3_ShowStepInfor());                 //5003.任务进度文字信息，参数1提示信息Logo，参数2标题文字，参数3进度显示文字Id（只能显示一个Id的文字）
                                                                  //--------------------------------------------------------------------------------------

        RegState(new GameControllV3_ShowGamePlot()); 

        //特殊处理定时器动作
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
            MessageBox.ASSERT("GameControllRead 读取的任务Id非法 ");
        }
        //_GameControllMachineManager.f_ChangeState((int)EM_GameControllAction.Read, iActionId);
        int iId = tGameControllDT.iId;
        if (iId > 100000)
        {
            iId = iId / 100000;
            MessageBox.DEBUG("Clock执行Action " + iId + " " + tGameControllDT.szName);
            tGameControllDT.iNeedEnd = 0;
            tGameControllDT.fEndSleepTime = 0;
            tGameControllDT.iEndAction = 0;
            DoClockState(tGameControllDT);
        }
        else
        {
            //MessageBox.DEBUG("主线任务执行Action " + iId);
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