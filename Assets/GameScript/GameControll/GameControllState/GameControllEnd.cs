using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class GameControllEnd : GameControllBaseState
{
    public GameControllEnd()
        : base((int)EM_GameControllAction.End)
    {

    }


    public override void f_Enter(object Obj)
    {
        MessageBox.DEBUG("---------主線任務執行完成,等待遊戲結束-------------");
        //GameMain.GetInstance().f_EndGame();
    }

    public override void f_Execute()
    {

    }
}
