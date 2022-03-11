using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using ccU3DEngine;
using System.Collections.Generic;
using ccILR;
using UnityEngine.SceneManagement;

public class glo_Main : MonoBehaviour
{
    /// <summary>
    /// 聲音控制器
    /// </summary>
    public AudioManager m_AudioManager;

    public EM_GameStatic m_EM_GameStatic = EM_GameStatic.Waiting;


    [HideInInspector]
    public ccMessagePoolV2 m_GameMessagePool = new ccMessagePoolV2();
    public ccMessagePoolV2 m_UIMessagePool = new ccMessagePoolV2();

    ccLog m_ccLog = null;
    public SC_Pool m_SC_Pool;

    //public MainLogic m_MainLogic;
    /// <summary>
    /// 遊戲資源管理器
    ///// </summary>
    public ResourceManager m_ResourceManager;

    private static glo_Main _Instance = null;
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

    void Awake()
    {
        m_ccLog = new ccLog("Log", true, true);
        //m_ccLog.f_SetUserId(SystemInfo.deviceUniqueIdentifier);
        //m_ccLog.f_Start();

        GloData.glo_iVer = ccMath.atoi(Application.version);
        GloData.glo_strVer = GloData.glo_strVer + GloData.glo_iVer;
        ResourceTools.f_UpdateURL();

        m_SC_Pool = new SC_Pool();
        m_ResourceManager = new ResourceManager();


    }


    void Start()
    {
        MessageBox.f_UseUnityDebug();
        MessageBox.DEBUG("初始遊戲");
        MessageBox.DEBUG(Application.persistentDataPath);

        DontDestroyOnLoad(this);    //自己不消失

        /// 初始引擎工作參數
        InitEngineParam();
        /// 初始化Unity工作環境
        InitGameUnityParams();
        InitPath();
        InitComponent();

        InitMessage();
        InitAudio();
        InitResManager();

    }

    /// <summary>
    /// 初始引擎工作參數
    /// </summary>
    private void InitEngineParam()
    {
        Application.runInBackground = true;
        Application.targetFrameRate = 90;

        gameObject.AddComponent<UpdateManager>();
        //gameObject.AddComponent<ccSceneMgr>();
        UpdateManager tUpdateManager = UpdateManager.Get();
        UpdateManager.Get().SetClientVersion(Application.version);
        gameObject.AddComponent<AssetLoader>();

        //打開模擬器模式 ，此模式下資源不需要經過打包輸出和上傳到資原始伺服器就可以直接使用
        //平時開發都採用模擬器模式來進行，後期整合時再切換回正常模式
        ccU3DEngineParam.m_bIsLocalAB = true;

        //設置當前編輯器及最終打包輸出的目標平臺
#if UNITY_EDITOR
        UnityEditor.BuildTarget tBuildTarget = UnityEditor.EditorUserBuildSettings.activeBuildTarget;
        if (tBuildTarget == UnityEditor.BuildTarget.Android)
        {
            ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.Android;
        }
        else if (tBuildTarget == UnityEditor.BuildTarget.iOS)
        {
            ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.iOS;
        }
        else if (tBuildTarget == UnityEditor.BuildTarget.StandaloneWindows || tBuildTarget == UnityEditor.BuildTarget.StandaloneWindows64)
        {
            ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.StandaloneWindows;
        }
        //else if (tBuildTarget == UnityEditor.BuildTarget.StandaloneOSXIntel || tBuildTarget == UnityEditor.BuildTarget.StandaloneOSXIntel64 ||
        //    tBuildTarget == UnityEditor.BuildTarget.StandaloneOSXUniversal)
        //{ 
        //    ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.Mac;
        //}
#elif UNITY_IOS
            ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.iOS;
#elif UNITY_ANDROID
           ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.Android;
#elif UNITY_STANDALONE
            ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.StandaloneWindows;
#elif UNITY_STANDALONE_OSX
            ccU3DEngineParam.m_CurBuildTarget = ccBuildTarget.StandaloneWindows;
#endif

    }

    /// <summary>
    /// 初始化Unity工作環境
    /// </summary>
    private void InitGameUnityParams()
    {
        //GameSetting.InitGameQuality();
        //QualitySettings.SetQualityLevel(GameSetting.Quality);
        //GameSetting.SetResolution(GameSetting.Quality);

        //Shader.SetGlobalColor("_GlobalColor", Color.white);

    }

    private void InitPath()
    {
        //string strCatchFilePath = PlayerPrefs.GetString("CATHFILEPATH");

        //if (string.IsNullOrEmpty(strCatchFilePath))
        //{
        //    Glo_Data.glo_strCatchFilePath = Environment.CurrentDirectory + "\\AvgRun\\AvgData";
        //}
        //else
        //{
        //    Glo_Data.glo_strCatchFilePath = strCatchFilePath;
        //}
        ////if (!Directory.Exists(Glo_Data.glo_strCatchFilePath))
        ////{
        ////    Directory.CreateDirectory(Glo_Data.glo_strCatchFilePath);
        ////}
    }

    /// <summary>
    /// 初始化核心組件
    /// </summary>
    private void InitComponent()
    {
        ccUIManage.GetInstance().f_SaveFactoryHandler(ccILR_ClassFactory.GetInstance());



        GameDataLoad.f_LoadGameSystemMemory();
    }

    /// <summary>
    /// 初始化資源管理
    /// 開始載入資源
    /// </summary>
    private void InitResManager()
    {
        MessageBox.DEBUG("InitResManager...");
        //m_GameMessagePool.f_RemoveListener(MessageDef.LOADSCSUC, CallBack_InitResManager);

        startLoadResourceTime = Time.realtimeSinceStartup;
        LoadResource _LoadResource = new LoadResource();
        _LoadResource.f_StartLoad(Callback_LoadResSuc);
    }

    private float startLoadResourceTime = 0;
    /// <summary>
    /// 資源成功載入完回檔、
    /// 初始化玩家資料結構、
    /// 廣播資源載入完成（通知顯示logo頁面可以進入遊戲）
    /// </summary>
    /// <param name="Obj"></param>
    void Callback_LoadResSuc(object Obj)
    {
        //Data_Pool.f_InitPool();

#if Control
        //Control;
        MessageBox.DEBUG("使用控制台開始遊戲");
        GameControllLogin();
#else
        MessageBox.DEBUG("不使用控制台直接開始遊戲");
        //GameMain.GetInstance().f_StartGame();   
#endif

        ccTimeEvent.GetInstance().f_RegEvent(0f, false, null, CallBack_StartZip);

    }


    #region 資源更新及切換到登陸場景

    void CallBack_StartZip(object Obj)
    {
        UpdateManager.Get().f_ZipBaseRes(CallBack_ZipBaseResProgress, CallBack_ZipBaseResSuc);
    }

    void CallBack_ZipBaseResProgress(object Obj)
    {
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(UIMessageDef.UI_UpdateInitProgress, Obj);
    }

    private void CallBack_ZipBaseResSuc(object Obj)
    {
        StartUpdateRes();
    }

    int iUpdateProcessId = 0;
    void StartUpdateRes()
    {
        UpdateManager.Get().m_remoteUri = new[] { GloData.glo_strABServerURL };
        UpdateManager.Get().BeginInitialize(m_SC_Pool.f_GetABVer(), OnUpdateInitialize, false);
        iUpdateProcessId = ccTimeEvent.GetInstance().f_RegEvent(0.5f, true, null, OnTime_UpdateProcess);
    }

    void OnTime_UpdateProcess(object Obj)
    {
        UpdateManager.UpdateProgress tUpdateProgress = UpdateManager.Get().GetProgress();
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(UIMessageDef.UI_UpdateInitProgress, tUpdateProgress.progressPercentage);
    }


    void OnUpdateInitialize()
    {
        MessageBox.DEBUG("開始更新資源。");
        UpdateManager.Get().BeginDownload(OnUpdateComplete);
    }

    private void OnUpdateComplete()
    {
        ccTimeEvent.GetInstance().f_UnRegEvent(iUpdateProcessId);

        ccILR_ClassFactory.GetInstance().f_LoadHotFixDLL(true);
        MessageBox.DEBUG("資源更新成功...");

        RegLanguageSC();
        LanguageManager.GetInstance().f_ChangeLanguage(Locale.zhTW);

        f_InitGame();
    }


    private void RegLanguageSC()
    {
        ////登陸相關語言腳本
        LanguageManager.GetInstance().f_RegSC("Login");
        ////系統級語言腳本
        LanguageManager.GetInstance().f_RegSC("System");
        LanguageManager.GetInstance().f_RegSC("GameMain");
    }

    #endregion


    private void InitAudio()
    {

    }


    private void InitMessage()
    {
        ccTimeEvent.GetInstance().f_ChangePingTime(0.02f);
    }

    void Update()
    {

        m_GameMessagePool.f_Update();
        m_UIMessagePool.f_Update();

        //GameSocket.GetInstance().f_Update();

    }

    private void OnDestroy()
    {

    }

    public void f_Destroy()
    {
        MessageBox.DEBUG("強制結束遊戲 QuitGame");

        MessageBox.DEBUG("................................................");

        ccTimeEvent.GetInstance().f_RegEvent(1, false, null, ApplicationQuit);
    }

    //void OnApplicationQuit()
    void ApplicationQuit(object Obj)
    {
        //GameSocket.GetInstance().f_Close();
        //m_ccLog.f_Quit();
        Application.Quit();
    }


    /// <summary>
    /// 登陸遊戲成功，初始遊戲資料準備進入遊戲
    /// </summary>
    public void f_InitGame()
    {
        InitPool();
        InitGameData();
        InitSocket();

        ccUIManage.GetInstance().f_SendMsgV2("UI_GameLogin", BaseUIMessageDef.UI_OPEN);

    }

    void InitPool()
    {
        //Data_Pool.GetInstance().f_Init();
    }

    void InitSocket()
    {
        //GameSocket.GetInstance().f_Login();
    }

    private void InitGameData()
    {


    }


    public Coroutine f_StartCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }



}

