using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;



/// <summary>
/// [指令] 畫面淡入淡出
/// </summary>
public class GameControllV3_FadeScreen : GameControllBaseState
{
    private float m_FadeTime = 0.5f;  // (參數3) 畫面變暗、變亮漸變的時間
    private float blackTime = 2.0f; // (參數4) 畫面暗多久才開始變明亮

    public GameControllV3_FadeScreen() :
    base((int)EM_GameControllAction.V3_FadeScreen)
    { }




    public override void f_Enter(object Obj) {
        _CurGameControllDT = (GameControllDT)Obj;
        _CurGameControllDT.iNeedEnd = 1;
        StartRun();

        Render(null);
        //_iRenderTimeId = ccTimeEvent.GetInstance().f_RegEvent(0.1f, true, null, Render);
        ccTimeEvent.GetInstance().f_RegEvent(m_FadeTime +0.101f+blackTime, false, null, CallBack_Complete); //待畫面變暗完成，維持完指定時間後，恢復畫面

        EndRun();
    }


    //4001 場景單純淡入淡出 （参数1为畫面變暗變亮的過程時間,参数2为黑畫面的的時間，参数3无效）

    //protected override void Run(object Obj) {
    //    base.Run(Obj);


    //}

    private int _iRenderTimeId = -99;
    private void Render(object Obj)
    {

        //畫面變暗變亮的過程時間
        if (_CurGameControllDT.szData1 != "")
        {
            m_FadeTime = ccMath.atof(_CurGameControllDT.szData1);
        }
        else
        {
            m_FadeTime = 1f;
        }


        //黑畫面的的時間
        if (_CurGameControllDT.szData2 != "")
        {
            blackTime = ccMath.atof(_CurGameControllDT.szData2);
        }
        else
        {
            blackTime = 2.0f;
        }

        //GameMain.GetInstance().SceneFadeTo(1, m_FadeTime + 0.1f);                                          //玩家畫面慢慢黑掉
        

        //if (_CurGameControllDT.iNeedEnd == 0)
        //{
        //    EndRun();
        //}
        //else if (_CurGameControllDT.iNeedEnd != 1)
        //{
        //    EndRun();
        //}

    }



    /// <summary>
    /// 玩家畫面恢復明亮
    /// </summary>
    private void CallBack_Complete(object obj) {
        //GameMain.GetInstance().SceneFadeTo(0, m_FadeTime);

        ccTimeEvent.GetInstance().f_UnRegEvent(_iRenderTimeId);
        _iRenderTimeId = -99;
        //if (_CurGameControllDT.iNeedEnd == 1) {
        //    EndRun();
        //}

    }





}


