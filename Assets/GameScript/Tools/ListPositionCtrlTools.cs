using System.Collections;
using System.Collections.Generic;
using ccU3DEngine;
using UnityEngine;

public class ListPositionCtrlTools
{

    public static void f_Create(ListPositionCtrl tListPositionCtrl, List<NBaseSCDT> aData)
    {
        List<ListItem> aList = new List<ListItem>();
        ccMathEx.f_CreateChild(tListPositionCtrl.gameObject, aData.Count);
        string strAB = "logo";  // StrResources.AssetBundle.Logo.bundleName;

        BaseItemDT tBaseItemDT;
        for (int i = 0; i < aData.Count; i++)
        {
            tBaseItemDT = (BaseItemDT)aData[i];
            Sprite tSprite = glo_Main.GetInstance().m_ResourceManager.f_LoadSpriteForAB(strAB, tBaseItemDT.f_GetLogo());
            string strGirlNum = "";// string.Format(LanguageManager.GetInstance().f_GetText("GmStr_StoryTeam_TextGirlNum"), tBaseItemDT.f_GetNum());

            ListItem tListItem = f_AddItem(tListPositionCtrl, i, tBaseItemDT, tBaseItemDT.f_GetName(), tSprite, strGirlNum);
            aList.Add(tListItem);
        }

        tListPositionCtrl.listBoxes = aList.ToArray();
    }

    static ListItem f_AddItem(ListPositionCtrl tListPositionCtrl, int iIndex, NBaseSCDT tData, string strName, Sprite tSprite, string strNum)
    {
        GameObject Obj = tListPositionCtrl.transform.GetChild(iIndex).gameObject;
        ListItem tListItem = Obj.GetComponent<ListItem>();
        if (tListItem == null)
        {
            tListItem = Obj.AddComponent<ListItem>();
        }
        tListItem.m_SCData = tData;
        tListItem.m_strName = strName;
        tListItem.m_strNum = strNum;

        tListItem.m_Logo = tSprite;
        Obj.name = tData.iId + "-" + strName;

        return tListItem;
    }
}