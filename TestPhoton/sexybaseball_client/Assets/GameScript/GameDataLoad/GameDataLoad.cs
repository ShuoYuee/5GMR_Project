using ccU3DEngine;

public static class GameDataLoad
{
    /// <summary>
    /// 加载游戏资料
    /// </summary>
    public static void f_LoadGameSystemMemory()
    {
        f_LoadGameSystem();
    }

    private static void f_LoadGameSystem()
    {
        //StaticValue.m_fBgmVolume = LocalDataManager.f_GetLocalData<float>("m_fBgmVolume", 0.5f);
        //StaticValue.m_fSoundVolume = LocalDataManager.f_GetLocalData<float>("m_fSoundVolume", 0.5f);
        //StaticValue.m_fEffectVolume = LocalDataManager.f_GetLocalData<float>("m_fEffectVolume", 0.5f);
        //StaticValue.m_iLangId = LocalDataManager.f_GetLocalData<int>("m_iLangId", (int)Locale.enUS);

        //StaticValue.m_strUsername = LocalDataManager.f_GetLocalData<string>("m_strUsername", "");
        //StaticValue.m_strClientKey = LocalDataManager.f_GetLocalData<string>("m_strClientKey", "");
        //StaticValue.m_strServerKey = LocalDataManager.f_GetLocalData<string>("m_strServerKey", "");
    }

    private static void f_LoadGameStep()
    {
        int iGameStepSection = LocalDataManager.f_GetLocalData<int>("iGameStepSection", 0);
        int iGameStepAvgGameStory = LocalDataManager.f_GetLocalData<int>("iGameStepAvgGameStory", 0);
        if (iGameStepSection == 0 || iGameStepAvgGameStory == 0)
        {
            MessageBox.DEBUG("未发现存档信息开始新游戏");
        }
        else
        {
            MessageBox.DEBUG("发现存档信息:" + iGameStepSection + "-" + iGameStepAvgGameStory);
        }
    }

    public static void f_LoginGame(string strUserName, long iId, int iTeam)
    {
        StaticValue.m_strUserName = strUserName;
        StaticValue.m_iUserID = iId;
        StaticValue.m_iTeam = iTeam;
    }

    public static void f_LogoutGame()
    {
        StaticValue.m_strUsername = "";
        StaticValue.m_strUserPwd = "";
        StaticValue.m_strUserName = "";
        StaticValue.m_iUserID = -1;
        StaticValue.m_iTeam = 0;
    }

    public static void f_SaveGameSystemMemory()
    {
        //LocalDataManager.f_SetLocalData<float>("m_fBgmVolume", StaticValue.m_fBgmVolume);
        //LocalDataManager.f_SetLocalData<float>("m_fSoundVolume", StaticValue.m_fSoundVolume);
        //LocalDataManager.f_SetLocalData<float>("m_fEffectVolume", StaticValue.m_fEffectVolume);
        //LocalDataManager.f_SetLocalData<int>("m_iLangId", StaticValue.m_iLangId);

        //LocalDataManager.f_SetLocalData<string>("m_strUsername", StaticValue.m_strUsername);
        //LocalDataManager.f_SetLocalData<string>("m_strClientKey", StaticValue.m_strClientKey);
        //LocalDataManager.f_SetLocalData<string>("m_strServerKey", StaticValue.m_strServerKey);
    }

    public static int f_MemorySessionLoad(string strSession, int iDefault = -99999)
    {
        return LocalDataManager.f_GetLocalData<int>(strSession, iDefault);
    }

    public static void f_MemorySessionSave(string strSession, int iData)
    {
        LocalDataManager.f_SetLocalData<int>(strSession, iData);
    }

    public static void f_MemorySessionDelete(string strSession)
    {
        LocalDataManager.f_DeleteLocalData(strSession);
    }
}