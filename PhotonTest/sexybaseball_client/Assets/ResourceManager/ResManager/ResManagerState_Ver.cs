using System.Collections;
using ccU3DEngine;
using UnityEngine;
using UnityEngine.Networking;

public class ResManagerState_Ver : ccMachineStateBase
{
    private string _strResourceMd5;

    private UnityWebRequest w = null;
    private bool _bInitWWW = false;
    private bool _bSaveCatchBuf = false;

    public ResManagerState_Ver()
        : base((int)EM_ResManagerStatic.Ver)
    {

    }

    public override void f_Enter(object Obj)
    {
        InitWWW();
    }

    private void InitWWW()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //弹出网络未打开提示
            glo_Main.GetInstance().m_GameMessagePool.f_Broadcast(MessageDef.GAMEMESSAGEBOX, (int)eMsgOperateResult.OR_Error_WIFIConnectTimeOut);
            return;
        }

        _bInitWWW = true;
        w = UnityWebRequest.Get(GloData.glo_strLoadVer);
        w.SendWebRequest();

        //关掉网络未打开提示
        glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.LOGINEROINFOR, "SUC");
    }

    public override void f_Execute()
    {
        if (w == null)
        {
            if (!_bInitWWW)
            {
                InitWWW();
            }
            return;
        }
        if (!w.isDone)
            return;

        if (w.error != null)
        {
            MessageBox.DEBUG("網路錯誤 " + w.error + " " + w.url);
            LoadFail();
        }
        else if (w.downloadHandler.text.Length < 4)
        {
            MessageBox.DEBUG("網路錯誤2");
            LoadFail();
        }
        else
        {
            LoadVerSuc(w.downloadHandler.text);
        }
        w.Dispose();
        w = null;
    }

    private void LoadFail()
    {
        glo_Main.GetInstance().m_GameMessagePool.f_Broadcast(MessageDef.GAMEMESSAGEBOX, (int)eMsgOperateResult.OR_Error_WIFIConnectTimeOut);
    }

    private void LoadVerSuc(string strVerData)
    {
        string strVer = PlayerPrefs.GetString("Ver");
        string[] aData = ccMath.f_String2ArrayString(strVerData, ":");

        if (aData.Length == 4)
        {
            DispVer(strVer, aData[0]);
            DispServerInfor(aData[1]);
            GloData.glo_iAutoUpdateLog = ccMath.atoi(aData[2]);
            GloData.glo_iAutoUpdateLogTime = ccMath.atoi(aData[3]);
        }
        else
        {
            MessageBox.DEBUG("加載版本失敗");
        }
    }

    private void DispVer(string strLocalVer, string strServerVer)
    {
        string[] aVerData = ccMath.f_String2ArrayString(strServerVer, "-");

        if (aVerData.Length == 1)
        {
            _strResourceMd5 = "";
        }
        else
        {
#if UNITY_WEBPLAYER
            _strResourceMd5 = aVerData[2];
#elif UNITY_ANDROID
            _strResourceMd5 = aVerData[4];
#elif UNITY_IPHONE
            _strResourceMd5 = aVerData[3];
#else
            _strResourceMd5 = aVerData[1];
#endif
        }

        bool bFileEro = false;
        if (!ccFile.f_ExistsFile(Application.persistentDataPath + "/" + GloData.glo_ProName + "/ccData.xlscc"))
        {
            bFileEro = true;
            MessageBox.DEBUG("脚本文件丢失强制更新");
        }

        if (bFileEro == true || strLocalVer != aVerData[0])
        {
            _bSaveCatchBuf = true;
            PlayerPrefs.SetString("RVer", aVerData[0]);
        }
        else
        {
            _bSaveCatchBuf = false;
        }

        f_SetComplete((int)EM_ResManagerStatic.LoadSC, _bSaveCatchBuf);
    }

    private void DispServerInfor(string ppSQL)
    {
    }

    public string f_GetResourceMD5()
    {
        return _strResourceMd5;
    }
}