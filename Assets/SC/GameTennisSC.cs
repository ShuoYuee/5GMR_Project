
using ccU3DEngine;
using System;
using System.Collections.Generic;



public class GameTennisSC : NBaseSC
{
    public GameTennisSC()
    {
        Create("GameTennisDT");
    }

    public override void f_LoadSCForData(string strData)
    {
        DispSaveData(strData);
    }

    private void DispSaveData(string ppSQL)
    {
        string[] ttt = ppSQL.Split(new string[] { "1#QW" }, System.StringSplitOptions.None);
        GameTennisDT DataDT;
        string[] tData;
        string[] tFoddScData = ttt[1].Split(new string[] { "|" }, System.StringSplitOptions.None);
        for (int i = 0; i < tFoddScData.Length; i++)
        {
            try
            {
                if (tFoddScData[i] == "")
                {
                    MessageBox.DEBUG(m_strRegDTName + "腳本存在空記錄, " + i);
                    continue;
                }
                tData = tFoddScData[i].Split(new string[] { "@," }, System.StringSplitOptions.None);
                int a = 0;
                DataDT = new GameTennisDT();
                DataDT.iId = ccMath.atoi(tData[a++]);
                DataDT.szName = tData[a++];
                DataDT.iSpeed = ccMath.atoi(tData[a++]);
                DataDT.iWinBall = ccMath.atoi(tData[a++]);
                DataDT.szOpponentModel = tData[a++];
                DataDT.iGameType = ccMath.atoi(tData[a++]);
                DataDT.iRandObj = ccMath.atoi(tData[a++]);
                SaveItem(DataDT);
            }
            catch
            {
                MessageBox.DEBUG(m_strRegDTName + "腳本記錄存在錯誤, " + i);
                continue;
            }
        }
    }

}

