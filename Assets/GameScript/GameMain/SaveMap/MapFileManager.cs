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
    [Tooltip("檔案操作面板的元件")]
    public CheckPanel _FileCanvas;
    [Tooltip("有關Load地圖確認面板的元件")]
    public CheckPanel _LoadCheck;
    [Tooltip("有關Import地圖確認面板的元件")]
    public CheckPanel _ImportCheck;
    [Tooltip("有關Save地圖確認面板的元件")]
    public CheckPanel _SaveCheck;
    [Tooltip("有關Delete存檔確認面板的元件")]
    public CheckPanel _DeleteCheck;
    #endregion

    /// <summary>當前選擇的檔案名</summary>
    private string _CurFileName;
    /// <summary>是否為覆蓋讀取場景</summary>
    private bool _bLoad = false;

    private void Start()
    {
        f_InitMessage();

        //設置確認面板點擊事件
        _FileCanvas.f_Init(f_FileCtrl, f_FileReturnPanel);
        _LoadCheck.f_Init(f_LoadMap, f_LoadReturnPanel);
        _ImportCheck.f_Init(f_ImportMap, f_ImportReturnPanel);
        _SaveCheck.f_Init(f_OverWriteMap, f_SaveReturnPanel);
        _DeleteCheck.f_Init(f_DelMap, f_DelReturnPanel);
    }

    private void f_InitMessage()
    {
        glo_Main.GetInstance().m_UIMessagePool.f_AddListener(MessageDef.UI_MapObjInit, f_SetLoadMapBtn);    //設定Load按鈕物件資料
        glo_Main.GetInstance().m_UIMessagePool.f_AddListener(MessageDef.UI_LoadBtn, f_OnClickFile); //讀檔點擊事件
    }

    #region 檔案管控
    public void f_FileCtrl()
    {
        _FileCanvas.f_SetFileText(_CurFileName);
    }

    private void f_FileReturnPanel()
    {
        _FileCanvas.f_PanelCtrl(true);
    }

    public void f_OnClickFile(int iState)
    {
        if (iState == 1)
        {
            _LoadCheck.f_PanelCtrl(false, true);
            _LoadCheck.onSelectFile.Invoke();
        }
        else if (iState == 2)
        {
            _ImportCheck.f_PanelCtrl(false, true);
            _ImportCheck.onSelectFile.Invoke();
        }
        else if (iState == 3)
        {
            _SaveCheck.f_PanelCtrl(true);
            _SaveCheck.onSelectFile.Invoke();
        }
        else if (iState == 4)
        {
            _DeleteCheck.f_PanelCtrl(false, true);
            _DeleteCheck.onSelectFile.Invoke();
        }
    }

    /// <summary>讀檔點擊事件</summary>
    private void f_OnClickFile(object e)
    {
        _CurFileName = (string)e;
        _FileCanvas.f_PanelCtrl(true);
        _FileCanvas.f_SetFileText(_CurFileName);

        /*if (_bLoad)
        {
            _LoadCheck.f_SetFileText(_CurFileName);
            _LoadCheck.onSelectFile.Invoke();
        }
        else
        {
            _ImportCheck.f_SetFileText(_CurFileName);
            _ImportCheck.onSelectFile.Invoke();
        }*/
    }
    #endregion

    #region 讀取或匯入地圖
    /// <summary>讀取地圖(覆蓋)</summary>
    private void f_LoadMap()
    {
        GameMain.GetInstance().m_MapPool.f_ResetMap();
        GameMain.GetInstance().m_MapPool.f_LoadMap(_CurFileName);
        _LoadCheck.onClickYes.Invoke();//調用點擊功能
        _LoadCheck.f_PanelCtrl(false, false);
        _CurFileName = "";
    }

    /// <summary>返回Load選取介面</summary>
    private void f_LoadReturnPanel()
    {
        _LoadCheck.onClickNO.Invoke();//調用點擊功能
        _LoadCheck.f_PanelCtrl(true);
        _CurFileName = "";
    }

    /// <summary>匯入地圖</summary>
    private void f_ImportMap()
    {
        GameMain.GetInstance().m_MapPool.f_LoadMap(_CurFileName);
        _ImportCheck.onClickYes.Invoke();
        _ImportCheck.f_PanelCtrl(false, false);
        _CurFileName = "";
    }

    /// <summary>返回Import選取介面</summary>
    private void f_ImportReturnPanel()
    {
        _ImportCheck.onClickNO.Invoke();//調用點擊功能
        _ImportCheck.f_PanelCtrl(true);
        _CurFileName = "";
    }
    #endregion

    #region 地圖檔案UI列表
    /*/// <summary>按下按鈕Load地圖(覆蓋)</summary>
    private void f_OnClickLoadSecene()
    {
        f_Reset();
        _bLoad = true;
    }

    /// <summary>按下按鈕匯入地圖</summary>
    private void f_OnClickImportSecene()
    {
        f_Reset();
        _bLoad = false;
    }*/

    /// <summary>重設地圖讀檔列表</summary>
    public void f_Reset()
    {
        if (_Pagination == null) { return; }
        _FileCanvas.f_PanelCtrl(false, true);
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
            //_SaveCheck.onSelectFile.Invoke();
            _SaveCheck.f_PanelCtrl(false, true);
        }
        else
        {
            f_OverWriteMap();
        }
    }

    /// <summary>存檔</summary>
    private void f_OverWriteMap()
    {
        GameMain.GetInstance().m_MapPool.f_SaveMap(_CurFileName);
        _SaveCheck.onClickYes.Invoke();//調用點擊功能
        _SaveCheck.f_PanelCtrl(false, false);
        _CurFileName = "";
    }

    /// <summary>返回存檔介面</summary>
    private void f_SaveReturnPanel()
    {
        _SaveCheck.onClickNO.Invoke();//調用點擊功能
        _SaveCheck.f_PanelCtrl(true);
        _CurFileName = "";
    }
    #endregion

    private void f_DelMap()
    {
        GameMain.GetInstance().f_DelMap(_CurFileName);
        _DeleteCheck.onClickYes.Invoke();
        _DeleteCheck.f_PanelCtrl(false, false);
        _CurFileName = "";
    }

    public void f_DelReturnPanel()
    {
        _DeleteCheck.onClickNO.Invoke();
        _DeleteCheck.f_PanelCtrl(true);
        _CurFileName = "";
    }
}

[System.Serializable]
public class CheckPanel
{
    public GameObject MainPanel, MidPanel;
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
        if (YesBtn != null)
        {
            YesBtn.onClick.AddListener(YesEvent);
        }
        if (NoBtn != null)
        {
            NoBtn.onClick.AddListener(NoEvent);
        }
    }

    public void f_PanelCtrl(bool bMain, bool bMid = false)
    {
        GameTools.f_SetGameObject(MainPanel, bMain);
        GameTools.f_SetGameObject(MidPanel, bMid);
    }

    public void f_SetFileText(string strFile)
    {
        if (FileText == null)
        {
            MessageBox.ASSERT("未設置檔案名文字組件");
            return;
        }
        FileText.text = strFile;
    }
}
