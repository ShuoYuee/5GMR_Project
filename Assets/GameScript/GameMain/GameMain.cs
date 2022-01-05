﻿using UnityEngine;
using ccU3DEngine;
using Epibyte.ConceptVR;

public class GameMain : MonoBehaviour
{
    public MapPool m_MapPool = new MapPool();
    public Camera m_MainCamera;
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

    public void f_LoadMap()
    {
        m_MapPool.f_LoadMap();
    }

    public void f_SaveMap()
    {
        m_MapPool.f_SaveMap();
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

    public bool _bEdit = false, _bSelectEdit = false;
    public EM_EidtState _EditEM = EM_EidtState.None;
    private EditObjControll _CurEditObjControll = null;
    private TabButton _CurEditBtn = null;
    public void f_Edit()
    {
        _bEdit = !_bEdit;
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapEditState, _bEdit);

        if (!_bEdit)
        {
            f_SetEditBtn();
            _CurEditObjControll.f_SetEditState(false);
            _bSelectEdit = false;
            _CurEditObjControll = null;
        }
    }

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

    public void f_SetCurEditObj(EditObjControll Obj)
    {
        _CurEditObjControll = Obj;
    }

    public EditObjControll f_GetCurEditObj()
    {
        return _CurEditObjControll;
    }
}
