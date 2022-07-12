using System;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;

public class RoleTools
{
    private static int iIndex = 0;
    /// <summary>創建隨機Id</summary>
    public static long CreateKeyId()
    {
        //1138817954
        long iTT = ccMathEx.DateTime2time_t(System.DateTime.Now);
        long iId = iTT * 100 + iIndex;
        iIndex++;
        if (iIndex > 90)
        {
            iIndex = 0;
        }
        return iId;
    }

    public static EditObjControll f_CreateEditObj(GameObject Obj, CharacterDT tCharacterDT, MapPoolDT tMapPoolDT)
    {
        EditObjControll tEditObjControll = Obj.GetComponent<EditObjControll>();
        if (tEditObjControll == null)
        {
            tEditObjControll = Obj.AddComponent<EditObjControll>();
            tEditObjControll.tag = "XRCubeCollider";
            tEditObjControll.f_Save(tMapPoolDT);
        }

        if (tCharacterDT.iType == (int)EM_RoleState.Player)
        {

        }
        else if (tCharacterDT.iType == (int)EM_RoleState.Anim)
        {//動畫物件
            f_SetAnimObj(tEditObjControll, tCharacterDT);
        }
        else if (tCharacterDT.iType == (int)EM_RoleState.URL)
        {//網頁物件
            f_SetConnectURL(tEditObjControll, tCharacterDT);
        }
        else if (tCharacterDT.iType == (int)EM_RoleState.AnimAndURL)
        {//動畫和網頁物件
            f_SetConnectURL(tEditObjControll, tCharacterDT);
            f_SetAnimObj(tEditObjControll, tCharacterDT);
        }

        return tEditObjControll;
    }

    private static void f_SetConnectURL(EditObjControll tEditObj, CharacterDT tCharacterDT)
    {
        if (tCharacterDT.szURL == "") { return; }

        ConnectURL tConnectURL = new ConnectURL();
        tConnectURL.f_Init(tCharacterDT);
        tConnectURL.fSetURL(tCharacterDT.szURL);
        tEditObj.f_AddComponent(tConnectURL);
    }

    private static void f_SetAnimObj(EditObjControll tEditObj, CharacterDT tCharacterDT)
    {
        if (tCharacterDT.szAI == "") { return; }

        //替物件裝上動畫機
        Animator tAnimator = tEditObj.gameObject.GetComponent<Animator>();
        if (tAnimator == null)
        {
            tAnimator = tEditObj.gameObject.AddComponent<Animator>();
        }
        if (tCharacterDT.szAI != null && tCharacterDT.szAI != "")
        {
            string[] strAnimator = ccMath.f_String2ArrayString(tCharacterDT.szAI, ";");
            try
            {
                tAnimator.runtimeAnimatorController = glo_Main.GetInstance().m_ResourceManager.f_CreateABAnimator(strAnimator[0], strAnimator[1]);
            }
            catch
            {
                MessageBox.DEBUG("動畫機載入出錯：" + tCharacterDT.iId);
            }
        }
        Anim_Interactable _Interactable = new Anim_Interactable();
        _Interactable.f_Init(tCharacterDT);
        _Interactable.f_Init(tAnimator);
        tEditObj.f_AddComponent(_Interactable);
    }
}
