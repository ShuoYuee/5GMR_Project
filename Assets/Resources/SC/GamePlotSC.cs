using ccU3DEngine;
using System;
using System.Collections.Generic;



public class GamePlotSC : NBaseSC
{
    public GamePlotSC()
    {
        Create("GamePlotDT");
    }

    public override void f_LoadSCForData(string strData)
    {
        DispSaveData(strData);
    }

    private void DispSaveData(string ppSQL)
    {
        string[] ttt = ppSQL.Split(new string[] { "1#QW" }, System.StringSplitOptions.None);
        GamePlotDT DataDT;
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
                DataDT = new GamePlotDT();
                DataDT.iId = ccMath.atoi(tData[a++]);
                DataDT.szName = tData[a++];
                DataDT.fStartSleepTime = ccMath.atof(tData[a++]);
                DataDT.iStartAction = ccMath.atoi(tData[a++]);
                DataDT.szData1 = tData[a++];
                DataDT.szData2 = tData[a++];
                DataDT.szData3 = tData[a++];
                DataDT.szData4 = tData[a++];
                DataDT.iNeedEnd = ccMath.atoi(tData[a++]);
                DataDT.fEndSleepTime = ccMath.atof(tData[a++]);
                DataDT.iEndAction = ccMath.atoi(tData[a++]);
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

