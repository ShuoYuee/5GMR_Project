using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Epibyte.ConceptVR;

/// <summary>
/// 地圖存檔UI管理
/// </summary>
public class MapFileManager : MonoBehaviour
{
    /// <summary>列表元件</summary>
    public Pagination _Pagination;
    /// <summary>按鈕物件</summary>
    public GameObject _oFileBtn;

    private void Start()
    {
        f_InitMessage();
    }

    private void f_InitMessage()
    {
        glo_Main.GetInstance().m_UIMessagePool.f_AddListener(MessageDef.UI_MapObjInit, f_SetLoadMapBtn);    //設定Load按鈕物件資料
    }

    /// <summary>重設地圖讀檔列表</summary>
    public void f_Reset()
    {
        if (_Pagination == null) { return; }
        f_ClearMapBtn();
        List<GameObject> oMapObj = new List<GameObject>();
        string[] aData = GameMain.GetInstance().m_MapPool.f_LoadPreviewData();
        for(int i = 0; i < aData.Length; i++)
        {
            oMapObj.Add(_oFileBtn);
        }
        _Pagination.items = oMapObj;
        _Pagination.f_Reset();
    }

    /// <summary>
    /// 讀取地圖
    /// </summary>
    /// <param name="text">檔案名稱</param>
    public void f_LoadMap(Text text)
    {
        GameMain.GetInstance().m_MapPool.f_LoadMap(text.text);
    }

    /// <summary>設定地圖讀檔按鈕</summary>
    private void f_SetLoadMapBtn(object e)
    {
        List<GameObject> oData = (List<GameObject>)e;
        string[] aData = GameMain.GetInstance().m_MapPool.f_LoadPreviewData();
        for (int i = 0; i < oData.Count; i++)
        {
            if(oData[i].GetComponent<LoadMapBtn>() == null) { return; }
            oData[i].GetComponentInChildren<Text>().text = aData[i];
            oData[i].name = aData[i];
            oData[i].transform.localScale = _oFileBtn.transform.localScale;
            oData[i].transform.localPosition = Vector3.zero;
        }
    }

    /// <summary>清空列表</summary>
    private void f_ClearMapBtn()
    {
        for(int i = 0; i < _Pagination.positions.childCount; i++)
        {
            ccMathEx.f_CreateChild(_Pagination.positions.GetChild(i).gameObject, 0);
        }
        
    }

    /// <summary>
    /// 儲存地圖
    /// </summary>
    /// <param name="text">檔案自定義名稱</param>
    public void f_SaveMap(Text text)
    {
        GameMain.GetInstance().m_MapPool.f_SaveMap(text.text);
    }
}
