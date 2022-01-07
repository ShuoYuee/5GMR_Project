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
    /// <summary>物件選單</summary>
    public Pagination m_Pagination;

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
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, 2);//開關提示文字
    }

    public void f_SaveMap()
    {
        m_MapPool.f_SaveMap();
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, 3);//開關提示文字
    }

    public void f_ResetMap()
    {
        int iCount = m_MapPool.f_Count();
        for(int i = 0; i < iCount; i++)
        {
            m_MapPool.f_DeleteObj(m_MapPool.f_GetAll()[0].iId);
        }
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, 4);//開關提示文字
    }

    public void f_ExitMap()
    {
        glo_Main.GetInstance().f_Destroy();
    }

    public EditObjControll f_AddObj(CharacterDT tCharacterDT)
    {
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, 5);//開關提示文字
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

    /// <summary>是否處於編輯模式</summary>
    public bool _bEdit = false;
    /// <summary>是否已選取編輯物件</summary>
    public bool _bSelectEdit = false;
    /// <summary>編輯類型</summary>
    public EM_EidtState _EditEM = EM_EidtState.None;
    /// <summary>當前編輯的物件</summary>
    private EditObjControll _CurEditObjControll = null;
    /// <summary>當前點選的編輯按鈕</summary>
    private TabButton _CurEditBtn = null;

    /// <summary>
    /// 點選編輯按鈕
    /// </summary>
    /// <param name="EditTpye">按鈕類型</param>
    public void f_SetEditBtn(string EditTpye)
    {
        switch (EditTpye)
        {
            case "Position":
                _EditEM = EM_EidtState.Position;
                break;
            case "RotationH":
                _EditEM = EM_EidtState.RotationH;
                break;
            case "RotationV":
                _EditEM = EM_EidtState.RotationV;
                break;
            case "Scale":
                _EditEM = EM_EidtState.Scale;
                break;
            case "Edit":
                _EditEM = EM_EidtState.None;
                f_Edit();
                break;
        }
    }

    /// <summary>點選編輯按鈕</summary>
    public void f_Edit()
    {
        _bEdit = !_bEdit;

        if (_bEdit)
        {
            f_SetEditBtn();
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, 1);//開關提示文字
        }
        else
        {
            f_SetEditBtn();
            if (_CurEditObjControll != null)
            {
                _CurEditObjControll.f_SetEditState(false);
            }
            
            _bSelectEdit = false;
            _CurEditObjControll = null;
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, -1);//開關提示文字
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, -6);//開關提示文字
        }
    }

    /// <summary>設定當前點選的編輯按鈕</summary>
    public void f_SetEditBtn(TabButton EditBtn = null)
    {
        if (_CurEditBtn != null && _CurEditBtn != EditBtn)
        {
            _CurEditBtn.DeactivateAllEffects();
            _CurEditBtn.isClicked = false;
        }
        _CurEditBtn = EditBtn;

        if (EditBtn == null) { return; }
        EditBtn.ActivateAllEffects();
        _CurEditBtn.isClicked = true;
    }

    /// <summary>設定當前編輯的物件</summary>
    public void f_SetCurEditObj(EditObjControll Obj)
    {
        _CurEditObjControll = Obj;
    }

    /// <summary>取得當前編輯的物件</summary>
    public EditObjControll f_GetCurEditObj()
    {
        return _CurEditObjControll;
    }

    /// <summary>
    /// 編輯物播放預覽動畫
    /// </summary>
    /// <param name="iAddIndex">增減動畫Index</param>
    public void f_EditObjAnimPlay(int iAddIndex)
    {
        if (!_bEdit || _CurEditObjControll == null) { return; }

        if (iAddIndex == 0)//停止播放預覽動畫
        {
            _CurEditObjControll.f_AnimStop();
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_EditObjAnim, null);//關閉動畫提示文字
        }

        _CurEditObjControll.f_AnimPlay(iAddIndex);
    }
    #endregion
}
