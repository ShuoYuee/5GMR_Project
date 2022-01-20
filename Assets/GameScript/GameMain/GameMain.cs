using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;
using Epibyte.ConceptVR;

public class GameMain : MonoBehaviour
{
    /// <summary>地圖物件池</summary>
    public MapPool m_MapPool = new MapPool();
    /// <summary>主攝影機</summary>
    public Camera m_MainCamera;
    /// <summary>物件主選單</summary>
    public GameObject m_MainMenu = null;
    /// <summary>素材元件選單</summary>
    public Pagination m_Pagination;

    public EditManager m_EditManager = new EditManager();

    ///// <summary>
    ///// 游戏为单线游戏直接启动单线逻辑
    ///// </summary>
    //private GameControll _GameControll = new GameControll(-99);

    //private GameControllV2 _GameControllV2 = new GameControllV2();

    public GameObject m_GameTable;

    private static GameMain _Instance = null;
    public static GameMain GetInstance()
    {
        return _Instance;
    }


    void Awake()
    {
        _Instance = this;
        m_GameTable.SetActive(true);
        ccUIManage.GetInstance().f_SendMsg("UI_GameMain", BaseUIMessageDef.UI_OPEN, null, true);

        ccTimeEvent.GetInstance().f_RegEvent(0.3f, true, null, f_InitMenuPos);
    }

    private void f_InitMenuPos(object e)
    {
        if(m_MainMenu != null)
        m_MainMenu.transform.position = m_MainCamera.transform.position + new Vector3(-0.35f, 0.05f, 0);
    }

    //void Start()
    //{
    //    InitMessage();
    //    InitManager();

    //}

    //private void InitManager()
    //{
    //    MessageBox.DEBUG("加载资源");

    //    ResManagerState_Loop tResManagerState_Loop = new ResManagerState_Loop();
    //    //GameObject tLoginPage = GameObject.Find("LoginPage");


    //}

    //void InitMessage()
    //{
    //    glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.GameStart, OnGameStart);
    //    glo_Main.GetInstance().m_GameMessagePool.f_AddListener(MessageDef.GAMEOVER, OnGameOver);

    //}

    //private void LoadGameDataForMemory()
    //{
    //    if (ccLocalDataManager.f_HasLocalData(EM_eLocalData.iCurGameControllIndex.ToString()))
    //    {
    //        StaticValue.m_iCurGameControllIndex = ccLocalDataManager.f_GetLocalData<int>(EM_eLocalData.iCurGameControllIndex.ToString());
    //    }
    //    else
    //    {
    //        StaticValue.m_iCurGameControllIndex = 1000;
    //    }

    //}

    //void OnGameStart(object Obj)
    //{
    //    //ccUIManage.GetInstance().f_SendMsg("UI_GameBattleUI", BaseUIMessageDef.UI_OPEN, null, true);
    //    MessageBox.DEBUG("開始:" + StaticValue.m_iCurGameControllIndex);

    //    LoadGameDataForMemory();
    //    _GameControll.f_Start(StaticValue.m_iCurGameControllIndex);
    //    //_GameStepManager.f_ChangeState((int)EM_eGameStep.PlayBall);
    //}

    //void OnGameOver(object Obj)
    //{

    //    MessageBox.DEBUG("OnGameOver");
    //}

    //private void Update()
    //{
    //    _GameControll.f_Update();

    //}


    #region 地圖功能
    public void f_LoadMap()
    {
        m_MapPool.f_LoadMap();
    }

    public void f_SaveMap()
    {
        m_MapPool.f_SaveMap();
    }

    public void f_ResetMap()
    {
        int iCount = m_MapPool.f_Count();
        for(int i = 0; i < iCount; i++)
        {
            m_MapPool.f_DeleteObj(m_MapPool.f_GetAll()[0].iId);
        }
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
    }

    /// <summary>點選編輯按鈕</summary>
    public void f_Edit()
    {
        m_EditManager.f_Edit();
    }

    public void f_OpenPanel(GameObject Panel)
    {
        m_EditManager.f_OpenPanel(Panel);
    }

    public void f_ClosePanel(GameObject Panel)
    {
        m_EditManager.f_ClosePanel(Panel);
    }

    public void f_LeaveEdit(TabButton button)
    {
        m_EditManager.f_LeaveEdit(button);
    }

    public void f_SetEditAxis(string strAxis)
    {
        m_EditManager.f_SetEditAxis(strAxis);
    }

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
    /// 編輯物播放預覽動畫
    /// </summary>
    /// <param name="iAddIndex">增減動畫Index</param>
    public void f_EditObjAnimPlay(int iAddIndex)
    {
        m_EditManager.f_EditObjAnimPlay(iAddIndex);
    }
    #endregion
}
