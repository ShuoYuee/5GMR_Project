using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class GameControllV3_AddParament : GameControllBaseState
{    
    public GameControllV3_AddParament() :
    base((int)EM_GameControllAction.V3_AddParament)
    { }


    public override void f_Enter(object Obj){
        _CurGameControllDT = (GameControllDT)Obj;
        //3002.变量值加上（参数1为变量名,参数2为变量要加上的值，参数3无效）
        StartRun();
    }

    protected override void Run(object Obj) {
        base.Run(Obj);
        //int iData = ccMath.atoi(GameMain.GetInstance().f_GetParamentData(_CurGameControllDT.szData1));
        //iData += ccMath.atoi(_CurGameControllDT.szData2);
        //GameMain.GetInstance().f_SetParamentData(_CurGameControllDT.szData1, "" + iData);
        EndRun();
    }


    
}


