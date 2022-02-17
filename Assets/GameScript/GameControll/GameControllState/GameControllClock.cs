using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;
using UnityEngine.Playables;


/// <summary>
/// 一次性計時器事件(等待操作對此指令無效)
/// 1303.計時器事件(參數1為定時時間到後執行的下一條指令,參數23無效)
/// </summary>
public class GameControllClock : GameControllBaseState
{
    private GameControllMunClock _GameControllMunClock = null;
    //private TimeLineMessageControll _TimeLineMessageControll = null;
    public GameControllClock(GameControllMunClock tGameControllMunClock)
        : base((int)EM_GameControllAction.AutoClock)
    {
        _GameControllMunClock = tGameControllMunClock;
    }

    public override void f_Enter(object Obj)
    {

        _CurGameControllDT = (GameControllDT)Obj;

        GameControllDT tGameControllDT = (GameControllDT)glo_Main.GetInstance().m_SC_Pool.m_GameControllSC.f_GetSC(ccMath.atoi(_CurGameControllDT.szData1));
        if (tGameControllDT == null)
        {
            MessageBox.ASSERT("讀取計時器事件對應的下一動作失敗 " + _CurGameControllDT.iId + " " + _CurGameControllDT.szData1);
            return;
        }
        _CurGameControllDT.fEndSleepTime = 0;
        if (_CurGameControllDT.fStartSleepTime > 0)
        {
            ccTimeEvent.GetInstance().f_RegEvent(_CurGameControllDT.fStartSleepTime, false, tGameControllDT, Doing);
        }
        else
        {
            Doing(tGameControllDT);
        }
        Run(null);
    }

    protected override void Run(object Obj)
    {
        base.Run(Obj);
    }

    private void Doing(object Obj)
    {
        if (glo_Main.GetInstance().m_EM_GameStatic == EM_GameStatic.Gaming)
        {
            MessageBox.DEBUG("Do Clock " + _CurGameControllDT.iId + ">>" + _CurGameControllDT.szData1);
            _GameControllMunClock.f_Read(Obj);
        }
    }


}

