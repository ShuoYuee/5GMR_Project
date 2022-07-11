﻿using System.Collections;
using System.Collections.Generic;
using ccU3DEngine;
using UnityEngine;

/// <summary>
/// 脚本管理器
/// </summary>
public class SC_Pool
{
    int _iABVer = 0;

    private bool _bLoadSuc;

    public DialoguesSC m_DialoguesSC = new DialoguesSC();
    public RPSGameSC m_RPSGameSC = new RPSGameSC();
    public SlotMachineSC m_SlotMachineSC = new SlotMachineSC();

    public TeamSC m_TeamSC = new TeamSC();
    public GirlSC m_GirlSC = new GirlSC();
    public GamePlotSC m_GamePlotSC = new GamePlotSC();

    List<NBaseSC> _aSCList = new List<NBaseSC>();
    public void f_LoadSC(byte[] bData)
    {
        _bLoadSuc = false;

        ///////////////////////////////////////////////////////////////

        _aSCList.Add(m_TeamSC);
        //_aSCList.Add(m_GirlSC);
        //_aSCList.Add(m_GamePlotSC);

        ///////////////////////////////////////////////////////////////

        int i = 0;
        MessageBox.DEBUG("解析脚本");

        string ppSQL;
        byte[] b = new byte[512];
        System.Array.Copy(bData, b, 5);
        int iHeadLen = int.Parse(System.Text.Encoding.UTF8.GetString(b));
        System.Array.Copy(bData, 5, b, 0, iHeadLen);
        string strHeadData = System.Text.Encoding.UTF8.GetString(b);
        string[] ttt = strHeadData.Split(new string[] { "," }, System.StringSplitOptions.None);
        int iMovePos = iHeadLen + 5;

        int iDataLen = int.Parse(ttt[i]);
        ppSQL = ZipTools.aaa556(bData, iMovePos, iDataLen);
        iMovePos = iMovePos + iDataLen;
        DispABVer(ppSQL);

        for (i = 0; i < _aSCList.Count; i++)
        {
            //yield return new WaitForSeconds(4.5f/_aSCList.Count);
            MessageBox.DEBUG("SC " + i + " " + _aSCList[i].m_strRegDTName);
            iDataLen = int.Parse(ttt[i + 1]);
            ppSQL = ZipTools.aaa556(bData, iMovePos, iDataLen);
            _aSCList[i].f_LoadSCForData(ppSQL);
            iMovePos = iMovePos + iDataLen;
        }

        _bLoadSuc = true;


        MessageBox.DEBUG("解析脚本成功");
    }


    private void DispABVer(string strData)
    {
        int[] aVer = ccMath.f_String2ArrayInt(strData, "-");

        if (aVer.Length == 4)
        {
            //0 $ProgrameVer = $_POST["ProgrameVer"]; 1 $AndriodVer = $_POST["AndriodVer"]; 2 $WindowVer = $_POST["WindowVer"]; 3 $IosVer = $_POST["IosVer"];
#if UNITY_EDITOR
            UnityEditor.BuildTarget tBuildTarget = UnityEditor.EditorUserBuildSettings.activeBuildTarget;
            if (tBuildTarget == UnityEditor.BuildTarget.Android)
            {
                _iABVer = aVer[1];
            }
            else if (tBuildTarget == UnityEditor.BuildTarget.iOS)
            {
                _iABVer = aVer[3];
            }
            else if (tBuildTarget == UnityEditor.BuildTarget.StandaloneWindows || tBuildTarget == UnityEditor.BuildTarget.StandaloneWindows64)
            {
                _iABVer = aVer[2];
            }
#elif UNITY_IOS
            _iABVer = aVer[3];
#elif UNITY_ANDROID
           _iABVer = aVer[1];
#elif UNITY_STANDALONE
            _iABVer = aVer[2];
#elif UNITY_STANDALONE_OSX
           _iABVer = aVer[3];
#endif
        }
        else
        {
            MessageBox.DEBUG("AB资源版本版本错误:" + strData);
        }

        MessageBox.DEBUG("AB资源版本:" + _iABVer);
    }

    /// <summary>
    /// 获取当前使用的资源版本号
    /// 此处可强制设置个人测试用的专用版本方便多人协作
    /// </summary>
    /// <returns></returns>
    public int f_GetABVer()
    {
        //此处可强制设置个人测试用的专用版本方便多人协作
        //return 13;    返回测试使用的13版本号
        return _iABVer;
    }


    public bool f_CheckLoadSuc()
    {
        return _bLoadSuc;
    }

}