using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;
using Epibyte.ConceptVR;
using ZenFulcrum.EmbeddedBrowser;

public class GameMain : MonoBehaviour
{
    #region 管理物件
    [Header("初始化")]
    [Tooltip("玩家")]
    /// <summary>玩家</summary>
    public Transform m_Player;
    [Tooltip("起始出生點")]
    /// <summary>起始出生點</summary>
    public Transform m_InitPos;

    [Space(10)]
    [Header("主要遊戲元件")]
    [Tooltip("主攝影機")]
    /// <summary>主攝影機</summary>
    public Camera m_MainCamera;
    [Tooltip("物件主選單")]
    /// <summary>物件主選單</summary>
    public GameObject m_MainMenu = null;
    [Tooltip("素材元件選單")]
    /// <summary>素材元件選單</summary>
    public Pagination m_Pagination;
    [Tooltip("VR浮動網頁視窗")]
    /// <summary>VR浮動網頁視窗</summary>
    public Browser m_Browser;

    [Space(10)]
    [Header("額外物件")]
    [Tooltip("替選擇物掛上區別用材質")]
    /// <summary>選擇物材質</summary>
    public Material _SelectMaterial;
    [Tooltip("生成物件將統一掛在該物件下")]
    /// <summary>主父物件</summary>
    public GameObject m_GameTable;
    [Tooltip("控制模式顯示文字")]
    /// <summary>控制模式顯示文字</summary>
    public TextMesh _PlayerCtrlText;
    [Tooltip("刪除物件顯示文字")]
    /// <summary>刪除物件顯示文字</summary>
    public TextMesh _DeleteText;

    [HideInInspector]
    /// <summary>地圖物件池</summary>
    public MapPool m_MapPool = new MapPool();   
    /// <summary>編輯管理器</summary>
    public EditManager m_EditManager = new EditManager();
    #endregion

    private static GameMain _Instance = null;
    public static GameMain GetInstance()
    {
        return _Instance;
    }


    void Awake()
    {
        _Instance = this;
        m_GameTable.SetActive(true);
        //ccUIManage.GetInstance().f_SendMsg("UI_GameMain", BaseUIMessageDef.UI_OPEN, null, true);
        ccUIManage.GetInstance().f_SendMsgV3("ui_mrcontorl.bundle", "UI_MRControl", BaseUIMessageDef.UI_OPEN);

        ccTimeEvent.GetInstance().f_RegEvent(0.3f, true, null, f_UpdateMenuPos);
    }

    private void Start()
    {
        f_InitGameObj();
    }

    /// <summary>手動導入場上物件</summary>
    private void f_InitGameObj()
    {
        if (m_GameTable.transform.childCount <= 0) { return; }
        EditObjControll[] m_InitGameObj = m_GameTable.GetComponentsInChildren<EditObjControll>();
        for (int i = 0; i < m_GameTable.transform.childCount; i++)
        {
            m_MapPool.f_ManualAddObj(m_InitGameObj[i]);
        }
    }

    /// <summary>更新菜單位置</summary>
    private void f_UpdateMenuPos(object e)
    {
        if (m_MainMenu != null)
            m_MainMenu.transform.localPosition = m_MainCamera.transform.localPosition + new Vector3(-0.4f, 0.05f, 0);
    }

    #region 地圖功能
    public void f_LoadMap(string strFileName)
    {
        m_MapPool.f_ResetMap();
        m_MapPool.f_LoadMap(strFileName);
    }

    public void f_ImportMap(string strFileName)
    {
        m_MapPool.f_LoadMap(strFileName);
    }

    public void f_SaveMap(string strFileName)
    {
        m_MapPool.f_SaveMap(strFileName);
    }

    public void f_DelMap(string strFileName)
    {
        m_MapPool.f_DelMap(strFileName);
    }

    public void f_ResetMap()
    {
        m_MapPool.f_ResetMap();
    }

    public void f_ExitMap()
    {
        glo_Main.GetInstance().f_Destroy();
    }

    public EditObjControll f_AddObj(CharacterDT tCharacterDT)
    {
        return m_MapPool.f_AddObj(tCharacterDT);
    }

    public void f_DelObj(long iId)
    {
        m_MapPool.f_DeleteObj(iId);
    }

    public Transform f_GetObjParent()
    {
        return m_GameTable.transform;
    }
    #endregion

    #region 編輯功能

    /// <summary>
    /// 點選編輯按鈕
    /// </summary>
    /// <param name="EditTpye">按鈕類型</param>
    public void f_SetEditBtn(string EditTpye)
    {
        m_EditManager.f_SetEditBtn(EditTpye);
        EditDisplay.GetInstance().f_SetPanel(true);
    }

    /// <summary>點選開始編輯按鈕</summary>
    public void f_Edit()
    {
        m_EditManager.f_Edit();
    }

    /// <summary>點選離開編輯按鈕</summary>
    public void f_ExitEdit()
    {
        m_EditManager.f_EditExit();
        EditDisplay.GetInstance().f_SetPanel(false);
    }

    /// <summary>
    /// 點選按鈕
    /// </summary>
    /// <param name="button">按鈕</param>
    public void f_LeaveEdit(TabButton button)
    {
        m_EditManager.f_LeaveEdit(button);
    }    

    /// <summary>
    /// 設定軸心模式
    /// </summary>
    /// <param name="strAxis">軸心模式(填軸心名稱)</param>
    public void f_SetEditAxis(string strAxis)
    {
        m_EditManager.f_SetEditAxis(strAxis);
    }

    public void f_SetEditAxis(int iAxis)
    {
        m_EditManager.f_SetEditAxis(iAxis);
    }

    /// <summary>
    /// 設定座標模式
    /// </summary>
    /// <param name="strPoint">座標模式(填座標名稱)</param>
    public void f_SetEditPoint(string strPoint)
    {
        m_EditManager.f_SetEditPoint(strPoint);
    }

    /// <summary>設定當前點選的編輯按鈕</summary>
    public void f_SetEditBtn(TabButton EditBtn = null)
    {
        m_EditManager.f_SetEditBtn(EditBtn);
    }

    /// <summary>設定Edit按鈕狀態</summary>
    public void f_SetEditBtnOnClick(TabButton EditBtn = null)
    {
        m_EditManager.f_SetEditBtnOnClick(EditBtn);
    }

    /// <summary>
    /// 設定按鈕UI文字
    /// </summary>
    /// <param name="textGroup">按鈕文字群</param>
    public void f_SetOnClickText(Transform textGroup)
    {
        m_EditManager.f_SetOnClickText(textGroup);
    }

    /// <summary>設定當前編輯的物件</summary>
    public void f_SetCurEditObj(EditObjControll Obj)
    {
        m_EditManager.f_SetCurEditObj(Obj);
    }

    /// <summary>取得當前編輯的物件</summary>
    public EditObjControll f_GetCurEditObj()
    {
        return m_EditManager.f_GetCurEditObj();
    }

    /// <summary>
    /// 設定當前軸心模式(確保每一頁面的模式都相同)
    /// </summary>
    /// <param name="TextGroup">按鈕文字群</param>
    public void f_SetCurPoint(Transform TextGroup)
    {
        m_EditManager.f_SetCurPoint(TextGroup);
    }

    /// <summary>
    /// 設定當前座標模式(確保每一頁面的模式都相同)
    /// </summary>
    /// <param name="TextGroup">按鈕文字群</param>
    public void f_SetCurAxis(Transform TextGroup)
    {
        m_EditManager.f_SetCurAxis(TextGroup);
    }

    #endregion

    /// <summary>刪除當前編輯物件</summary>
    public void f_DelEditObj()
    {
        m_MapPool.f_DeleteObj(m_EditManager.f_GetCurEditObj().f_GetId());
    }

    /// <summary>
    /// 編輯物播放預覽動畫
    /// </summary>
    /// <param name="iAddIndex">增減動畫Index</param>
    public void f_EditObjAnimPlay(int iAddIndex)
    {
        m_EditManager.f_EditObjAnimPlay(iAddIndex);
    }

    /// <summary>設定刪除物顯示文字</summary>
    public void f_SetDelText()
    {
        _DeleteText.text = "確定刪除該物件？//n" + m_EditManager.f_GetCurEditObj().name;
    }

    /// <summary>斗內後創建啦啦隊</summary>
    public void f_Sponsor()
    {
        glo_Main.GetInstance().m_ResourceManager.f_CreateResource("Model/DancingGirlA");
    }
}
