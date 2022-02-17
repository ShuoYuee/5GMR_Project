using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class GameControllV3_SetParament : GameControllBaseState
{    
    public GameControllV3_SetParament() :
    base((int)EM_GameControllAction.V3_SetParament)
    { }


    public override void f_Enter(object Obj){
        _CurGameControllDT = (GameControllDT)Obj;
        //3001.設置變數的值（參數1為變數名, 參數2為變數值，參數3無效）    
        StartRun();
    }

    protected override void Run(object Obj) {
        base.Run(Obj);
        //GameMain.GetInstance().f_SetParamentData(_CurGameControllDT.szData1, _CurGameControllDT.szData2);
        EndRun();
    }


    
}


