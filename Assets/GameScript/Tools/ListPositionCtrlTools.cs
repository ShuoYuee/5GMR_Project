using System.Collections;
using System.Collections.Generic;
using ccU3DEngine;
using UnityEngine;

public class ListPositionCtrlTools
{

    /// <summary>
    /// 創建圖示
    /// </summary>
    /// <param name="tListPositionCtrl">圖示控制器</param>
    /// <param name="aData">圖示資料庫</param>
    public static void f_Create(ListPositionCtrl tListPositionCtrl, List<NBaseSCDT> aData)
    {
        List<ListItem> aList = new List<ListItem>();
        ccMathEx.f_CreateChild(tListPositionCtrl.gameObject, aData.Count);
        string strAB = "logo";  // StrResources.AssetBundle.Logo.bundleName;

        BaseItemDT tBaseItemDT;
        for (int i = 0; i < aData.Count; i++)
        {
            tBaseItemDT = (BaseItemDT)aData[i];
            Sprite tSprite = glo_Main.GetInstance().m_ResourceManager.f_LoadSpriteForAB(strAB, tBaseItemDT.f_GetLogo());//Load Logo圖示
            string strGirlNum = "";// string.Format(LanguageManager.GetInstance().f_GetText("GmStr_StoryTeam_TextGirlNum"), tBaseItemDT.f_GetNum());

            ListItem tListItem = f_AddItem(tListPositionCtrl, i, tBaseItemDT, tBaseItemDT.f_GetName(), tSprite, strGirlNum);
            aList.Add(tListItem);
        }

        tListPositionCtrl.listBoxes = aList.ToArray();
    }

    /// <summary>
    /// 增加圖示資料
    /// </summary>
    /// <param name="tListPositionCtrl">圖示控制器</param>
    /// <param name="iIndex">圖示Index</param>
    /// <param name="tData">圖示資料</param>
    /// <param name="strName">圖示名稱</param>
    /// <param name="tSprite">圖片</param>
    /// <param name="strNum"></param>
    /// <returns></returns>
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

    public static void f_Create(ListPositionCtrl tListPositionCtrl, string[] aData)
    {
        List<ListItem> aList = new List<ListItem>();
        ccMathEx.f_CreateChild(tListPositionCtrl.gameObject, aData.Length);

        for (int i = 0; i < aData.Length; i++)
        {
            ListItem tListItem = f_AddItem(tListPositionCtrl, i, aData[i], aData[i]);
            aList.Add(tListItem);
        }

        tListPositionCtrl.listBoxes = aList.ToArray();
    }

    private static ListItem f_AddItem(ListPositionCtrl tListPositionCtrl, int iIndex, string strName, string strNum)
    {
        GameObject Obj = tListPositionCtrl.transform.GetChild(iIndex).gameObject;
        ListItem tListItem = Obj.GetComponent<ListItem>();
        if (tListItem == null)
        {
            tListItem = Obj.AddComponent<ListItem>();
        }
        tListItem.m_SCData = null;
        tListItem.m_strName = strName;
        tListItem.m_strNum = strNum;

        Obj.name = strName;

        return tListItem;
    }
}