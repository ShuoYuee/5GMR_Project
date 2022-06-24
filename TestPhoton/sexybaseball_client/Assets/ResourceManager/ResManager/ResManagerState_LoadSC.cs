using ccU3DEngine;
using UnityEngine;
using UnityEngine.Networking;

public class ResManagerState_LoadSC : ccMachineStateBase
{
    private UnityWebRequest w = null;
    private bool _bUpdate = false;

    public ResManagerState_LoadSC() : base((int)EM_ResManagerStatic.LoadSC) { }

    public override void f_Enter(object Obj)
    {
        _bUpdate = (bool)Obj;

        if (_bUpdate)
        {
            MessageBox.DEBUG("更新脚本");
            string strUrl = "";

#if UNITY_WEBPLAYER
            strUrl = GloData.glo_strLoadAllSC + "ccData_W.bytes";
#elif UNITY_ANDROID
            strUrl = GloData.glo_strLoadAllSC + "ccData_A.bytes";
#elif UNITY_IPHONE
            strUrl = GloData.glo_strLoadAllSC + "ccData_I.bytes";
#else
            strUrl = GloData.glo_strLoadAllSC + "ccData_W.bytes";
#endif
            w = UnityWebRequest.Get(strUrl);
            w.SendWebRequest();
        }
        else
        {
            MessageBox.DEBUG("加载脚本");
            LoadSuc(ccFile.f_ReadFileForByte(Application.persistentDataPath + "/" + GloData.glo_ProName + "/", "ccData.xlscc"));
        }
    }

    public override void f_Execute()
    {
        if (w == null || !w.isDone)
            return;

        if (w.error != null)
        {
            MessageBox.DEBUG("網路錯誤");
            LoadFail();
        }
        else if (w.downloadHandler.text.Length < 4)
        {
            MessageBox.DEBUG("網路錯誤2");
            LoadFail();
        }
        else
        {
            LoadSuc(w.downloadHandler.data);
        }
        w.Dispose();
        w = null;

        MessageBox.DEBUG("下载脚本成功");
    }

    private void LoadFail()
    {

    }

    private void LoadSuc(byte[] aBytes)
    {
        if (_bUpdate)
        {
            ccFile.f_SaveFileForByte(Application.persistentDataPath + "/" + GloData.glo_ProName + "/", "ccData.xlscc", aBytes);
            string strServerVer = PlayerPrefs.GetString("RVer");
            PlayerPrefs.SetString("Ver", strServerVer);
            MessageBox.DEBUG("更新脚本版本为：" + strServerVer);
        }
        f_SetComplete((int)EM_ResManagerStatic.DispSC, aBytes);
    }
}