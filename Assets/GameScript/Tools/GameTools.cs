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

    public static void f_AnimPlay(Animator animator, string strAnim, float fTime = 0.01f, bool IsPlay = false)
    {
        if (animator)
        {
            int iStateId = Animator.StringToHash(strAnim);
            bool bHasAction = animator.HasState(0, iStateId);
            if (bHasAction)
            {
                if (IsPlay)
                {
                    animator.Play(strAnim);
                }
                else
                {
                    animator.CrossFade(strAnim, fTime);

                }
            }
        }
    }
}