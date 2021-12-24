﻿

//#define  VER_LAN      //使用 #define  VER_LAN   表示使用外部脚本 (外部腾讯云服务器)
//#define  LOCAL_LAN  //使用 #define  LOCAL_LAN 表示使用网内测试脚本，

using UnityEngine;
using System.Collections;


/// <summary>
/// 项目说明：
/// 项目名称：
/// 工程开始时间                          2017-02-01
/// </summary>
public class GloData// : MonoBehaviour
{
    public static int glo_iVer = 1000001;
    public static string glo_strVer = "r.";
       

    /// <summary>
    /// 控制台是否输出Log
    /// </summary>
    public static bool m_bDebugLog = true;
                
    public static int glo_iPingTime = 60;
    public static int glo_iGameId = 90;


#if Server
    public static string glo_strSvrIP = "192.168.0.227";
    public static int glo_iSvrPort = 9227;

    public static string glo_ProName = "MR_Edit";
    public static string glo_strHttpServerIP = "123.207.87.187";

#elif Local
    /// <summary>
    /// 设置游戏服务器的工作IP
    /// </summary>
    public static string glo_strSvrIP = "192.168.0.227";
    public static int glo_iSvrPort = 9227;

    //本地测试设置
    public static string glo_ProName = "MR_Edit";
    /// <summary>
    /// 设置游戏脚本服务器工作IP
    /// </summary>
    public static string glo_strHttpServerIP = "192.168.0.227";
    

    

#else
#endif

    public static string glo_strLoadVer   = "http://" + glo_strHttpServerIP + "/" + glo_ProName + "/ver/LoadVer.php";
    public static string glo_strLoadAllSC = "http://" + glo_strHttpServerIP + "/" + glo_ProName + "/ver/";
    public static string glo_strSaveLog   = "http://" + glo_strHttpServerIP + "/" + glo_ProName + "/Log/SaveLog.php";

    public static string glo_strABServerURL = "http://" + glo_strHttpServerIP + "/" + glo_ProName + "/ABRes/UpdateCatchData/update";

    public static float glo_fCatchBufSleepTime = 0.1f;
    public static float glo_fAutoReLoginSleepTime = 10f;


    public static int glo_iRecvPingTime = 300;
    

    /// <summary>
    /// 资源密码
    /// </summary>
    public static string glo_strResourcePwd = "Night4";
    
    /// <summary>
    /// 最大日志存储量
    /// </summary>
    public static int glo_iMaxLogSize = 100;

    /// <summary>
    /// 0不自动上传LOG， 1自动上传游戏LOG, 自动上传游戏LOG和UnityLog
    /// </summary>
    public static int glo_iAutoUpdateLog = 0;

    /// <summary>
    /// 自动上传LOG时间
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
    /// 闪电移动速度
    /// </summary>
    public static float glo_fThunderSpeed = 2;
    /// <summary>
    /// 闪电动画时间
    /// </summary>
    public static float glo_fThunderHitTime = 0.5f;

    /// <summary>
    /// 场景里最大处理的人语音数
    /// </summary>
    public static int glo_iVoiceSoundNum = 5;
    
    /// <summary>
    /// Fish in Net Size
    /// </summary>
    public static float glo_iFishInNetSize = 2f;

    public static int glo_iLog = 3;

    public static int glo_iFishUpY = 1;

   
   
}