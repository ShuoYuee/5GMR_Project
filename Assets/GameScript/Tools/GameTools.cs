using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTools
{
    public static string f_GetPlayerName(int iPlayer)
    {        
        return "P" + iPlayer;
    }
    
    public static void f_SetGameObj(GameObject Obj, bool bSet)
    {
        if (Obj != null)
        {
            Obj.SetActive(bSet);
        }
    }
}