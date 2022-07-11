using UnityEngine;
using System.Collections;

public class GloData
{
    public static int glo_iVer = 1000001;
    public static string glo_strVer = "r.";


    /// <summary>
    /// 控制台是否輸出Log
    /// </summary>
    public static bool m_bDebugLog = true;

    public static int glo_iPingTime = 60;
    public static int glo_iGameId = 90;


#if Server
    public static string glo_strSvrIP = "192.168.0.227";
    public static int glo_iSvrPort = 9227;

    public static string glo_ProName = "MR_Edit";
    public static string glo_strHttpServerIP = "123.207.87.187";
    //public static string glo_ProName = "MR_Edit5";
    //public static string glo_strHttpServerIP = "192.168.0.182";

#elif Local
    /// <summary>
    /// 設置遊戲伺服器的工作IP
    /// </summary>
    public static string glo_strSvrIP = "127.0.0.1";
    public static int glo_iSvrPort = 4530;

    //本地測試設置
    public static string glo_ProName = "MR_Edit";
    /// <summary>
    /// 設置遊戲腳本伺服器工作IP
    /// </summary>
    public static string glo_strHttpServerIP = "127.0.0.1";



#else
#endif

    public static string glo_strLoadVer = "http://" + glo_strHttpServerIP + "/ver/LoadVer.php";
    public static string glo_strLoadAllSC = "http://" + glo_strHttpServerIP + "/ver/";
    public static string glo_strSaveLog = "http://" + glo_strHttpServerIP + "/Log/SaveLog.php";

    public static string glo_strABServerURL = "http://" + glo_strHttpServerIP + "/ABRes/UpdateCatchData/update";

    public static float glo_fCatchBufSleepTime = 0.1f;
    public static float glo_fAutoReLoginSleepTime = 10f;


    public static int glo_iRecvPingTime = 300;


    /// <summary>
    /// 資源密碼
    /// </summary>
    public static string glo_strResourcePwd = "Night4";

    /// <summary>
    /// 最大日誌存儲量
    /// </summary>
    public static int glo_iMaxLogSize = 100;

    /// <summary>
    /// 0不自動上傳LOG， 1自動上傳遊戲LOG, 自動上傳遊戲LOG和UnityLog
    /// </summary>
    public static int glo_iAutoUpdateLog = 0;

    /// <summary>
    /// 自動上傳LOG時間
    /// </summary>
    public static int glo_iAutoUpdateLogTime = 60;

    public static int glo_iStartRoleId = 1000;
    public static float glo_fMaxRunTimeOut = 10;

    public static int glo_iViewAngle = 120;

    public static float glo_fMinGoHomeDis = 0.5f;
    public static float glo_fMaxGoHomeDis = 1f;

    public static float glo_fMaxLen = 10;
    public static int glo_iDefaultAwawrd = 1;

    /// <summary>
    /// 閃電移動速度
    /// </summary>
    public static float glo_fThunderSpeed = 2;
    /// <summary>
    /// 閃電動畫時間
    /// </summary>
    public static float glo_fThunderHitTime = 0.5f;

    /// <summary>
    /// 場景裡最大處理的人語音數
    /// </summary>
    public static int glo_iVoiceSoundNum = 5;

    /// <summary>
    /// Fish in Net Size
    /// </summary>
    public static float glo_iFishInNetSize = 2f;

    public static int glo_iLog = 3;

    public static int glo_iFishUpY = 1;



}
