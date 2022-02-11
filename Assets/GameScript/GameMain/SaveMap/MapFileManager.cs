using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Epibyte.ConceptVR;

/// <summary>
/// 地圖存檔UI管理
/// </summary>
public class MapFileManager : MonoBehaviour
{
    #region Public
    [Tooltip("放置讀檔按鈕的列表元件")]
    /// <summary>列表元件</summary>
    public Pagination _Pagination;
    [Tooltip("按鈕物件")]
    /// <summary>按鈕物件</summary>
    public GameObject _oFileBtn;

    [Space(10)]
    [Tooltip("有關Load地圖確認面板的元件")]
    public CheckPanel _LoadCheck;
    [Tooltip("有關Import地圖確認面板的元件")]
    public CheckPanel _ImportCheck;
    [Tooltip("有關Save地圖確認面板的元件")]
    public CheckPanel _SaveCheck;
    #endregion

    /// <summary>當前選擇的檔案名</summary>
    private string _CurFileName;
    /// <summary>是否為覆蓋讀取場景</summary>
    private bool _bLoad = false;

    private void Start()
    {
        f_InitMessage();

        //設置確認面板點擊事件
        _LoadCheck.f_Init(f_LoadMap, f_LoadReturnPanel);
        _ImportCheck.f_Init(f_ImportMap, f_ImportReturnPanel);
        _SaveCheck.f_Init(f_OverWriteMap, f_SaveReturnPanel);
    }

    private void f_InitMessage()
    {
        glo_Main.GetInstance().m_UIMessagePool.f_AddListener(MessageDef.UI_MapObjInit, f_SetLoadMapBtn);    //設定Load按鈕物件資料
        glo_Main.GetInstance().m_UIMessagePool.f_AddListener(MessageDef.UI_LoadBtn, f_OnClickLoad); //讀檔點擊事件
    }

    #region 讀取或匯入地圖
    /// <summary>確認是否Load地圖</summary>
    public void f_OnClickLoad(object e)
    {
        _CurFileName = (string)e;

        if (_bLoad)
        {
            _LoadCheck.f_SetFileText(_CurFileName);
            _LoadCheck.onSelectFile.Invoke();
        }
        else
        {
            _ImportCheck.f_SetFileText(_CurFileName);
            _ImportCheck.onSelectFile.Invoke();
        }
    }

    /// <summary>讀取地圖(覆蓋)</summary>
    public void f_LoadMap()
    {
        GameMain.GetInstance().m_MapPool.f_ResetMap();
        GameMain.GetInstance().m_MapPool.f_LoadMap(_CurFileName);
        _LoadCheck.onClickYes.Invoke();//調用點擊功能
        _CurFileName = "";
    }

    /// <summary>匯入地圖</summary>
    public void f_ImportMap()
    {
        GameMain.GetInstance().m_MapPool.f_LoadMap(_CurFileName);
        _ImportCheck.onClickYes.Invoke();
        _CurFileName = "";
    }

    /// <summary>返回Load選取介面</summary>
    public void f_LoadReturnPanel()
    {
        _LoadCheck.onClickNO.Invoke();//調用點擊功能
        _CurFileName = "";
    }

    /// <summary>返回Import選取介面</summary>
    public void f_ImportReturnPanel()
    {
        _ImportCheck.onClickNO.Invoke();//調用點擊功能
        _CurFileName = "";
    }
    #endregion

    #region 地圖檔案UI列表
    /// <summary>按下按鈕Load地圖(覆蓋)</summary>
    public void f_OnClickLoadSecene()
    {
        f_Reset();
        _bLoad = true;
    }

    /// <summary>按下按鈕匯入地圖</summary>
    public void f_OnClickImportSecene()
    {
        f_Reset();
        _bLoad = false;
    }

    /// <summary>重設地圖讀檔列表</summary>
    private void f_Reset()
    {
        if (_Pagination == null) { return; }
        f_ClearMapBtn();
        List<GameObject> oMapObj = new List<GameObject>();
        string[] aData = GameMain.GetInstance().m_MapPool.f_LoadPreviewData();
        for (int i = 0; i < aData.Length; i++)
        {
            oMapObj.Add(_oFileBtn);
        }
        _Pagination.items = oMapObj;
        _Pagination.f_Reset();
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
    #endregion

    #region 儲存地圖
    /// <summary>
    /// 儲存地圖
    /// </summary>
    /// <param name="text">檔案自定義名稱</param>
    public void f_SaveMap(Text text)
    {
        //GameMain.GetInstance().m_MapPool.f_SaveMap(text.text);
        _CurFileName = text.text;
        if (GameMain.GetInstance().m_MapPool.f_CheckFileName(_CurFileName))
        {
            _SaveCheck.onSelectFile.Invoke();
        }
        else
        {
            f_OverWriteMap();
        }
    }

    /// <summary>覆蓋存檔</summary>
    public void f_OverWriteMap()
    {
        GameMain.GetInstance().m_MapPool.f_SaveMap(_CurFileName);
        _SaveCheck.onClickYes.Invoke();
        _CurFileName = "";
    }

    /// <summary>返回存檔介面</summary>
    public void f_SaveReturnPanel()
    {
        _SaveCheck.onClickNO.Invoke();//調用點擊功能
        _CurFileName = "";
    }
    #endregion
}

[System.Serializable]
public class CheckPanel
{
    public GameObject Panel;
    public Text FileText;
    public Button YesBtn, NoBtn;

    //按鈕事件
    [Space(10)]
    public OnClickYesBtn onClickYes;
    public OnClickNOBtn onClickNO;
    public OnSelectFile onSelectFile;

    [System.Serializable]
    public class OnSelectFile : UnityEvent { }
    [System.Serializable]
    public class OnClickYesBtn : UnityEvent { }
    [System.Serializable]
    public class OnClickNOBtn : UnityEvent { }

    public void f_Init(UnityAction YesEvent, UnityAction NoEvent)
    {
        if (YesBtn == null || NoBtn == null)
        {
            MessageBox.ASSERT("未設置讀檔確認按鈕");
            return;
        }
        YesBtn.onClick.AddListener(YesEvent);
        NoBtn.onClick.AddListener(NoEvent);
    }

    public void f_PanelCtrl(bool e)
    {
        if (Panel == null)
        {
            MessageBox.ASSERT("未設置讀檔確認介面");
            return;
        }
        Panel.SetActive(e);
    }

    public void f_SetFileText(string strFile)
    {
        FileText.text = strFile;
    }
}
