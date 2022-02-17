using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllTools
{

    public static bool f_CheckStateTypeIsRight(EM_GameControllAction tEM_GameControllAction)
    {
        if (
            tEM_GameControllAction == EM_GameControllAction.ShowUIText ||        // (30) 顯示UI文字
            tEM_GameControllAction == EM_GameControllAction.UIActionShow ||      // (??) ??

            tEM_GameControllAction == EM_GameControllAction.AutoClock


            )
        {
            return true;
        }
        return false;
    }


    //↓這裡也要填什麼Action對應什麼類型
    public static GameControllBaseState f_CreateState(EM_GameControllAction tEM_GameControllAction)
    {
        if (tEM_GameControllAction == EM_GameControllAction.End)
        {
            return new GameControllEnd();
        }

        else
        {
            MessageBox.ASSERT("未註冊的狀態機 " + tEM_GameControllAction.ToString());
        }

        return null;
    }


    public static GameControllDT f_LoadGameControllDT(int iActionId)
    {

        if (iActionId > 0)
        {
            return (GameControllDT)glo_Main.GetInstance().m_SC_Pool.m_GameControllSC.f_GetSC(iActionId);
        }
        return null;
    }


}
