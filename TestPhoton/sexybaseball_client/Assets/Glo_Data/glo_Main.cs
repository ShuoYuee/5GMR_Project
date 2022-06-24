using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ccILR;
using ccPhotonSocket;
using ccU3DEngine;
using SexyBaseball.Server;
using UnityEngine;
using UnityEngine.Events;

public class glo_Main : MonoBehaviour
{
    public const int ccTimeEventEmptyID = -99;

    private static glo_Main _Instance = null;
    public static string DataRoot { get; private set; }

    public ResourceManager m_ResourceManager;

    public ccMessagePoolV2 m_GameMessagePool = new ccMessagePoolV2();
    public ccMessagePoolV2 m_UIMessagePool = new ccMessagePoolV2();

    public SC_Pool m_SC_Pool;
    public ccServerSocketPeer m_GameSocket = new ccServerSocketPeer();

    private ccLog m_ccLog = null;


    public static glo_Main GetInstance()
    {
        if (null == _Instance)
        {
            _Instance = (glo_Main)FindObjectOfType(typeof(glo_Main));
            if (null == _Instance)
            {
                MessageBox.ASSERT("init glo_Main Fail");
                return null;
            }
        }
        return _Instance;
    }

    private void Awake()
    {
        m_ccLog = new ccLog("Log", true, true);

        GloData.glo_iVer = ccMath.atoi(Application.version);
        GloData.glo_strVer = GloData.glo_strVer + GloData.glo_iVer;

        m_SC_Pool = new SC_Pool();
        m_ResourceManager = new ResourceManager();
    }

    private void Start()
    {
        MessageBox.f_UseUnityDebug();
        MessageBox.DEBUG("初始游戏");

        DontDestroyOnLoad(this);

        f_InitEngineParam();

        f_InitGameUnityParams();
        f_InitPath();
        f_InitComponent();

        f_InitMessage();
        f_InitAudio();
        f_InitResManager();
    }

    private void Update()
    {
        m_GameMessagePool.f_Update();
        m_UIMessagePool.f_Update();

#if OFFLINE_TESTING
#else
        m_GameSocket.f_Update();
#endif

    }

    private void OnDestroy()
    {
#if OFFLINE_TESTING
#else
        m_GameSocket.f_Disconnect();
#endif
    }

    public void f_Destroy()
    {
        MessageBox.DEBUG("强制结束游戏 QuitGame");
        MessageBox.DEBUG("................................................");

#if OFFLINE_TESTING
#else
        m_GameSocket.f_Disconnect();
#endif
        ccTimeEvent.GetInstance().f_RegEvent(1, false, null, Callback_ApplicationQuit);
    }

    private void Callback_ApplicationQuit(object Obj)
    {
        Application.Quit();
    }

    public Coroutine f_StartCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    private void f_InitEngineParam()
    {
        Application.runInBackground = true;
        Application.targetFrameRate = 90;

        bool m_UseLocalAB = true;
#if UNITY_EDITOR
        //打开模拟器模式 ，此模式下资源不需要经过打包输出和上传到资源服务器就可以直接使用
        //平时开发都采用模拟器模式来进行，后期整合时再切换回正常模式
        ccU3DEngineParam.m_bIsLocalAB = m_UseLocalAB;
#else
                ccU3DEngineParam.m_bIsLocalAB = false;
#endif

        //设置当前编辑器及最终打包输出的目标平台
#if UNITY_EDITOR
        UnityEditor.BuildTarget tBuildTarget = UnityEditor.EditorUserBuildSettings.activeBuildTarget;
        if (tBuildTarget == UnityEditor.BuildTarget.Android)
        {
            ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.Android;
        }
        else if (tBuildTarget == UnityEditor.BuildTarget.StandaloneWindows || tBuildTarget == UnityEditor.BuildTarget.StandaloneWindows64)
        {
            ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.StandaloneWindows;
        }
#elif UNITY_ANDROID
           ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.Android;
#elif UNITY_STANDALONE
            ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.StandaloneWindows;
#endif
    }

    // 各平台需要初始化的參數
    private void f_InitGameUnityParams()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
#elif UNITY_STANDALONE
#endif
    }

    // 各平台資源路徑初始化設定
    private void f_InitPath()
    {
#if UNITY_EDITOR
        DataRoot = "Data";
#elif UNITY_ANDROID
        DataRoot = "Data";
#elif UNITY_STANDALONE
        DataRoot = "Data";
#endif
    }

    // 初始化核心组件
    private void f_InitComponent()
    {
        // 熱更新
        ccUIManage.GetInstance().f_SaveFactoryHandler(ccILR_ClassFactory.GetInstance());

        // 
        gameObject.AddComponent<UpdateManager>();
        UpdateManager.Get().SetClientVersion(CurrentBundleVersion.version);

        gameObject.AddComponent<AssetLoader>();
    }

    private void f_InitMessage()
    {
        ccTimeEvent.GetInstance().f_ChangePingTime(0.02f);
    }

    private void f_InitAudio()
    {

    }

    // 初始化资源管理，开始加载资源
    private float startLoadResourceTime = 0;
    private void f_InitResManager()
    {
        startLoadResourceTime = Time.realtimeSinceStartup;
        LoadResource loadRes = new LoadResource();
        loadRes.f_StartLoad(f_OnLoadResSuc);
    }

    private void f_OnLoadResSuc(object Obj)
    {
        ccTimeEvent.GetInstance().f_RegEvent(0f, false, null, Callback_StartUpdateRes);
    }

    #region 资源更新

    private void Callback_StartUpdateRes(object Obj)
    {
        UpdateManager.Get().m_remoteUri = new[] { GloData.glo_strABServerURL };
        UpdateManager.Get().BeginInitialize("1.0", m_SC_Pool.f_GetABVer(), Callback_UpdateInitialize, false);
    }

    private void Callback_UpdateInitialize()
    {
        MessageBox.DEBUG("开始更新资源。");
        UpdateManager.Get().BeginDownload(f_OnUpdateComplete);
    }

    private void f_OnUpdateComplete()
    {
        UpdateManager.Get().f_ZipBaseRes(Callback_ZipBaseResProgress, CallBack_ZipBaseResSuc);
    }

    private void Callback_ZipBaseResProgress(object Obj)
    {
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_UpdateInitProgress, Obj);
    }

    private void CallBack_ZipBaseResSuc(object Obj)
    {
        ccILR_ClassFactory.GetInstance().f_LoadHotFixDLL(true);
        MessageBox.DEBUG("资源更新成功...");
        m_UIMessagePool.f_Broadcast(MessageDef.UI_UpdateInitSuccess, Obj);

        f_InitGame();
    }

    #endregion //资源更新及切换到登陆场景

    #region 準備進入遊戲

    private void f_RegLanguageSC()
    {
        ////登陆相关语言脚本
        LanguageManager.GetInstance().f_RegSC("Login");
        ////系统级语言脚本
        LanguageManager.GetInstance().f_RegSC("System");
        LanguageManager.GetInstance().f_RegSC("GameMain");
    }

    public void f_InitGame()
    {
        f_RegLanguageSC();

        f_InitPool();
        f_InitGameData();
        f_InitSocket();

        //ccUIManage.GetInstance().f_SendMsgV2("UI_GameLogin", BaseUIMessageDef.UI_OPEN);
        ccUIManage.GetInstance().f_SendMsg(StrUI.Login, BaseUIMessageDef.UI_OPEN);

    }

    private void f_InitPool()
    {

    }

    private void f_InitSocket()
    {
        m_GameSocket.f_Connect(GloData.glo_strSvrIP, GloData.glo_iSvrPort);

        //m_GameSocket.f_AddListener((int)SocketCommand.PlayerEnergy_Resp, new CMsg_PlayerEnergyResp(), Callback_PlayerEnergyResp);
        //m_GameSocket.f_AddListener((int)SocketCommand.PlayerItemActivation_Resp, new CMsg_PlayerItemActivationResp(), Callback_PlayerItemActivationResp);
    }

    private void f_InitGameData()
    {
        // 載入遊戲設定
        GameDataLoad.f_LoadGameSystemMemory();

        LanguageManager.GetInstance().f_ChangeLanguage((Locale)StaticValue.m_iLangId);
        MessageBox.DEBUG($"Loaded '{(Locale)StaticValue.m_iLangId}'");
    }



    //public void f_UpdateItemInUse(CMsg_ItemInUse[] items)
    //{
    //    m_lstItems.Clear();
    //    for (int i = 0; i < items.Length; i++)
    //    {
    //        var item = (EM_Item)items[i].m_iItem;
    //        if (item == EM_Item.None)
    //            continue;

    //        string timeUpDate = items[i].m_szTimeUpDate;
    //        bool isOneTimeUse = string.IsNullOrWhiteSpace(timeUpDate);
    //        System.DateTime dateTime = System.DateTime.MinValue;
    //        if (!isOneTimeUse)
    //        {
    //            dateTime = System.DateTime.ParseExact(timeUpDate, "O", System.Globalization.CultureInfo.InvariantCulture);
    //        }

    //        m_lstItems.Add(new ItemInUse
    //        {
    //            m_emItem = item,
    //            m_bIsOneTimeUse = isOneTimeUse,
    //            m_dtTimeUpDate = dateTime
    //        });
    //    }
    //}

    //public bool f_IsItemInList(EM_Item item)
    //{
    //    for (int i = 0; i < m_lstItems.Count; i++)
    //    {
    //        return m_lstItems[i].m_emItem == item;
    //    }
    //    return false;
    //}

    //private void Callback_PlayerEnergyResp(object obj)
    //{
    //    var response = (CMsg_PlayerEnergyResp)obj;

    //    f_UpdateEnergy(response.m_iEnergy, response.m_szLastConsumeTime);
    //}

    //private void Callback_PlayerItemActivationResp(object obj)
    //{
    //    var response = (CMsg_PlayerItemActivationResp)obj;

    //    f_UpdateItemInUse(response.m_arrstItems);
    //}

    #endregion // 準備進入遊戲
}

public static class Extend
{
    #region ccTimeEvent
    /// <summary>
    /// Unregister time event and clear event id.
    /// </summary>
    /// <param name="timeEvent">ccTimeEvent.</param>
    /// <param name="iRefEventID">Referent event id variable.</param>
    public static void f_UnRegEvent2(this ccTimeEvent timeEvent, ref int iRefEventID)
    {
        if (iRefEventID == glo_Main.ccTimeEventEmptyID)
        {
            return;
        }

        timeEvent.f_UnRegEvent(iRefEventID);
        iRefEventID = glo_Main.ccTimeEventEmptyID;
    }

    /// <summary>
    /// Register time event and auto-unregister if event id was not empty.
    /// </summary>
    /// <param name="timeEvent">ccTimeEvent.</param>
    /// <param name="iRefEventID">Referent event id variable.</param>
    public static void f_RegEvent2(this ccTimeEvent timeEvent, ref int iRefEventID, float fDelayTime, int iRunTime, object oData, ccCallback tccCallback)
    {
        if (iRefEventID != glo_Main.ccTimeEventEmptyID)
        {
            timeEvent.f_UnRegEvent(iRefEventID);
        }

        iRefEventID = timeEvent.f_RegEvent(fDelayTime, iRunTime, oData, tccCallback);
    }

    /// <summary>
    /// Register time event and auto-unregister if event id was not empty.
    /// </summary>
    /// <param name="timeEvent">ccTimeEvent.</param>
    /// <param name="iRefEventID">Referent event id variable.</param>
    public static void f_RegEvent2(this ccTimeEvent timeEvent, ref int iRefEventID, float fDelayTime, bool bRePeat, object oData, ccCallback tccCallback, bool bUsePause = false)
    {
        if (iRefEventID != glo_Main.ccTimeEventEmptyID)
        {
            timeEvent.f_UnRegEvent(iRefEventID);
        }

        iRefEventID = timeEvent.f_RegEvent(fDelayTime, bRePeat, oData, tccCallback, bUsePause);
    }

    /// <summary>
    /// Register time event and auto-unregister if event id was not empty.
    /// </summary>
    /// <param name="timeEvent">ccTimeEvent.</param>
    /// <param name="iRefEventID">Referent event id variable.</param>
    public static void f_RegEvent2(this ccTimeEvent timeEvent, ref int iRefEventID, float fStartWaitTime, float fDelayTime, bool bRePeat, object oData, ccCallback tccCallback, bool bUsePause = false)
    {
        if (iRefEventID != glo_Main.ccTimeEventEmptyID)
        {
            timeEvent.f_UnRegEvent(iRefEventID);
        }

        iRefEventID = timeEvent.f_RegEvent(fStartWaitTime, fDelayTime, bRePeat, oData, tccCallback, bUsePause);
    }
    #endregion
}
